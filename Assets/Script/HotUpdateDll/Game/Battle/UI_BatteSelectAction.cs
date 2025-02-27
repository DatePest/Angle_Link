using Client;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
namespace Assets.Script.HotUpdateDll.Game.Battle_
{
    public class UI_BatteSelectAction : MonoBehaviour
    {
        [SerializeField] string AsssetObjName;
        ActionBar actionBar;
        SkillSelectButton skillButtons;
        BatteSelectActionControl actionControl;
        void Awake()
        {
            actionBar = new(gameObject, AsssetObjName);
            skillButtons = new(gameObject);
            actionControl = new(gameObject, actionBar, skillButtons);

            var b = BattleRoot.Get();
            if (b != null)
            {
                b.GetClientEvent().SelecetAceion += actionBar.Excute;
                b.GetClientEvent().OnWaitSelect += actionControl.ExcuteSelect;
            }
        }
        public void RegSelectEndSendToServer(Action<List<BattleSelectOrder>> action)
        {
            actionControl.SelectEndSendToServer += action;
        }

        private void OnDestroy()
        {
            actionBar.Dispose();
            skillButtons.Dispose();
            actionControl.Dispose();
        }
        public class BatteSelectActionControl : IDisposable
        {
            bool CanSelect = false;
            int CurrentIndex;

            public Action<List<BattleSelectOrder>> SelectEndSendToServer;
            List<BattleSelectOrder> SelectOrders = new();

            GameObject target;
            ActionBar actionBar;
            SkillSelectButton skillButtons;
            Button ButtonPrevious;
            public BatteSelectActionControl(GameObject g, ActionBar Bar, SkillSelectButton skillbutton)
            {
                target = g;
                actionBar = Bar;
                skillButtons = skillbutton;
                skillButtons.OnSelect += OnSelectCallback;
                ButtonPrevious = g.transform.Find("ButtonPrevious").GetComponent<Button>();
                ButtonPrevious.onClick.AddListener(Previous);
            }
            public void Dispose()
            {
                SelectOrders = null;
                target = null;
                actionBar = null;
                skillButtons = null;
                SelectEndSendToServer = null;
                ButtonPrevious = null;
            }
            public void OnSelectCallback(BattleSelectOrder order)
            {
                BattleSelectOrder Temp = null;
                foreach (BattleSelectOrder o in SelectOrders)
                {
                    if (o.Caster == order.Caster)
                    {
                        Temp = o;
                        break;
                    }
                }
                SelectOrders.Remove(Temp);
                SelectOrders.Add(order);

                Check();
            }
            void Check()
            {
                if (Next()) return;
                SelectEndSendToServer?.Invoke(SelectOrders);
                SelectOrders.Clear();
                CanSelect = false;
                target.SetActive(false);
            }
            public void ExcuteSelect()
            {
                CanSelect = true;
                CurrentIndex = -1;
                Next();
                target.SetActive(true);
            }

            void SwichButtonPrevious()
            {
                if (CurrentIndex <= 0)
                    ButtonPrevious.interactable = false;
                else
                    ButtonPrevious.interactable = true;
            }
            bool Next()
            {
                if (CanSelect != true) return false;
                CurrentIndex = actionBar.GetNextUnit(CurrentIndex, out var u);
                SwichButtonPrevious();
                if (u != null)
                {
                    skillButtons.Excute(u);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            void Previous()
            {
                if (CanSelect != true) return;
                CurrentIndex = actionBar.GetPreviousUnit(CurrentIndex, out var u);
                SwichButtonPrevious();
                if (u != null)
                {
                    skillButtons.Excute(u);
                    return;
                }
                // There is no executable, which can be equal to clearing all
                SelectOrders.Clear();


            }


        }
        public class SkillSelectButton : IDisposable
        {
            Image CharacterIcon;
            GameObject[] Buttons = new GameObject[4];
            public Action<BattleSelectOrder> OnSelect;
            public void Excute(Unit u)
            {
                CharacterIcon.sprite = u.GetArt().CharacterIcon;
                for (int i = 0; i < Buttons.Length; i++)
                {
                    SetSkillButton(Buttons[i], u,i);
                }
            }
            public SkillSelectButton(GameObject g)
            {
                CharacterIcon = g.transform.Find("CharacterIcon").GetComponent<Image>();
                var Sk = g.transform.Find("Skill");
                int i = 0;
                foreach (Transform t in Sk)
                {
                    int Index = i;
                    Buttons[Index] = t.gameObject;
                    i++;
                }
            }

            void SetSkillButton(GameObject g, Unit u, int AbilityOrder)
            {
                var Ab = u.GetUnitAbility(AbilityOrder);
                var Nu = g.transform.Find("NotUse");
                var SkillButton = g.GetComponent<Button>();
                SkillButton.onClick.RemoveAllListeners();
                if (Ab == null)
                {
                    g.gameObject.SetActive(false);
                    SkillButton.interactable = false;
                    return;
                }
                SkillButton.interactable = true;
                g.gameObject.SetActive(true);
                Nu.gameObject.SetActive(false);
                var Adata = Ab.abilityData.GetData();
                var Icon = g.transform.Find("Icon").GetComponent<Image>();
                Icon.sprite = Adata.Icon != null ? Adata.Icon : null;
                var SkillName = g.transform.Find("SkillName").GetComponent<TextMeshProUGUI>();

                SkillName.text = Adata.title;
                var CD = g.transform.Find("CD").GetComponent<TextMeshProUGUI>();
                //Skill CD
                if (Adata.CoolDown > 0)
                {
                    SkillButton.interactable = false;
                    Nu.gameObject.SetActive(true);
                    CD.text = $"CD ; {Adata.CoolDown}";
                    return;
                }
                else
                {
                    CD.text = "";
                }

                var DescButton = g.transform.Find("Desc").GetComponent<Button>();
                DescButton.onClick.RemoveAllListeners();
                DescButton.onClick.AddListener(
                    () => { Ui_SystemMsg.Get().ShowMsg(Adata.desc); });


                SkillButton.onClick.AddListener(
                 () => AddBattleSelectOrder(u, Ab)
                 );
            }
            void AddBattleSelectOrder(Unit caster, UnitAbility ability)
            {
                var BSO = new BattleSelectOrder(caster, ability);
                OnSelect?.Invoke(BSO);
            }
            public void Dispose()
            {
                Buttons = null;
            }
        }
        public class ActionBar : IDisposable
        {
            ObjectPool<GameObject> Pool;
            GameObject TargetAssObj;
            List<GameObject> ActicegameObjects = new List<GameObject>();
            Sprite[] IconTyptSprites;
            List<Unit> SelectUnits;
            byte PlayerId => BattleRoot.Get().GetClientPlayerID();
            public ActionBar(GameObject g, string AsssetObjName)
            {
                IconTyptSprites = YooAsset_Tool.GetPackageDataSprites(GameConstant.PackName_GameCore, "CharacterButtonInfo");
                TargetAssObj = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, AsssetObjName);
                var Show = g.transform.Find("ActionIcons");
                var Hide = g.transform.Find("PoolHide");
                Pool = new ObjectPool<GameObject>(
                   () => GameObject.Instantiate(TargetAssObj, Show, false),
                    actionOnGet: obj => { obj.SetActive(true); obj.transform.SetParent(Show); },
                   actionOnRelease: obj => { obj.SetActive(false); obj.transform.SetParent(Hide); },
                   actionOnDestroy: obj => GameObject.Destroy(obj),
                   collectionCheck: true,
                   defaultCapacity: 9,
                   maxSize: 12
                   ); ;
            }
            public int GetNextUnit(int Order, out Unit unit)
            {
                unit = null;
                for (int i = Order + 1; i < SelectUnits.Count; i++)
                {
                    if (SelectUnits[i].OwnerID == PlayerId)
                    {
                        unit = SelectUnits[i];
                        return i;
                    }
                }
                return SelectUnits.Count + 1;
            }
            public int GetPreviousUnit(int Order, out Unit unit)
            {
                unit = null;
                for (int i = Order - 1; i >= 0; i--)
                {
                    if (SelectUnits[i].OwnerID == PlayerId)
                    {
                        unit = SelectUnits[i];
                        return i;
                    }
                }
                return -1;
            }
            public void Excute(BattleData battle)
            {
                ResActObj();
                SelectUnits = battle.GetAllUnit();
                SelectUnits.Sort((x, y) => y.UnitAttribute.Speed.CompareTo(x.UnitAttribute.Speed));

                foreach (var a in SelectUnits)
                {
                    Get_SetInfo(a);
                }
            }
            Sprite GetSP(string Name)
            {
                foreach (Sprite sprite in IconTyptSprites)
                {
                    if (sprite.name == Name)
                        return sprite;
                }
                return default;
            }
            void Get_SetInfo(Unit U)
            {
                var P = Pool.Get();
                ActicegameObjects.Add(P);
                var data = U.UnitData.GetData();
                P.transform.Find("Icon").GetComponent<RawImage>().texture = data.Art.SDIcon.texture;
                var id = BattleRoot.Get().GetClientPlayerID();

                Sprite sp;
                if (U.OwnerID == id)
                    sp = GetSP("CharacterButtonInfo_Frame1");
                else
                    sp = GetSP("CharacterButtonInfo_Frame2");
                P.transform.Find("Frame").GetComponent<Image>().sprite = sp;
                P.transform.Find("Fill Area").GetComponent<Slider>().value = U.UnitAttribute.HP / U.UnitAttribute.HPMax;
            }
            void ResActObj()
            {
                foreach (GameObject obj in ActicegameObjects) { Pool.Release(obj); }
                ActicegameObjects.Clear();
            }



            public void Dispose()
            {
                TargetAssObj = null;
                ActicegameObjects.Clear();
                ActicegameObjects = null;
                IconTyptSprites = null;
                SelectUnits = null;
                Pool.Dispose();
            }
        }

    }

}
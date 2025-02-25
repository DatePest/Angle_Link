using Assets.Script.Core.GameDevelop;
using Assets.Script.Core.UI;
using Client;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static Assets.Script.Core.GameDevelop.Develop_CharacterLv;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.CanvasScaler;
namespace Assets.Script.HotUpdateDll.Game.CharacterDevelop
{
    public class Develop_Ui_Lv :  IUi_Switch 
    {

        const string ObjName = "Develop_Ui_Lv";
        const string LayoutObjName = "LvExpItemUseObj";

        GameObject Target;
        TextMeshProUGUI LvText, ExpText;
        Slider Slider;
        Button Use, Reset, Auto;
        List<ExpItemUseObj> expItems;
        ISelectCharacter TargetCharacter;
        // Client.ClientUserData User => Client.ClientRoot.Get().ClientUserData;
        public GameObject TargetObj { get => Target; }
        public Action OnShow { get; set; }
        public Action OnHide { get; set; }
        public Action CallUpdata;
        public Develop_Ui_Lv(ISelectCharacter character)
        {
            TargetCharacter = character;
            var t = GameObject.Find("Develop");
            var Asset = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, ObjName);
            Target = GameObject.Instantiate(Asset, t.transform, false);
            Slider = t.GetComponentInChildren<Slider>();
            InitItemLayout(3);
            InitIButtons();
            LvText = Target.transform.Find("LvText").gameObject.GetComponent<TextMeshProUGUI>();
            ExpText = Target.transform.Find("ExpText").gameObject.GetComponent<TextMeshProUGUI>();

        }
        void InitIButtons()
        {
            var Bs = Target.transform.Find("Buttons");
            Use = Bs.transform.Find("Use").GetComponent<Button>();
            Reset = Bs.transform.Find("Reset").GetComponent<Button>();
            Auto = Bs.transform.Find("Auto").GetComponent<Button>();


            Use.onClick.AddListener(onUse);
            Reset.onClick.AddListener(onReset);
            Auto.onClick.AddListener(onAuto);
        }
        void InitItemLayout(int i)
        {
            var Layout = Target.transform.Find("ItemLayout");
            expItems = new();

            for (int j = 0; j < i; j++)
            {
                var Item = new ExpItemUseObj(Layout, LayoutObjName, $"ExpItem0{j + 1}");
                Item.SetCharacter(TargetCharacter);
                Item.OnUpdata += UpdataUI;
                expItems.Add(Item);
            }

        }
        private void onAuto()
        {
            CalculateOptimalDistribution(expItems,TargetCharacter.TCharacter);
        }

        private void onReset()
        {
            foreach(var a in expItems)
            {
                a.Reset();
            }
        }

        private void onUse()
        {
            var r = new GameApi.ItemListRequst();
            r.ItemList1 = new();
            foreach (var i in expItems)
            {
                if (i.CurrentAmount == 0) continue;
                r.ItemList1.Add( new(i.ItemNet.AssetName, i.CurrentAmount));
            }
            if(r.ItemList1.Count == 0) return;
            r.Arg1 = TargetCharacter.TCharacter.characterNetData.AssetName;

            Action a = LoaclTolevelUP;
            ClientRoot.Get().ClientUserData.AddCache(GameConstant.DevelopEvent, a, true);
            EventSystemToolExpand.Publish(ClientEventTag.SendCharacterDevelopLv, NetworkMsg_HandlerTag.GameEvent, default, r);
        }

        void LoaclTolevelUP()
        {
            
            int Exp = 0;
            foreach (var a in expItems)
            {
                Exp += a.GetNowExp();
                if (a.ItemNet != null)
                    a.ItemNet.Amount -= a.CurrentAmount;
            }
            ToLevelUP(TargetCharacter.TCharacter.characterNetData, Exp);
            TargetCharacter.TCharacter.ReCalculateLv();

            CallUpdata?.Invoke();
            onReset();
        }
      
        void UpdataUI()
        {
            int Exp = 0;
            foreach(var a in expItems)
            {
                Exp += a.GetNowExp();
            }
            var Pre = PreviewLevelUP(TargetCharacter.TCharacter, Exp);
            UpdataSlider(Pre);
            Preview(Pre);
        }
        public void Preview(PreviewCharacterLevel preview)
        {
            string Var = $"Lv\r\n<size=150%><b>{preview.Level}</b></size>\r\n       /{GameConstant.CharacterMaxLevel}\r\n";
            LvText.text = Var;
            var Ne = GetLvNeedExp(preview.Level);
            Var = $"Next      {preview.Exp}/{Ne}";
            ExpText.text = Var;
        }
        void UpdataSlider(PreviewCharacterLevel preview)
        {
            var Ne = GetLvNeedExp(preview.Level);
            Slider.value = (float)preview.Exp / Ne;
        }


        public static void CalculateOptimalDistribution(List<ExpItemUseObj> items, Character character)
        {
            int totalExpNeeded = 0;
            int level = character.characterNetData.Lv;
            int exp = character.characterNetData.CurrentExp;

            // 計算達到滿級需要的總經驗值
            while (level < GameConstant.CharacterMaxLevel)
            {
                totalExpNeeded += GetLvNeedExp(level);
                level++;
            }

            int expRemaining = totalExpNeeded - exp;
            int[] usedItems = new int[items.Count];

            // 按照道具的效率從大到小排序
            var sortedItems = items.OrderByDescending(item => item.ItemExp).ToList();

            // 貪婪算法計算，確保在每次使用道具時檢查數量
            for (int i = 0; i < sortedItems.Count; i++)
            {
                var item = sortedItems[i];
                if (expRemaining <= 0)
                    break;

                // 計算當前道具需要的數量
                int itemsNeeded = Mathf.CeilToInt((float)expRemaining / item.ItemExp);

                // 限制道具使用數量
                itemsNeeded = Mathf.Min(itemsNeeded, item.MaxAmount);

                usedItems[i] = itemsNeeded; // 記錄所使用的道具數量
                //usedItems[items.IndexOf(item)] = itemsNeeded;
                expRemaining -= itemsNeeded * item.ItemExp; // 更新剩餘的經驗需求
            }

            // 確保達到滿級，如果還有剩餘經驗需求，則從小道具開始填補
            if (expRemaining > 0)
            {
                // 從小道具開始填補剩餘的經驗需求
                for (int i = sortedItems.Count - 1; i >= 0; i--)
                {
                    var item = sortedItems[i];
                    if (expRemaining <= 0)
                        break;

                    // 計算還需要多少道具
                    int additionalNeeded = Mathf.CeilToInt((float)expRemaining / item.ItemExp);
                    int available = item.MaxAmount - usedItems[i];

                    if (available > 0)
                    {
                        additionalNeeded = Mathf.Min(additionalNeeded, available);
                        usedItems[i] += additionalNeeded;
                        expRemaining -= additionalNeeded * item.ItemExp; // 更新剩餘的經驗需求
                    }
                }

                // 如果仍有剩餘經驗需求，則考慮使用更大的道具
                if (expRemaining > 0)
                {
                    for (int i = 0; i < sortedItems.Count; i++)
                    {
                        var item = sortedItems[i];
                        if (expRemaining <= 0)
                            break;

                        // 計算需要的道具數量
                        int itemsNeeded = Mathf.CeilToInt((float)expRemaining / item.ItemExp);
                        int available = item.MaxAmount - usedItems[i];

                        if (available > 0)
                        {
                            // 避免溢出，並盡量使用最少的道具
                            int effectiveNeeded = Mathf.Min(itemsNeeded, available);
                            usedItems[i] += effectiveNeeded;
                            expRemaining -= effectiveNeeded * item.ItemExp; // 更新剩餘的經驗需求
                        }
                    }
                }
            }

            // 更新角色的經驗
            //character.characterNetData.CurrentExp += totalExpNeeded - expRemaining;

            // 記錄結果
#if UNITY_EDITOR
            for (int i = 0; i < sortedItems.Count; i++)
            {
                Debug.Log($"Item: {sortedItems[i].ItemExp}, Used: {usedItems[i]}");
                sortedItems[i].CurrentAmount = usedItems[i];
                sortedItems[i].Updata();
            }
#endif
        }

        public void Dispose()
        {
            foreach(var i in expItems)
            {
                i.Dispose();
            }
            expItems.Clear();
            expItems = null;
            CallUpdata = null;
            TargetCharacter = null;
            OnShow = null;
            OnHide = null;
            LvText = null;
            ExpText = null;
        }

        public void ActiveSwitch(bool b)
        {
            if (TargetCharacter.TCharacter == null)
            {
                Target.SetActive(false);
                return;
            }

            Target.SetActive(b);
            if (b)
            {
                onReset();
                UpdataUI();
                OnShow?.Invoke();
            }
            else
            {
                OnHide?.Invoke();
            }
        }

        public class ExpItemUseObj : IDisposable
        {
            public Action OnUpdata;
            Button Add, Remove, Max;
            ISelectCharacter TargetCharacter;
            TextMeshProUGUI HaveAmount, GetExp, UseAmount;
            public Item ItemNet;
            public int MaxAmount { get { return ItemNet != null ? ItemNet.Amount : 0; } }
            public int ItemExp;
            public int CurrentAmount;
            public ExpItemUseObj(Transform target, string AssetName, string itemName)
            {
                var Ass = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, AssetName);

                var Obj = GameObject.Instantiate(Ass, target, false);
                Init(Obj, itemName);
            }
            async void Init(GameObject Obj, string itemName)
            {
                var itemData = await AssetFInd.GetItemData_Async(itemName);
                var ItemIcon = Obj.transform.Find("ItemIcon").GetComponent<Image>();
                ItemIcon.sprite = itemData.Icon != null ? itemData.Icon : null;
                #region
                GetExp = Obj.transform.Find("GetExp").Find("Amount").GetComponent<TextMeshProUGUI>();
                HaveAmount = Obj.transform.Find("ItemAmount").Find("Amount").GetComponent<TextMeshProUGUI>();
                UseAmount = Obj.transform.Find("UseAmount").Find("Amount").GetComponent<TextMeshProUGUI>();
                Add = Obj.transform.Find("Button_Add").GetComponent<Button>();
                Remove = Obj.transform.Find("Button_Remove").GetComponent<Button>();
                Max = Obj.transform.Find("Button_Max").GetComponent<Button>();
                #endregion
                Add.onClick.AddListener(() => AddItem(true));
                Remove.onClick.AddListener(() => AddItem(false));
                Max.onClick.AddListener(AddMax);

                Develop_CharacterLv.ExpItems.TryGetValue(itemData.name, out var v);
                if (v == 0) throw new Exception($"{itemData.name} is Not ExpItem Plz Check");
                ItemExp = v;

                ItemNet = Client.ClientRoot.Get().ClientUserData.GetPlayerItem(itemName);

            }
            public void Updata()
            {
                HaveAmount.text = $"{MaxAmount}";
                UseAmount.text = $"{CurrentAmount}";
                GetExp.text = $"{CurrentAmount * ItemExp}";
                OnUpdata?.Invoke();
            }
            public int GetNowExp() => CurrentAmount * ItemExp;
            public void SetCharacter(ISelectCharacter c) => TargetCharacter = c;
            private void AddMax()
            {
                int Max = MaxAmount;
                int itemsNeeded = 0;
                int expToNextLevel;
                int level = TargetCharacter.TCharacter.characterNetData.Lv;
                int exp = TargetCharacter.TCharacter.characterNetData.CurrentExp;

                while (level < GameConstant.CharacterMaxLevel)
                {
                    // 計算到下一級所需的經驗值
                    expToNextLevel = Develop_CharacterLv.GetLvNeedExp(level);
                    int expRequiredForLevelUp = expToNextLevel - exp;

                    // 如果當前經驗值不足以升級，累加經驗需求
                    if (expRequiredForLevelUp > 0)
                    {
                        // 計算所需道具數量並累加
                        int itemsForThisLevel = Mathf.CeilToInt((float)expRequiredForLevelUp / ItemExp);

                        if (Max > itemsNeeded + itemsForThisLevel)
                            itemsNeeded += itemsForThisLevel;
                        else
                        {
                            itemsNeeded = Max;
                            break;
                        }
                        // 更新當前經驗值，假設道具能完全覆蓋升級所需經驗
                        exp += itemsForThisLevel * ItemExp;
                    }
                    // 升到下一級
                    exp -= expToNextLevel;
                    level++;
                }

                CurrentAmount = itemsNeeded;
                Updata();
            }

            public void Reset()
            {
                CurrentAmount = 0;
                Updata();
            }
            void AddItem(bool b)
            {
                if (b)
                {
                    CurrentAmount++;
                }
                else
                {
                    CurrentAmount--;
                  
                }
                CurrentAmount = Mathf.Clamp(CurrentAmount, 0, MaxAmount);
                Updata();
            }

            public void Dispose()
            {
                Add = null;
                Remove = null;
                Max = null;
                HaveAmount = null;
                GetExp = null;
                UseAmount = null;
                TargetCharacter = null;
                OnUpdata = null;
            }
        }
    }
}

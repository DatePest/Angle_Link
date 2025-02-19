using Assets.Script.Core.UI;
using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace Assets.Script.HotUpdateDll.Game.CharacterDevelop
{
    public class DevelopCharacterSelect : IUi_Switch
    {
        GameObject Target;
        Ui_Layout ui_Select;

        public GameObject TargetObj { get => Target; }

        ISelectCharacter TargetCharacter;
        public Action OnShow { get; set; }
        public Action OnHide { get; set; }

        public DevelopCharacterSelect(ISelectCharacter character)
        {
            Init();
            TargetCharacter = character;
            LoadPlayerCharacters();
        }

        void Init()
        {
            var g = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, "DevelopSelectCharacterLayout");
            var u = GameObject.Find("SelectCharacter");
            Target =GameObject.Instantiate(g, u.transform, false);
        }
      
        void LoadPlayerCharacters()
        {
            var u = GameObject.Find("SelectCharacter");
            if (u == null) throw new System.Exception("SelectCharacter is null");
            ui_Select = u.GetComponentInChildren<Ui_Layout>();
            var cs = ClientRoot.Get().ClientUserData.GetPlayerCharacters();
            var g = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, "DevelopCharacterSelectButton");

            foreach (var c in cs)
            {
                var target = GameObject.Instantiate(g);
                var Sbutton = target.GetComponent<Button>();
                SetCharacterButton(target, c);
                Sbutton.onClick.AddListener(
                   () => {
                       SelectCharacterClick(c);
                   }
                    );

                ui_Select.AddSelectContent(target);
            }
        }
        void SetCharacterButton(GameObject g, Character character)
        {
            ArtData data = character.characterData.GetData().Art;
            foreach (Transform a in g.transform)
            {
                a.gameObject.SetActive(true);
                switch (a.name)
                {
                    case "Icon":
                        a.GetComponent<RawImage>().texture = data.CharacterIcon.texture;
                        break;
                    case "Type":
                        //var r = a.GetComponent<Image>();
                        //switch (character.characterData.GetData().Attribute.RoleType)
                        //{
                        //    case RoleType.Attacker:
                        //        r.sprite = GetSP("CharacterButtonInfo_Attacker");
                        //        break;
                        //    case RoleType.Support:
                        //        r.sprite = GetSP("CharacterButtonInfo_Support");
                        //        break;
                        //    case RoleType.Tank:
                        //        r.sprite = GetSP("CharacterButtonInfo_Tank");
                        //        break;
                        //    case RoleType.Healer:
                        //        r.sprite = GetSP("CharacterButtonInfo_Healer");
                        //        break;
                        //}
                        a.gameObject.SetActive(false);
                        break;
                    case "Name":
                        a.GetComponent<TextMeshProUGUI>().text = character.characterData.GetData().UnitName;
                        break;
                }
                
            }
        }
        private void SelectCharacterClick(Character c)
        {
            Target.SetActive(false);
            TargetCharacter.SetCharacter(c);
            OnHide?.Invoke();
        }

        public void ActiveSwitch(bool b)
        {
            Target.SetActive(b);
            if (b)
            {
                OnShow?.Invoke();
            }
            else
            {
                OnHide?.Invoke();
            }
        }

        public void Dispose()
        {
            OnShow = null;
            OnHide = null;
        }
    }
}

using Assets.Script.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace Assets.Script.HotUpdateDll.Game.CharacterDevelop
{
    public class CharacterDevelpoStatuData : IUi_Switch
    {
        const string ObjName = "Develop_UI_CharacterData";
        public Action OnShow { get; set; }
        public Action OnHide { get; set; }
        GameObject Target;

        Image CharacterIcon;
        public GameObject TargetObj => throw new NotImplementedException();
        ISelectCharacter TargetCharacter;
        public CharacterDevelpoStatuData(ISelectCharacter select)
        {
            TargetCharacter = select;
            Init();
        }
        void Init()
        {
            var t = GameObject.Find("Develop");
            var Asset = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, ObjName);
            Target = GameObject.Instantiate(Asset, t.transform, false);
            CharacterIcon = Target.transform.Find("CharacterIcon").gameObject.GetComponent<Image>();
        }
        private void SetCharacter()
        {
            ArtData data = TargetCharacter.TCharacter.characterData.GetData().Art;
            CharacterIcon.sprite = data.CharacterIcon;
        }
        public void Updata()
        {
            //Todo CharacterDevelpoData View Updata
        }
        public void ActiveSwitch(bool b)
        {
            if (b)
            {
                SetCharacter();
                Target.SetActive(true);
                OnShow?.Invoke();
            }
            else
            {
                Target.SetActive(false);
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

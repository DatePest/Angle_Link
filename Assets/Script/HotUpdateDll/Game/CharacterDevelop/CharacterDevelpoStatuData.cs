using Assets.Script.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using TMPro;

namespace Assets.Script.HotUpdateDll.Game.CharacterDevelop
{
    public class CharacterDevelpoStatuData : IUi_Switch
    {
        const string ObjName = "Develop_UI_CharacterData";
        public Action OnShow { get; set; }
        public Action OnHide { get; set; }
        GameObject Target;

        Image CharacterIcon;
        public GameObject TargetObj { get => Target; }
        ISelectCharacter TargetCharacter;
        public Dictionary<string,TextMeshProUGUI> UITextMeshPros;
        public CharacterDevelpoStatuData(ISelectCharacter select)
        {
            TargetCharacter = select;
            Init();
            InitSetTexts();
            Updata();
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
            UpdateCharacter();
        }
        void UpdateCharacter()
        {
            if (TargetCharacter.TCharacter == null) return;
            var ua = TargetCharacter.TCharacter.UnitAttribute;
            SetAttribute("Hp",ua.HP);
            SetAttribute("Atk", ua.Atk);
            SetAttribute("Def", ua.Def);
            SetAttribute("Speed", ua.Speed);
            SetAttribute("MAtk", ua.MAtk);
            SetAttribute("MDef", ua.MDef);
        }
        void SetAttribute(string name , int i)
        {
            UITextMeshPros.TryGetValue(name, out var textMeshPro);
            if (textMeshPro == null) return;
            textMeshPro.text = i.ToString();
        }
      
        void InitSetTexts()
        {
            UITextMeshPros = new();
            var attribute = Target.transform.Find("Attribute").gameObject.transform;
            findTMP(attribute, "Hp");
            findTMP(attribute, "Atk");
            findTMP(attribute, "Def");
            findTMP(attribute, "Speed");
            findTMP(attribute, "MAtk");
            findTMP(attribute, "MDef");
        }

        void findTMP(Transform transform, string text)
        {
            var t = transform.Find(text);
            var TM = t.Find("Stat").GetComponent<TextMeshProUGUI>();

            UITextMeshPros.Add(text, TM);
        }
        public void ActiveSwitch(bool b)
        {
            if (b)
            {
                SetCharacter();
                Target.SetActive(true);
                OnShow?.Invoke();
                Updata();
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
            UITextMeshPros = null;
        }
    }
}

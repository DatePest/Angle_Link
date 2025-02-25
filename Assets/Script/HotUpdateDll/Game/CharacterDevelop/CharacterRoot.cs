using Assets.Script.Core.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
namespace Assets.Script.HotUpdateDll.Game.CharacterDevelop
{

    public class CharacterRoot : MonoBehaviour, ISelectCharacter
    {
        GameObject Current;
        Dictionary<string, IUi_Switch> FuncObjs = new();
        CharacterDevelpoStatuData develpoData;
        Character ISelectCharacter.TCharacter { get; set; }

        public Action CallUpdata;

        private void Awake()
        {
            InitDevelop_Ui_Lv();
            InitSelectCharact();
            InitDevelpoData();


        }
        void InitDevelop_Ui_Lv()
        {
            var lv = new Develop_Ui_Lv(this);
            InitButtons("CharacterLvButton", lv);
            lv.CallUpdata += CallUpdata;

        }
        void InitSelectCharact()
        {
            var Select = new DevelopCharacterSelect(this);
            InitButtons("ToSelectCharacteButton", Select);

            Select.ActiveSwitch(true);
            Select.OnHide += () => SwitchButton("CharacterLvButton");
            Select.OnShow += () => develpoData.ActiveSwitch(false);
        }
        void InitDevelpoData()
        {
            develpoData = new(this);
            foreach (var a in FuncObjs)
            {
                if (a.Key == "ToSelectCharacteButton") break;
                a.Value.OnShow += () => develpoData.ActiveSwitch(true);
            }
            develpoData.ActiveSwitch(false);
            CallUpdata +=  develpoData.Updata;
            FuncObjs.Add("DevelpoData", develpoData);
        }
        void InitButtons(string Name, IUi_Switch Obj)
        {
            var b = GameObject.Find(Name).GetComponent<Button>();
            if (b == null) throw new System.Exception($"Find  {Name} is null");
            b.onClick.AddListener(() => SwitchButton(Name));
            FuncObjs.Add(Name, Obj);

            Obj.ActiveSwitch(false);
        }
        private void OnDestroy()
        {
            foreach (var obj in FuncObjs)
            {
                obj.Value.Dispose();
            }
            FuncObjs.Clear();
        }

        void SwitchButton(string name)
        {
            if (Current != null) Current.SetActive(false);

            if (FuncObjs.TryGetValue(name, out var v))
            {
                v.ActiveSwitch(true);
                Current = v.TargetObj;
            }
        }

    }




}

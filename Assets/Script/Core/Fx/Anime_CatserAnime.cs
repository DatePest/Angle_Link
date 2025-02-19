using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Core.Fx
{
    public class Anime_CatserAnime : IFx
    {
        Image image;
        TextMeshProUGUI SName;
        public Anime_CatserAnime(GameObject TargetParent)
        {
            var Ass = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, "CatserAnime");
            var g = GameObject.Instantiate(Ass, TargetParent.transform, false);
            Init(g);
            g.gameObject.SetActive(false);
        }

        protected override void Init(GameObject g)
        {

            mAnimation = g.GetComponent<Animation>();
            image = g.transform.Find("CharacterCaster").GetComponent<Image>();
            SName = g.transform.Find("SkillName").GetComponent<TextMeshProUGUI>();

        }

        public override async UniTask Excute(object Unit, object AbilityData)
        {
            var U = Unit as Unit;
            var Ad = AbilityData as AbilityData;
            image.sprite = U.GetArt().CharacterIcon;
            SName.text = Ad.title;
            await GameUtilityTool.PlayAnime("Ability_StartCasting", mAnimation);
        }

        public override void Dispose()
        {
            base.Dispose();
            image = null;
            SName = null;
        }
    }

}

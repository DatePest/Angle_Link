using Client;
using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Script.HotUpdateDll.Game.Battle_
{
    public class SlotsManager : MonoBehaviour
    {
        public SpriteRenderer[] Player1 = new SpriteRenderer[6];
        public SpriteRenderer[] Player2 = new SpriteRenderer[6];

        Animation mAnimation;
        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            var b = BattleRoot.Get();
            if (b != null)
            {
                b.GetClientEvent().OnGameUpdata += UpdataSlot;
                b.RegisterBattleSpeed((f) => GameUtilityTool.AnimeSpeed(f, mAnimation));
                //ClientEvent and BattleSpeed  Has IDisposable In BattleRoot.OnDestroy
            }
        }
        #region Init
        private void Init()
        {
            mAnimation = GetComponent<Animation>();
            var Own = transform.Find("Own");
            SetSoltObj(Player1, Own);
            SetSoltObj(Player2, transform.Find("Opponent"));
        }
        void SetSoltObj(SpriteRenderer[] Target, Transform transform)
        {
            int i = 0;
            foreach (Transform t in transform)
            {
                Target[i] = t.gameObject.GetComponent<SpriteRenderer>();
                i++;
                if (i >= 6) return;
            }
        }
        #endregion
        public void UpdataSlot(BattleData battle)
        {
          
            ResetSlot();
            var P1 = battle.GetPlayer();
            if(P1 != null) 
            foreach (Unit u in P1.Units)
            {
                Player1[u.SoltId].gameObject.SetActive(true);
                var i = Player1[u.SoltId];
                i.sprite = u.GetArt().CharacterIcon;
                u.Solt = i.gameObject;
            }
            var P2 = battle.GetMonster();
            if (P2 != null)
                foreach (Unit u in P2.Units)
            {
                Player2[u.SoltId].gameObject.SetActive(true);
                var i = Player2[u.SoltId];
                i.sprite = u.GetArt().CharacterIcon;
                u.Solt = i.gameObject;
            }
        }
        void ResetSlot()
        {
            foreach (var g in Player1)
            {
                g.gameObject.SetActive(false);
                g.sprite = null;
            }
            foreach (var g in Player2)
            {
                g.gameObject.SetActive(false);
                g.sprite = null;
            }
        }

        public void HideAll()
        {
            SwichOwnTeam(false);
            SwichMonsterTeam(false);
        }
        public void SwichOwnTeam(bool b)
        {
            foreach (var g in Player1)
            {
                if (g.sprite != null)
                    g.gameObject.SetActive(b);
                else
                    g.gameObject.SetActive(false);
            }
        }
        public void SwichMonsterTeam(bool b)
        {
            foreach (var g in Player2)
            {
                if (g.sprite != null)
                    g.gameObject.SetActive(b);
                else
                    g.gameObject.SetActive(false);
            }
        }
        public async UniTask P1TeamEnters_Animation()
        {
            SwichOwnTeam(true);
            await GameUtilityTool.PlayAnime("Team_Enters_Animation", mAnimation, true);
        }
    }
}
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Script.Core.Fx.FXObj;

namespace Assets.Script.Core.Fx
{
    public class AbilityExcuteFx : IFx
    {
        float CurrentSp = 1f;
        public override void RegisterSpeed(ref Action<float> action)
        {
            action += (f) => CurrentSp = f;
        }
        public override async UniTask Excute(object BattleOrder)
        {
            var battleOrder = BattleOrder as Client_BattleOrder;
            var Fx = battleOrder.OrderLog.GetAbilityData().FXData;
            var OrderWaitKey = "AbilityLog";
            battleOrder.AddWaitUse(OrderWaitKey);
            await ExcuteFx(Fx,battleOrder, CurrentSp);
            battleOrder.SetWaitUse(OrderWaitKey,true);
        }

        protected override void Init(GameObject g)
        {
            return;
        }

        public async UniTask ExcuteFx(FXObj Fx,Client_BattleOrder battleOrder, float CurrnetSpeed)
        {
            var Sr = battleOrder.Caster.Solt.GetComponent<SpriteRenderer>();
            if (Sr != null) { Sr.sortingOrder += 1; }
            battleOrder.SetSpeed(CurrnetSpeed);
            await UniTask.SwitchToMainThread();
            await AbilityReady(Fx,battleOrder);
            await AbilityStart(Fx, battleOrder);
            await AbilityMakeEffect(Fx, battleOrder);
            await AbilityEnd(Fx, battleOrder);
            if (Sr != null) { Sr.sortingOrder -= 1; }

        }
        async UniTask AbilityReady(FXObj Fx, Client_BattleOrder battleOrder)
        {
            if (!Fx.ReadyAnime) return;
            if (battleOrder.ClientEvent.Ability_StartCasting != null)
                await UniTask.WhenAll(battleOrder.ClientEvent.Ability_StartCasting(battleOrder.Caster, battleOrder.Adata));
        }
        async UniTask AbilityStart(FXObj Fx, Client_BattleOrder battleOrder)
        {
            if (Fx.moveType != StartMoveType.No)
            {
                var Tv = GetTargetMovePostint(Fx,battleOrder.Targets[0]);
                await AnimFX_Tool.RunAnimeFx(AnimFX_Tool.MoveTo(battleOrder.Caster.Solt, Tv, battleOrder.MoveTime / battleOrder.Speed));
            }
            if (battleOrder.ClientEvent.Ability_StartCasting != null)
                await UniTask.WhenAll(battleOrder.ClientEvent.Ability_StartCasting(battleOrder.Caster, battleOrder.Adata));
            //await StartFX?.Excute();
        }
        async UniTask AbilityMakeEffect(FXObj Fx, Client_BattleOrder battleOrder)
        {

            if (Fx.MakeFX != null)
            {
                var OrderWaitKey = "AbilityLog";
                for (int i = 0; i < battleOrder.OrderLog.ExecutionResult.Count; i++)
                {

                    var temp = battleOrder.OrderLog.ExecutionResult[i];
                    var target = FindUnit(battleOrder.Targets, temp.Target);
                    var TargetpKey = OrderWaitKey + temp.Target;
                    battleOrder.AddWaitUse(TargetpKey);
                    Fx.MakeFX?.Excute(battleOrder.Caster, target, battleOrder.Speed, () =>
                    {
                        battleOrder.ClientEvent.OnExecutionResult?.Invoke(temp, battleOrder.Caster, target);
                        if (temp.After != null)
                        {
                            battleOrder.ClientEvent.OnExecutionResult?.Invoke(temp.After, battleOrder.Caster, target);

                        }
                        battleOrder.SetWaitUse(TargetpKey, true);
                    });
                }
            }
            else
            {
                foreach (var a in battleOrder.OrderLog.ExecutionResult)
                {
                    var target = FindUnit(battleOrder.Targets, a.Target);
                    battleOrder.ClientEvent.OnExecutionResult?.Invoke(a, battleOrder.Caster, target);
                }

            }

        }
        async UniTask AbilityEnd(FXObj Fx, Client_BattleOrder battleOrder)
        {
            if (battleOrder.Caster.Solt.transform.position != battleOrder.StartPosition)
            {
                await AnimFX_Tool.RunAnimeFx(AnimFX_Tool.MoveTo(battleOrder.Caster.Solt, battleOrder.StartPosition, battleOrder.MoveTime / battleOrder.Speed));
            }

            // await EndFX?.Excute();
        }

        Unit FindUnit(List<Unit> Targets, string uid)
        {
            if (Targets == null) return default;
            foreach (var target in Targets)
            {
                if (target.Uid == uid)
                    return target;
            }
            return null;
        }

        Vector3 GetTargetMovePostint(FXObj Fx, Unit Target)
        {

            if (Fx.moveType == StartMoveType.Target)
            {
                return Target.Solt.transform.position;
            }
            if (Fx.moveType == StartMoveType.CenterOfMap)
            {
                return new Vector3(0, 0, 0);
            }
            return default;
        }


    }

}

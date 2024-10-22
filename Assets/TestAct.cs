using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class TestAct : MonoBehaviour
{


  


    //BattleLogManager battleLogManager =new();

    //private void Start()
    //{
    //    TestData_1();
    //    TestData_2();
    //    Test_A();
    //}
    //void TestData_1()
    //{
    //    var D1 = new BattleSelectOrder();
    //    D1.AbilityLv = 100;
    //    D1.OrderUid = ""+123456;
    //    D1.Caster = "A";
    //    battleLogManager.Add_OrderLog(D1,out var log);
    //    var AED = new AbilityExcuteData("ABC");
    //    AED.AbilityMainParameter = 100;
    //    log.ExcuteData.Add(AED);
    //    var ER = new ExecutionResult();
    //    ER.Caster = AED.Caster;
    //    ER.Target = ER.Caster; 
    //    log.ExecutionResult.Add(ER);
    //}
    //void TestData_2()
    //{
    //    Battle battle = new Battle(null, "Test2");

    //    battle.gameState = GameState.GameEnd;
    //    battle.CurrentWave = 100;

    //    var e = new BattleEventLog();
    //    e.data = battle;
    //    e.ID = BattleEventTag.Updata;
    //    battleLogManager.Add_Log(e);
    //}
    //void Test_A()
    //{

    //    var mdata = new MsgLogs();
    //    mdata.GameLogs = battleLogManager.GetLogs();
    //    var f = SendAction(9999, mdata);
    //    ///
    //    var R  = new FastBufferReader(f,Allocator.Temp);
    //    R.ReadValueSafe(out ushort type);
    //    Debug.Log($"TypeID = {type}");
    //    ///
    //    var S = new NetSerializedData(R);
    //    var msg = S.Get<MsgLogs>();
    //    //msg.GameLogs = battleLogManager.GetLogs();
    //    var Json =ApiTool.ToJson(msg);
    //    Debug.Log(Json);

    //}

    //protected FastBufferWriter SendAction<T>(ushort type, T data, NetworkDelivery delivery = NetworkDelivery.Reliable) where T : INetworkSerializable
    //{
    //    FastBufferWriter writer = new FastBufferWriter(4096,Allocator.Temp, Network.Msg_size);
    //    writer.WriteValueSafe(type);
    //    writer.WriteNetworkSerializable(data);
    //    //Network.Get().Messaging.Send(SendMsgTag, TargetID, writer, delivery);
    //    //writer.Dispose();
    //    return writer;
    //}
}

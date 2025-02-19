using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

public class TestAct : MonoBehaviour
{
    Queue<Func<UniTask>> Funcs { get; set; } = new();

    private void Start()
    {
        TaskAdd();
        Excute();
    }
    async UniTask TesUniTask()
    {
        Debug.Log("A");
        await UniTask.Delay(1000);
        Debug.Log($"B ");
      
    }
    void TaskAdd()
    {
        Funcs.Enqueue(TesUniTask);
        Funcs.Enqueue(TesUniTask);
        Funcs.Enqueue(TesUniTask);
   
    }
    async void Excute()
    {
       
        while (Funcs.Count != 0)
        {
            var a = Funcs.Dequeue();
            await UniTask.Yield();
            await UniTask.SwitchToMainThread();
            await a.Invoke();

            await UniTask.Delay(1000); 
        }
    }

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
public class TestCalculateAddValue
{
    public T1 TT1 = new T1();
    public T2 TT2 = new T2();    

    public void Run()
    {
        ref int  i = ref  GetV(TT1);
        var v = ParametersTypeTool.CalculateAddValue(i, 2, ModfireType.Multiplication);

        Debug.Log(v);
        i = v;
        Debug.Log(TT1.a);

    }


    public static  ref int GetV(TestV test)
    {
        return  ref test.a;
    }
    public class TestV
    {
        public int a;
    }
    public class T1 : TestV
    {

        public T1()
        {
            a = 10;
        }

    }
    public class T2 : TestV
    {
        public T2()
        {
            a = 20;
        }
    }
}

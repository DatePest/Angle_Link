using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsManager : MonoBehaviour
{
    public GameObject[] Player1 = new GameObject[6];
    public GameObject[] Player2 = new GameObject[6];
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        var b = BattleRoot.GetBattleData();
        if (b != null) b.GameEvent.OnGameUpdata += UpdataSlot;
    }
    private void OnDestroy()
    {
        var b = BattleRoot.GetBattleData();
        if (b != null ) b.GameEvent.OnGameUpdata -= UpdataSlot;
    }
    private void Init()
    {
        SetSoltObj(Player1, transform.Find("Own"));
        SetSoltObj(Player2, transform.Find("Opponent"));
    }
    void SetSoltObj(GameObject[] Target,Transform transform)
    {
        int i = 0;
        foreach (Transform t in transform)
        {
            Target[i] = t.gameObject;
            i++;
            if (i >= 6) return;
        }
    }
    public void UpdataSlot(Battle logic )
    {
        foreach (Unit u in logic.Player[0])
        {
            RawImage i = Player1[u.SoltId].GetComponent<RawImage>();
            if (i != null)
                i.texture = u.UnitData.GetData().Art.CharacterIcon.texture;
        }
        foreach (Unit u in logic.Player[1])
        {
            RawImage i = Player1[u.SoltId].GetComponent<RawImage>();
            if (i != null)
                i.texture = u.UnitData.GetData().Art.CharacterIcon.texture;
        }
    }
}

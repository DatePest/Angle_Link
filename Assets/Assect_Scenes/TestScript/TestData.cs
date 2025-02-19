using Client;
using UnityEngine;
using System.Collections.Generic;

public class TestData : ITestData
{
    [SerializeField] UserData playerData;


    [SerializeField] CharacterData[] characters;
    public override void Excute()
    {
        Addcharacters();

        ClientRoot.Get().ClientUserData.SetUserData(playerData);
    }


    void Addcharacters()
    {
        var List = new List<CharacterData_Net>();

        foreach (var c in characters)
        {
            List.Add(CharacterData_Net.Create(c));
        }

        playerData.PlayerData.Characters = List;

       
    }
}

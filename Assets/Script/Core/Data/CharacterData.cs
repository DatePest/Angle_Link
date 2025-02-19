using System;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "AL/Character", order = 10)]
public class CharacterData : IUnitData
{
    public int CharacterRank = 1;
    public CharacterPersonal characterPersonal;

}
[System.Serializable]
public class CharacterPersonal
{
    public int Birthday_month;
    public int Birthday_day;
    public int Height;
    public string CV;
    public string Designer;
}
[System.Serializable]
public class Character
{
    public UnitAttribute UnitAttribute ;
    public NetworkFilteredData<CharacterData> characterData;
    public CharacterData_Net characterNetData;
    public void SetCharacterData(CharacterData data)
    {
        UnitAttribute = UnitAttribute.Create(data.Attribute);
        characterData = new(data);
    }
    public void SetAttributeData(CharacterData_Net data)
    {
        characterNetData = data;
        UnitAttribute.Lv = data.Lv;
        UnitAttribute.CalculateLvGrowth(characterNetData);
    }
     
    public static  async Task<Character> Create(CharacterData_Net character_Net)
    {
        var C = new Character();
        var data = await AssetFInd.GetCharacterData_Async(character_Net.AssetName);
        C.SetCharacterData(data);
        C.SetAttributeData(character_Net);

        return C;

    }
}

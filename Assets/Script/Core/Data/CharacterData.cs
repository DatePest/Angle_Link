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
    public UnitAttribute UnitAttribute;
    public CharacterData characterData;
    public CharacterData_Net characterAbilityData;
    Character() { }
    public void SetCharacterData(CharacterData data)
    {
        UnitAttribute = new UnitAttribute(data.Attribute);
        characterData = data;
    }
    public void SetAttributeData(CharacterData_Net data)
    {
        characterAbilityData = data;
        UnitAttribute.Lv = data.Lv;
        UnitAttribute.CalculateLvGrowth(characterAbilityData);
    }

    public static Character Create(CharacterData_Net character_Net)
    {
        var C = new Character();
        var data = YooAsset_Tool.GetPackageData<CharacterData>(GameConstant.PackName_GameCore, character_Net.IUnitDataName);
        C.SetCharacterData(data);
        C.SetAttributeData(character_Net);

        return C;

    }
}

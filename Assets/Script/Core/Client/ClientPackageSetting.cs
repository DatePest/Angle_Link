using UnityEngine;
using YooAsset;
[CreateAssetMenu(fileName = "ClientPackageSetting", menuName = "Client/ClientPackageSetting", order = 0)]
public class ClientPackageSetting : ScriptableObject
{
    public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;
    public string[] PackageNames;

    public static ClientPackageSetting Get()
    {
        var temp = Resources.Load<ClientPackageSetting>("ClientPackageSetting");
        //var temp = YooAsset_Tool.GetPackageData("Initial", "ClientPackageSetting").GetAssetObject<ClientPackageSetting>();
        if (temp != null) return temp;
        return null;

    }
}

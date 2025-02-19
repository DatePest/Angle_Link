using Assets.Script.Core.Data;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class NetworkFilteredData<T> where T : iSobj_Name
{
    T Data;
    public string TargetNameOrId;
    public string DataInPackageName;
    public NetworkFilteredData() { }

    public NetworkFilteredData(T data, string dataInPackageName = GameConstant.PackName_GameCore)
    {
        Data = data;
        TargetNameOrId = data.AssetName;
        DataInPackageName = dataInPackageName;
    }
    public static async Task<NetworkFilteredData<T>> New_Async(T data, string dataInPackageName = GameConstant.PackName_GameCore)
    {
        await UniTask.SwitchToMainThread();
        var Ndata = new NetworkFilteredData<T>();
        Ndata.Data = data;
        Ndata.TargetNameOrId = data.AssetName;
        Ndata.DataInPackageName = dataInPackageName;
        return Ndata;
    }
    public T GetData()
    {
        if (Data == null) FindData(DataInPackageName);
        return Data;
    }
    public async Task<T> GetData_Async()
    {
        if (Data == null) return null;
        await UniTask.SwitchToMainThread();
        FindData(DataInPackageName);
        return Data;
    }
    public T FindData(string packagname)
    {
        if (TargetNameOrId == string.Empty) throw new Exception($" NetworkFilteredData {typeof(T)} TargetNameOrId is null");
        if (DataInPackageName == string.Empty) throw new Exception($" NetworkFilteredData {typeof(T)} DataInPackageName is null");
        T data = YooAsset_Tool.GetPackageData_Sync<T>(packagname, TargetNameOrId);
        if (data == null) throw new Exception("Find Target is null");
        Data = data;

        return Data;
    }

}
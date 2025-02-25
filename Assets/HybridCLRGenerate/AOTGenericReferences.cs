using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"Core.dll",
		"OtherTools.dll",
		"System.Core.dll",
		"UniTask.dll",
		"UnityEngine.CoreModule.dll",
		"YooAsset.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Assets.Script.HotUpdateDll.Game.Battle_.SlotsManager.<P1TeamEnters_Animation>d__12>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Assets.Script.HotUpdateDll.Game.Battle_.SlotsManager.<P1TeamEnters_Animation>d__12>
	// Cysharp.Threading.Tasks.ITaskPoolNode<object>
	// Cysharp.Threading.Tasks.UniTaskCompletionSourceCore<Cysharp.Threading.Tasks.AsyncUnit>
	// NetworkFilteredData<object>
	// SingletonTool.SigMono<object>
	// System.Action<byte>
	// System.Action<float>
	// System.Action<object,object,object>
	// System.Action<object,object>
	// System.Action<object>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<int>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Dictionary.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<int>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.KeyValuePair<object,int>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<int>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<object>
	// System.Func<Cysharp.Threading.Tasks.UniTask>
	// System.Func<int>
	// System.Func<object,Cysharp.Threading.Tasks.UniTask>
	// System.Func<object,int>
	// System.Func<object,object,Cysharp.Threading.Tasks.UniTask>
	// System.Func<object,object,object>
	// System.Func<object,object>
	// System.Func<object>
	// System.Linq.Buffer<object>
	// System.Linq.EnumerableSorter<object,int>
	// System.Linq.EnumerableSorter<object>
	// System.Linq.OrderedEnumerable.<GetEnumerator>d__1<object>
	// System.Linq.OrderedEnumerable<object,int>
	// System.Linq.OrderedEnumerable<object>
	// System.Predicate<object>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<object>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<object>
	// System.Threading.Tasks.Task<object>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<object>
	// System.Threading.Tasks.TaskFactory<object>
	// UnityEngine.Events.InvokableCall<object>
	// UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene,int>
	// UnityEngine.Events.UnityAction<object>
	// UnityEngine.Events.UnityEvent<object>
	// }}

	public void RefMethods()
	{
		// object Client.ClientUserData.GetCache<object>(string,bool)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Assets.Script.HotUpdateDll.Game.Battle_.SlotsManager.<P1TeamEnters_Animation>d__12>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Assets.Script.HotUpdateDll.Game.Battle_.SlotsManager.<P1TeamEnters_Animation>d__12&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Assets.Script.HotUpdateDll.Game.Battle_.SlotsManager.<P1TeamEnters_Animation>d__12>(Assets.Script.HotUpdateDll.Game.Battle_.SlotsManager.<P1TeamEnters_Animation>d__12&)
		// System.Linq.IOrderedEnumerable<object> System.Linq.Enumerable.OrderByDescending<object,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.List<object> System.Linq.Enumerable.ToList<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitOnCompleted<UnityEngine.Awaitable.Awaiter,HomeRoot.<onSceneLoad>d__4>(UnityEngine.Awaitable.Awaiter&,HomeRoot.<onSceneLoad>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.YieldAwaitable.Awaiter,HomeRoot.<Awake>d__2>(Cysharp.Threading.Tasks.YieldAwaitable.Awaiter&,HomeRoot.<Awake>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.YieldAwaitable.Awaiter,NewsRoot.<Awake>d__3>(Cysharp.Threading.Tasks.YieldAwaitable.Awaiter&,NewsRoot.<Awake>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,Assets.Script.HotUpdateDll.Game.Battle_.Client_GameEnd.<Excute>d__8>(System.Runtime.CompilerServices.TaskAwaiter&,Assets.Script.HotUpdateDll.Game.Battle_.Client_GameEnd.<Excute>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,Assets.Script.HotUpdateDll.Game.Battle_.Client_GameEnd.<updataItem>d__6>(System.Runtime.CompilerServices.TaskAwaiter<object>&,Assets.Script.HotUpdateDll.Game.Battle_.Client_GameEnd.<updataItem>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,Assets.Script.HotUpdateDll.Game.CharacterDevelop.Develop_Ui_Lv.ExpItemUseObj.<Init>d__14>(System.Runtime.CompilerServices.TaskAwaiter<object>&,Assets.Script.HotUpdateDll.Game.CharacterDevelop.Develop_Ui_Lv.ExpItemUseObj.<Init>d__14&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<Assets.Script.HotUpdateDll.Game.Battle_.Client_GameEnd.<Excute>d__8>(Assets.Script.HotUpdateDll.Game.Battle_.Client_GameEnd.<Excute>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<Assets.Script.HotUpdateDll.Game.Battle_.Client_GameEnd.<updataItem>d__6>(Assets.Script.HotUpdateDll.Game.Battle_.Client_GameEnd.<updataItem>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<Assets.Script.HotUpdateDll.Game.CharacterDevelop.Develop_Ui_Lv.ExpItemUseObj.<Init>d__14>(Assets.Script.HotUpdateDll.Game.CharacterDevelop.Develop_Ui_Lv.ExpItemUseObj.<Init>d__14&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<HomeRoot.<Awake>d__2>(HomeRoot.<Awake>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<HomeRoot.<onSceneLoad>d__4>(HomeRoot.<onSceneLoad>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<NewsRoot.<Awake>d__3>(NewsRoot.<Awake>d__3&)
		// object UnityEngine.Component.GetComponent<object>()
		// object UnityEngine.Component.GetComponentInChildren<object>()
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>(bool)
		// object UnityEngine.Object.FindAnyObjectByType<object>()
		// object UnityEngine.Object.FindFirstObjectByType<object>()
		// object UnityEngine.Object.FindFirstObjectByType<object>(UnityEngine.FindObjectsInactive)
		// object UnityEngine.Object.Instantiate<object>(object)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform,bool)
		// object YooAsset.AssetHandle.GetAssetObject<object>()
		// YooAsset.AssetHandle YooAsset.ResourcePackage.LoadAssetSync<object>(string)
		// System.Collections.Generic.List<object> YooAsset_Tool.GetGameDatas<object>(string,string)
		// object YooAsset_Tool.GetPackageData_Sync<object>(string,string)
	}
}
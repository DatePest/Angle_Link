using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace EditorTool
{
    public class EditorAPI
    {

        public static List<T> FindAsset<T>(string path) where T : UnityEngine.Object
        {
            var data = new List<T>();
            string[] guids = null;
            if (path == "" || string.IsNullOrEmpty(path))
            {
                guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            }
            else
            {
                string[] searchInFolders = new string[] { path };
                guids = AssetDatabase.FindAssets($"t:{typeof(T)}", searchInFolders);
            }
            foreach (var guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var temp = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (temp == null) continue;
                data.Add(temp);
            }
             return data;
        }
    }
}
#endif

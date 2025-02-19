using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using System.IO;
using EditorTool;

public class CharacterEditorWindow : EditorWindow
{
    private Texture2D selectedCharacterIcon;
    CharacterData character;
    string Asspath;
    Object selectedFolder;
    [MenuItem("MyEditor/CharacterEditor")]
    public static void ShowWindow()
    {
        var win =GetWindow<CharacterEditorWindow>("MyTestEditor",true);
        win.minSize = new Vector2(800,600);
    }
    void OnGUI()
    {
        GUILayout.Label("使用 Unity 內建圖標作為按鈕", EditorStyles.boldLabel);
        SelectedFolder();
        GUILayout.BeginHorizontal();
        LoadData();
        GUILayout.BeginVertical();
        {
            Icon();
            Button1();
            Button_IconContent();
        }
        GUILayout.EndVertical();
        {
            ShowTargetProperty();
        }
        GUILayout.EndHorizontal();
    }
    public void ShowTargetProperty()
    {
        if (character == null) return;
        GUILayout.BeginVertical();
        var Sobj = new SerializedObject(character);
        var property = Sobj.GetIterator();
        property.NextVisible(true);
        do
        {
            Debug.Log(property.name);
            EditorGUILayout.PropertyField(property, true); 
        } 
        while (property.NextVisible(false));
        GUILayout.EndVertical();
    }
    public void SelectedFolder()
    {
        selectedFolder = EditorGUILayout.ObjectField("Select Folder", selectedFolder, typeof(Object), false);
        
        if (selectedFolder != null)
        {
            string path = AssetDatabase.GetAssetPath(selectedFolder);

            // 確認是資料夾
            if (Directory.Exists(path))
            {
                string folderName = new DirectoryInfo(path).Name;
                Asspath = path;
                GUILayout.Label($"Folder Path: {path}");
                //GUILayout.Label($"Folder Name: {folderName}");
            }
            else
            {
                GUILayout.Label("The selected object is not a folder.");
            }
        }
        else
        {
            GUILayout.Label("No folder selected.");
        }
    }
    public void LoadData()
    {
        GUILayout.BeginVertical();
        {
            var style = new GUIStyle(EditorStyles.boldLabel);
            style.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("Assset", style);
        }
    
        if (Asspath == string.Empty) return;
        var datas =EditorAPI.FindAsset<CharacterData>(Asspath);
        foreach (var data in datas)
        {
            if (GUILayout.Button(data.AssetName))
            {
                character = data;
                Debug.Log(data.AssetName);
            }
        }
        GUILayout.EndVertical();
    }
    public  void Icon()
    {
        GUILayout.BeginVertical();
        //selectedCharacterIcon = (Texture2D)EditorGUILayout.ObjectField("Character Icon", selectedCharacterIcon, typeof(Texture2D), false);
        character = (CharacterData)EditorGUILayout.ObjectField("CharacterData", character, typeof(CharacterData), false);

        if (character == null) return;

        Texture2D t = character.Art.CharacterIcon.texture;
        if (t != null)
        {
            GUILayout.Label("Preview:", EditorStyles.label);
            GUILayout.Box(t, GUILayout.Width(200), GUILayout.Height(200));
        }
        else
        {
            GUILayout.Label("No character selected or no icon assigned.");
        }
        GUILayout.EndVertical();
    }
    public static void Button1()
    {
        GUIContent findIconContent = EditorGUIUtility.IconContent("d_Toolbar Plus@2x");
        //findIconContent.text = "Test";
        if (GUILayout.Button(findIconContent, GUILayout.Height(40), GUILayout.Width(40)))
        {
            Debug.Log("按鈕被點擊！");
        }
    }

    public static void Button_IconContent()
    {
        GUIContent customContent = EditorGUIUtility.IconContent("UnityEditor.ConsoleWindow");
        customContent.text = "Console Button";
        if (GUILayout.Button(customContent, GUILayout.Height(40)))
        {
            Debug.Log("點擊了帶文字的按鈕！");
        }

    }
}

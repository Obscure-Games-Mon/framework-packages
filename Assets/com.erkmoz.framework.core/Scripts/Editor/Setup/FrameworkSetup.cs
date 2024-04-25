using System;
using System.IO;
using Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class FrameworkSetup : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Tools/Framework/Setup")]
    public static void ShowExample()
    {
        FrameworkSetup wnd = GetWindow<FrameworkSetup>();
        wnd.titleContent = new GUIContent("Framework Setup");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        root.Q<Button>("button-prefab-registry").clicked += CreatePrefabRegistry;
    }

    private void OnDestroy()
    {
        VisualElement root = rootVisualElement;

        root.Q<Button>("button-prefab-registry").clicked -= CreatePrefabRegistry;
    }

    private void CreatePrefabRegistry()
    {
        this.Log("Creating prefab registry.");

        string[] guids = AssetDatabase.FindAssets("t:PrefabRegistry");
        
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            PrefabRegistry existingAsset = AssetDatabase.LoadAssetAtPath<PrefabRegistry>(assetPath);
            
            if (existingAsset != null)
            {
                this.LogError($"Prefab registry already exists at: {assetPath}");
                return;
            }
        }
        
        PrefabRegistry asset = ScriptableObject.CreateInstance<PrefabRegistry>();

        string folderPath = "Assets/Framework/Resources";

        if (!Directory.Exists("Assets/Framework"))
        {
            AssetDatabase.CreateFolder("Assets", "Framework");
        }
        
        if (!Directory.Exists("Assets/Framework/Resources"))
        {
            AssetDatabase.CreateFolder("Assets/Framework", "Resources");
        }
        
        AssetDatabase.CreateAsset(asset,  Path.Combine(folderPath, "(Prefab Registry) Prefab Registry.asset"));
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}

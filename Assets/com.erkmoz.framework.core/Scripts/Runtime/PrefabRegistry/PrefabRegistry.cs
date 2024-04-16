using System.Collections.Generic;

#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

#endif

using UnityEngine;

namespace Framework
{
    #if UNITY_EDITOR
    
    class MyCustomBuildProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }
        
        public void OnPreprocessBuild(BuildReport report)
        {
            
        }
    }
    
    #endif
    
    [CreateAssetMenu]
    public class PrefabRegistry : ScriptableObject
    {
        [System.Serializable]
        public class PrefabRegistryItem
        {
            public string m_Type;
            public GameObject m_Prefab;
        }

        [SerializeField] private List<PrefabRegistryItem> m_Prefabs = new();

        public void Register(System.Type type, MonoBehaviour prefab)
        {
            Debug.Log($"Registering {type} for {prefab.gameObject.name}");

            foreach (PrefabRegistryItem prefabItem in m_Prefabs)
            {
                if (prefabItem.m_Type == type.ToString())
                {
                    prefabItem.m_Prefab = prefab.gameObject;
                    return;
                }
            }

            m_Prefabs.Add(new PrefabRegistryItem() { m_Type = type.ToString(), m_Prefab = prefab.gameObject });
            
            #if UNITY_EDITOR
            
            EditorUtility.SetDirty(Core.m_PrefabRegistry);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            #endif
        }

        public static bool LoadUniquePrefab<T>(out T loadedObject) where T : MonoBehaviour
        {
            Debug.LogError($"Searching in prefabs: {Core.m_PrefabRegistry.m_Prefabs.Count}");

            foreach (var prefabItem in Core.m_PrefabRegistry.m_Prefabs)
            {
                Debug.LogError($"Searching for {typeof(T).ToString()} in {prefabItem.m_Type}");
                if (prefabItem.m_Type == typeof(T).ToString())
                {
                    loadedObject = prefabItem.m_Prefab.GetComponent<T>();
                    Debug.LogError("Returning loaded object");
                    return loadedObject;
                }
            }

            Debug.LogError("Returning null prefab");

            loadedObject = null;
            return false;
        }
    }
}
#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Framework
{
    public static partial class Core
    {
        public static partial class EditorUtility
        {
            public static bool LoadUniquePrefab<T>(out T loadedObject) where T : UnityEngine.Object
            {
                loadedObject = null;
                string storedPath = EditorPrefs.GetString($"Framework-Path-{typeof(T)}");
            
                if (!string.IsNullOrEmpty(storedPath))
                {
                    T prefab = AssetDatabase.LoadAssetAtPath<T>(storedPath);

                    if (prefab)
                    {
                        prefab.Log("Found prefab with pre-stored path.");
                        loadedObject = prefab;
                        return true;
                    }
                }
            
                string[] guids = AssetDatabase.FindAssets("t:Prefab"); 

                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    T prefab = AssetDatabase.LoadAssetAtPath<T>(path);

                    if (prefab)
                    {
                        loadedObject = prefab;
                        EditorPrefs.SetString($"Framework-Path-{typeof(T)}", path);
                        prefab.Log("Searched and found prefab.");
                        return true;
                    }   
                }

                return false;
            }

            /// <summary>
            /// Gets the path of a given class type
            /// </summary>
            /// <param name="classTypeOf">Use typeof() to grab class type.</param>
            /// <param name="path"></param>
            /// <returns></returns>
            public static bool GetScriptPath(System.Type classTypeOf, out string path)
            {
                return DoGetScriptPath(classTypeOf, out path);
            }

            /// <summary>
            /// Gets the path of a given class
            /// </summary>
            public static bool GetScriptPath<T>(out string path)
            {
                return DoGetScriptPath(typeof(T), out path);
            }

            private static bool DoGetScriptPath(System.Type scriptTypeOf, out string path)
            {
                string scriptType = scriptTypeOf.ToString();
                path = string.Empty;
                
                int lastIndex = scriptType.LastIndexOf('.') + 1;
                string scriptName = scriptType[lastIndex..];
                
                string[] guids = AssetDatabase.FindAssets($"t:script {scriptName}");

                foreach (var guid in guids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    int lastSlashIndex = assetPath.LastIndexOf('/');
                    
                    var substring = assetPath[lastSlashIndex..];
            
                    if (substring == $"/{scriptName}.cs")
                    {
                        path = assetPath;
                        return true;
                    }
                }

                return false;
            }

            public static UnityEditor.Build.NamedBuildTarget GetCurrentNamedBuildTarget()
            {
                BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
                BuildTargetGroup targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
                return UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(targetGroup);
            }
            
            public static void RemoveFromDefineSymbols(params string[] symbolsToRemove)
            {
                string currentSymbols = PlayerSettings.GetScriptingDefineSymbols(GetCurrentNamedBuildTarget());
                string updatedSymbols = currentSymbols;

                foreach (string symbolToRemove in symbolsToRemove)
                {
                    updatedSymbols = updatedSymbols.Replace(symbolToRemove + ";", "");
                    updatedSymbols = updatedSymbols.Replace(symbolToRemove, "");
                }
                
                Debug.Log($"Symbols should now be: {updatedSymbols}");
            
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, updatedSymbols);
                AssetDatabase.Refresh();
                UnityEditor.EditorUtility.RequestScriptReload();
            }
        }
    }
}

#endif
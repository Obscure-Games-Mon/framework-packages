using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mardonpol
{
    public class Exporter
    {
        [MenuItem("Assets/Framework/Export package", false, 15)]
        public static void Export()
        {
            Object selectedObject = Selection.activeObject;

            if (!selectedObject)
            {
                Debug.LogError("Nothing selected.");
            }

            string savePath = EditorUtility.SaveFilePanel("Choose save path", Application.dataPath, "Real package", ".unitypackage");
            
            EditorApplication.ExecuteMenuItem("Assets/Select Dependencies");
            var selectedAssets = Selection.objects;
            var validSelectedAssets = new List<string>(Selection.objects.Length);

            foreach (var selectedAsset in selectedAssets)
            {
                var path = AssetDatabase.GetAssetPath(selectedAsset);

                if (path.StartsWith("Assets"))
                {
                    validSelectedAssets.Add(path);
                    Debug.Log($"Selected asset: {path}");
                }
            }

            Selection.objects = null;
            AssetDatabase.ExportPackage(validSelectedAssets.ToArray(), savePath,ExportPackageOptions.Default | ExportPackageOptions.Interactive);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Framework
{
    public static partial class Core
    {
        #region Extensions

        #region Logs

        #if UNITY_2022_3_OR_NEWER
        [HideInCallstack]
        #endif
        public static void Log<T>(this T contextObject, string message)
        {
            Debug.Log($"<color=#627bc4>[{contextObject.GetType()}] </color>{message}");
        }
        
        #if UNITY_2022_3_OR_NEWER
        [HideInCallstack]
        #endif
        public static void LogWarning<T>(this T contextObject, string message)
        {
            Debug.Log($"<color=#ab953c>[{contextObject.GetType()}] </color>{message}");
        }
        
        #if UNITY_2022_3_OR_NEWER
        [HideInCallstack]
        #endif
        public static void LogImportant<T>(this T contextObject, string message)
        {
            Debug.Log($"<color=#b988d1>[{contextObject.GetType()}] </color>{message}");
        }
        
        #if UNITY_2022_3_OR_NEWER
        [HideInCallstack]
        #endif
        public static void LogError<T>(this T contextObject, string message)
        {
            Debug.LogError($"<color=#d9766f>[{contextObject.GetType()}] </color>{message}");
        }
        
        #endregion
        
        #endregion

        public static class Prefs
        {
            private static readonly Dictionary<string, string> Keys = new Dictionary<string, string>()
            {
                {Key.LogColor, "627bc4"},
                {Key.ImportantLogColor, "ab953c"},
                {Key.WarningLogColor, "b988d1"},
                {Key.ErrorLogColor, "d9766f"}
            };
            
            public class Key
            {
                public const string LogColor = "LogColor";
                public const string ImportantLogColor = "ImportantLogColor";
                public const string WarningLogColor = "WarningLogColor";
                public const string ErrorLogColor = "ErrorLogColor";
                
                public const string Logs = "Logs";
            }

            public static Color GetColor(string key)
            {
                var colorString = EditorPrefs.GetString(key, Keys[key]);
                ColorUtility.TryParseHtmlString($"#{colorString}", out Color parsedColor);
                
                return parsedColor;
            }
        }
    }
}
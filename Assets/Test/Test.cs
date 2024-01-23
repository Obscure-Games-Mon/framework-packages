using Framework;
using Framework.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestNamespace
{
    public class Test : MonoBehaviour
    {
        //public static class StaticClass
        //{
        //    
        //}

        public class NormalClass
        {
            
        }
        
        [ContextMenu("Test")]
        private void TestMethod()
        {
            //float tmp = 0;
            //tmp.MapValue(0, 0, 0, 0);
            
            //gameObject.GetComponent<Collider>();
            //transform.GetCachedComponent<Collider>();
            //gameObject.GetCachedComponent<Collider>();

            Core.EditorUtility.GetScriptPath(typeof(StaticClass), out var path);
            Core.EditorUtility.GetScriptPath<Test>(out var path2);
            
            this.Log($"Path: {path}, path2: {path2}");
        }
        
        [ContextMenu("Test 2")]
        private void TestMethod2()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
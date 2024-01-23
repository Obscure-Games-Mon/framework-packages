using Framework;
using Framework.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestNamespace
{
    public class Test : MonoBehaviour
    {
        [ContextMenu("Test")]
        private void TestMethod()
        {
            float tmp = 0;
            tmp.MapValue(0, 0, 0, 0);
            
            //gameObject.GetComponent<Collider>();
            transform.GetCachedComponent<Collider>();
            //gameObject.GetCachedComponent<Collider>();
        }
        
        [ContextMenu("Test 2")]
        private void TestMethod2()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
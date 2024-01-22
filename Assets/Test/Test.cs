using Framework;
using Framework.Mathematics;
using UnityEngine;

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
    }
}
using Framework;
using Framework.Mathematics;
using UnityEngine;

namespace TestNamespace
{
    public class Test : MonoBehaviour
    {
        private void TestMethod()
        {
            float tmp = 0;
            tmp.MapValue(0, 0, 0, 0);
            this.Log(tmp.ToString("C"));
            
            gameObject.GetComponent<Collider>();
            transform.GetCachedComponent<Collider>();
            gameObject.GetCachedComponent<Collider>();
        }
    }
}
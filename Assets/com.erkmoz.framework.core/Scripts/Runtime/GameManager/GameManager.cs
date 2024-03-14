using UnityEngine;

namespace Framework
{
    public class GameManager : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Load()
        {
            if (Core.Utility.LoadUniquePrefab(out GameManager prefab))
            {
                Instantiate(prefab);
            }
        }
    }
}
using UnityEngine;

namespace Game.Scenes.Common.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ArrowConfiguration", menuName = "Arrow Configuration", order = 0)]
    public class ArrowConfiguration : ScriptableObject
    {
        public GameObject prefab;
        public float shootForce;
    }
}
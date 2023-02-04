using UnityEngine;

namespace Equipment
{
    [CreateAssetMenu(fileName = "Veggy", menuName = "Create/Veggy", order = 0)]
    public class VeggySo : ScriptableObject
    {
        public string veggeyName;
        public Sprite veggeySprite;
        public GameObject veggeyPrefab;
        public GameObject seedPrefab;
    }
}
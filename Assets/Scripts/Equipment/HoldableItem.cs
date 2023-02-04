using System.Collections;
using UnityEngine;

namespace HoldableItems
{
    public class HoldableItem : MonoBehaviour
    {
        public enum ItemType { SEED, VEGTABLE }

        public ItemType Type;
    }
}
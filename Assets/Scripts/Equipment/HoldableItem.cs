using System.Collections;
using Equipment;
using UnityEngine;

namespace HoldableItems
{
    public class HoldableItem : MonoBehaviour
    {
        public enum ItemType { SEED, VEGTABLE }

        public ItemType Type;
        
        private VeggySo _currentVeggy;
        public VeggySo CurrentVeggy
        {
            get { return _currentVeggy; }
            set { _currentVeggy = value; }
        }
    }
}
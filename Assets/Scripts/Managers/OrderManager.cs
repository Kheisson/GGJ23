using Equipment;
using UI;
using UnityEngine;

namespace Managers
{
    public class OrderManager : MonoBehaviour
    {
        [SerializeField] private Order[] orders;
        [SerializeField] private VeggySo[] possibleVeggies;
        private const int MAX_ORDERS_ON_SCREEN = 3;
        
    }
}
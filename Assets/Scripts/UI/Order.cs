using Equipment;
using UnityEngine;

namespace UI
{
    public class Order: MonoBehaviour
    {
        [SerializeField] private OrderItem[] _orderItems;
        private int _orderSize;
        
        private void FillOrder(VeggySo[] veggies)
        {
            _orderSize = Random.Range(0, _orderItems.Length);
            var randomVeggy = veggies[Random.Range(0, veggies.Length)];
            
            for (int i = 0; i < _orderSize; i++)
            {
                _orderItems[i].SetVeggy(randomVeggy);
            }
        }
        
        private void TryCompleteOrder(VeggySo veggy)
        {
            for (int i = 0; i < _orderItems.Length; i++)
            {
                if (_orderItems[i].Veggy == veggy)
                {
                    _orderItems[i].Complete();
                    break;
                }
            }
        }
    }
}
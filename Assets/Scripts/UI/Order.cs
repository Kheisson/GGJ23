using System;
using Equipment;
using Managers;
using Timers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class Order: MonoBehaviour
    {
        [SerializeField] private OrderItem[] _orderItems;
        [SerializeField] private Slider orderTimeLeftSlider;
        private Timer _orderTimer;
        private int _orderSize;
        private OrderManager orderManager;

        public float OrderTimeout { get; set; } = 120f;
        public event Action orderCompleted;
        public event Action orderFailed;

        public void setParent(OrderManager orderManager) { 
            if (this.orderManager == null) 
            { 
                this.orderManager = orderManager;
            }
        }
        public void FillOrder(VeggySo[] veggies)
        {
            _orderSize = Random.Range(1, _orderItems.Length);

            for (int i = 0; i < _orderSize; i++)
            {
                var selectedOrder = _orderItems[i];
                selectedOrder.gameObject.SetActive(true);
                selectedOrder.SetVeggy(veggies[Random.Range(0, veggies.Length)]);
            }
            
            StartCountdown();
        }

        private void Update()
        {
            if(_orderTimer == null)
            {
                return;
            }
            
            _orderTimer.UpdateTimer();
            orderTimeLeftSlider.value = _orderTimer.GetTime();
        }

        public bool TryCompleteOrder(VeggySo veggy, Action<bool> onOrderComplete = null)
        {
            var orderComplete = false;
            
            for (int i = 0; i < _orderItems.Length; i++)
            {
                if(_orderItems[i] == null || _orderItems[i].gameObject.activeInHierarchy == false || _orderItems[i].OrderComplete)
                {
                    continue;
                }
                
                if (_orderItems[i].Veggy.veggeyName.Equals(veggy.veggeyName))
                {
                    _orderItems[i].Complete();
                    orderComplete = true;

                    break;
                }
            }
            
            onOrderComplete?.Invoke(orderComplete);
            CheckOrderComplete();
            return orderComplete;
        }

        private void StartCountdown()
        {
            _orderTimer = new Timer(OrderTimeout);
            orderTimeLeftSlider.maxValue = OrderTimeout;
            _orderTimer.StartTimer();
            _orderTimer.OnTimerEnd += OnOrderTimerEnd;
        }

        private void OnDestroy()
        {
            _orderTimer.OnTimerEnd -= OnOrderTimerEnd;
        }

        private void CheckOrderComplete()
        {
            for (int i = 0; i < _orderItems.Length; i++)
            {
                if (_orderItems[i] == null || !_orderItems[i].gameObject.activeInHierarchy) continue;
                
                if (!_orderItems[i].OrderComplete)
                {
                    return;
                }
            }

            orderCompleted?.Invoke();
            Debug.Log("Order Complete");
            if (gameObject) orderManager.DestroyOrder(this);
        }
        
        private void OnOrderTimerEnd()
        {
            orderFailed?.Invoke();
            Debug.Log("Order Failed");
            orderManager.DestroyOrder(this);
        }

    }
}
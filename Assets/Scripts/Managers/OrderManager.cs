using System;
using System.Collections.Generic;
using Equipment;
using TMPro;
using UI;
using UnityEngine;

namespace Managers
{
    public class OrderManager : MonoBehaviour
    {
        [SerializeField] private List<Order> orders;
        [SerializeField] private VeggySo[] possibleVeggies;
        [SerializeField] private Order orderPrefab;
        [SerializeField] private TextMeshProUGUI scoreCounter;
        [SerializeField] private int orderCompleteReward = 100;
        [SerializeField] private int orderFailedReduction = 20;

        private const int MAX_ORDERS_ON_SCREEN = 3;
        private const int START_TAKING_OVER_TIME = 10;
        private const int INTERVAL_BETWEEN_ORDERS = 20;
        
        public bool HasOrdersOnScreen()
        {
            return orders.Count > 0;
        }
        
        public void TryCompleteOrder(VeggySo veggy, Action<bool> onOrderComplete = null)
        {
            if (orders.Count == 0)
            {
                return;
            }
            
            foreach (var order in orders)
            {
                if (order == null)
                {
                    orders.Remove(order);

                    return;
                }
                
                if (order.TryCompleteOrder(veggy, onOrderComplete))
                {
                    break;
                }
            }
        }

        private void Start()
        {
            orders = new List<Order>();
            InvokeRepeating(nameof(AddOrder), START_TAKING_OVER_TIME, INTERVAL_BETWEEN_ORDERS);
        }
        
        private void AddOrder()
        {
            foreach (var selection in orders)
            {
                if (selection != null)
                {
                    continue;
                }

                orders.Remove(selection);
                break;
            }
            
            
            if (orders.Count >= MAX_ORDERS_ON_SCREEN)
            {
                Debug.Log("Reached Max Orders");
                return;
            }

            var order = Instantiate(orderPrefab, transform);
            order.setParent(this);
            order.FillOrder(possibleVeggies);
            order.orderCompleted += increaseScore;
            order.orderFailed += decreaseScore;
            orders.Add(order);
        }

        private void increaseScore()
        {
            int currScore;
            int.TryParse(scoreCounter.text, out currScore);
            currScore += orderCompleteReward;
            scoreCounter.text = currScore.ToString();
        }

        private void decreaseScore()
        {
            int currScore;
            int.TryParse(scoreCounter.text, out currScore);
            currScore -= orderFailedReduction;
            scoreCounter.text = currScore.ToString();
        }

        public void DestroyOrder(Order order)
        {
            order.orderCompleted -= increaseScore;
            order.orderFailed -= decreaseScore;
            Destroy(order.gameObject);
        }
    }
}
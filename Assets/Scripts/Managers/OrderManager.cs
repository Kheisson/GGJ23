using System.Collections.Generic;
using Equipment;
using UI;
using UnityEngine;

namespace Managers
{
    public class OrderManager : MonoBehaviour
    {
        [SerializeField] private List<Order> orders;
        [SerializeField] private VeggySo[] possibleVeggies;
        [SerializeField] private Order orderPrefab;
        private const int MAX_ORDERS_ON_SCREEN = 3;
        private const int START_TAKING_OVER_TIME = 10;
        private const int INTERVAL_BETWEEN_ORDERS = 20;

        private void Start()
        {
            orders = new List<Order>();
            InvokeRepeating(nameof(AddOrder), START_TAKING_OVER_TIME, INTERVAL_BETWEEN_ORDERS);
        }
        
        private void AddOrder()
        {
            if (orders.Count >= MAX_ORDERS_ON_SCREEN)
            {
                Debug.Log("Reached Max Orders");
                return;
            }

            var order = Instantiate(orderPrefab, transform);
            order.FillOrder(possibleVeggies);
            orders.Add(order);
        }
    }
}
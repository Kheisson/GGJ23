using System;
using Equipment;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OrderItem : MonoBehaviour
    {
        [SerializeField] private GameObject _completeEffect;
        private VeggySo _veggy;
        private Image _sprite;
        private bool _orderComplete;
        
        public VeggySo Veggy => _veggy;
        public bool OrderComplete => _orderComplete;

        private void Awake()
        {
            _sprite = GetComponent<Image>();
        }

        public void SetVeggy(VeggySo veggy)
        {
            _veggy = veggy;
            _sprite.sprite = veggy.veggeySprite;
        }
        
        public void Complete()
        {
            _completeEffect.SetActive(true); 
            _orderComplete = true; 
            _veggy = null;
        }
    }
}
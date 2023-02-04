using Equipment;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OrderItem : MonoBehaviour
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private VeggySo _veggy;
        [SerializeField] private GameObject _completeEffect;
        
        public VeggySo Veggy => _veggy;
        
        public void SetVeggy(VeggySo veggy)
        {
            _veggy = veggy;
            _sprite.sprite = veggy.veggeySprite;
            gameObject.SetActive(true);
        }
        
        public void Complete()
        {
            _completeEffect.SetActive(true);
            _veggy = null;
        }
    }
}
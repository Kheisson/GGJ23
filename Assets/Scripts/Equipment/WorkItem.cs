using System.Collections;
using UnityEngine;

namespace Equipment
{
    public class WorkItem : MonoBehaviour
    {
        public enum ItemType { SHOVEL, WATERCAN, HANDS }
        [SerializeField] private ItemType type;
        [SerializeField] private ParticleSystem effect;
        private readonly WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);
        public ItemType Type => type;
        
        public void EnableEffect()
        {
            StartCoroutine(EjectParticle());
        }
        
        private IEnumerator EjectParticle()
        {
            effect.gameObject.SetActive(true);
            yield return _waitForSeconds;
            effect.gameObject.SetActive(false);
        }
    }
}
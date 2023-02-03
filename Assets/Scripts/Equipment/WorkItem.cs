using System.Collections;
using UnityEngine;

namespace Equipment
{
    public class WorkItem : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private ParticleSystem effect;
        private readonly WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);
        public int Id => id;
        
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
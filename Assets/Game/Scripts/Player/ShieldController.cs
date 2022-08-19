using UnityEngine;

namespace Player
{
    public class ShieldController : MonoBehaviour
    {
        public void Despawn()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
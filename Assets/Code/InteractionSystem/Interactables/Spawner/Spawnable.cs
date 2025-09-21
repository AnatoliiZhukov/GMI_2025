using System;
using UnityEngine;

namespace InteractionSystem.Interactables.Spawner
{
    /*
     * Can be spawned by a Spawner.
     */
    [RequireComponent(typeof(Rigidbody))]
    public class Spawnable : CommonInteractable
    {
        public event Action OnSpawnableDestroyed;
        
        private void OnDisable()
        {
            // Invokes the Action, which notifies all subscribers
            OnSpawnableDestroyed?.Invoke();
        }
        
        public void DestroySpawnable()
        {
            Destroy(gameObject);
        }
    }
}

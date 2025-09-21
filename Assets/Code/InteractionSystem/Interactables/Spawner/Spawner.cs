using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InteractionSystem.Interactables.Spawner
{
    /*
     * Can be toggled by interacting with it.
     * Instantiates a new Spawnable upon being enabled.
     * When the Spawnable is destroyed, instantiates a new one.
     */
    public class Spawner : CommonInteractable
    {
        [SerializeField] private bool bEnabledOnStart = false;
        [SerializeField] private Spawnable spawnablePrefab;
        [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 1f, 0f);
    
        private bool _bEnabled = false;
        private bool _bCanSpawn = true;

        private void Start()
        {
            if (bEnabledOnStart && !_bEnabled) ToggleSpawner(); 
        }

        private void OnSpawnerEnabled()
        {
            if (!_bCanSpawn) return;
            
            StartCoroutine(SpawnAfterDelay());
        }

        private void OnSpawnableDestroyed()
        {
            _bCanSpawn = true;
            if (_bEnabled) StartCoroutine(SpawnAfterDelay());
        }
        
        private IEnumerator SpawnAfterDelay(float delay = 1f)
        {
            if (delay <= 0.01f) yield break;
            
            yield return new WaitForSeconds(delay);
            
            if (spawnablePrefab == null)
            {
                Debug.LogError("A Spawner is trying to spawn a null prefab.");
                yield break;
            }
        
            var newSpawnable = Instantiate(spawnablePrefab, transform.position + spawnOffset, transform.rotation);
            _bCanSpawn = false;
            
            // Subscribe to the OnDestroyed action
            newSpawnable.OnSpawnableDestroyed += OnSpawnableDestroyed;
            
            // Apply force to the new Spawnable
            var force = Vector3.zero;
            force.x = Random.Range(-10f, 10f);
            force.y = 100f;
            force.z = Random.Range(-10f, 10f);
            newSpawnable.GetComponent<Rigidbody>().AddForce(force);
        }
    
        public void ToggleSpawner()
        {
            _bEnabled = !_bEnabled;
            if (_bEnabled)
            {
                OnSpawnerEnabled();
            }
        }
    }
}

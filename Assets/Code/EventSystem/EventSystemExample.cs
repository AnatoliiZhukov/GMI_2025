using UnityEngine;

namespace EventSystem
{
    /*
     * Triggers a UnityEvent on key press. Useful for testing.
     * Allows to customize the key and behaviour for each instance.
     */
    public class EventSystemExample : MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode = KeyCode.Space;
        
        // The game event(s) this script can trigger
        // Any other GameObject can subscribe to these, no references required
        [Header("GameEvents")]
        public GameEvent onExampleEventTriggered;
        
        private void Update()
        {
            if (!Input.GetKeyDown(keyCode)) return;
        
            // Raise the event, pass in the sender (this) & some data
            // In this example, every event listener will have access to this script + the key used to call the event
            onExampleEventTriggered.Raise(this, keyCode);
        }
    }
}

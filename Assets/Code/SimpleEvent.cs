using EventSystem;
using UnityEngine;
using UnityEngine.Events;

/*
 * Triggers a UnityEvent on key press. Useful for testing.
 * Allows to customize the key and behaviour for each instance.
 */
public class SimpleEvent : MonoBehaviour
{
    // This is only to customize what happens when the key is pressed per instance
    // Not a part of the event system
    [SerializeField] private UnityEvent onKeyPressed;
    
    // The game events this script can trigger
    // Any other GameObject can subscribe to these, no references required
    [Header("GameEvents")]
    public GameEvent onSimpleEventTriggered;
    
    [SerializeField] private KeyCode keyCode = KeyCode.Space;

    private void Update()
    {
        if (!Input.GetKeyDown(keyCode)) return;
        
        // First activate the Unity event (make this script do something)
        onKeyPressed?.Invoke();
        // Then notify all listeners of the GameEvent
        onSimpleEventTriggered.Raise(this, keyCode);
    }
}

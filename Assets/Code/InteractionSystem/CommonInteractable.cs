using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem
{
    /*
     * Invokes a UnityEvent on Interact(), allowing to customize the response in the editor.
     */
    public class CommonInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent onInteractTriggered;
        
        public void Interact()
        {
            onInteractTriggered?.Invoke();
        }
    }
}

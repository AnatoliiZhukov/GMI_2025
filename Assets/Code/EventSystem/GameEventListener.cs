using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    /*
     * Extended UnityEvent class that allows to send data.
     */
    [System.Serializable]
    public class CustomUnityEvent : UnityEvent<Component, object> { }
    
    /*
     * Attach this script to an object that you want to register as a listener to a global GameEvent.
     */
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private CustomUnityEvent response;
    
        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }
        
        public void OnEventRaised(Component sender, params object[] data)
        {
            response.Invoke(sender, data);
        }
    }
}

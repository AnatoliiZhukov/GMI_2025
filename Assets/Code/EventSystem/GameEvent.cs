using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    /*
     * Add a variable of this type to any script if you want it to broadcast a global event.
     * Call "Raise" on it when you want to notify all listeners.
     */
    [CreateAssetMenu(menuName = "GameEvent")]
    public class GameEvent : ScriptableObject
    {
        // List of GameEventListeners that will be notified about the event when it's raised
        private readonly List<GameEventListener> _listeners = new List<GameEventListener>();
        
        public void Raise(Component sender, params object[] data)
        {
            foreach (var t in _listeners)
            {
                t.OnEventRaised(sender, data);
            }
        }
        
        public void RegisterListener(GameEventListener listener)
        {
            if(!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (_listeners.Contains(listener)) _listeners.Remove(listener);
        }
    }
}

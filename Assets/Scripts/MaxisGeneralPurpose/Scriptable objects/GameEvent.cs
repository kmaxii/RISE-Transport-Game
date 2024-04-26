using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace MaxisGeneralPurpose.Scriptable_objects
{
    
    public delegate void SimpleEventHandler();
    
    
    
    [CreateAssetMenu(menuName = "Custom/Event/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField] private bool debug;
        private readonly List<SimpleEventHandler> _listeners = new();

        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i]();
                if (debug)
                {
                    Debug.Log($"Event raised: {_listeners[i].Method.Name} from object {_listeners[i].Target.GetType().Name}");
                }
            }
        }

        public void RegisterListener(SimpleEventHandler listener)
        {
            _listeners.Add(listener);
            if (debug)
            {
                //Print out the listeners name
                Debug.Log($"Listener added: {listener.Method.Name} from object {listener.Target.GetType().Name}");
            }
        }

        public void UnregisterListener(SimpleEventHandler listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }
    }
}

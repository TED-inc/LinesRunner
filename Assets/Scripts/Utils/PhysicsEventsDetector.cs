using System;
using UnityEngine;
using UnityEngine.Events;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// calls events on collision of gameobject for another classes
    /// </summary>
    public class PhysicsEventsDetector : MonoBehaviour
    {
        public ColliderEvent onTriggerEnter;
        public ColliderEvent onTriggerExit;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.Equals(gameObject))
                return;

            onTriggerEnter.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.Equals(gameObject))
                return;

            onTriggerExit.Invoke(other);
        }
    }


    [Serializable]
    public class ColliderEvent : UnityEvent<Collider> { }
}
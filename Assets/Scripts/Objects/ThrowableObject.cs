using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class ThrowableObject : MonoBehaviour
    {
        public UnityEvent OnMiss;

        private Collider _collider;
        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnCollisionEnter(Collision other)
        {
            var tag = other.gameObject.tag;
            if (tag.Equals("Table"))
            {
                var table = other.gameObject.GetComponent<TableController>();
                if (table && table.IsOrdering)
                {
                    _rigidbody.isKinematic = true;
                    _collider.enabled = false;
                }
                else
                {
                    OnMiss.Invoke();
                }

            }
            else
            {
                OnMiss.Invoke();
            }

        }
    }
}
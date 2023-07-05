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
        public UnityEvent OnHit;

        private Collider _collider;
        private Rigidbody _rigidbody;
        private bool hitted = false;

        private void OnEnable()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
            hitted = false;
        }

        public void OnCollisionEnter(Collision other)
        {
            if (hitted)
            {
                return;
            }
            
            
            var tag = other.gameObject.tag;
            if (tag.Equals("Table"))
            {
                var table = other.gameObject.GetComponent<TableController>();
                if (table && table.IsOrdering)
                {
                    _rigidbody.isKinematic = true;
                    _collider.enabled = false;
                    OnHit.Invoke();
                }
                else
                {
                    OnMiss.Invoke();
                }
                hitted = true;
                StartCoroutine(DestroyAfter(1f));
            }
            else if(tag != "SelectingFood")
            {
                OnMiss.Invoke();
                hitted = true;
                StartCoroutine(DestroyAfter(1f));
            }
            
        }
        
        IEnumerator DestroyAfter(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
            
    }
}
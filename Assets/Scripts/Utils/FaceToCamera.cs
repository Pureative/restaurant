using System;
using UnityEngine;

namespace Utils
{
    public class FaceToCamera : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        private void OnEnable()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }
        }
        
        void Update()
        {
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward,
                _camera.transform.rotation * Vector3.up);
        }
    }
}
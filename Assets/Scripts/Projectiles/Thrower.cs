using System;
using Objects;
using UnityEngine;
using UnityEngine.Events;

namespace Projectiles
{
    [RequireComponent(typeof(TrajectoryPredictor))]
    public class Thrower : MonoBehaviour
    {
        public UnityEvent OnMiss;

        [Range(0.0f, 100.0f)] public float maxForce;


        [SerializeField] Transform startDirection;

        [SerializeField] Transform endDirection;
        [SerializeField] Rigidbody objectToThrow;


        [NonSerialized] public float force;
        [NonSerialized] public string currentFoodName;

        private TrajectoryPredictor _trajectoryPredictor;
        private Vector3 _direction;

        private void OnEnable()
        {
            _trajectoryPredictor = GetComponent<TrajectoryPredictor>();
            objectToThrow = Instantiate(objectToThrow, endDirection.position, Quaternion.identity);
            objectToThrow.gameObject.SetActive(false);
        }

        public void Predict()
        {
            CalcDirection();
            if (objectToThrow)
            {
                _trajectoryPredictor.PredictTrajectory(ProjectileData());
            }
        }
        
        public void SetThrowObject(GameObject obj)
        {
            foreach (Transform child in objectToThrow.transform)
            {
                Destroy(child.gameObject);
            }
            
            var instance = Instantiate(obj, objectToThrow.transform);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            currentFoodName = obj.name;
        }

        void CalcDirection()
        {
            Vector3 direction = endDirection.position - startDirection.position;
            _direction = direction.normalized;
        }

        ProjectileData ProjectileData()
        {
            ProjectileData projectileData = new ProjectileData();
            Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

            projectileData.direction = _direction;
            projectileData.initialPosition = endDirection.position;
            projectileData.initialSpeed = force;
            projectileData.mass = r.mass;
            projectileData.drag = r.drag;
            
            return projectileData;
        }

        public void ThrowObject()
        {
            Rigidbody thrownObject = Instantiate(objectToThrow, endDirection.position, Quaternion.identity);
            thrownObject.gameObject.SetActive(true);
            thrownObject.GetComponent<ThrowableObject>().OnMiss.AddListener(OnMiss.Invoke);
            thrownObject.AddForce(_direction * force, ForceMode.Impulse);
        }
    }
}
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


        [NonSerialized] public float force;
        [NonSerialized] public Rigidbody objectToThrow;

        private TrajectoryPredictor _trajectoryPredictor;
        private Vector3 _direction;

        private void OnEnable()
        {
            _trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        }

        public void Predict()
        {
            CalcDirection();
            if (objectToThrow)
            {
                _trajectoryPredictor.PredictTrajectory(ProjectileData());
            }
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
            thrownObject.GetComponent<ThrowableObject>().OnMiss.AddListener(OnMiss.Invoke);
            thrownObject.AddForce(_direction * force, ForceMode.Impulse);
        }
    }
}
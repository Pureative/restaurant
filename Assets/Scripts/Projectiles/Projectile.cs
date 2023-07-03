using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Projectiles
{
    [RequireComponent(typeof(TrajectoryPredictor))]
    public class Projectile : MonoBehaviour
    {

        [SerializeField]
        Rigidbody objectToThrow;

        [SerializeField, Range(0.0f, 100.0f)]
        float force;

        [SerializeField]
        Transform startDirection;

        [SerializeField]
        Transform endDirection;
        

        private TrajectoryPredictor _trajectoryPredictor;
        private PlayerControllActions _input;
        private Vector3 _direction;

        private void OnEnable()
        {
            _trajectoryPredictor = GetComponent<TrajectoryPredictor>();

            _input = new PlayerControllActions();
            _input.ActionMap.Fire.performed += ThrowObject;
            _input.Enable();
        }

        void Update()
        {
            CalcDirection();
            Predict();
        }

        void Predict()
        {
            _trajectoryPredictor.PredictTrajectory(ProjectileData());
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

        void ThrowObject(InputAction.CallbackContext ctx)
        {
            Rigidbody thrownObject = Instantiate(objectToThrow, endDirection.position, Quaternion.identity);
            thrownObject.AddForce(_direction * force, ForceMode.Impulse);
        }
    }
}
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryPredictor : MonoBehaviour
    {

        LineRenderer trajectoryLine;
        
        [SerializeField, Range(10, 100)]
        int maxPoints = 15;

        [SerializeField, Range(0.01f, 0.5f)]
        float increment = 0.025f;
        
        [SerializeField, Range(1.05f, 2f)]
        float rayOverlap = 1.1f;


        private void Start()
        {
            if (trajectoryLine == null)
                trajectoryLine = GetComponent<LineRenderer>();

            SetTrajectoryVisible(true);
        }

        public void PredictTrajectory(ProjectileData projectile)
        {
            Vector3 velocity = projectile.initialSpeed / projectile.mass * projectile.direction;
            Vector3 position = projectile.initialPosition;
            Vector3 nextPosition;
            float overlap;

            UpdateLineRender(maxPoints, (0, position));

            for (int i = 1; i < maxPoints; i++)
            {
                velocity = CalculateNewVelocity(velocity, projectile.drag, increment);
                nextPosition = position + velocity * increment;
                
                overlap = Vector3.Distance(position, nextPosition) * rayOverlap;
                
                if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap))
                {
                    UpdateLineRender(i, (i - 1, hit.point));
                    break;
                }
                
                position = nextPosition;
                UpdateLineRender(maxPoints, (i, position));
            }
        }

        private void UpdateLineRender(int count, (int point, Vector3 pos) pointPos)
        {
            trajectoryLine.positionCount = count;
            trajectoryLine.SetPosition(pointPos.point, pointPos.pos);
        }

        private Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
        {
            velocity += Physics.gravity * increment;
            velocity *= Mathf.Clamp01(1f - drag * increment);
            return velocity;
        }

        public void SetTrajectoryVisible(bool visible)
        {
            trajectoryLine.enabled = visible;
        }
        
        public void ResetTransform()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
using Projectiles;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Screen = UnityEngine.Device.Screen;

namespace Input
{
    [RequireComponent(typeof(TrajectoryPredictor), typeof(Thrower))]
    public class DragAndThrow : MonoBehaviour
    {
        public Vector2 clampInDegrees = new(360f, 180f);
        public Vector2 sensitivity = new(0.1f, 0.1f);
        public Vector2 smoothing = new(1f, 1f);
        public bool lockCursor;
        public GameObject characterBody;
        public bool lockX;
        public bool lockY;
        public float minForce = 15f;
        public UnityEvent OnThrow;


        private Thrower _thrower;
        private TrajectoryPredictor _trajectoryPredictor;
        private Vector2 _mouseFinal;
        private Vector2 _smoothMouse;
        private Vector2 _targetDirection;
        private Vector2 _targetCharacterDirection;
        private PlayerControllActions _input;
        private Vector2 _dragStart;
        private Vector2 _currentDrag;
        private bool _isDragging;

        private void OnEnable()
        {
            _input = new PlayerControllActions();
            _input.ActionMap.ClickAction.started += StartDrag;
            _input.ActionMap.ClickAction.canceled += EndDrag;
            _input.Enable();
        }

        private void StartDrag(InputAction.CallbackContext obj)
        {
            _isDragging = true;
            _trajectoryPredictor.SetTrajectoryVisible(true);
            _dragStart = _input.ActionMap.DragAction.ReadValue<Vector2>();
            _currentDrag = _dragStart;
        }

        private void EndDrag(InputAction.CallbackContext obj)
        {
            _isDragging = false;
            _trajectoryPredictor.SetTrajectoryVisible(false);
            if (_thrower.force > 0.8f * minForce && _thrower.objectToThrow)
            {
                OnThrow.Invoke();
            }
            _trajectoryPredictor.ResetTransform();
        }

        void Start()
        {
            // Set target direction to the camera's initial orientation.
            _targetDirection = transform.localRotation.eulerAngles;
            _thrower = GetComponent<Thrower>();
            _trajectoryPredictor = GetComponent<TrajectoryPredictor>();

            // Set target direction for the character body to its inital state.
            if (characterBody)
                _targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;
        }

        Vector2 ScaleAndSmooth(Vector2 delta)
        {
            delta = Vector2.Scale(delta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

            if (!lockX)
                _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, delta.x, 1f / smoothing.x);
            if (!lockY)
                _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, delta.y, 1f / smoothing.y);

            return _smoothMouse;
        }

        void LateUpdate()
        {
            if (lockCursor)
                Cursor.lockState = CursorLockMode.Locked;
            if (_isDragging)
            {
                Vector2 mouseDelta = _input.ActionMap.DragAction.ReadValue<Vector2>();
                _mouseFinal += ScaleAndSmooth(mouseDelta);
                _currentDrag += mouseDelta;
                CalculateTrajectory();

                ClampValues();
                ApplyToTransform();
            }
        }

        private void CalculateTrajectory()
        {
            float forcePercentage = -_currentDrag.y / Screen.currentResolution.height * 2;
            _thrower.force = forcePercentage * _thrower.maxForce + minForce;
            _thrower.Predict();

        }

        void ClampValues()
        {
            if (clampInDegrees.x < 360)
                _mouseFinal.x = Mathf.Clamp(_mouseFinal.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

            if (clampInDegrees.y < 360)
                _mouseFinal.y = Mathf.Clamp(_mouseFinal.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

            var targetOrientation = Quaternion.Euler(_targetDirection);
            transform.localRotation = Quaternion.AngleAxis(-_mouseFinal.y, targetOrientation * Vector3.right) *
                                      targetOrientation;
        }

        void ApplyToTransform()
        {
            var targetCharacterOrientation = Quaternion.Euler(_targetCharacterDirection);
            Quaternion yRotation;

            if (characterBody)
            {
                yRotation = Quaternion.AngleAxis(_mouseFinal.x, Vector3.up);
                characterBody.transform.localRotation = yRotation * targetCharacterOrientation;
            }
            else
            {
                yRotation = Quaternion.AngleAxis(_mouseFinal.x, transform.InverseTransformDirection(Vector3.up));
                transform.localRotation *= yRotation;
            }
        }
    }
}
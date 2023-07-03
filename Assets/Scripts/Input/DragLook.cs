using UnityEngine;

namespace Input
{
    public class DragLook : MonoBehaviour
    {
        public Vector2 clampInDegrees = new(360f, 180f);
        public Vector2 sensitivity = new(0.1f, 0.1f);
        public Vector2 smoothing = new(1f, 1f);
        public bool lockCursor;
        public GameObject characterBody;
        public bool lockX;
        public bool lockY;


        private Vector2 _mouseFinal;
        private Vector2 _smoothMouse;
        private Vector2 _targetDirection;
        private Vector2 _targetCharacterDirection;
        private PlayerControllActions _input;

        private void OnEnable()
        {
            _input = new PlayerControllActions();
            _input.Enable();
        }

        void Start()
        {
            // Set target direction to the camera's initial orientation.
            _targetDirection = transform.localRotation.eulerAngles;

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
            var clickValue = _input.ActionMap.ClickAction.ReadValue<float>();
            if (clickValue > 0)
            {
                Vector2 mouseDelta = _input.ActionMap.DragAction.ReadValue<Vector2>();
                _mouseFinal += ScaleAndSmooth(mouseDelta);

                ClampValues();
                ApplyToTransform();
            }
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
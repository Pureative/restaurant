using Projectiles;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Input
{
    public class SelectFood : MonoBehaviour
    {
        public float selectedFoodYOffset = 1;
        public float selectedFoodScale = 1.5f;
        public Camera mainCamera;
        public Thrower thrower;
        
        public bool IsSelectingFood { get; set; }
        public GameObject CurrentSelectedFood { get; set; }
        
        private Vector3 _initialPosition;
        private PlayerControllActions _input;
        
        private void Awake()
        {
            _input = new PlayerControllActions();
            _input.ActionMap.ClickAction.performed += OnPointerDown;
            _input.ActionMap.ClickAction.canceled += ctx => IsSelectingFood = false;
            _input.Enable();
            
            if(!mainCamera)
                mainCamera = Camera.main;
            
        }
        
        void OnPointerDown(InputAction.CallbackContext context)
        {
            IsSelectingFood = true;
            var screenPos = Pointer.current.position.ReadValue();
            var ray = mainCamera.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject.CompareTag("SelectingFood"))
                {
                    if (CurrentSelectedFood)
                    {
                        ResetSelectedFood();
                    }

                    SelectFoodObject(hit.collider.gameObject);
                }
            }
        }
        
        private void SelectFoodObject(GameObject food)
        {
            CurrentSelectedFood = food;
            _initialPosition = food.transform.position;
            food.transform.position = new Vector3(_initialPosition.x, _initialPosition.y + selectedFoodYOffset, _initialPosition.z);
            food.transform.localScale = new Vector3(selectedFoodScale, selectedFoodScale, selectedFoodScale);
            var throwableObject = GameConfig.Instance.GetFoodConfig(food.name);
            var throwableRigidbody = throwableObject.prefab.GetComponent<Rigidbody>();
            thrower.objectToThrow = throwableRigidbody;
        }
        
        private void ResetSelectedFood()
        {
            CurrentSelectedFood.transform.position = _initialPosition;
            CurrentSelectedFood.transform.localScale = Vector3.one;
            CurrentSelectedFood = null;
            thrower.objectToThrow = null;
        }
    }
}
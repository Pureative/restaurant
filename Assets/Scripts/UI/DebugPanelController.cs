using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class DebugPanelController : MonoBehaviour
    {
        public AudioListener AudioListener;
        public Camera MainCamera;
        public SliderValueController CameraPositionValueX;
        public SliderValueController CameraPositionValueY;
        public SliderValueController CameraPositionValueZ;
        public SliderValueController CameraRotationValueX;
        public SliderValueController CameraRotationValueY;
        public SliderValueController CameraRotationValueZ;
        public SliderValueController GameplayDurationValue;

        private void Start()
        {
            var audioEnableValue = PlayerPrefs.GetInt("audioEnableValue", 1);
            AudioListener.enabled = audioEnableValue == 1;

            // camera position
            var cameraPosition = MainCamera.transform.position;
            var cameraPositionX = PlayerPrefs.GetFloat("cameraPositionX", cameraPosition.x);
            var cameraPositionY = PlayerPrefs.GetFloat("cameraPositionY", cameraPosition.y);
            var cameraPositionZ = PlayerPrefs.GetFloat("cameraPositionZ", cameraPosition.z);
            cameraPosition.x = cameraPositionX;
            cameraPosition.y = cameraPositionY;
            cameraPosition.z = cameraPositionZ;
            MainCamera.transform.position = cameraPosition;
            CameraPositionValueX.SetValue(cameraPositionX);
            CameraPositionValueY.SetValue(cameraPositionY);
            CameraPositionValueZ.SetValue(cameraPositionZ);
            CameraPositionValueX.ValueChanged.AddListener(OnCameraPositionXSliderChanged);
            CameraPositionValueY.ValueChanged.AddListener(OnCameraPositionYSliderChanged);
            CameraPositionValueZ.ValueChanged.AddListener(OnCameraPositionZSliderChanged);
            
            // camera rotation
            var cameraRotation = MainCamera.transform.rotation.eulerAngles;
            var cameraRotationX = PlayerPrefs.GetFloat("cameraRotationX", cameraRotation.x);
            var cameraRotationY = PlayerPrefs.GetFloat("cameraRotationY", cameraRotation.y);
            var cameraRotationZ = PlayerPrefs.GetFloat("cameraRotationZ", cameraRotation.z);
            cameraRotation.x = cameraRotationX;
            cameraRotation.y = cameraRotationY;
            cameraRotation.z = cameraRotationZ;
            MainCamera.transform.rotation = Quaternion.Euler(cameraRotation);
            CameraRotationValueX.SetValue(cameraRotationX);
            CameraRotationValueY.SetValue(cameraRotationY);
            CameraRotationValueZ.SetValue(cameraRotationZ);
            CameraRotationValueX.ValueChanged.AddListener(OnCameraRotationXSliderChanged);
            CameraRotationValueY.ValueChanged.AddListener(OnCameraRotationYSliderChanged);
            CameraRotationValueZ.ValueChanged.AddListener(OnCameraRotationZSliderChanged);
            
            // gameplay
            var gameplayDuration = PlayerPrefs.GetFloat("gameplayDuration", GameManager.Instance.LevelDuration);
            GameManager.Instance.LevelDuration = gameplayDuration;
            GameplayDurationValue.SetValue(gameplayDuration);
            GameplayDurationValue.ValueChanged.AddListener(OnGameplayDurationSliderChanged);
            
        }

        public void TurnOnAudio()
        {
            AudioListener.enabled = true;
            PlayerPrefs.SetInt("audioEnableValue", 1);
        }
        
        public void TurnOffAudio()
        {
            AudioListener.enabled = false;
            PlayerPrefs.SetInt("audioEnableValue", 0);
        }

        public void OnCameraPositionXSliderChanged(float value)
        {
            var position = MainCamera.transform.position;
            position.x = value;
            MainCamera.transform.position = position;
            PlayerPrefs.SetFloat("cameraPositionX", value);
        }
        
        public void OnCameraPositionYSliderChanged(float value)
        {
            var position = MainCamera.transform.position;
            position.y = value;
            MainCamera.transform.position = position;
            PlayerPrefs.SetFloat("cameraPositionY", value);
        }
        
        public void OnCameraPositionZSliderChanged(float value)
        {
            var position = MainCamera.transform.position;
            position.z = value;
            MainCamera.transform.position = position;
            PlayerPrefs.SetFloat("cameraPositionZ", value);
        }
        
        public void OnCameraRotationXSliderChanged(float value)
        {
            var rotation = MainCamera.transform.rotation.eulerAngles;
            rotation.x = value;
            MainCamera.transform.rotation = Quaternion.Euler(rotation);
            PlayerPrefs.SetFloat("cameraRotationX", value);
        }
        
        public void OnCameraRotationYSliderChanged(float value)
        {
            var rotation = MainCamera.transform.rotation.eulerAngles;
            rotation.y = value;
            MainCamera.transform.rotation = Quaternion.Euler(rotation);
            PlayerPrefs.SetFloat("cameraRotationY", value);
        }
        
        public void OnCameraRotationZSliderChanged(float value)
        {
            var rotation = MainCamera.transform.rotation.eulerAngles;
            rotation.z = value;
            MainCamera.transform.rotation = Quaternion.Euler(rotation);
            PlayerPrefs.SetFloat("cameraRotationZ", value);
        }
        
        public void OnGameplayDurationSliderChanged(float value)
        {
            GameManager.Instance.LevelDuration = value;
            PlayerPrefs.SetFloat("gameplayDuration", value);
        }
    }
}
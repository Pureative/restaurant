using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SliderValueController : MonoBehaviour
    {
        public float Range = 1f;
        public bool NegativeRange = true;
        public TextMeshProUGUI ValueTMP;
        public Slider Slider;

        public UnityEvent<float> ValueChanged;
        
        private void Awake()
        {
            Slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float sliderValue)
        {
            var value = 0f;
            if (NegativeRange)
            {
                value = -Range + sliderValue * Range * 2;
            }
            else
            {
                value = sliderValue * Range;
            }
            
            ValueTMP.text = value.ToString("F2");
            ValueChanged.Invoke(value);
        }

        public void SetValue(float value)
        {
            var sliderValue = 0f;
            if (NegativeRange)
            {
                sliderValue = (value + Range) / (Range * 2);
            }
            else
            {
                sliderValue = value / Range;
            }
            
            Slider.value = sliderValue;
            ValueTMP.text = value.ToString("F2");
        }
    }
}
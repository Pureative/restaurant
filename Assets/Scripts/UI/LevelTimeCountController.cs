using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LevelTimeCountController : MonoBehaviour
    {
        private TextMeshProUGUI _tmp;
        
        private void Awake()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            SetTime(GameManager.Instance.LevelDuration - GameManager.Instance.TimeCount);
        }

        public void SetTime(float time)
        {
            SetTime((int) time);
        }
        
        public void SetTime(int time)
        {
            _tmp.text = time.ToString();
        }
    }
}
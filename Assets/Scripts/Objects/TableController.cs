using RadialFills;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class TableController : MonoBehaviour
    {
        public SpriteRenderer orderSprite;
        public RadialSpriteFill progressFill;
        public Animator customerAnimator;
        public UnityEvent TimeUp;
        public UnityEvent<HitType> OnHit;

        [Range(0, 1)] public float excellentTimeInPercent = 0.4f;
        [Range(0, 1)] public float perfectTimeInPercent = 0.4f;
        
        public string CurrentOrderName { get; private set; }

        private float _orderTime;
        private float _excellentTime;
        private float _perfectTime;
        private float _currentTime;
        private int _makeOrderAnimationHash = Animator.StringToHash("MakeOrder");
        private int _idleAnimationHash = Animator.StringToHash("Idle");

        public bool IsOrdering { get; set; }

        private void OnEnable()
        {
            _orderTime = GameConfig.Instance.orderTime;
            _excellentTime = _orderTime * excellentTimeInPercent;
            _perfectTime = _orderTime * perfectTimeInPercent;
        }

        public void MakeOrder()
        {
            IsOrdering = true;
            var foodConfig = RandomFood();
            _currentTime = _orderTime;
            progressFill.fillAmount = 1.0f;
            progressFill.gameObject.SetActive(true);
            orderSprite.sprite = foodConfig.sprite;
            customerAnimator.ResetTrigger(_idleAnimationHash);
            customerAnimator.SetTrigger(_makeOrderAnimationHash);
            CurrentOrderName = foodConfig.name;
        }
        
        public void CancelOrder()
        {
            IsOrdering = false;
            progressFill.gameObject.SetActive(false);
            customerAnimator.ResetTrigger(_makeOrderAnimationHash);
            customerAnimator.SetTrigger(_idleAnimationHash);
        }

        private void Update()
        {
            if (IsOrdering)
            {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0)
                {
                    TimeUp.Invoke();
                    IsOrdering = false;
                    progressFill.gameObject.SetActive(false);
                }

                progressFill.fillAmount = _currentTime / _orderTime;
            }
        }

        private FoodConfig RandomFood()
        {
            int index = Random.Range(0, GameConfig.Instance.foodConfigs.Count);
            return GameConfig.Instance.foodConfigs[index];
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Food"))
            {
                HitType hitType;
                if(_currentTime >= _excellentTime)
                    hitType = HitType.Excellent;
                else if (_currentTime >= _perfectTime)
                    hitType = HitType.Perfect;
                else
                    hitType = HitType.Great;
                
                OnHit.Invoke(hitType);
            }
        }
    }
}
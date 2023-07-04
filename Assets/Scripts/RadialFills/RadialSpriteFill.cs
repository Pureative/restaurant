using UnityEngine;

namespace RadialFills
{
    [ExecuteAlways]
    public class RadialSpriteFill : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Material radialFillMaterial;
        
        [Range(0, 1)]
        public float fillAmount = 1.0f;
        
        [SerializeField]
        private Color fullColor = Color.green;
        
        [SerializeField]
        private Color emptyColor = Color.red;
        
        private float _arc = 0.0f;
        private Material _material;
        
        private void OnEnable()
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            _material = new Material(radialFillMaterial);
            spriteRenderer.sharedMaterial = _material;
        }
        
        private void Update()
        {
            _arc = 360.0f * (1.0f -fillAmount);
            var color = Color.Lerp(emptyColor, fullColor, fillAmount);
            _material.SetColor("_Color", color);
            _material.SetFloat("_Arc2", _arc);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace BeeCocket.EasyParallax
{
    public class ParallaxLayer : MonoBehaviour
    {
        public float speedFactor;
        private RectTransform _rectTransform;
        private Vector2 _defaultPosition;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _defaultPosition = _rectTransform.anchoredPosition;
        }

        private void OnEnable()
        {
            ParallaxController.Instance.OnGyroscopeShift -= ShiftLayer;
            ParallaxController.Instance.OnGyroscopeShift += ShiftLayer;
        }

        private void OnDisable()
        {
            ParallaxController.Instance.OnGyroscopeShift -= ShiftLayer;
        }

        public void ShiftLayer(Vector2 shift)
        {
            _rectTransform.anchoredPosition = _defaultPosition;
            _rectTransform.anchoredPosition += shift * speedFactor;
        }
    }
}
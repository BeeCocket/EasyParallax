using UnityEngine;

namespace WorkerHoneybee.EasyParallax
{
    public class ParallaxLayer : MonoBehaviour
    {
        [field: SerializeField]
        private float SpeedFactor;

        private RectTransform RectTransformComponent { get; set; }
        private Vector2 DefaultPosition { get; set; }

        protected virtual void Awake()
        {
            RectTransformComponent = GetComponent<RectTransform>();
            DefaultPosition = RectTransformComponent.anchoredPosition;
        }

        protected virtual void Start()
        {
            ParallaxController.Instance.OnGyroShift -= ShiftLayer;
            ParallaxController.Instance.OnGyroShift += ShiftLayer;
        }

        protected virtual void OnDestroy()
        {
            ParallaxController.Instance.OnGyroShift -= ShiftLayer;
        }

        public void ShiftLayer(Vector2 shift)
        {
            RectTransformComponent.anchoredPosition = DefaultPosition + (shift * SpeedFactor);
        }
    }
}
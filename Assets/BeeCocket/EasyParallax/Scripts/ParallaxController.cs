using System;
using UnityEngine;

namespace BeeCocket.EasyParallax
{
    public class ParallaxController : MonoBehaviour
    {
        public float maxShift;
        public GameObject popup;

        private Gyroscope _gyroscope;
        private bool _isGyroscopeEnabled;
        private Vector2 _gyroscopeShift = Vector2.zero;

        public Action<Vector2> OnGyroscopeShift = delegate { };

        public static ParallaxController Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _isGyroscopeEnabled = EnableGyro();
        }

        private bool EnableGyro()
        {
            if (SystemInfo.supportsGyroscope)
            {
                _gyroscope = Input.gyro;
                _gyroscope.enabled = true;
                return true;
            }
            else
            {
                if (popup != null)
                    popup.SetActive(true);
            }
            return false;
        }

        void Update()
        {
            if (_isGyroscopeEnabled)
            {
                _gyroscopeShift.x -= _gyroscope.rotationRateUnbiased.y;
                _gyroscopeShift.y += _gyroscope.rotationRateUnbiased.x;

                _gyroscopeShift.x = _gyroscopeShift.x > maxShift ? maxShift : _gyroscopeShift.x;
                _gyroscopeShift.y = _gyroscopeShift.y > maxShift ? maxShift : _gyroscopeShift.y;

                _gyroscopeShift.x = _gyroscopeShift.x < -maxShift ? -maxShift : _gyroscopeShift.x;
                _gyroscopeShift.y = _gyroscopeShift.y < -maxShift ? -maxShift : _gyroscopeShift.y;

                OnGyroscopeShift(_gyroscopeShift);
            }
        }
    }
}
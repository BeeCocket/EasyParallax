using System;
using UnityEngine;

namespace WorkerHoneybee.EasyParallax
{
    public class ParallaxController : MonoBehaviour
    {
        public event Action<Vector2> OnGyroShift;
        public static ParallaxController Instance;


        [Range(-1.0f, 1.0f)]
        [field: SerializeField]
        private float XShift;

        [Range(-1.0f, 1.0f)]
        [field: SerializeField]
        private float YShift;

        [field: SerializeField]
        private float MaxShift;

        private Gyroscope Gyroscope { get; set; }
        private bool IsGyroEnabled { get; set; }

        private Vector2 GyroShiftCache;

        protected virtual void Awake()
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

        protected virtual void OnEnable()
        {
            OnGyroShift = delegate { };
            GyroShiftCache = Vector2.zero;
#if UNITY_EDITOR
            IsGyroEnabled = false;
#else
            IsGyroEnabled = EnableGyro();
#endif
        }

        private bool EnableGyro()
        {
            if (SystemInfo.supportsGyroscope == true)
            {
                Gyroscope = Input.gyro;
                Gyroscope.enabled = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        protected virtual void Update()
        {
#if UNITY_EDITOR
            OnGyroShift(new Vector2(XShift * MaxShift, YShift * MaxShift));
#else
            if (IsGyroEnabled)
            {
                GyroShiftCache.x -= Gyroscope.rotationRateUnbiased.y;
                GyroShiftCache.y += Gyroscope.rotationRateUnbiased.x;

                GyroShiftCache.x = GyroShiftCache.x > MaxShift ? MaxShift : GyroShiftCache.x;
                GyroShiftCache.y = GyroShiftCache.y > MaxShift ? MaxShift : GyroShiftCache.y;

                GyroShiftCache.x = GyroShiftCache.x < -MaxShift ? -MaxShift : GyroShiftCache.x;
                GyroShiftCache.y = GyroShiftCache.y < -MaxShift ? -MaxShift : GyroShiftCache.y;

                OnGyroShift(GyroShiftCache);
            }
#endif
        }
    }
}
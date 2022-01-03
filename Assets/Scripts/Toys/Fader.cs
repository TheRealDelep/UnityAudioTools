namespace Delep.Audio.Toys
{
    using UnityEngine;

    public class Fader
    {
        private float from;
        private float to;
        private SlopeType slopeType;
        private float slopeValue;

        public float From
        {
            get => from;
            set
            {
                from = value;
                SetCurveHandlePosition();
            }
        }

        public float To
        {
            get => to;
            set
            {
                to = value;
                SetCurveHandlePosition();
            }
        }

        public SlopeType SlopeType
        {
            get => slopeType;
            set
            {
                slopeType = value;
                SetCurveHandlePosition();
            }
        }

        public float SlopeValue
        {
            get => slopeValue;
            set
            {
#if UNITY_EDITOR
                if (value < 0 || value > 1)
                {
                    Debug.LogWarning($"SlopeValue should be a value between 0 and 1. Current value: {value}");
                }
#endif
                slopeValue = Mathf.Clamp(value, 0, 1);
                SetCurveHandlePosition();
            }
        }

        public float Duration { get; set; }

        private float CurveHandlePosition { get; set; }

        public Fader(float from, float to, float duration)
            : this(from, to, duration, SlopeType.Linear, 0)
        {
        }

        public Fader(float from, float to, float duration, SlopeType slopeType, float slopeValue)
        {
            this.from = from;
            this.to = to;
            this.slopeType = slopeType;
            this.slopeValue = slopeValue;

            Duration = duration;
        }

        /// <summary>
        /// Returns the interpolated value.
        /// </summary>
        /// <param name="time">
        /// The current time of the fade effect. Must be a value between 0 and the duration of the fade.
        /// </param>
        /// <returns></returns>
        public float GetValue(float time)
        {
#if UNITY_EDITOR
            if (time < 0 || time > Duration)
            {
                Debug.LogWarning($"Time should be a value between 0 and Length({Duration}). Current value: {time}");
            }
#endif
            time = Mathf.Clamp(time, 0, Duration) / Duration;

            if (SlopeType == SlopeType.Linear || SlopeValue == 0)
            {
                return Mathf.Lerp(From, To, time);
            }

            var p0 = Lerp(From, CurveHandlePosition, time);
            var p1 = Lerp(CurveHandlePosition, To, time);
            return Lerp(p0, p1, time);
        }

        private void SetCurveHandlePosition()
        {
            var length = To - From;

            CurveHandlePosition = SlopeType switch
            {
                SlopeType.Linear => From,
                SlopeType.Concave => From + (SlopeValue * length),
                SlopeType.Convex => To - (SlopeValue * length),
                _ => From
            };
        }

        // Avoids Getting Nan if a and b are equals
        private float Lerp(float a, float b, float t)
            => a == b ? a : Mathf.Lerp(a, b, t);
    }
}
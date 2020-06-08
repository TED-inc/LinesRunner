using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class TurnPlatform : PlatformBase
    {
        public override float offsetRotation { get { return _rotation; } protected set { _rotation = value; } }
        public override Vector3 offset => CalcuateOffset(offsetRotation, length);


        [SerializeField, Range(-360f, 360f)]
        private float _rotation = 45f;


        protected virtual Vector3 CalcuateOffset(float rotation, float distance)
        {
            float angleRad = Mathf.PI * rotation / 180f;
            float radius = distance / angleRad;

            if (float.IsInfinity(radius) || float.IsNaN(radius))
                return distance * Vector3.right;

            return Vector3.back * radius + Quaternion.Euler(0f, rotation, 0f) * Vector3.forward * radius;
        }

        public override void ElevateLines(float localDistance, out Vector3 localLeftLine, out Vector3 localRightLine)
        {
            float localRotation = Mathf.Lerp(0f, offsetRotation, localDistance / length);
            localLeftLine = CalcuateOffset(localRotation, localDistance) + Quaternion.Euler(0f, localRotation, 0f) * Vector3.forward;
            localRightLine = CalcuateOffset(localRotation, localDistance) + Quaternion.Euler(0f, localRotation, 0f) * Vector3.back;
        }
    }
}
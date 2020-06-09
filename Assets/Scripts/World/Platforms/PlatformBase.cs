using UnityEngine;

namespace TEDinc.LinesRunner
{
    public abstract class PlatformBase : MonoBehaviour, IPlatform
    {
        public virtual float length { get { return _length; } protected set { _length = value; } }
        public virtual float offsetRotation { get; protected set; }
        public virtual Vector3 offset => length * Vector3.right;
        public float totalOffsetRotation { get; private set; }
        public Vector3 totalOffset { get; private set; }

        [SerializeField]
        private float _length = 5f;



        public virtual void ElevateLines(float localDistance, out Vector3 localLeftLine, out Vector3 localRightLine)
        {
            localLeftLine = new Vector3(localDistance, 0f, GameConst.platformWidth / 2f);
            localRightLine = new Vector3(localDistance, 0f, -GameConst.platformWidth / 2f);
        }

        public void SetWorldData(float totalOffsetRotation, Vector3 totalOffset)
        {
            this.totalOffsetRotation = totalOffsetRotation;
            this.totalOffset = totalOffset;
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            //draw lines
            int iterations = 20;
            Vector3 localLeftLine, localRightLine;
            ElevateLines(0f, out localLeftLine, out localRightLine);

            for (int i = 1; i <= iterations; i++)
            {
                Vector3 localLeftLineNext, localRightLineNext;
                ElevateLines(Mathf.Lerp(0f, length, (float)i / iterations), out localLeftLineNext, out localRightLineNext);

                for (int j = 0; j < GameConst.linesCount; j++)
                {
                    float t = j / (GameConst.linesCount - 1f);
                    Gizmos.color = Color.HSVToRGB(t * 0.7f, 1f, 1f); //red on left line, next by hue gradient from 0 til 0.7
                    Gizmos.DrawLine(
                        Vector3.Lerp(transform.TransformPoint(localLeftLine), transform.TransformPoint(localRightLine), t),
                         Vector3.Lerp(transform.TransformPoint(localLeftLineNext), transform.TransformPoint(localRightLineNext), t));
                }

                localLeftLine = localLeftLineNext;
                localRightLine = localRightLineNext;
            }

            //draw connectors
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.2f); //start connector
            Gizmos.DrawSphere(transform.TransformPoint(offset), 0.2f); //end connector
            Gizmos.DrawLine(transform.TransformPoint(offset), //end rotation vector
                transform.TransformPoint(offset)
                + Quaternion.Euler(0f, offsetRotation + 90, 0f)
                * transform.forward);
        }
#endif
    }
}
using UnityEngine;

namespace TEDinc.LinesRunner
{
    public abstract class PlatformBase : MonoBehaviour, IPlatform
    {
        public virtual float length { get { return _length; } protected set { _length = value; } }
        public virtual float offsetRotation { get; protected set; }
        public virtual Vector3 offset => length * Vector3.right;
        

        [SerializeField]
        private float _length = 5f;



        public virtual void ElevateLines(float localDistance, out Vector3 localLeftLine, out Vector3 localRightLine)
        {
            localLeftLine = new Vector3(localDistance, 0f, GameConst.platformWidth / 2f);
            localRightLine = new Vector3(localDistance, 0f, -GameConst.platformWidth / 2f);
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
                    Gizmos.color = Color.HSVToRGB((j / (GameConst.linesCount - 1f)) * 0.7f + 0.15f, 1f, 1f);
                    Gizmos.DrawLine(
                        Vector3.Lerp(transform.TransformPoint(localLeftLine), transform.TransformPoint(localRightLine), j / (GameConst.linesCount - 1f)),
                         Vector3.Lerp(transform.TransformPoint(localLeftLineNext), transform.TransformPoint(localRightLineNext), j / (GameConst.linesCount - 1f)));
                }

                localLeftLine = localLeftLineNext;
                localRightLine = localRightLineNext;
            }

            //draw connectors
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.2f);
            Gizmos.DrawSphere(transform.TransformPoint(offset), 0.2f);
            Gizmos.DrawLine(transform.TransformPoint(offset), transform.TransformPoint(offset) + Quaternion.Euler(0f, offsetRotation + 90, 0f) * transform.forward);
        }
#endif
    }
}
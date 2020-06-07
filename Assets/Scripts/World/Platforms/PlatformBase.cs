using UnityEngine;

namespace TEDinc.LinesRunner
{
    public abstract class PlatformBase : MonoBehaviour, IPlatform
    {
        public virtual float length { get { return _length; } protected set { _length = value; } }
        public virtual float offsetRotation { get { return _offsetRotation; } protected set { _offsetRotation = value; } }
        public virtual Vector3 offset => length * Vector3.right;
        

        [SerializeField]
        private float _length = 5f;
        [SerializeField]
        private float _offsetRotation = 0f;



        public virtual void ElevateLines(float localDistance, out Vector3 localLeftLine, out Vector3 localRightLine)
        {
            localLeftLine = new Vector3(localDistance, 0f, -1f);
            localRightLine = new Vector3(localDistance, 0f, 1f);
        }
    }
}
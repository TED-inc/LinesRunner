using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class WorldController: IWorldController
    {
        protected readonly IWorld world;

        public void ElevateLines(float distance, out Vector3 leftLine, out Vector3 rightLine)
        {
            leftLine = new Vector3(distance, 0f, -1f);
            rightLine = new Vector3(distance, 0f, 1f);
        }

        public WorldController(IWorld world)
        {
            this.world = world;
        }
    }
}
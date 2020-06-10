namespace TEDinc.LinesRunner
{
    public static class GameConst
    {
        public const int linesCount = 3;
        public const float platformWidth = 2f;

        public const float startPlayerSpeed = 10f;
        public const float playerAcceleration = 0.1f;

        public const float playerChangeLineDyration = 0.15f;

        public const float playerJumpSpeed = 10f;
        public const float playerGravityMultipyaer = 4f;

        public const float loadDistance = 120f;
        public const float disableDistance = 25f;
        public const float destroyDistance = 100f;

        public const float startObstacleDistance = 40f;
        public const float minObstacleDistance = 5f;
        public const float maxObstacleDistance = 25f;

        public const string playerPrefsCoin = "coin";

        public const string platformMeshObjectName = "mesh";
        public const string platformMeshSavePath = "Assets/Assets/Graphics/Models";

        public const float inputTouchMinLenthToMove = 50f;
    }
}
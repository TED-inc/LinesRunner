using UnityEngine;
using UnityEditor;

namespace TEDinc.LinesRunner.Tools
{
    public class PlatformMeshBuilderWindow : EditorWindow
    {
        static PlatformMeshBuilder platformMeshBuilder;
        private PlatformBase platform;
        static float depth = 1f;
        static float sideOffset = 0.5f;
        static int iterations = 10;


        [MenuItem(nameof(Tools) + "/" + nameof(PlatformMeshBuilder))]
        private static void Init()
        {
            PlatformMeshBuilderWindow window =
                GetWindow(
                    typeof(PlatformMeshBuilderWindow),
                    false,
                    ObjectNames.NicifyVariableName(nameof(PlatformMeshBuilder))) 
                as PlatformMeshBuilderWindow;
        }

        private void OnGUI()
        {
            platform = EditorGUILayout.ObjectField(platform, typeof(PlatformBase), true) as PlatformBase;

            depth = EditorGUILayout.FloatField(ObjectNames.NicifyVariableName(nameof(depth)), depth);
            sideOffset = EditorGUILayout.FloatField(ObjectNames.NicifyVariableName(nameof(sideOffset)), sideOffset);
            iterations = EditorGUILayout.IntField(ObjectNames.NicifyVariableName(nameof(iterations)), iterations);

            EditorGUI.BeginDisabledGroup(platform == null);
            if (GUILayout.Button(nameof(Build)))
                Build();
            EditorGUI.EndDisabledGroup();
        }

        private void Build()
        {
            SetupBuilder();
            Mesh mesh = platformMeshBuilder.Build();
            SaveMeshAsset();
            AssignMesh();

            void SetupBuilder()
            {
                if (platformMeshBuilder == null)
                    platformMeshBuilder = new PlatformMeshBuilder();
                platformMeshBuilder.platform = platform;
                platformMeshBuilder.depth = depth;
                platformMeshBuilder.sideOffset = sideOffset;
                platformMeshBuilder.iterations = iterations;
            }

            void AssignMesh()
            {
                Transform meshObject = platform.transform.Find(GameConst.platformMeshObjectName);
                MeshFilter meshFilter = meshObject.GetComponent<MeshFilter>();
                MeshCollider meshCollider = meshObject.GetComponent<MeshCollider>();

                meshFilter.sharedMesh = mesh;
                if (meshCollider != null)
                    meshCollider.sharedMesh = mesh;
            }

            void SaveMeshAsset()
            {
                if (AssetDatabase.IsValidFolder(GameConst.platformMeshSavePath))
                    AssetDatabase.CreateAsset(mesh, GameConst.platformMeshSavePath + "/" + platform.name + "Mesh.asset");
                AssetDatabase.SaveAssets();
            }
        }
    }
}
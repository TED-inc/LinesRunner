using UnityEngine;

namespace TEDinc.LinesRunner.Tools
{
    public class PlatformMeshBuilder
    {
        public PlatformBase platform;
        public float depth;
        public float sideOffset;
        public int iterations;


        public Mesh Build()
        {
            Mesh mesh = new Mesh();
            GenerateMesh();
            return mesh;
            

            void GenerateMesh()
            {
                int vertCount = 0, trisCount = 0;
                Vector3 leftLine, rightLine;

                Vector3[] vertices = new Vector3[iterations * 4 * 2]; //4 sides * 2 normals of each vertex
                Vector3[] normals = new Vector3[vertices.Length];
                Vector2[] uv = new Vector2[vertices.Length];
                int[] triangles = new int[(iterations - 1) * 4 * 2 * 3]; //4 sides * 2 triangles per square * 3 indexes in triangle

                Generate();

                mesh.vertices = vertices;
                mesh.normals = normals;
                mesh.uv = uv;
                mesh.triangles = triangles;



                void Generate()
                {
                    for (int i = 0; i < iterations; i++)
                    {
                        float elevation = Mathf.Lerp(0f, platform.length, i / (iterations - 1f));
                        platform.ElevateLines(elevation, out leftLine, out rightLine);

                        if (i != iterations - 1)
                            FillTris();
                        FillVertFull(i / (iterations - 1f));
                    }
                }

                void FillTris()
                { //verCo = 8, 0 - 7
                    for (int i = 0; i < 4; i++)
                        SetSquare(i * 2);

                    void SetSquare(int i)
                    {
                        ///square shema - clockwise
                        /// b-d
                        /// |\|
                        /// a-c

                        //set first triangle //1 2 9
                        triangles[trisCount++] = vertCount + i + 1; //a
                        triangles[trisCount++] = vertCount + i + 2; //b
                        triangles[trisCount++] = vertCount + i + 8 + 1; //c

                        //set second triangle
                        if (i != 6)
                        {
                            triangles[trisCount++] = vertCount + i + 2; //c
                            triangles[trisCount++] = vertCount + i + 8 + 2; //b
                            triangles[trisCount++] = vertCount + i + 8 + 1; //d
                        }
                        else
                        {
                            triangles[trisCount++] = vertCount + i + 1;
                            triangles[trisCount++] = vertCount;
                            triangles[trisCount++] = vertCount + i + 2;
                        }
                    }
                }

                void FillVertFull(float f)
                {
                    /// verticies shema - counter clockwise
                    /// a----d
                    /// \    / 
                    ///  b--c

                    //a
                    vertices[vertCount++] = Vector3.LerpUnclamped(leftLine, rightLine, -sideOffset / GameConst.platformWidth);
                    AddSubVertexAndNormals(Vector3.up, Vector3.forward);
                    //b
                    vertices[vertCount++] = leftLine + Vector3.down * depth;
                    AddSubVertexAndNormals(Vector3.forward, Vector3.down);
                    //c
                    vertices[vertCount++] = rightLine + Vector3.down * depth;
                    AddSubVertexAndNormals(Vector3.down, Vector3.back);
                    //d
                    vertices[vertCount++] = Vector3.LerpUnclamped(leftLine, rightLine, 1f + sideOffset / GameConst.platformWidth);
                    AddSubVertexAndNormals(Vector3.back, Vector3.up);

                    void AddSubVertexAndNormals(Vector3 vertNormal, Vector3 subVertNormal)
                    {
                        Quaternion rotation = Quaternion.LookRotation(leftLine - rightLine, Vector3.up);

                        normals[vertCount - 1] = rotation * vertNormal;
                        normals[vertCount] = rotation * subVertNormal;

                        uv[vertCount - 1] = new Vector2(0f, f);
                        uv[vertCount] = new Vector2(1f, f);

                        vertices[vertCount++] = vertices[vertCount - 2];
                    }
                }
            }
        }
    }
}
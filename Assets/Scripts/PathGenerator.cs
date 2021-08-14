using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public static PathGenerator pathGen;
    [SerializeField] float pathDetailGap;
    [SerializeField] int pathDetailCount;
    [SerializeField] int width;
    [SerializeField] float noiseSmoothness;
    [SerializeField] float noiseScale;

    Player player = WorldInfo.player;

    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;

    int triangleIndex;
    // Start is called before the first frame update
    void Start()
    {
        pathGen = this;
        pathDetailCount += 1;
        mesh = new Mesh();
        vertices = new Vector3[2 * pathDetailCount];
        uvs = new Vector2[2 * pathDetailCount];
        triangles = new int[(2 - 1) * (pathDetailCount - 1) * 6];

        //screw magic numbers
        const int widthDetail = 2;

        int vertexIndex = 0;
        for (int y = 0; y < pathDetailCount; y++)
        {
            for (int x = 0; x < widthDetail; x++)
            {
                vertices[vertexIndex] = new Vector3(transform.position.z + (y * pathDetailGap), 0, (x * width) - (width / 2f));
                uvs[vertexIndex] = new Vector2(x / (float)widthDetail, y / (float)pathDetailCount);

                if (x < widthDetail - 1 && y < pathDetailCount - 1)
                {
                    AddTriangle(vertexIndex, vertexIndex + widthDetail + 1, vertexIndex + widthDetail);
                    AddTriangle(vertexIndex + widthDetail + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }
        UpdateMesh();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        void AddTriangle(int a, int b, int c)
        {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;
            triangleIndex += 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pathDetailCount; i++)
        {
            float h = GetPathCenterAtPos(pathDetailGap * i);
            vertices[i * 2].z = h - (width / 2);
            vertices[i * 2 + 1].z = h + (width / 2);
        }
        UpdateMesh();
    }

    public float GetPathCenterAtPos(float pos)
    {
        return (Mathf.PerlinNoise((pos + WorldInfo.i.worldLocation) / noiseSmoothness, 0) * noiseScale) - (noiseScale / 2);
    }

    public void UpdateMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}

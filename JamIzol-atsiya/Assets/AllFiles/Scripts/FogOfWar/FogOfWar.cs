using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FogOfWar : MonoBehaviour
{
    public static FogOfWar Instance { get; private set; }
    [SerializeField] private float levelSizeX = 1000f;
    [SerializeField] private float levelSizeZ = 1000f;
    [SerializeField] private int cellsCountX = 10;
    [SerializeField] private int cellsCountZ = 10;
    [SerializeField] private int polygonsInCell = 10;
    [SerializeField] private Material fogMaterial;

    private readonly Dictionary<MeshFilter, MeshRenderer> meshesData = new Dictionary<MeshFilter, MeshRenderer>();
    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
    }
    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        GenerateMesh();
        BuildCollider();
    }
    public void Disperse(Vector3 point, float radius)
    {
        Vector3 direction = (mainCamera.transform.position - point).normalized;
        if(Physics.Raycast(point,direction, out RaycastHit hit))
        {
            point = hit.point;
        }
        var availableCells = meshesData.Where(m => CheckIfInsideOfCell(m.Value, point, radius));

        float sqrRadius = radius * radius;

        foreach (var pair in availableCells)
        {
            var sharedMesh = pair.Key.sharedMesh;
            var vertices = sharedMesh.vertices;
            var colors = sharedMesh.colors;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (colors[i].a < 0.01f)
                {
                    continue;
                }

                var vertexGlobalPosition = vertices[i] + pair.Key.transform.position;

                var sqrMagnitude = (vertexGlobalPosition - point).sqrMagnitude;
                if (sqrMagnitude > sqrRadius)
                {
                    continue;
                }

                var alpha = sqrMagnitude / sqrRadius;
                if (colors[i].a > alpha)
                {
                    colors[i].a = alpha;
                }
            }
            sharedMesh.colors = colors;
            pair.Key.sharedMesh = sharedMesh;
        }
    }
    private static bool CheckIfInsideOfCell(Renderer meshRenderer, Vector3 point, float radius)
    {
        var bounds = meshRenderer.bounds;
        var min = bounds.min;
        var max = bounds.max;

        if (point.x > min.x && point.x < max.x && point.z > min.z && point.z < max.z)
        {
            return true;
        }
        if (Mathf.Abs(point.x - min.x) < radius || Mathf.Abs(point.x - max.x) < radius)
        {
            return true;
        }
        if (Mathf.Abs(point.z - min.z) < radius || Mathf.Abs(point.z - max.z) < radius)
        {
            return true;
        }
        return false;
    }


    private void GenerateMesh()
    {
        float polygonSizeX = levelSizeX / cellsCountX / polygonsInCell;
        float polygonSizeZ = levelSizeZ / cellsCountZ / polygonsInCell;

        float cellSizeX = polygonSizeX * polygonsInCell;
        float cellSizeZ = polygonSizeZ * polygonsInCell;

        for (int x = 0; x < cellsCountX; x++)
        {
            for (int z = 0; z < cellSizeZ; z++)
            {
                GameObject obj = new GameObject($"Cell x{x} z{z}");
                obj.transform.SetParent(transform);
                obj.isStatic = true;

                MeshFilter meshFilter = obj.AddComponent<MeshFilter>();

                meshFilter.sharedMesh = GenerateMesh(polygonsInCell, polygonSizeX, polygonSizeZ);

                MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
                meshRenderer.sharedMaterial = fogMaterial;

                meshesData[meshFilter] = meshRenderer;

                Vector3 position = new Vector3(x * cellSizeX, 2f, z * cellSizeZ);
                obj.transform.localPosition = position;
            }
        }
    }
    private void BuildCollider()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = new Vector3(levelSizeX * 0.5f, 0f, levelSizeZ * 0.5f);
        boxCollider.size = new Vector3(levelSizeX, 0.01f, levelSizeZ);
    }
    private static Mesh GenerateMesh(int polygonsCount, float polygonSizeX, float polygonSizeZ)
    {
        var vertices = new List<Vector3[]>();
        var triangles = new List<int>();
        var uvs = new List<Vector2>();
        var colors = new List<Color>();

        int width = polygonsCount + 1;
        //Generate
        for (int z = 0; z < width; z++)
        {
            vertices.Add(new Vector3[width]);
            for (int x = 0; x < width; x++)
            {
                Vector3 currentPoint = new Vector3(x * polygonSizeX, 0f, z * polygonSizeZ);
                vertices[z][x] = currentPoint;
                uvs.Add(new Vector2(x, z));
                if (x == 0 || z == 0 || x == width)
                {
                    continue;
                }

                triangles.Add(x + z * width);
                triangles.Add(x + (z - 1) * width);
                triangles.Add((x - 1) + (z - 1) * width);

                triangles.Add(x + z * width);
                triangles.Add((x - 1) + (z - 1) * width);
                triangles.Add((x - 1) + z * width);
            }
        }
        var unfoldedVertex = new Vector3[width * width];
        int counter = 0;
        foreach (var v in vertices)
        {
            v.CopyTo(unfoldedVertex, counter * width);
            counter++;
        }
        for(var i = 0; i < unfoldedVertex.Length; i++)
        {
            colors.Add(Color.black);
        }
        var mesh = new Mesh
        {
            vertices = unfoldedVertex,
            triangles = triangles.ToArray(),
            uv = uvs.ToArray(),
            colors = colors.ToArray()
        };
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }
}

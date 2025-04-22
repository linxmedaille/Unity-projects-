using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class MeshGenerator : MonoBehaviour
{

    public GameObject[] myPrefabs;
    public float noiseScale = 0.5f; // Controls how spread-out noise is
    public float noiseThreshold = 0.6f; // Higher = fewer trees
    public float biomeScale = 0.1f;
    float biomeOffsetX;
    float biomeOffsetZ;
    Mesh mesh;
    MeshCollider meshCollider;
    Vector3[] vertices;
    int[] triangles;
    Color[] vertexColors; // Array to store colors for each vertex
    public int xSize = 20;
    public int zSize = 20;
    public float strength;
    List<Vector3> placedTrees = new List<Vector3>();

    void Start()
    {
        mesh = new Mesh();
        biomeOffsetX = Random.Range(0f, 1000f);
        biomeOffsetZ = Random.Range(0f, 1000f);
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        generateTrees();

    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        vertexColors = new Color[vertices.Length]; // Initialize the vertex colors array

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * strength, z * strength) * 4;
                vertices[i] = new Vector3(x * 4f, y, z * 4f);

                // Apply biome offset when determining biome color
                string biome = GetBiome((vertices[i].x), (vertices[i].z));
                vertexColors[i] = GetBiomeColor(biome); // Assign color based on biome

                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                int vertIndex = z * (xSize + 1) + x;

                // First triangle (lower left)
                triangles[tris + 0] = vertIndex;                  // Current vertex
                triangles[tris + 1] = vertIndex + (xSize + 1);    // Vertex below
                triangles[tris + 2] = vertIndex + 1;              // Vertex to right

                // Second triangle (upper right)
                triangles[tris + 3] = vertIndex + 1;              // Vertex to right
                triangles[tris + 4] = vertIndex + (xSize + 1);    // Vertex below
                triangles[tris + 5] = vertIndex + (xSize + 1) + 1; // Vertex below and to right

                tris += 6;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        
        transform.position = new Vector3(-xSize*2, 0, 0-7.5f);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = vertexColors; // Apply the vertex colors to the mesh
        mesh.RecalculateNormals();
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += new Vector3(-xSize *2, 0, 0);
        }
        //OnDrawGizmos();
    }

    void generateTrees()
    {
        int step = Random.Range(1, 3); // Check every 6th vertex to space things out

        for (int i = 0; i < vertices.Length; i += step)
        {
            step = Random.Range(1, 3);
            float xValue = vertices[i].x;
            RaycastHit hit;
            float yValue = vertices[i].y + 4;
            Vector3 origin = new Vector3(xValue, 10, vertices[i].z);
            Vector3 direction = Vector3.down;
            float randomScale = Random.Range(0.8f, 1.4f);
            int randomTree = 1; //Random.Range(0,4);

            // Apply the biome offset here as well
            string biome = GetBiome((vertices[i].x), (vertices[i].z)); // Apply offsets to biome
            float noise = Mathf.PerlinNoise(vertices[i].x * noiseScale, vertices[i].z * noiseScale);
            GameObject newTree;

            if (noise > noiseThreshold)
            {
                Vector3 positionToGenerate = new Vector3(xValue, 10, vertices[i].z);
                switch (biome)
                {
                    case "Oak":
                        if (randomTree == 0) { }
                        yValue = vertices[i].y;
                        newTree = Instantiate(myPrefabs[3], positionToGenerate, Quaternion.Euler(-90f, Random.Range(0f, 360f), 0f));
                        newTree.transform.localScale *= randomScale;
                        if (Physics.Raycast(origin, direction, out hit, 200f))
                        {
                            yValue = (hit.point.y) * randomScale;
                        }
                        newTree.transform.position = new Vector3(xValue, yValue, vertices[i].z);
                        break;

                    case "Spruce":
                        if (randomTree == 1)
                        {
                            newTree = Instantiate(myPrefabs[1], positionToGenerate, Quaternion.Euler(-90f, Random.Range(0f, 360f), 0f));
                            newTree.transform.localScale *= randomScale;
                            if (Physics.Raycast(origin, direction, out hit, 200f))
                            {
                                yValue = (hit.point.y + 0.2f) * randomScale;
                            }
                            newTree.transform.position = new Vector3(xValue, yValue, vertices[i].z);
                        }
                        if (randomTree == 2)
                        {
                            newTree = Instantiate(myPrefabs[2], positionToGenerate, Quaternion.Euler(-90f, Random.Range(0f, 360f), 0f));
                            newTree.transform.localScale *= randomScale;
                            if (Physics.Raycast(origin, direction, out hit, 200f))
                            {
                                yValue = (hit.point.y + 5.2f) * randomScale;
                            }
                            newTree.transform.position = new Vector3(xValue, yValue, vertices[i].z);
                        }
                        if (randomTree == 3)
                        {
                            newTree = Instantiate(myPrefabs[0], positionToGenerate, Quaternion.Euler(-90f, Random.Range(0f, 360f), 0f));
                            newTree.transform.localScale *= randomScale;
                            if (Physics.Raycast(origin, direction, out hit, 200f))
                            {
                                yValue = (hit.point.y + 3.2f) * randomScale;
                            }
                            newTree.transform.position = new Vector3(xValue, yValue, vertices[i].z);
                        }
                        break;
                }
            }
        }
    }

    string GetBiome(float x, float z)
    {
        float biomeValue = Mathf.PerlinNoise((x + biomeOffsetX) * biomeScale, (z + biomeOffsetZ) * biomeScale); ;

        if (biomeValue < 0.3f) return "Oak";
        else if (biomeValue < 0.5f) return "Grassland";
        else if (biomeValue < 0.7f) return "Forest";
        else if (biomeValue < 0.9f) return "Spruce";
        else return "Snow";
    }

    Color GetBiomeColor(string biome)
    {
        switch (biome)
        {
            case "Oak":
                return new Color(0.6f, 0.4f, 0.1f); // Light brownish color for Oak
            case "Grassland":
                return new Color(0.8f, 0.8f, 0.2f); // Yellowish color for Grassland
            case "Forest":
                return new Color(0.2f, 0.6f, 0.2f); // Dark green color for Forest
            case "Spruce":
                return new Color(0.3f, 0.5f, 0.3f); // Lighter green for Spruce
            case "Snow":
                return Color.white; // White color for Snow
            default:
                return Color.green; // Default to green if no match
        }
    }


}
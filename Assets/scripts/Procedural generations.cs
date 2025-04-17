using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MeshGenerator : MonoBehaviour
{
    public GameObject[] myPrefabs;
    Mesh mesh;
    MeshCollider meshCollider;
    Vector3[] vertices;
    int[] triangles;
    public int xSize = 20;
    public int zSize = 20;
    public float treeDensity;
    public float strength;
    List<Vector3> placedTrees = new List<Vector3>();
    public float minDistance; // adjust as needed
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        generateTrees();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];


        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * strength, z * strength) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        transform.position = new Vector3(-xSize/2,0 , -zSize / 2);
    }
    /**
    void generateTrees()
    {
        placedTrees.Clear();
        for (int i = 0; i < vertices.Length; i++)
        {

            float yValue = vertices[i].y;
            if (yValue > treeDensity)
            {
                if (vertices[i].x > xSize / 2)
                {
                    Vector3 positionToGenerate = new Vector3(vertices[i].x-xSize , yValue+5, vertices[i].z );
                    placedTrees.Add(positionToGenerate);
                    foreach(Vector3 treePos in placedTrees)
                    {
                        if (!(Vector3.Distance(treePos, positionToGenerate) < minDistance))
                        {
                            GameObject newObject = Instantiate(myPrefabs[0], positionToGenerate, Quaternion.Euler(-90f, 0f, 0f));
                        }
                    }
                
                    
                }
                else
                {
                    Vector3 positionToGenerate = new Vector3(vertices[i].x , yValue + 5, vertices[i].z );
                    placedTrees.Add(positionToGenerate);
                    foreach (Vector3 treePos in placedTrees)
                    {
                        if (!(Vector3.Distance(treePos, positionToGenerate) < minDistance))
                        {
                            GameObject newObject = Instantiate(myPrefabs[0], positionToGenerate, Quaternion.Euler(-90f, 0f, 0f));
                        }
                    }
                }
                 
               
            }
            
        }
   
    }
    **/
    void generateTrees()
    {
        float noiseScale = 0.5f;      // Controls how spread-out noise is
        float noiseThreshold = 0.6f;  // Higher = fewer trees
        int step = 4;                 // Check every 5th vertex to space things out

        for (int i = 0; i < vertices.Length; i += step)
        {
            float yValue = vertices[i].y;
            Debug.Log(yValue);


            float noise = Mathf.PerlinNoise(vertices[i].x * noiseScale, vertices[i].z * noiseScale);

                if (noise > noiseThreshold)
                {
                    Vector3 positionToGenerate = new Vector3(
                        vertices[i].x > xSize / 2 ? vertices[i].x - xSize : vertices[i].x,
                        yValue+5,
                        vertices[i].z
                    );

                    GameObject newTree = Instantiate(myPrefabs[0], positionToGenerate, Quaternion.Euler(-90f, Random.Range(0f, 360f), 0f));
                    newTree.transform.localScale *= Random.Range(0.8f, 1.3f);


            }
        }
    }

}
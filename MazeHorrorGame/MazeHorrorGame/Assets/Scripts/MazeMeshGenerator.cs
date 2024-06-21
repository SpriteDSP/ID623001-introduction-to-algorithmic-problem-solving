/// <remarks>
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class should generate a 3d maze using mazeconstructors data, creating and combining mesh data for the floor, walls, and roof
/// </summary>

using System.Collections.Generic;
using UnityEngine;

public class MazeMeshGenerator : MonoBehaviour
{
    //reference to the MazeConstructor
    public MazeConstructor mazeContructor;

    // width and height of the maze cells
    public float width = 3.75f;
    public float height = 3.5f;

    private void Start()
    {
        MazeConstructor mazeConstructor = new MazeConstructor();

        //generate maze
        GenerateMaze(mazeContructor.Data);
    }

    //creates a maze using the three different meshes
    public void GenerateMaze(int[,] data)
    {
        // Generate the floor mesh from maze data
        Mesh floorMesh = GenerateFloorMeshFromData(data);
        // Create a game object for the floor and assign the floor mesh and material
        CreateMazeGameObject("MazeFloor", floorMesh, "floor-mat");

        // Generate the wall mesh from maze data
        Mesh wallMesh = GenerateWallMeshFromData(data);
        // Create a game object for the walls and assign the wall mesh and material
        CreateMazeGameObject("MazeWalls", wallMesh, "wall-mat");

        // Generate the roof mesh from maze data
        Mesh roofMesh = GenerateRoofMeshFromData(data);
        // Create a game object for the roof and assign the roof mesh and material
        CreateMazeGameObject("MazeRoof", roofMesh, "roof-mat");
    }

    //this will create a floor mesh
    public Mesh GenerateFloorMeshFromData(int[,] data)
    {
        // name new mesh
        Mesh floorMesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        //get the maximum row and column index
        int rMax = data.GetUpperBound(0);
        int cMax = data.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (data[i, j] != 1)
                {
                    //generate a quad for the floor at the cell position
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(j * width, 0, i * width),
                        Quaternion.LookRotation(Vector3.down),
                        new Vector3(width, width, 1)
                    ), ref vertices, ref uvs, ref triangles);
                }
            }
        }

        //assign vertices, uvs, and triangles to the floor mesh
        floorMesh.vertices = vertices.ToArray();
        floorMesh.uv = uvs.ToArray();
        floorMesh.triangles = triangles.ToArray();

        floorMesh.RecalculateNormals();

        //give it a collider
        MeshCollider floorCollider = gameObject.AddComponent<MeshCollider>();
        floorCollider.sharedMesh = floorMesh;

        return floorMesh;
    }

    //this will create a roof mesh
    private Mesh GenerateRoofMeshFromData(int[,] data)
    {
        Mesh roofMesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        int rMax = data.GetUpperBound(0);
        int cMax = data.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (data[i, j] != 1)
                {
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(j * width, height, i * width),
                        Quaternion.LookRotation(Vector3.up),
                        new Vector3(width, width, 1)
                    ), ref vertices, ref uvs, ref triangles);
                }
            }
        }

        roofMesh.vertices = vertices.ToArray();
        roofMesh.uv = uvs.ToArray();
        roofMesh.triangles = triangles.ToArray();
        
        roofMesh.RecalculateNormals();

        return roofMesh;
    }

    //this will create a wall mesh
    private Mesh GenerateWallMeshFromData(int[,] data)
    {
        Mesh wallMesh = new Mesh();
        
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        int rMax = data.GetUpperBound(0);
        int cMax = data.GetUpperBound(1);

        //calculate half of the wall height
        float halfH = height * .5f;

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (data[i, j] != 1)
                {
                    if (i - 1 < 0 || data[i - 1, j] == 1)
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(j * width, halfH, (i - .5f) * width),
                            //Quaternion.LookRotation(Vector3. ) is important to get the walls to face as intended
                            Quaternion.LookRotation(Vector3.back),
                            new Vector3(width, height, 1)
                        ), ref vertices, ref uvs, ref triangles);

                    if (j + 1 > cMax || data[i, j + 1] == 1)
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((j + .5f) * width, halfH, i * width),
                            Quaternion.LookRotation(Vector3.right),
                            new Vector3(width, height, 1)
                        ), ref vertices, ref uvs, ref triangles);

                    if (j - 1 < 0 || data[i, j - 1] == 1)
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((j - .5f) * width, halfH, i * width),
                            Quaternion.LookRotation(Vector3.left),
                            new Vector3(width, height, 1)
                        ), ref vertices, ref uvs, ref triangles);

                    if (i + 1 > rMax || data[i + 1, j] == 1)
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(j * width, halfH, (i + .5f) * width),
                            Quaternion.LookRotation(Vector3.forward),
                            new Vector3(width, height, 1)
                        ), ref vertices, ref uvs, ref triangles);
                }
            }
        }

        wallMesh.vertices = vertices.ToArray();
        wallMesh.uv = uvs.ToArray();
        wallMesh.triangles = triangles.ToArray();

        wallMesh.RecalculateNormals();

        return wallMesh;
    }

    //this method is responsible for creating a quad, transforming it, and helping render it in 3d space
    private void AddQuad(Matrix4x4 matrix, ref List<Vector3> newVertices, ref List<Vector2> newUVs, ref List<int> newTriangles)
    {
        int index = newVertices.Count;

        Vector3 vert1 = new Vector3(-.5f, -.5f, 0);
        Vector3 vert2 = new Vector3(-.5f, .5f, 0);
        Vector3 vert3 = new Vector3(.5f, .5f, 0);
        Vector3 vert4 = new Vector3(.5f, -.5f, 0);

        newVertices.Add(matrix.MultiplyPoint3x4(vert1));
        newVertices.Add(matrix.MultiplyPoint3x4(vert2));
        newVertices.Add(matrix.MultiplyPoint3x4(vert3));
        newVertices.Add(matrix.MultiplyPoint3x4(vert4));

        newUVs.Add(new Vector2(1, 0));
        newUVs.Add(new Vector2(1, 1));
        newUVs.Add(new Vector2(0, 1));
        newUVs.Add(new Vector2(0, 0));

        newTriangles.Add(index + 0);
        newTriangles.Add(index + 1);
        newTriangles.Add(index + 2);
        newTriangles.Add(index + 0);
        newTriangles.Add(index + 2);
        newTriangles.Add(index + 3);
    }

    //this method will make the maze parts a generated gameobject with the needed components
    private void CreateMazeGameObject(string name, Mesh mesh, string materialName)
    {
        //creates a new gameobject for the maze part
        GameObject mazePart = new GameObject(name);
        //world origin position
        mazePart.transform.position = Vector3.zero;

        //add meshfilter component and auto assign the mesh
        MeshFilter meshFilter = mazePart.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        //add meshrenderer and auto assign
        MeshRenderer meshRenderer = mazePart.AddComponent<MeshRenderer>();
        meshRenderer.material = Resources.Load<Material>(materialName);

        //add meshcollider and auto assign
        MeshCollider meshCollider = mazePart.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }
}

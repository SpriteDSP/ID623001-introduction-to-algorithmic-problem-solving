using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private MazeMeshGenerator mazeMeshGenerator;
    [SerializeField] private MazeConstructor mazeConstructor;

    [SerializeField] private GameObject playerControllerPrefab;
    [SerializeField] private GameObject monsterPrefab;

    public void Start()
    {
        GeneratePlayer();
        GenerateMonster();
    }

    public void GeneratePlayer()
    {
        int xCoord = 1;
        int zCoord = 1;
        Vector3 startPos = new Vector3(xCoord * mazeMeshGenerator.width, 1, zCoord * mazeMeshGenerator.width);

        GameObject player = Instantiate(playerControllerPrefab, startPos, Quaternion.identity);
    }
    public void GenerateMonster()
    {
        // -1 would place the monster inside the final cell, which is closed. 
        // -2 places it in the first guaranteed open cell (assuming your maze has odd-numbered rows + cols)
        int xCoord = rows - 2;
        int zCoord = cols - 2;
        Vector3 startPos = new Vector3(xCoord * mazeMeshGenerator.width, 0, zCoord * mazeMeshGenerator.width);
        GameObject newMonster = Instantiate(monsterPrefab, startPos, Quaternion.identity);
    }
}
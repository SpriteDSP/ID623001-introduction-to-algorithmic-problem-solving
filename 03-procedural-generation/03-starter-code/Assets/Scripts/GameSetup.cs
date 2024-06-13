using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private MazeMeshGenerator mazeMeshGenerator;
    [SerializeField] private MazeConstructor mazeConstructor;

    [SerializeField] private GameObject playerControllerPrefab;
    [SerializeField] private GameObject monsterPrefab;

    public void Start()
    {
        // generate the player at the start of the game
        GeneratePlayer();
        // generate the monster at the start of the game
        GenerateMonster();
    }

    public void GeneratePlayer()
    {
        // coordinates for the player's starting position
        int xCoord = 1;
        int zCoord = 1;
        // calculate the starting position for the player based on maze cell width
        Vector3 startPos = new Vector3(xCoord * mazeMeshGenerator.width, 1, zCoord * mazeMeshGenerator.width);

        // instantiate the player controller at the calculated starting position
        GameObject player = Instantiate(playerControllerPrefab, startPos, Quaternion.identity);
    }

    public void GenerateMonster()
    {
        // -1 would place the monster inside the final cell, which is closed
        // -2 places it in the first guaranteed open cell (assuming your maze has odd-numbered rows and columns)
        // int xCoord = rows - 2;
        // int zCoord = cols - 2;
        // calculate the starting position for the monster based on maze cell width
        // Vector3 startPos = new Vector3(xCoord * mazeMeshGenerator.width, 0, zCoord * mazeMeshGenerator.width);
        // instantiate the monster at the calculated starting position
        // GameObject newMonster = Instantiate(monsterPrefab, startPos, Quaternion.identity);
    }
}

using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private MazeMeshGenerator mazeMeshGenerator;
    [SerializeField] private MazeConstructor mazeConstructor;

    [SerializeField] private GameObject playerControllerPrefab;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject treasurePrefab;

    private int rows = 30; // Example maze dimensions
    private int cols = 30;

    public void Start()
    {
        // Initialize our game state
        mazeConstructor.GenerateNewMaze(rows, cols);

        // generate the player at the start of the game
        GeneratePlayer();

        // generate the monster at the end of the maze
        GenerateMonster();

        // generate the treasure in a reasonable distance from the player's spawn point
        GenerateTreasure();
    }

    public void GeneratePlayer()
    {
        // For simplicity, placing player at the entrance (0, 1) in maze coordinates
        Vector3 startPos = new Vector3(0, 1, 0) * mazeMeshGenerator.width;
        GameObject player = Instantiate(playerControllerPrefab, startPos, Quaternion.identity);
    }

    public void GenerateMonster()
    {
        // Generate maze data if not already generated
        int[,] mazeData = mazeConstructor.Data;

        // Calculate the ending position for the monster
        int numRows = mazeData.GetLength(0);
        int numCols = mazeData.GetLength(1);
        Vector3 endPos = Vector3.zero;
        bool monsterPlaced = false;

        // Find a suitable position for the monster (furthest reachable point)
        // You can implement your own logic based on maze solving algorithms if needed
        endPos = new Vector3((numRows - 1) * mazeMeshGenerator.width, 0, (numCols - 1) * mazeMeshGenerator.width);

        // Instantiate the monster prefab at the calculated ending position
        GameObject newMonster = Instantiate(monsterPrefab, endPos, Quaternion.identity);
    }

    private void GenerateTreasure()
    {
        // Get player's starting position
        Vector3 playerPos = new Vector3(0, 1, 0) * mazeMeshGenerator.width;

        // Generate treasure position
        Vector3 treasurePosition = FindTreasurePosition(playerPos);

        // Spawn the treasure
        GameObject treasure = Instantiate(treasurePrefab, treasurePosition, Quaternion.identity);
    }

    private Vector3 FindTreasurePosition(Vector3 playerPos)
    {
        int xCoord = 0;
        int zCoord = 0;

        // Keep trying random cells until we find a suitable one
        int repeats = 0;
        while (repeats < 1000)
        {
            xCoord = Random.Range(1, rows - 1);
            zCoord = Random.Range(1, cols - 1);

            Vector3 treasurePosition = new Vector3(xCoord * mazeMeshGenerator.width, 0, zCoord * mazeMeshGenerator.width);

            // Check if it's an open space and not too close to the player's start position
            if (mazeConstructor.Data[zCoord, xCoord] == 0 && Vector3.Distance(treasurePosition, playerPos) > 10f)
            {
                return treasurePosition;
            }

            repeats++;
        }

        // If no suitable position found, return a default value (should handle this case in actual implementation)
        return Vector3.zero;
    }
}

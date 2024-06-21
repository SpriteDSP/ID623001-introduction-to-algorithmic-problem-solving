/// <remarks>
/// Bugs: DisableMonsterPathfinding doesnt work, when a game is won it will still move. its most likely because its not hooked up and referenced properly
/// </remarks>
// <summary>
/// This class manages nearly everything about the games flow, it sets the players objectives and obstacles into the 3d environment
/// ontop of that it manages what happens upon losing and winning
/// </summary>

using UnityEngine;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    //singleton instance setup
    public static GameSetup Instance;

    //serialized fields to assign in editor
    [SerializeField] private MazeMeshGenerator mazeMeshGenerator;
    [SerializeField] private MazeConstructor mazeConstructor;

    [SerializeField] private GameObject playerControllerPrefab;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject treasurePrefab;

    [SerializeField] private Text uiText;

    //maze data, rows and columns
    private int[,] mazeData;
    private int rows;
    private int cols;

    //instance of game objects, these are for the gamelose and gamewon methods
    private GameObject playerInstance;
    private GameObject monsterInstance;
    private GameObject treasureInstance;

    private bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        //initialize
        mazeData = mazeConstructor.GenerateMazeDataFromDimensions(rows, cols);

        //generate maze mesh
        mazeMeshGenerator.GenerateMaze(mazeData);

        // generate the player, enemy ai, and objective to win
        GeneratePlayer();
        GenerateMonster();
        GenerateTreasure();
    }

    public void GeneratePlayer()
    {
        //coordinates for starting position
        int xCoord = 1;
        int zCoord = 1;

        //calculate the starting position based on maze cell width
        Vector3 startPos = new Vector3(xCoord * mazeMeshGenerator.width, 1, zCoord * mazeMeshGenerator.width);

        //instantiate
        playerInstance = Instantiate(playerControllerPrefab, startPos, Quaternion.identity);
    }

    public void GenerateMonster()
    {
        //ensure the rows and cols are initialized correctly
        int rows = mazeConstructor.Data.GetLength(0);
        int cols = mazeConstructor.Data.GetLength(1);

        // -2 places it in the first guaranteed open cell
        int xCoord = rows - 2;
        int zCoord = cols - 2;

        Vector3 startPos = new Vector3(xCoord * mazeMeshGenerator.width, 0, zCoord * mazeMeshGenerator.width);

        monsterInstance = Instantiate(monsterPrefab, startPos, Quaternion.identity);
    }

    private void GenerateTreasure()
    {
        //ensure rows and cols are initialized correctly
        int rows = mazeConstructor.Data.GetLength(0);
        int cols = mazeConstructor.Data.GetLength(1);

        //assuming maze is at least 6x6
        int xCoord = UnityEngine.Random.Range(rows - 5, rows - 2);
        int zCoord = UnityEngine.Random.Range(cols - 5, cols - 2);

        //this will keep trying random cells until we find an open one
        //or if its been attempted 1000 times
        int repeats = 0;
        while (mazeConstructor.Data[zCoord, xCoord] != 0 && repeats < 1000)
        {
            xCoord = UnityEngine.Random.Range(rows - 5, rows - 2);
            zCoord = UnityEngine.Random.Range(cols - 5, cols - 2);
            repeats++;
        }

        Vector3 treasurePos = new Vector3(xCoord * mazeMeshGenerator.width, 0, zCoord * mazeMeshGenerator.width);

        treasureInstance = Instantiate(treasurePrefab, treasurePos, Quaternion.identity);
    }

    //method with all the functions that create gamewon
    public void GameWon()
    {
        gameEnded = true;

        uiText.text = "You Escaped!";
        uiText.gameObject.SetActive(true);

        DisablePlayerControls();

        DisableMonsterPathfinding();

        PlayTreasureAudio();

        //restarts the game after a delay
        Invoke("RestartGame", 3f);
    }

    //method with all the functions that create gamelose
    public void GameLose()
    {
        gameEnded = true;

        uiText.text = "You Were Caught!";
        uiText.gameObject.SetActive(true);

        DisablePlayerControls();

        PlayPlayerAudio();

        Invoke("RestartGame", 3f);
    }

    private void DisablePlayerControls()
    {
        FpsMovement playerController = playerInstance.GetComponent<FpsMovement>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }

    private void DisableMonsterPathfinding()
    {
        PathFinder monsterPathfinding = monsterInstance.GetComponent<PathFinder>();
        if (monsterPathfinding != null)
        {
            monsterPathfinding.enabled = false;
        }
    }
    private void PlayTreasureAudio()
    {
        AudioSource audioSource = treasureInstance.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void PlayPlayerAudio()
    {
        AudioSource audioSource = playerInstance.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void RestartGame()
    {
        //reload scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}

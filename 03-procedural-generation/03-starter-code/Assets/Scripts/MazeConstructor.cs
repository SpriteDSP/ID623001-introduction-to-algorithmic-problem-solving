//this script should generate a maze with now a corresponding node graph
//
using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    //a variable to display a debug of the maze
    public bool showDebug;

    //the maze data
    public int[,] Data { get; private set; }

    //variable for the maze dimensions
    public int rows, cols;

    //node graph to show walkable and non-walkable cells
    public Node[,] Graph { get; private set; }

    void Awake()
    {
        GenerateNewMaze(rows, cols);
    }

    // gui = graphical user interface
    void OnGUI()
    {
        if (!showDebug)
            return;

        int[,] maze = Data;

        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        //loops through the rows from the first index to the last (top to bottom)
        for (int i = 0; i <= rMax; i++)
        {
            //loops through the columns from the first index to the last (left to right)
            for (int j = 0; j <= cMax; j++)
            {
                //if cell is open space (0) add ...., and if the cell is a wall (1), add ==
                //thisll make the debug more easy to read instead of ones and zeros
                msg += maze[i, j] == 0 ? "...." : "==";
            }
            msg += "\n";
        }
        //displays string on the screen as a label
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    //method name self explanatory
    public int[,] GenerateMazeDataFromDimensions(int numRows, int numCols)
    {
        int[,] maze = new int[numRows, numCols];

        //threshold for random placement of walls (90 percent chance)
        float placementThreshold = 0.1f;

        for (var x = 0; x < numRows; x++)
        {
            for (var y = 0; y < numCols; y++)
            {
                if (x == 0 || y == 0 || x == numRows - 1 || y == numCols - 1)
                {
                    maze[x, y] = 1;
                }
                else if (x % 2 == 0 && y % 2 == 0)
                {
                    if (Random.value > placementThreshold)
                    {
                        maze[x, y] = 1;

                        int delta = Random.value > 0.5f ? -1 : 1;
                        int[] offset = new int[2];
                        int offsetIndex = Random.value > 0.5f ? 0 : 1;
                        offset[offsetIndex] = delta;

                        maze[x + offset[0], y + offset[1]] = 1;
                    }
                }
            }
        }
        return maze;
    }

    //generate a node graph to represent the walkable and non-walkable cells
    public void GenerateNodeGraph(int sizeRows, int sizeCols)
    {
        Graph = new Node[sizeRows, sizeCols];

        for (int i = 0; i < sizeRows; i++)
        {
            for (int j = 0; j < sizeCols; j++)
            {
                bool isWalkable = (Data[i, j] == 0);
                Graph[i, j] = new Node(i, j, isWalkable);
            }
        }

        //debug visual representation
        Debug.Log(GenerateGraphDebugString());
    }

    //generates a new maze using the generated data, also has node graph now
    public void GenerateNewMaze(int numRows, int numCols)
    {
        rows = numRows;
        cols = numCols;
        Data = GenerateMazeDataFromDimensions(rows, cols);
        GenerateNodeGraph(rows, cols);
    }

    //create a string version of the node graph for debugging
    private string GenerateGraphDebugString()
    {
        string graphString = "Node Graph:\n";

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                graphString += Graph[i, j].isWalkable ? "O " : "X ";
            }
            graphString += "\n";
        }
        return graphString;
    }
}
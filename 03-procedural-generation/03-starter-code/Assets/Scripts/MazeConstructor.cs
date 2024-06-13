using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    // a debug variable to display a "hud" of the maze
    public bool showDebug;
    // maze data
    private int[,] data;

    void Awake()
    {
        // generates a maze with the provided dimensions when the script starts and stores it
        data = GenerateMazeDataFromDimensions(30, 30);
    }

    // gui = graphical user interface, this entire function will create what i like to call "hud"
    void OnGUI()
    {
        // if showDebug is not ticked, nothing will happen
        if (!showDebug)
            return;

        // otherwise..
        // get the maze data to display
        int[,] maze = data;
        // get maximum index for the rows.
        int rMax = maze.GetUpperBound(0);
        // get maximum index for the columns.
        int cMax = maze.GetUpperBound(1);

        // an empty string to build the visual representation of the maze.
        string msg = "";

        // loop through the rows from the last index to the first (bottom to top) ?
        for (int i = rMax; i >= 0; i--)
        {
            // loop through the columns from the first index to the last (left to right) ?
            for (int j = 0; j <= cMax; j++)
            {
                // if the cell is 0 (is an open space), add "....", if the cell is 1 (is a wall), add "=="
                msg += maze[i, j] == 0 ? "...." : "==";
            }
            // add a new line at the end of each row to separate rows visually.
            msg += "\n";
        }

        // display the maze string on the screen as a label.
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    // this function generates the maze data based on the specified number of rows and columns.
    public int[,] GenerateMazeDataFromDimensions(int numRows, int numCols)
    {
        // create a 2D array to represent the maze with the given number of rows and columns.
        int[,] maze = new int[numRows, numCols];
        // define the probability threshold for placing walls (10% chance to place a wall).
        float placementThreshold = 0.1f;

        // loop through each row in the maze.
        for (var x = 0; x < numRows; x++)
        {
            // loop through each column in the maze.
            for (var y = 0; y < numCols; y++)
            {
                // if the current cell is on the edge of the maze (first or last row/column)...
                if (x == 0 || y == 0 || x == numRows - 1 || y == numCols - 1)
                {
                    // ...make it a wall (set the cell value to 1).
                    maze[x, y] = 1;
                }
                // if the current cell is at an even row and column (excluding edges)...
                else if (x % 2 == 0 && y % 2 == 0)
                {
                    // ...and if a random value is greater than the placement threshold...
                    if (Random.value > placementThreshold)
                    {
                        // ...make this cell a wall (set the cell value to 1).
                        maze[x, y] = 1;

                        // randomly choose to offset by -1 or 1 (to place an adjacent wall).
                        int delta = Random.value > 0.5f ? -1 : 1;
                        // create an array to store the offset values.
                        int[] offset = new int[2];
                        // randomly decide whether to apply the offset to the x or y coordinate.
                        int offsetIndex = Random.value > 0.5f ? 0 : 1;
                        // apply the delta value to the chosen coordinate in the offset array.
                        offset[offsetIndex] = delta;

                        // make the adjacent cell a wall by applying the offset.
                        maze[x + offset[0], y + offset[1]] = 1;
                    }
                }
            }
        }

        // return the generated maze data.
        return maze;
    }
}

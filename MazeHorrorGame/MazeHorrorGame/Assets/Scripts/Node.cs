/// <remarks>
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class represents a single node within the grid map. it will support the pathfinder script
/// </summary>

public class Node
{
    //coordinates of node
    public int x;
    public int y;

    //costs used in pathfinding
    public int gCost;
    public int hCost;
    public int fCost;

    //previous node reference
    public Node prevNode;

    //indicator if the node can be walked on
    public bool isWalkable;

    public Node(int x, int y, bool isWalkable)
    {
        this.x = x;
        this.y = y;
        hCost = 0;
        this.isWalkable = isWalkable;
    }

    //method to calculate fcost
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
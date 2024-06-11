using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Lets us reference this instance from any other script.
    public static GameManager Instance;

    // A number tracking how many sheep we've saved.
    // [HideInInspector] tells Unity not to show this variable in the editor, even though it is public.
    [HideInInspector]
    public int sheepSaved;

    // A number tracking how many sheep have been dropped.
    [HideInInspector]
    public int sheepDropped;

    // A number specifying how many sheep can be dropped before we lose the game.
    public int sheepDroppedBeforeGameOver;

    // A reference to the sheep manager.
    public SheepManager sheepSpawner;

    public void Awake()
    {
        Instance = this;
    }

    public void SaveSheep()
    {
        // TODO => Increase the number of sheep saved by 1.
    }

    private void GameOver()
    {
        // TODO => Destroy all the sheep on the map and stop the sheep manager from
        // spawning more sheep.
    }

    public void DroppedSheep()
    {
        // TODO => Record that we have dropped a sheep, 
        // then call GameOver() if we have dropped too many.
    }

}

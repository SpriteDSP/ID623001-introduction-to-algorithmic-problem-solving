using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterLevel
{
    public int cost; // How much it costs to go to this level (we will implement this later)
    public GameObject visualization; // A GameObject that is a child of the monster. There are 3 on the provided prefab.
}
public class MonsterData : MonoBehaviour
{
    public List<MonsterLevel> levels; // Start by defining 3 of these in the editor, 1 for each monster visualization.

    private MonsterLevel currentLevel; // We won't modify this directly, instead we'll use the CurrentLevel accessor.


    // Sets our level to the first level when this GameObject is enabled.
    public void OnEnable()
    {
        CurrentLevel = levels[0];
    }

    // Accessor for the monster's level:
    public MonsterLevel CurrentLevel
    {
        get { return currentLevel; }
        set
        {
            currentLevel = value;

            // ___EXPLANATION___
            // This setter runs whenever we set the monster's CurrentLevel (which we are going to do from the IncreaseLevel() function below.)
            // - We want it to automatically show the correct visualization for the monster based on its level.
            // - We can find that visualization in our "levels" list.
            // _________________

            // __PSEUDO_CODE:__
            // - Get a reference to the visualization that is associated with this MonsterLevel.
            // - Iterate over all of the visualizations for our different levels
            // {
            // - If this is the visualization associated with this MonsterLevel, its gameObject should be set to active.
            // - If this is NOT the visualization associated with this MonsterLevel, its gameObject should be set to INACTIVE.
            // }
            // ________________
        }

    }

    public MonsterLevel GetNextLevel()
    {
        // This function should return the monster level that is 1 higher than our current level (from the "levels" list).
        // If we are already at the highest level (i.e. currentLevel is set to the last level in the "levels" list),
        // then this function should return null.
    }

    public void IncreaseLevel()
    {
        // This function should set CurrentLevel to the next level in our "levels" list, if we're not already at max level.
    }
}

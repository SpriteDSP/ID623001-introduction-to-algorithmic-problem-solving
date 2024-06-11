using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
//    public GameObject sheepPrefab;
//    public Transform[] sheepSpawnPoints;
//    public float spawnInterval = 2f;
//
//    void Start()
//    {
//        StartCoroutine(SpawnSheep());
//    }
//
//    IEnumerator SpawnSheep()
//    {
//        // I left this one part out and it resulted in only spawning the sheep once, this is important to loop the spawning
//        while (true)
//        {
//            // Instead of one spawnpoint I created 3, one for each bridge
//            int randomIndex = Random.Range(0, sheepSpawnPoints.Length);
//            Transform selectedSpawnPoint = sheepSpawnPoints[randomIndex];
//
//            // sheepSpawnPoint.rotation is important to get the right orientation, previously it was Quaternion.identity which by definition is just default rotation, I had to swap it to this to get it correct
//            Instantiate(sheepPrefab, selectedSpawnPoint.position, selectedSpawnPoint.rotation);
//
//            yield return new WaitForSeconds(spawnInterval);
//        }
//        
//    }
// Above is the old version before adding lists and events

    public Sheep sheepPrefab;
    public Transform[] sheepSpawnPoints;
    public float spawnInterval = 2f;

    private List<Sheep> sheepList = new List<Sheep>();

    //private voids make it so that other classes or scripts can call upon this 
    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void SpawnSheep()
    {
        int randomIndex = Random.Range(0, sheepSpawnPoints.Length);
        Transform selectedSpawnPoint = sheepSpawnPoints[randomIndex];

        Sheep sheep = Instantiate(sheepPrefab, selectedSpawnPoint.position, selectedSpawnPoint.rotation);

        sheep.OnAteHay.AddListener(HandleSheepAteHay);
        sheep.OnDropped.AddListener(HandleSheepDropped);
        sheepList.Add(sheep);
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnSheep();

            yield return new WaitForSeconds(spawnInterval);
        }
        
    }

    private void HandleSheepAteHay(Sheep sheep)
    {
        sheepList.Remove(sheep);
    }

    private void HandleSheepDropped(Sheep sheep)
    {
        sheepList.Remove(sheep);
    }
}

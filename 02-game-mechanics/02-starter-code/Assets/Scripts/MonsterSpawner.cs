using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject MonsterPrefab;
    private GameObject placedMonster;

    //private Collider2D myCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        //myCollider2D = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        if (placedMonster == null)
        {
            placedMonster = Instantiate(MonsterPrefab, transform.position, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRoom : MonoBehaviour
{

    public Enemy[] enemies;
    public GameObject[] door;
    public Transform[] doorEndPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EndRoom();
    }


    private bool enemiesDead()
    {
        bool allDead = true;
        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null && enemies[i].AmIDead() == false)
            {
                allDead = false;
            }
        }

        return allDead;
    }

    private void EndRoom()
    {
        if (enemiesDead())
        {
            OpenDoors();
            Debug.Log("All Dead");
        }
    }

    private void OpenDoors()
    {
        for(int i =0; i< door.Length; i++)
        {
            door[i].GetComponent<BoxCollider2D>().enabled = false;
            if(door[i].transform != doorEndPos[i].transform)
            {
                door[i].transform.position = doorEndPos[i].transform.position;
            }
        }


    }
}

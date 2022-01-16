using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SceneType
{
    Easy, Medium, Hard, Upgrade, Victory, Count
};
public class ManageRoom : MonoBehaviour
{

    public HealthSystem[] enemies;
    public GameObject[] door;
    public Transform[] doorEndPos;
    public Transform[] playerSpawnPos;
    public GameObject player;
    public Player_Controller playerController;
    public SceneType sceneType;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Controller>().gameObject;
        playerController = FindObjectOfType<Player_Controller>();
        if(playerController.lastRoomExit == RoomPos.RIGHT) //Spawn on left
            player.transform.position = playerSpawnPos[0].position;
        else //Spawn bottom
            player.transform.position = playerSpawnPos[1].position;

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
            if(enemies[i] != null && enemies[i].currentHealth > 0)
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
                
                //door[i].transform.position = new Vector3(Mathf.Lerp(door[i].transform.position.x, doorEndPos[i].transform.position.x, 200), Mathf.Lerp(door[i].transform.position.y, doorEndPos[i].transform.position.y, 200), Mathf.Lerp(door[i].transform.position.z, doorEndPos[i].transform.position.z, 200));
                
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string NextScene;
    public GameObject player;
    //public GameObject allNextScenes;

    private ManageRoom manageRoom;
    private RoomSystem roomSys;
    public SceneType actualSceneType;
    private SceneType nextSceneType;

    private int randomRoomType;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerLifeManagement>().gameObject;
        manageRoom = FindObjectOfType<ManageRoom>();
        actualSceneType = manageRoom.sceneType;
        roomSys = FindObjectOfType<RoomSystem>();
        SetNextRoomType();
        SetNextRoom();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Scene followingScene = SceneManager.GetSceneByName(NextScene);
            roomSys.RemoveRoom(actualSceneType);
            SceneManager.LoadScene(NextScene, LoadSceneMode.Additive);
        }
    }

    IEnumerator LoadNxtScene()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private void SetNextRoomType()
    {
        bool roomRemains;
        do //Puede reventar si no quedan salas en ningún pool
        {
            roomRemains = true;
            randomRoomType = UnityEngine.Random.Range(0, (int)SceneType.Count);
            if (roomSys.RoomsRemaining((SceneType)randomRoomType))
                nextSceneType = (SceneType)randomRoomType;
            else
                roomRemains = false;

        } while (!roomRemains);
        
    }

    private void SetNextRoom()
    {
        
        if(nextSceneType == SceneType.Easy)
        {
            int roomNum = UnityEngine.Random.Range(0, roomSys.EasyScenes.Count);
            NextScene = roomSys.EasyScenes[roomNum];
        }
        else if (nextSceneType == SceneType.Medium)
        {
            int roomNum = UnityEngine.Random.Range(0, roomSys.MediumScenes.Count);
            NextScene = roomSys.MediumScenes[roomNum];
        }
        else if (nextSceneType == SceneType.Hard)
        {
            int roomNum = UnityEngine.Random.Range(0, roomSys.HardScenes.Count);
            NextScene = roomSys.HardScenes[roomNum];
        }

    }
}

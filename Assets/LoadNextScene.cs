using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum  RoomPos { TOP, RIGHT};
public class LoadNextScene : MonoBehaviour
{
    public string NextScene;
    public GameObject player;
    public Player_Controller playerController;
    public Animator animator;
    private float transitionTime = 0.5f;

    //public GameObject allNextScenes;

    private ManageRoom manageRoom;
    private RoomSystem roomSys;
    public SceneType actualSceneType;
    public RoomPos roomPosition;
    private SceneType nextSceneType;

    private int randomRoomType;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerLifeManagement>().gameObject;
        playerController = FindObjectOfType<Player_Controller>();
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
            playerController.lastRoomExit = roomPosition;
            StartCoroutine(LoadNxtScene(animator, transitionTime));
        }
    }

    IEnumerator LoadNxtScene(Animator transition, float transitionTime)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        roomSys.RemoveRoom(actualSceneType);
        SceneManager.LoadScene(NextScene, LoadSceneMode.Additive);
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

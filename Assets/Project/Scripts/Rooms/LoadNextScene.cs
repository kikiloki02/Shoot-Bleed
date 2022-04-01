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
    public SceneType nextSceneType;

    private int randomRoomType;

    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<PlayerLifeManagement>().gameObject;
        playerController = FindObjectOfType<Player_Controller>();
        manageRoom = FindObjectOfType<ManageRoom>();
        actualSceneType = manageRoom.sceneType;
        roomSys = FindObjectOfType<RoomSystem>();
        //Remove actual scene from the list
        RemoveActualRoom();
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
        if(nextSceneType == SceneType.Victory)
            SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
        else
        {
            if (actualSceneType != SceneType.Upgrade)
            {
                roomSys.UnloadRoom();
                roomSys.fightingRoomsCompleted++;
            }
            SceneManager.LoadScene(NextScene, LoadSceneMode.Additive);
        }
        
    }

    private void SetNextRoomType()
    {
        if (roomSys.fightingRoomsCompleted == 10 ||(!roomSys.RoomsRemaining(SceneType.Easy) && !roomSys.RoomsRemaining(SceneType.Medium) && !roomSys.RoomsRemaining(SceneType.Hard)))
        {
            nextSceneType = SceneType.Victory;
            return;
        }
        bool roomRemains;
        do //Puede reventar si no quedan salas en ning�n pool
        {
            roomRemains = true;
            roomSys.upgradeRoomProbability = 20 * (roomSys.fightingRoomsCompleted - roomSys.lastUpgradeRoom);
            int random = UnityEngine.Random.Range(0, 100);
            float roomsProbability = (100 - roomSys.upgradeRoomProbability) / 3;

            if(random <= roomsProbability) { randomRoomType = (int)SceneType.Easy; }
            else if(random <= roomsProbability * 2) { randomRoomType = (int)SceneType.Medium; }
            else if(random <= roomsProbability * 3) { randomRoomType = (int)SceneType.Hard; }
            else { randomRoomType = (int)SceneType.Upgrade; }
            /*
            if((roomSys.fightingRoomsCompleted) % roomSys.numberOfRoomsForUpgrade == 0 && (roomSys.lastUpgradeRoom != roomSys.fightingRoomsCompleted && roomSys.lastUpgradeRoom != roomSys.fightingRoomsCompleted - 1))
            {
                randomRoomType = (int)SceneType.Upgrade;
            }
            else if((roomSys.lastUpgradeRoom != roomSys.fightingRoomsCompleted && roomSys.lastUpgradeRoom != roomSys.fightingRoomsCompleted - 1))
            {
                randomRoomType = UnityEngine.Random.Range(0, (int)SceneType.Count - 1);
            }
            else
            {
                randomRoomType = UnityEngine.Random.Range(0, (int)SceneType.Count - 2);
            }*/

            if (roomSys.RoomsRemaining((SceneType)randomRoomType))
                nextSceneType = (SceneType)randomRoomType;
            else
                roomRemains = false;

        } while (!roomRemains);

        if(randomRoomType == (int)SceneType.Upgrade) 
        { 
            roomSys.lastUpgradeRoom = roomSys.fightingRoomsCompleted;
            roomSys.upgradeRoomProbability = 0;
        }
        
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
        else if(nextSceneType == SceneType.Upgrade)
        {
            int roomNum = UnityEngine.Random.Range(0, roomSys.UpgradeScenes.Count);
            NextScene = roomSys.UpgradeScenes[roomNum];
        }
        else if(nextSceneType == SceneType.Victory)
        {
            int roomNum = UnityEngine.Random.Range(0, roomSys.VictoryScenes.Count);
            NextScene = roomSys.VictoryScenes[roomNum];
        }

    }

    private void RemoveActualRoom()
    {
        if (manageRoom.roomRemoved == false && actualSceneType != SceneType.Upgrade)
        {
            roomSys.RemoveRoom(actualSceneType);
            manageRoom.roomRemoved = true;
        }
    }
}

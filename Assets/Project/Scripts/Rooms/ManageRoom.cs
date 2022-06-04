using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SceneType
{
    Easy, Medium, Hard, Upgrade, Victory, Count
};
public class ManageRoom : MonoBehaviour
{

    //public HealthSystem[] enemies;
    public int totalEnemies;
    public Animator[] doorsAnim;
    public Transform[] playerSpawnPos;
    public GameObject player;
    public Player_Controller playerController;
    public SceneType sceneType;
    public bool roomRemoved;

    private bool deadOnce = false;
    private NewAudioManager newAudioManager;
    // Start is called before the first frame update
    void Awake()
    {
        roomRemoved = false;
        player = FindObjectOfType<Player_Controller>().gameObject;
        playerController = FindObjectOfType<Player_Controller>();
        if(playerController.lastRoomExit == RoomPos.RIGHT) //Spawn on left
            player.transform.position = playerSpawnPos[0].position;
        else //Spawn bottom
            player.transform.position = playerSpawnPos[1].position;

    }

    private void Start()
    {
        newAudioManager = FindObjectOfType<NewAudioManager>();

        if (sceneType == SceneType.Upgrade)
        {
            newAudioManager.FadeInMusic("ExtraMusicVolume");
        }
    }

    // Update is called once per frame
    void Update()
    {
        EndRoom();
    }


    private bool enemiesDead()
    {
        return totalEnemies <= 0;
    }

    private void EndRoom()
    {
        if (enemiesDead() && !deadOnce)
        {
            deadOnce = !deadOnce;
            StartCoroutine(PlayDoorEffecr());
            OpenDoors();
        }
    }

    private void OpenDoors()
    {
        //Play sounds
        if (sceneType != SceneType.Upgrade)
        {
            newAudioManager.FadeOutMusic("MusicVolume");
        }
        for (int i =0; i< doorsAnim.Length; i++)
        {
            doorsAnim[i].SetTrigger("Open"); //Activate Animation
        }
    }

    IEnumerator PlayDoorEffecr()
    {
        yield return new WaitForSeconds(0.25f);
        newAudioManager.Play("DoorEffect");
    }
}

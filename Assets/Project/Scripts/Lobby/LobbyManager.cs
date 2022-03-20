using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LobbyRoom
{
    PracticeRoom,Store
};

public class LobbyManager : MonoBehaviour
{

    public GameObject[] door;
    public Transform[] doorEndPos;
    public GameObject player;
    public SceneType sceneType;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Controller>().gameObject;
        player.transform.position = new Vector3(0f, -2.5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Room_Caller : MonoBehaviour
{
    private GameObject _upgrade_room_manager;

    // Start is called before the first frame update
    void Start()
    {
        _upgrade_room_manager = FindObjectOfType<Upgrade_Room_Manager>().gameObject;

        _upgrade_room_manager.GetComponent<Upgrade_Room_Manager>().StartUpgradeRoom();
    }
}

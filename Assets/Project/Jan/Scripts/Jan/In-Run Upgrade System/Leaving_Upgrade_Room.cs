using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaving_Upgrade_Room : MonoBehaviour
{
    private GameObject _upgrade_room_manager;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") { return; }

        _upgrade_room_manager = FindObjectOfType<Upgrade_Room_Manager>().gameObject;

        _upgrade_room_manager.GetComponent<Upgrade_Room_Manager>().RemoveFromUpgradeList();

        _upgrade_room_manager.GetComponent<Upgrade_Room_Manager>().Clean();
    }
}

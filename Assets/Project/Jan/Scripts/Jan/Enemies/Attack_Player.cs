using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : MonoBehaviour
{
    public GameObject _enemy;
    public GameObject _player;

    // private bool _canDealDamage = true;

    // ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start()
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") { _player.GetComponent<PlayerLifeManagement>().GetDamage(_enemy.GetComponent<Enemy>()._attackValue); }
    }

}

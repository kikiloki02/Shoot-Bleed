using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : MonoBehaviour
{
    public GameObject _enemy;
    public GameObject _player;

    private bool _canDealDamage = true;

    // ------ START / UPDATE / FIXEDUPDATE: ------

    private void Start()
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;

        // _enemy = FindObjectOfType<Enemy>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") { _player.GetComponent<PlayerLifeManagement>().GetDamage(_enemy.GetComponent<Enemy>()._attackValue); }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" && _canDealDamage) { StartCoroutine(CallGetDamage(_enemy.GetComponent<Enemy>()._attackValue)); }
    //}

    // ------ METHODS: ------

    /*void CallGetDamage(int damage)
    {
        Debug.Log("Player was Hit");

        _player.GetComponent<PlayerLifeManagement>().GetDamage(damage);
    }*/

    //IEnumerator CallGetDamage(int damage)
    //{
    //    Debug.Log("Player was Hit");

    //    _canDealDamage = false;

    //    _player.GetComponent<Player_Controller>().GetHit(damage);

    //    // float seconds = _player.GetComponent<Player_Controller>()._invincibilityTimeBetweenHitsInSeconds;
    //    yield return new WaitForSeconds(1f); // Wait

    //    _canDealDamage = true;
    //}
}

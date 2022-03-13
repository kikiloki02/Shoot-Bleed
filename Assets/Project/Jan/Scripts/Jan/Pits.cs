using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pits : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Controller>().Fall();
        }
    }
}

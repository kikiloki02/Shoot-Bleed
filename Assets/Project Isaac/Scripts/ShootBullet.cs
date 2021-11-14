using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    public GameObject bullet;
    public GameObject blueBullet;
    public GameObject player;
    public ParticleSystem shootParticles;

    public float knockback = 4.5f;

    public Transform spawnPoint;
    Vector3 directionToMouse;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        directionToMouse = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)) - this.transform.position).normalized;
        this.transform.right = new Vector2(directionToMouse.x, directionToMouse.y);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           player.GetComponent<PlayerLifeManagement>().GetDamage(1);
        }
    }

    void Shoot()
    {
        GameObject newBullet;

        if (player.GetComponent<PlayerLifeManagement>().criticalState)
        {
            newBullet = Instantiate(blueBullet, spawnPoint.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, directionToMouse)));
        }
        else
        {
            newBullet = Instantiate(bullet, spawnPoint.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, directionToMouse)));
            player.GetComponent<PlayerLifeManagement>().LoseLife();
        }

        //Little Knockback to the player
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.AddForce( ((directionToMouse.normalized) * knockback) * -0.1f , ForceMode2D.Force);
        //CameraShake
        CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
        shootParticles.Play();
    }

}
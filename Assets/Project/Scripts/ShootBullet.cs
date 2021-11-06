using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    public GameObject bullet;
    public GameObject blueBullet;

    public float startVelocity = 20f;

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
    }

    void Shoot()
    {


        /*
        //Si critical state
        GameObject newBlueBullet = Instantiate(blueBullet, transform.position, Quaternion.identity);
        
        newBlueBullet.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
        newBlueBullet.GetComponent<Bullet>().BulletForce(force);*/

        GameObject newBullet = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().AddForce(directionToMouse * startVelocity, ForceMode2D.Force);
    }

}
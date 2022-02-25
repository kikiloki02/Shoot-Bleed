using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bullet;
    public GameObject blueBullet;
    public GameObject player;
    public ParticleSystem shootParticles;

    public SpriteRenderer _renderer;
    public Sprite _normalSprite;
    public Sprite _criticalSprite;

    public float cameraShake;
    public float timeToShake;

    public float knockback;

    public Transform spawnPoint;
    Vector3 directionToMouse;

    public AudioSource _shoot1;
    public AudioSource _shoot2;
    private bool canShoot = true;

    public float secondsPerBullet;

    // Start is called before the first frame update
    void Start()
    {
        secondsPerBullet = player.GetComponent<Player_Controller>()._secondsPerBullet;
        bullet.GetComponent<Bullet>().damage = player.GetComponent<Player_Controller>()._bloodBulletDamage;
        blueBullet.GetComponent<Bullet>().damage = player.GetComponent<Player_Controller>()._normalBulletDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0.0f)
            return;

        directionToMouse = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)) - this.transform.position).normalized;
        this.transform.right = new Vector2(directionToMouse.x, directionToMouse.y);

        if (this.transform.right.x < 0) { _renderer.flipY = true; }
        else { _renderer.flipY = false; }

        if (player.GetComponent<PlayerLifeManagement>().criticalState == false) { _renderer.sprite = _normalSprite; }
        else { _renderer.sprite = _criticalSprite; }

        if (Input.GetKey(KeyCode.Mouse0) && canShoot)
        {
            StartCoroutine(AvailableShoot(secondsPerBullet));
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
           player.GetComponent<PlayerLifeManagement>().RecoverHealth(5);
        }
    }

    void Shoot()
    {
        GameObject newBullet;

        if (player.GetComponent<PlayerLifeManagement>().criticalState)
        {
            _shoot2.Play(); // Bullet 2 SFX

            newBullet = Instantiate(blueBullet, spawnPoint.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, directionToMouse)));
        }
        else
        {
            _shoot1.Play(); // Bullet 1 SFX

            newBullet = Instantiate(bullet, spawnPoint.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, directionToMouse)));
            player.GetComponent<PlayerLifeManagement>().LoseLife();
        }

        //Little Knockback to the player
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.AddForce(((directionToMouse.normalized) * knockback) * -0.1f , ForceMode2D.Force);
        //CameraShake
        CinemachineShake.Instance.ShakeCamera(cameraShake, timeToShake);
        shootParticles.Play();
    }

    IEnumerator AvailableShoot(float seconds)
    {
        canShoot = false;
        yield return new WaitForSeconds(seconds);
        canShoot = true;
    }
}
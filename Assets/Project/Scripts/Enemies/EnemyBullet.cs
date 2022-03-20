using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;
    public float OutVelocity;
    public Collider2D _collider2D;
    public GameObject _smallBullet;
    public float explodeTime;
    private bool collision = false;
    public TrailParticles trailParticles;

    // Start is called before the first frame update
    void Start()
    {
        if (_smallBullet != null) { StartCoroutine(Timer(explodeTime)); }

        rb.AddForce(this.transform.right * OutVelocity, ForceMode2D.Force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Restar vida al player
            other.gameObject.GetComponent<HealthSystem>().GetDamage(damage);

            //Destriur bala
            trailParticles.DetachAndDestroy();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Bullet"))
        {
            //Destruir bala
            trailParticles.DetachAndDestroy();
            Destroy(this.gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (collision)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //Restar vida al player
                other.gameObject.GetComponent<HealthSystem>().GetDamage(damage);

                //Destriur bala
                trailParticles.DetachAndDestroy();
                Destroy(this.gameObject);
            }
            else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Bullet"))
            {
                //Destruir bala
                trailParticles.DetachAndDestroy();
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        trailParticles.DetachAndDestroy();

        GameObject newBullet1;
        GameObject newBullet2;
        GameObject newBullet3;
        GameObject newBullet4;

        newBullet1 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, Vector2.up)));
        newBullet2 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, Vector2.down)));
        newBullet3 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, Vector2.left)));
        newBullet4 = Instantiate(_smallBullet, this.transform.position, Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, Vector2.right)));

        Destroy(this.gameObject);
    }
}

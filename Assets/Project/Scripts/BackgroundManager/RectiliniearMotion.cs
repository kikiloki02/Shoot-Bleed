using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectiliniearMotion : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] Vector2 originalSpeed;

    Vector2 speed;

    private void Start()
    {
        speed = originalSpeed;
    }

    void Update()
    {
        Vector2 movement = direction * speed * Time.deltaTime;
        gameObject.transform.position += new Vector3(movement.x, movement.y, 0.0f);
    }

    public void SpeedUp(Vector2 newSpeed, float time)
    {
        StopAllCoroutines();
        StartCoroutine(SetSpeed(newSpeed, time));
    }

    IEnumerator SetSpeed(Vector2 newSpeed, float time)
    {
        speed = newSpeed;

        yield return new WaitForSeconds(time);

        speed = originalSpeed;
    }
}

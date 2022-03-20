using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject _gameObjectToRotateAround;

    public float _rotationRate;
    private float currentAngle;
    public float distance;

    private void Start()
    {
        transform.position = _gameObjectToRotateAround.transform.position + Vector3.right * distance;
    }

    void Update()
    {
        if (_gameObjectToRotateAround == null)
            Destroy(this.gameObject);

        currentAngle += _rotationRate * Time.deltaTime;
        transform.position = _gameObjectToRotateAround.transform.position + new Vector3(Mathf.Sin(currentAngle * Mathf.Deg2Rad), Mathf.Cos(currentAngle * Mathf.Deg2Rad)) * distance;
        transform.right = (transform.position - _gameObjectToRotateAround.transform.position).normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}

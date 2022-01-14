using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(target.position.x, target.position.y, 0.0f), 30f * Time.deltaTime);
    }
}

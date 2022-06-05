using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgorundScroller : MonoBehaviour
{
    float toLeftSlowStartingY = 197;
    float toRightSlowStartingY = -197;

    float toLeftFastStartingY = 207;
    float toRightFastStartingY = -187;

    [Header("To Left Slow")]
    [SerializeField] Transform toLeftSlow1;
    [SerializeField] Transform toLeftSlow2;

    [Header("To Left Fast")]
    [SerializeField] Transform toLeftFast1;
    [SerializeField] Transform toLeftFast2;

    void Update()
    {
        if (toLeftSlow1.localPosition.x < toRightSlowStartingY)
        {
            toLeftSlow1.localPosition = new Vector3(toLeftSlowStartingY, toLeftSlow1.localPosition.y, toLeftSlow1.localPosition.z);
        }
        if (toLeftSlow2.localPosition.x < toRightSlowStartingY)
        {
            toLeftSlow2.localPosition = new Vector3(toLeftSlowStartingY, toLeftSlow1.localPosition.y, toLeftSlow2.localPosition.z);
        }

        if (toLeftFast1.localPosition.x < toRightFastStartingY)
        {
            toLeftFast1.localPosition = new Vector3(toLeftFastStartingY, toLeftFast1.localPosition.y, toLeftFast1.localPosition.z);
        }
        if (toLeftFast2.localPosition.x < toRightFastStartingY)
        {
            toLeftFast2.localPosition = new Vector3(toLeftFastStartingY, toLeftFast1.localPosition.y, toLeftFast2.localPosition.z);
        }
    }

    public void SpeedUpFor(Vector2 speed, float time)
    {
        toLeftSlow1.gameObject.GetComponent<RectiliniearMotion>().SpeedUp(speed, time);
        toLeftSlow2.gameObject.GetComponent<RectiliniearMotion>().SpeedUp(speed, time);

        toLeftFast1.gameObject.GetComponent<RectiliniearMotion>().SpeedUp(speed, time);
        toLeftFast2.gameObject.GetComponent<RectiliniearMotion>().SpeedUp(speed, time);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public void CanvasSetActive(GameObject component)
    {
        component.SetActive(true);
    }
    public void CanvasSetInactive(GameObject component)
    {
        component.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMesh text;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        //text.text = "20/20";
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

}

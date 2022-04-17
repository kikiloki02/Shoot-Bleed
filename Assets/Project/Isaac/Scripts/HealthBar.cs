using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public RectTransform bar;
    public TextMesh text;
    float maxHealth;

    public void SetMaxHealth(int health)
    {
        maxHealth = (float)health;
    }

    public void SetHealth(int health)
    {
        bar.offsetMin = new Vector2(4, 11);
        bar.offsetMax = new Vector2(-4, -Mathf.Lerp(46.5f, 11f, (float)health / maxHealth));
    }
}

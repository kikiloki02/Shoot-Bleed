using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashCooldown : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;

    [SerializeField]
    private TMP_Text textCooldown;

    

    private bool isCooldown = false;
    private float cooldownTime;
    private float cooldownTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        cooldownTime = FindObjectOfType<Player_Controller>()._dashCooldown;
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSpell();
        }

        if (isCooldown)
        {
            ApplyCooldown();
        }
        
    }

    void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer< 0.0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void UseSpell()
    {
        if (isCooldown)
        {
            //user has clicked the spell while not disponible
        }
        else
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            cooldownTimer = cooldownTime;
        }
    }
}

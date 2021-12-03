using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManagement : HealthSystem
{
    public GameObject _player;

    public bool criticalState = false;
    public HealthBar healthBar;

    private float currentTime = 0.0f;
    private float _hitEffectDuration = 0.075f;

    public AudioSource _hit;

    private SpriteRenderer _renderer;

    private Color32 _spriteRedColor;
    private Color32 _spriteWhiteColor;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        _renderer = _player.GetComponent<SpriteRenderer>();

        _spriteRedColor = new Color32(255, 100, 100, 255);
        _spriteWhiteColor = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        // Slow effect:
        currentTime = Mathf.Min(0.85f, currentTime + Time.unscaledDeltaTime);
        // Time.timeScale = Mathf.Sin((currentTime / 0.5f) * Mathf.PI * 0.5f);
        Time.timeScale = Mathf.Pow((currentTime / 0.85f), 1f);

        criticalState = isCritical();
    }

    public void LoseLife()
    {
        currentHealth -= 1;
        healthBar.SetHealth(currentHealth);
    }

    bool isCritical()
    {
        return (currentHealth == 0);
    }

    public override void GetDamage(int damage)
    {
        currentTime = 0f;

        if (criticalState) { Destroy(this.gameObject); }
        else 
        {
            StartCoroutine(GetHitEffect());

            currentHealth -= damage;
            if (currentHealth < 0)
                currentHealth = 0;
            healthBar.SetHealth(currentHealth);
            CinemachineShake.Instance.ShakeCamera(5f, 0.15f);
        }
    }

    public void RecoverHealth(int addHp)
    {
        currentHealth += addHp;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator GetHitEffect()
    {
        _hit.Play(); // Hit SFX

        _renderer.color = _spriteRedColor;

        yield return new WaitForSeconds(_hitEffectDuration); // Wait

        _renderer.color = _spriteWhiteColor;
    }
}

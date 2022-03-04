using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerLifeManagement : HealthSystem
{
    public GameObject _player;

    public bool criticalState = false;

    private bool _canGetHit = true;

    public bool loselife;

    public HealthBar healthBar;

    private float currentTime = 0.0f;
    private float _hitEffectDuration = 0.075f;

    public AudioSource _hit;
    public AudioSource _heal;
    public AudioSource _criticalStateSound;

    public GameObject _criticalStateScreenEffect;

    private bool _available = true;

    private SpriteRenderer _renderer;

    private Color32 _spriteRedColor;
    private Color32 _spriteWhiteColor;

    private float _invencibilityTimeInSeconds;

    public UnityEvent OnDeath;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        _renderer = _player.GetComponent<SpriteRenderer>();

        _spriteRedColor = new Color32(255, 100, 100, 255);
        _spriteWhiteColor = new Color32(255, 255, 255, 255);

        _invencibilityTimeInSeconds = _player.GetComponent<Player_Controller>()._invincibilityTimeBetweenHitsInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        // Slow effect:
        currentTime = Mathf.Min(0.85f, currentTime + Time.unscaledDeltaTime);
        // Time.timeScale = Mathf.Sin((currentTime / 0.5f) * Mathf.PI * 0.5f);
        Time.timeScale = Mathf.Pow((currentTime / 0.85f), 0.75f);

        criticalState = isCritical();

        if (criticalState && !_criticalStateSound.isPlaying)
        {
            _criticalStateSound.Play();
        }
        else if (!criticalState || !_criticalStateSound.isPlaying)
        {
            _criticalStateSound.Stop();
        }

        if (criticalState && _available)
        {
            StartCoroutine(UICriticalStateEffect(1f));
        }
    }

    public void LoseLife()
    {
        if (loselife)
        {
            currentHealth -= 1;
            healthBar.SetHealth(currentHealth);
        }
       
    }

    bool isCritical()
    {
        return (currentHealth == 0);
    }

    public override void GetDamage(int damage)
    {
        if (!_canGetHit)
        {
            return;
        }

        GetComponent<Animator>().SetTrigger("Hit");

        // Start invencibility coroutine:
        StartCoroutine(InvencibilityTime(_invencibilityTimeInSeconds));

        currentTime = 0f;

        if (criticalState) {
            OnDeath.Invoke();
            Destroy(this.gameObject); }
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
        _heal.Play();

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

    public IEnumerator UICriticalStateEffect(float seconds)
    {
        _available = false;

        _criticalStateScreenEffect.GetComponent<Image>().DOColor(new Color32(255, 50, 50, 125), seconds);

        yield return new WaitForSeconds(seconds);

        _criticalStateScreenEffect.GetComponent<Image>().DOColor(new Color32(255, 50, 50, 0), seconds);

        yield return new WaitForSeconds(seconds);

        _available = true;
    }

    private IEnumerator InvencibilityTime(float seconds)
    {
        _canGetHit = false;

        yield return new WaitForSeconds(seconds);

        _canGetHit = true;
    }
}

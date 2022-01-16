using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _enemy;

    public AudioSource _spawningSound;

    public ParticleSystem _spawningParticles;

    public float _spawningTime;
    public float _timeOffset;

    public bool _isTrigger;

    private bool _activate;
    private bool _hasOffset;

    // Start is called before the first frame update
    void Start()
    {
        if (_timeOffset != 0) { _hasOffset = true; }

        if (_isTrigger) { _activate = false; }
        else { _activate = true; }
    }

    void Update()
    {
        if (_activate) 
        {
            _activate = false;

            if (_hasOffset)
            {
                StartCoroutine(WaitThenAct(_timeOffset));
            }
            else
            {
                StartCoroutine(Spawning(_spawningTime));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTrigger)
        {
            if (collision.gameObject.tag == "Player")
            {
                _activate = true;
            }
        }
    }

    IEnumerator Spawning(float seconds)
    {
        _spawningSound.Play();
        _spawningParticles.Play();

        yield return new WaitForSeconds(seconds);

        _spawningParticles.Stop();

        GameObject enemy = Instantiate(_enemy, this.transform.position, Quaternion.Euler(0, 0, 0));

        Destroy(this.gameObject);
    }

    IEnumerator WaitThenAct(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        StartCoroutine(Spawning(_spawningTime));

        _hasOffset = false;
    }
}

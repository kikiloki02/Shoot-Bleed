using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailParticles : MonoBehaviour
{
    public ParticleSystem explosionParticles;
    // Start is called before the first frame update
    public void DetachAndDestroy()
    {
        this.transform.parent = null;
        explosionParticles.transform.parent = null;
        explosionParticles.Play();
        Invoke("Destroy", 1f);
    }

    void Destroy()
    {
        Destroy(explosionParticles.gameObject);
        Destroy(this.gameObject);
    }
}

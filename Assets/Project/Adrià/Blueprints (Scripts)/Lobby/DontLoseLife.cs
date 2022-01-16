using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontLoseLife : MonoBehaviour
{
    // Start is called before the first frame update
    public bool loseLife;

    void Start()
    {
        FindObjectOfType<PlayerLifeManagement>().loselife = loseLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

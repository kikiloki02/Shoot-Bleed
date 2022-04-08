using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public int price;
    protected int idx;
    protected bool isActive;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Activate() 
    {
        isActive = true;
    }
    
    public virtual void Disactivate()
    {
        isActive = false;
    }
   
}

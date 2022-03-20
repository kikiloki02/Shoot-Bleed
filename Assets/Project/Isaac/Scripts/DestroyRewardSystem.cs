using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRewardSystem : MonoBehaviour
{
    private RewardSystem rwdSys;
    // Start is called before the first frame update
    void Start()
    {
        rwdSys = FindObjectOfType<RewardSystem>();
        if(rwdSys != null) { Destroy(rwdSys.gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

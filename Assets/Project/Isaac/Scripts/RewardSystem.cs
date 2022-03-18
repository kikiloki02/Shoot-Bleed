using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : MonoBehaviour
{

    public int maxCombo;
    public int actualCombo;
    public int enemiesKilled;
    public int stageReached;
    public int hitsTaken;

    public float accuracy; //Bullets hit / Bullets shot
    public int bulletsHit;
    public int bulletsShot;

    private CanvasText canvasTxt;

    // Start is called before the first frame update
    void Start()
    {
        canvasTxt = FindObjectOfType<CanvasText>();
        actualCombo = maxCombo = 0;
        hitsTaken = 0;
        stageReached = 0;
        enemiesKilled = 0;
        accuracy = 100;
        bulletsHit = bulletsShot = 0;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CalculateAccuracy());
    }

    public void StopCombo()
    {
        hitsTaken++;
        actualCombo = 0;
        canvasTxt.SetComboTxt(actualCombo);
    }

    public void AddCombo()
    {
        actualCombo++;
        canvasTxt.SetComboTxt(actualCombo);
        if (actualCombo > maxCombo)
        {
            maxCombo = actualCombo;
            canvasTxt.SetMaxComboTxt(maxCombo);
        }
    }


    public int CalculateReward()
    {
        return (int)((enemiesKilled + (((float)stageReached * 1.5) - hitsTaken)) * (float)(maxCombo / 10.0f));
    }

    public int CalculateReward2()
    {
        return (int)((enemiesKilled/2) + (stageReached - (float)(hitsTaken*1.5)) * maxCombo / 10);
    }

    IEnumerator CalculateAccuracy()
    {
        while (true)
        {


            yield return new WaitForSeconds(1);

            if (bulletsShot != 0)
            {
                accuracy = ((float)bulletsHit / (float)bulletsShot) * 100;
            }
        }
    }
}

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
    public int bloodGems;

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
        StartCoroutine(SetBloodGems());

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("u"))
        {
            bloodGems += 50;
            canvasTxt.SetBloodGemsTxt(bloodGems);
            PlayerPrefs.SetInt("BloodGem", bloodGems);
        }
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


            yield return new WaitForSeconds(0.2f);

            if (bulletsShot != 0)
            {
                accuracy = ((float)bulletsHit / (float)bulletsShot) * 100;
            }
        }
    }

    IEnumerator SetBloodGems()
    {
        yield return new WaitForSeconds(1);

        bloodGems = PlayerPrefs.GetInt("BloodGem", 0);
        canvasTxt.SetBloodGemsTxt(bloodGems);
    }
}

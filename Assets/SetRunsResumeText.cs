using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetRunsResumeText : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    public TextMeshProUGUI[] titles;
    private int[] textValues;
    RewardSystem rwdSys;
    public CinemachineShake shake;

    // Start is called before the first frame update
    void Start()
    {
        textValues = new int[6];
        rwdSys = FindObjectOfType<RewardSystem>();
        SetStringTexts();
        for (int i = 0; i < texts.Length; i++)
        {
            titles[i].gameObject.SetActive(false);
            texts[i].gameObject.SetActive(false);
        }
        StartCoroutine("TextAnimation");
    }

    void SetStringTexts()
    {
        textValues[0] = rwdSys.enemiesKilled;
        textValues[1] = rwdSys.hitsTaken;
        textValues[2] = rwdSys.maxCombo;
        textValues[3] = (int)rwdSys.accuracy;
        textValues[4] = rwdSys.CalculateReward();
        textValues[5] = rwdSys.CalculateReward2();
        Destroy(rwdSys.gameObject);
    }

    IEnumerator TextAnimation()
    {
        //Setup the scene
        yield return null;
        for (int i = 0; i < texts.Length; i++)
        {
            titles[i].gameObject.SetActive(true);
            shake.ShakeCamera(7.5f, 0.4f);
            yield return new WaitForSeconds(0.5f);
            //Increment number
            int target = (int)Random.Range(50f, 399f);
            float currentTime = 0f;
            while(currentTime < 1f)
            {
                currentTime = Mathf.Min(currentTime + Time.deltaTime, 1f);
                texts[i].gameObject.SetActive(true);
                texts[i].text = ((int)Mathf.Lerp(0f, textValues[i], Mathf.Sin(currentTime * Mathf.PI * 0.5f))).ToString();
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
        }

    }
}

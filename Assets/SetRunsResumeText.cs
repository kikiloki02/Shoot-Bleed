using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetRunsResumeText : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    RewardSystem rwdSys;
    // Start is called before the first frame update
    void Start()
    {
        rwdSys = FindObjectOfType<RewardSystem>();
        SetTexts();
    }

    void SetTexts()
    {
        texts[0].text = rwdSys.enemiesKilled.ToString();
        texts[1].text = rwdSys.hitsTaken.ToString();
        texts[2].text = rwdSys.maxCombo.ToString();
        texts[3].text = rwdSys.accuracy + "%";
        texts[4].text = rwdSys.CalculateReward().ToString();
        texts[5].text = rwdSys.CalculateReward2().ToString();
        Destroy(rwdSys.gameObject);
    }
}

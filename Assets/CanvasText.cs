using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CanvasText : MonoBehaviour
{
    public TextMeshProUGUI stage;
    public TextMeshProUGUI combo;
    public TextMeshProUGUI maxCombo;
    // Start is called before the first frame update
    void Start()
    {
        stage.text = "Stage: 0";
        combo.text = "Combo: 0";
        maxCombo.text = "Max Combo: 0";
    }

    public void SetStageTxt(int stg)
    {
        stage.text = "Stage: "+ stg.ToString();
    }

    public void SetComboTxt(int cmbo)
    {
        combo.text = "Combo: " + cmbo.ToString();
    }

    public void SetMaxComboTxt(int mCmbo)
    {
        maxCombo.text = "Max Combo: " + mCmbo.ToString();
    }
}

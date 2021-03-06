using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Room_Manager : MonoBehaviour
{
    public List<GameObject> _upgrades;

    private int _leftUpgrade;
    private int _rightUpgrade;

    private GameObject _pickedUpgrade;

    // ------ METHODS: ------

    void ChooseTwoUpgradesRandomly()
    {
        if (_upgrades.Count <= 0) { return; }

        int randomNumber1 = Random.Range(0, _upgrades.Count); // min included, max excluded

        _upgrades[randomNumber1].gameObject.transform.position = new Vector3(-4.5f, -0.5f, 0f);
        _upgrades[randomNumber1].gameObject.GetComponent<In_Run_Upgrade>()._icon.SetActive(true);

        int randomNumber2;

        if(_upgrades.Count > 1)
        {
            do
            {
                randomNumber2 = Random.Range(0, _upgrades.Count); // min included, max excluded
            }
            while (randomNumber2 == randomNumber1) ;

        }
        else
        {
            randomNumber2 = randomNumber1;
        }


        _upgrades[randomNumber2].gameObject.transform.position = new Vector3(2.5f, -0.5f, 0f);
        _upgrades[randomNumber2].gameObject.GetComponent<In_Run_Upgrade>()._icon.SetActive(true);


        _leftUpgrade = randomNumber1;
        _rightUpgrade = randomNumber2;
    }

    public void RemoveFromUpgradeList()
    {
        if (_upgrades.Count <= 0) { return; }

        _upgrades.Remove(_pickedUpgrade);

        Destroy(_pickedUpgrade);
    }

    public void PickedUpUpgrade(GameObject gameObject)
    {
        if (_upgrades.Count <= 0) { return; }
        _pickedUpgrade = gameObject;

        _upgrades[_leftUpgrade].GetComponent<In_Run_Upgrade>().Disable();
        _upgrades[_rightUpgrade].GetComponent<In_Run_Upgrade>().Disable();
    }

    public void StartUpgradeRoom()
    {
        if (_upgrades.Count <= 0) { return; }

        Clean();

        ChooseTwoUpgradesRandomly();
    }

    public void Clean()
    {
        if (_upgrades.Count <= 0) { return; }

        for (int i = 0; i < _upgrades.Count; i++)
        {
            _upgrades[i].transform.position = new Vector3(-500, 0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player_Upgrade : MonoBehaviour
{
    public List <GameObject> playerUpgrades;
    public int skillsOwned;
    
    public bool addedNewAb = false;


    // Update is called once per frame
    void Update()
    {
        CheckActiveAbility();
    }

    private void Save()
    {
        for (int i = 0; i < playerUpgrades.Count; i++)
        {
            skillsOwned +=  (int)Mathf.Pow(2,playerUpgrades[i].GetComponent<Upgrades>().GetIndex());
        }
        PlayerPrefs.SetInt("UpgradesOwned", skillsOwned);
    }

    private void InitActivate(ShopUpgrade shopUpgrade, int index) 
    {
        shopUpgrade.upgradesInShop[index].GetComponent<Upgrades>().Activate();

        playerUpgrades.Add(shopUpgrade.upgradesInShop[index]);

        shopUpgrade.upgradesInShop.Remove(shopUpgrade.upgradesInShop[index]);

    }

    public void Activate(GameObject upg)
    {
        upg.GetComponent<Upgrades>().Activate();

        upg.GetComponent<Upgrades>().Disable();

        Save();
    }

    public void Load(ShopUpgrade shopUpgrade) 
    {

        skillsOwned = PlayerPrefs.GetInt("UpgradesOwned",0);
       
        for(int i = 0; i < shopUpgrade.upgradesInShop.Count; i++)
        {
            if((skillsOwned << shopUpgrade.upgradesInShop[i].GetComponent<Upgrades>().GetIndex()) % 2 != 0)
            {
                InitActivate(shopUpgrade,i);             
            }
        }
        
    }

    private void CheckActiveAbility()
    {
        if (!addedNewAb) { return; }

        for(int i = 0; i < playerUpgrades.Count; i++)
        {
            if (!playerUpgrades[i].GetComponent<Upgrades>().GetIsActive())
            {
                Activate(playerUpgrades[i]);
            }
        }

        addedNewAb = false;
    }


}

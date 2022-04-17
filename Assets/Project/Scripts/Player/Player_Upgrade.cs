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

    private void Save(GameObject upg)
    {
        skillsOwned +=  (int)Mathf.Pow(2,upg.GetComponent<Upgrades>().GetIndex());

        PlayerPrefs.SetInt("UpgradesOwned", skillsOwned);
    }

    private void InitActivate(ShopUpgrade shopUpgrade, int index) 
    {
        shopUpgrade.upgradesInShop[index].GetComponent<Upgrades>().Activate();

        playerUpgrades.Add(shopUpgrade.upgradesInShop[index]);

    }

    private void RemoveFromUpgradesInShop(ShopUpgrade shopUpgrade, GameObject upg)
    {
        if(upg != null)
        {
          shopUpgrade.upgradesInShop.Remove(upg);
        }
    }

    public GameObject FindAbilityWithIndex(ShopUpgrade shopUpgrade, int abilityIndex)
    {
        for(int i = 0; i < shopUpgrade.upgradesInShop.Count; i++)
        {
            if(shopUpgrade.upgradesInShop[i].GetComponent<Upgrades>().GetIndex() == abilityIndex) { return shopUpgrade.upgradesInShop[i]; }
        }
        return null;
    }

    public void Activate(GameObject upg)
    {
        upg.GetComponent<Upgrades>().Activate();

        upg.GetComponent<Upgrades>().Disable();

        Save(upg);
    }

    public void Load(ShopUpgrade shopUpgrade) 
    {

        skillsOwned = PlayerPrefs.GetInt("UpgradesOwned",0);
        int initialCount = shopUpgrade.upgradesInShop.Count;
        for (int i = 0; i < initialCount; i++)
        {
            int actualCheckSkill = (skillsOwned >> shopUpgrade.upgradesInShop[i].GetComponent<Upgrades>().GetIndex()) % 2;
            if (actualCheckSkill != 0)
            {
                InitActivate(shopUpgrade,i); 
                
            }
        }
        for (int i = 0; i < initialCount; i++)
        {
            int actualCheckSkill = (skillsOwned >> shopUpgrade.upgradesInShop[i].GetComponent<Upgrades>().GetIndex()) % 2;

            if (actualCheckSkill != 0)
            {
                RemoveFromUpgradesInShop(shopUpgrade, FindAbilityWithIndex(shopUpgrade, shopUpgrade.upgradesInShop[i].GetComponent<Upgrades>().GetIndex()));
                i--;
                initialCount--;
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

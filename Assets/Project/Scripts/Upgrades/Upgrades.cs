using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    private int bloodGems;
    public int idx;
    protected bool isActive;
    protected bool ready; // està dins del collider de la Hab per comprarla
    protected bool disabled;
    protected bool pickedUp;
    protected Player_Upgrade playerUpgrade;

    public int price;
    public GameObject sprite;
    public GameObject popUpCanvas;
    public GameObject descriptionCanvas;
    public GameObject PickedUpCanvas;
    public ShopUpgrade shopUpgrade;
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        bloodGems += PlayerPrefs.GetInt("BloodGem");
        playerUpgrade = FindObjectOfType<Player_Upgrade>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && ready)
        {
            Debug.Log("E pressed");
             if(bloodGems >= price)
             {

                int bloodGems = PlayerPrefs.GetInt("BloodGem");
                bloodGems -= price;
                PlayerPrefs.SetInt("BloodGem", bloodGems);

                

                StartCoroutine(shopUpgrade.ShowPickedUpTextShopKeeper(1f));
                pickedUp = true;

                playerUpgrade.playerUpgrades.Add(this.gameObject);
                shopUpgrade.exposedUpgrades.Remove(this.gameObject);
                playerUpgrade.addedNewAb = true;

                //fer que surti missatge del tendero
             }

             else
             {
                Debug.Log(bloodGems);
                StartCoroutine(shopUpgrade.ShowNotEnoughBloodGems(1f));
                //fer que surti missatge del tendero
             }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") { return; }

        ready = true;

        popUpCanvas.SetActive(true);
        descriptionCanvas.SetActive(true);
    }
       
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player") { return; }

        ready = false;

        popUpCanvas.SetActive(false);
        descriptionCanvas.SetActive(false);
    }

    public virtual void Activate() 
    {
        isActive = true;
    }

    public virtual void Disable()
    {
        disabled = true;
        ready = false;

        sprite.SetActive(false);
        popUpCanvas.SetActive(false);
        descriptionCanvas.SetActive(false);

        if (pickedUp) {

            StartCoroutine(ShowPickedUpText(1f));
            StartCoroutine(shopUpgrade.ShowPickedUpTextShopKeeper(1f)); 
        }

    }

    public virtual void Disactivate()
    {
        isActive = false;
    }

    public virtual int GetIndex()
    {
        return idx;
    }

    public virtual bool GetIsActive()
    {
        return isActive;
    }

    public virtual IEnumerator ShowPickedUpText(float seconds)
    {
        PickedUpCanvas.SetActive(true);

        yield return new WaitForSeconds(seconds);

        PickedUpCanvas.SetActive(false);

        //Destroy(this.gameObject);
    }

}

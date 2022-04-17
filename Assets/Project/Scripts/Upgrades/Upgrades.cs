using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    private int bloodGems;
    public int idx;
    protected bool isActive;
    protected bool ready; // està dins del collider de la Hab per comprarla
    protected bool disabled;
    protected bool pickedUp;
    protected Player_Upgrade playerUpgrade;
    
    public TextMeshProUGUI priceText;

    private CanvasText canvasTxt;

    public int price;
    public GameObject sprite;
    public GameObject popUpCanvas;
    public GameObject descriptionCanvas;
    public GameObject PickedUpCanvas;
    public ShopUpgrade shopUpgrade;
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        canvasTxt = FindObjectOfType<CanvasText>();
        priceText.text = price.ToString();
        bloodGems += PlayerPrefs.GetInt("BloodGem");
        playerUpgrade = FindObjectOfType<Player_Upgrade>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
       // priceText.text = price.ToString();

        if (Input.GetKeyDown(KeyCode.E) && ready)
        {
            bloodGems = PlayerPrefs.GetInt("BloodGem");
            Debug.Log("E pressed");

             if(bloodGems >= price)
             {

                bloodGems -= price;
                PlayerPrefs.SetInt("BloodGem", bloodGems);

                canvasTxt.SetBloodGemsTxt(bloodGems);

                StartCoroutine(shopUpgrade.ShowPickedUpTextShopKeeper(2f));
                pickedUp = true;

                playerUpgrade.playerUpgrades.Add(this.gameObject);
                shopUpgrade.exposedUpgrades.Remove(this.gameObject);
                playerUpgrade.addedNewAb = true;

                //fer que surti missatge del tendero
             }

             else
             {
                Debug.Log(bloodGems);
                StartCoroutine(shopUpgrade.ShowNotEnoughBloodGems(2f));
             }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") { return; }

        if (isActive) { return; }
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

    public virtual void ChangeTextPrice(int _price)
    {
        price = _price;
        priceText.text = price.ToString();
    }

    public virtual IEnumerator ShowPickedUpText(float seconds)
    {
        PickedUpCanvas.SetActive(true);

        yield return new WaitForSeconds(seconds);

        PickedUpCanvas.SetActive(false);

        //Destroy(this.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUpgrade : MonoBehaviour
{
    public List<GameObject> upgradesInShop; // les que té
    

    //crear pref habilidfaes ,añadirlas desde el inspector

    public List<GameObject> exposedUpgrades; //les que s'exposen
    

    public Transform[] positions;


    private int numberOfUpgInStock;

    public List <string> pickedUpMessage;
    public List <string> notEnoughMoneyMessage;
    public List <string> recivingMessage;
    public List <string> farewellMessage;

    public TextMeshProUGUI shopKeeperMessage;


    // Start is called before the first frame update
    void Start()
    {
        Player_Upgrade upg = FindObjectOfType<Player_Upgrade>();
        upg.Load(this);

        //posicion en el mundo
        if (upgradesInShop.Count < 3) { numberOfUpgInStock = upgradesInShop.Count; }
        else { numberOfUpgInStock = 3; }

        for (int i = 0; i < numberOfUpgInStock; i++)
        {
            //if(upgradesInShop.Capacity <= 0) { return; }

            int randomNumber = (int)Random.Range(0f, upgradesInShop.Count);
            exposedUpgrades.Add(upgradesInShop[randomNumber]);
            upgradesInShop.Remove(upgradesInShop[randomNumber]);


            //exposedUpgrades[i].gameObject.transform.position = positions[i].position;

            Instantiate(exposedUpgrades[i], positions[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") { return; }
        StartCoroutine(ShowWelcomingShopKeeper(1f));
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player") { return; }
        StartCoroutine(ShowFarewellShopKeeper(1f));
       
    }

    public IEnumerator ShowPickedUpTextShopKeeper(float seconds)
    {
        int randomNumber = (int)Random.Range(0f, pickedUpMessage.Capacity);
        shopKeeperMessage.text = pickedUpMessage[randomNumber];

        shopKeeperMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(seconds);

        shopKeeperMessage.gameObject.SetActive(false);

    }

    public IEnumerator ShowWelcomingShopKeeper(float seconds)
    {
        int randomNumber = (int)Random.Range(0f, recivingMessage.Capacity);
        shopKeeperMessage.text = recivingMessage[randomNumber];

        shopKeeperMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(seconds);

        shopKeeperMessage.gameObject.SetActive(false);

    }

    public IEnumerator ShowFarewellShopKeeper(float seconds)
    {
        int randomNumber = (int)Random.Range(0f, farewellMessage.Capacity);
        shopKeeperMessage.text = farewellMessage[randomNumber];

        shopKeeperMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(seconds);

        shopKeeperMessage.gameObject.SetActive(false);

    }
    public IEnumerator ShowNotEnoughBloodGems(float seconds)
    {
        int randomNumber = (int)Random.Range(0f, notEnoughMoneyMessage.Capacity);
        shopKeeperMessage.text = notEnoughMoneyMessage[randomNumber];

        shopKeeperMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(seconds);

        shopKeeperMessage.gameObject.SetActive(false);

    }
}

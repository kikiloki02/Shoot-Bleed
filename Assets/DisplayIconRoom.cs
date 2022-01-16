using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayIconRoom : MonoBehaviour
{
    public LoadNextScene doorTrigger;
    public SceneType doorType;
    public Sprite[] doorSprites;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetDoorSprite());
    }



    IEnumerator SetDoorSprite()
    {
        yield return new WaitForSeconds(0.25f);
        doorType = doorTrigger.nextSceneType;

        switch (doorType)
        {
            case SceneType.Easy:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = doorSprites[0];
                break;
            case SceneType.Medium:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = doorSprites[1];
                break;
            case SceneType.Hard:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = doorSprites[2];
                break;
            case SceneType.Upgrade:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = doorSprites[3];
                break;

        }
    }
}

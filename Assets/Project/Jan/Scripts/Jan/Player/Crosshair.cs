using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject _player;

    public SpriteRenderer _renderer;

    public Sprite _normalSprite;
    public Sprite _criticalSprite;

    private void Start()
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        // this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));

        if (_player.GetComponent<PlayerLifeManagement>().criticalState == false)
        {
            _renderer.sprite = _normalSprite;
        }
        else
        {
            _renderer.sprite = _criticalSprite;
        }
    }
}

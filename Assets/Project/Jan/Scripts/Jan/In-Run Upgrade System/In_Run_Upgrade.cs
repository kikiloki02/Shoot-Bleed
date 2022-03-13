using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class In_Run_Upgrade : MonoBehaviour
{
    public int _id;
    public float _pickedUpTextDuration;

    public GameObject _icon;

    public GameObject _pop_up_canvas;
    public GameObject _description_canvas;
    public GameObject _picked_up_canvas;

    private GameObject _player;
    private GameObject _upgrade_room_manager;

    private bool _ready;
    private bool _disabled;
    private bool _pickedUp;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player_Controller>().gameObject;
        _upgrade_room_manager = FindObjectOfType<Upgrade_Room_Manager>().gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _ready)
        {
            _player.GetComponent<Player_Controller>().PlayUpgradeAnimation();

            Debug.Log("E pressed");

            PickedUp(true);

            _player.GetComponent<Player_Controller>().ActivateUpgrade(_id);

            _upgrade_room_manager.GetComponent<Upgrade_Room_Manager>().PickedUpUpgrade(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_disabled == true) { return; }

        if (other.tag != "Player") { return; }

        _ready = true;

        _pop_up_canvas.SetActive(true);
        _description_canvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_disabled == true) { return; }

        if (other.tag != "Player") { return; }

        _ready = false;

        _pop_up_canvas.SetActive(false);
        _description_canvas.SetActive(false);
    }

    public void Disable()
    {
        _disabled = true;
        _ready = false;

        _icon.SetActive(false);
        _pop_up_canvas.SetActive(false);
        _description_canvas.SetActive(false);

        if (_pickedUp) { StartCoroutine(ShowPickedUpText(_pickedUpTextDuration)); }
    }

    public void PickedUp(bool state)
    {
        _pickedUp = state;
    }

    IEnumerator ShowPickedUpText(float seconds)
    {
        _picked_up_canvas.SetActive(true);

        yield return new WaitForSeconds(seconds);

        _picked_up_canvas.SetActive(false);
    }
}

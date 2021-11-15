using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string NextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadNxtScene());
        }
    }

    IEnumerator LoadNxtScene()
    {
        SceneManager.LoadScene(NextScene, LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}

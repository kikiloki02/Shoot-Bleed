using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public int transitionTime;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTransition()
    {
        StartCoroutine(Transition(animator, transitionTime));
    }

    IEnumerator Transition(Animator transition,int transitionTime)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

    }
}

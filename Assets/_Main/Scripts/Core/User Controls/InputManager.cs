using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) )
        {
            promptAdvance();
        } 
    }


    public void promptAdvance()
    {
        DialogueSystem.instance.TriggerUserPromptNext();

	}

}

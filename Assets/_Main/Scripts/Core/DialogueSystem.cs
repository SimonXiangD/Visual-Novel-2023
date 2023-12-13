using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public DialogueContainer dialogueContainer = new DialogueContainer();

    public static DialogueSystem instance;

	private void Awake()
	{
        // singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else DestroyImmediate(gameObject);
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

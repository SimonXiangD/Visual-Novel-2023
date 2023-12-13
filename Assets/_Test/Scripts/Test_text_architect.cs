using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_text_architect : MonoBehaviour
{
    DialogueSystem dialogueSystem;
    public TextArchitect textArchitect;
    string[] strSet = new string[4]
    {
        "Today it's snowing!",
        "What to have for dinner?",
        "Future is bright!",
        "Finally a shower tonight..."
    };
    void Start()
    {
        dialogueSystem = DialogueSystem.instance;
        textArchitect = new TextArchitect(dialogueSystem.dialogueContainer.contentText);
        textArchitect.buildMethod = TextArchitect.BuildMethod.instant;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
		{
            textArchitect.Build(strSet[Random.Range(0, strSet.Length)]);
		}
    }
}

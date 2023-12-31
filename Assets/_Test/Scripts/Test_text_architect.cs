using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

namespace Dirnot.Test
{
    public class Test_text_architect : MonoBehaviour
    {
        DialogueSystem dialogueSystem;
        public TextArchitect textArchitect;
        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;
        string[] strSet = new string[4]
        {
        "Today it's snowing!",
        "What to have for dinner?",
        "Future is bright!",
        "Finally a shower tonight..."
        };
        string longStr = "Some long long long string just for testing. " +
            "I do not know if I am dead or alive and how can I reach eternity or just fall as a mortal but I will " +
            "spend all my efforts into chasing the eternal future for you and me so I will never give up to reunite with you" +
            "because you are all that I really care and need.";


        void Start()
        {
            //longStr = "dfasdfadf";
            Debug.Log("Text architect debug script on!");
            dialogueSystem = DialogueSystem.instance;
            textArchitect = new TextArchitect(dialogueSystem.dialogueContainer.contentText);
            textArchitect.buildMethod = TextArchitect.BuildMethod.fade;

        }

        void Update()
        {
            if (bm != textArchitect.buildMethod)
            {
                textArchitect.buildMethod = bm;
                textArchitect.Stop();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                textArchitect.Stop();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (textArchitect.isBuilding)
                {
                    //if (!textArchitect.hurryUp)
                    //{
                    //    textArchitect.hurryUp = true;
                    //}
                    //else
                    {
                        textArchitect.ForceComplete();
                    }
                }
                else
                {
                    //textArchitect.Build(strSet[Random.Range(0, strSet.Length)]);
                    textArchitect.Build(longStr);

                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                textArchitect.Append(strSet[Random.Range(0, strSet.Length)]);
            }
        }
    }
}
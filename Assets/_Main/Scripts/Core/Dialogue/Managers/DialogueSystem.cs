using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    // Dialogue system in control of the dialogues

    // What we hope: we throw in a bunch of lines to the system
    // the system can give us the corresponding result

    // so the system must have a ui subsystem, where we can outupt corresponding to the lines

    // in a line, we now can change name, content

    // so the ui subsystem is bound to the name and content, and we can control the ui subsystem through easy functions, which is achieved by functions inside ui subsystem(DialogueContainer)

    // Once we have the ui system, we need to deal with lines to control the uis. So we have a conversation system, where we throw in a bunch of lines, it will control the existing ui system, and we can control it

    public class DialogueSystem : MonoBehaviour
    {
        public DialogueContainer dialogueContainer = new DialogueContainer();
        public DialogueParser dialogueParser;
        public ConversationController conversationController;
		public TextArchitect textArchitect;
        public string narratorName = "narrator";

        public DialogueSystemConfigurationSO config;

		public static DialogueSystem instance { get; private set; }

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent userPromptNext;

        public void TriggerUserPromptNext()
        {
            userPromptNext?.Invoke();
        }

        private void Awake()
        {
            // singleton pattern
            if (instance == null)
            {
                instance = this;
				init();

			}
			else DestroyImmediate(gameObject);
        }

        private void init()
        {
			dialogueParser = new DialogueParser();
			conversationController = new ConversationController();
			textArchitect = new TextArchitect(dialogueContainer.contentText);
            //Debug.Log(conversationController);
			//Debug.Log("Dialogue System inited!");
		}

		private void Start()
		{
            
	    }


        public void showName(string text = "")
        {
            dialogueContainer.showName(text);
        }

        public void hideName()
        {
            dialogueContainer.hideName();
        }

        public Coroutine Say(string line)
        {
            List<string> lines = new List<string>() { line};
            return Say(lines);
        }

        public Coroutine Say(List<string> lines)
        {
			return conversationController.StartConversation(lines);
		}
    }
}
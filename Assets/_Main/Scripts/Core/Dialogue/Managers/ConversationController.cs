using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationController
{
    private DialogueSystem dialogueSystem => DialogueSystem.instance;
    private TextArchitect textArchitect => dialogueSystem.textArchitect;

    Coroutine process;

    bool isRunning => process != null;

    public void StartConversation(List<string> lines)
    {
        if(isRunning)
        {
            Stop();
        }
        process = dialogueSystem.StartCoroutine(RunningConversations(lines));
    }

    bool userPrompt = false;

    public ConversationController()
    {
		dialogueSystem.userPromptNext += TriggerUserPromptNext;

	}

    private void TriggerUserPromptNext()
    {
		userPrompt = true;
	}

	void Stop()
    {
        if (!isRunning) return;
		dialogueSystem.StopCoroutine(process);
        process = null;
    }

    IEnumerator RunningConversations( List<string> lines )
    {
        for(int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
			if (line.Trim() == string.Empty) continue;
			DIALOGUE_LINE dialogue_line = dialogueSystem.dialogueParser.parse(line);
            // show name or hide name
            if(dialogue_line.hasSpeaker)
            {
                if(dialogue_line.speaker == dialogueSystem.narratorName)
                {
                    dialogueSystem.hideName();
                }
				else dialogueSystem.showName(dialogue_line.speakerName);
			}
			// show content
			{
				yield return RunDialogue(dialogue_line.content);
            }
            if(dialogue_line.hasCommand)
            {
                yield return RunCommand(dialogue_line.command);
            }

            // can be used for auto mode
            //yield return new WaitForSeconds(2);

            yield return WaitForInput();
		}

	}

    IEnumerator RunDialogue(string content)
    {
        textArchitect.Build(content);
        while(textArchitect.isBuilding)
        {
            if(userPrompt)
            {
                if(!textArchitect.hurryUp)
                {
                    textArchitect.hurryUp = true;
                }
                else textArchitect.ForceComplete();
                userPrompt = false;
            }
        }
		yield return null;

	}

	IEnumerator RunCommand(string command)
    {
        yield return null;
    }

    IEnumerator WaitForInput()
    {
        while(!userPrompt)
        {
            yield return null;
        }
        userPrompt = false;
    }

}

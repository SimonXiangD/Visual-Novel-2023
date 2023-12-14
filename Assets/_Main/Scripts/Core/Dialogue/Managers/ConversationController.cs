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
				else dialogueSystem.showName(dialogue_line.speaker);
			}
            // show content
			//if (dialogue_line.hasContent)
            {
                yield return RunDialogue(dialogue_line.content);
            }
            if(dialogue_line.hasCommand)
            {
                yield return RunCommand(dialogue_line.command);
            }
            yield return new WaitForSeconds(2);
		}

	}

    IEnumerator RunDialogue(string content)
    {
        textArchitect.Build(content);
        while(textArchitect.isBuilding)
        {
            yield return null;
        }
    }

    IEnumerator RunCommand(string command)
    {
        yield return null;
    }

}

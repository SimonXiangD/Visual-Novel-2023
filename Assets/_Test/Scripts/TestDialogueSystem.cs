using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dirnot.Test
{
    public class TestDialogueSystem : MonoBehaviour
    {
		public string fileName = "test4";

		void Start()
		{
			
			startConv();
		}

		void startConv( )
		{
			var stringList = FileManager.readLinesOfTxt(FilePaths.testResourcePath + FilePaths.dialoguePathName + fileName);
			DialogueSystem.instance.Say(stringList);
		}
	}
}
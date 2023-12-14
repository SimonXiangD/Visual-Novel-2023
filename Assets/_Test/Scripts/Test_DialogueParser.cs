using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dirnot.Test
{
    public class Test_DialogueParser : MonoBehaviour
    {
		[SerializeField] private string fileName;
        void Start()
        {

			Debug.Log(fileName);
			StartCoroutine(loadText(FilePaths.testResourcePath + FilePaths.dialoguePathName + fileName));
		
		}

		IEnumerator loadText( string path )
		{
			DialogueParser parser = new DialogueParser();

			var stringList = FileManager.readLinesOfTxt(path);
			foreach (string text in stringList)
			{
				if (string.Empty == text.Trim()) continue;
				string toMatchText = text;
				toMatchText += " ";
				//toMatchText = " " + text;
				parser.parse(toMatchText);
			}
			yield return null;
		}
	}

}
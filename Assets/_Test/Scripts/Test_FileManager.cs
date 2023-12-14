using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dirnot.Test
{
    public class Test_FileManager : MonoBehaviour
    {
        void Start()
        {
			Debug.Log("Text architect debug script on!");
			string fileName = "test1";
            Debug.Log(fileName);
            StartCoroutine(loadText(FilePaths.testResourcePath + FilePaths.dialoguePathName + fileName));
        }

        IEnumerator loadText( string path )
        {
            var stringList = FileManager.readLinesOfTxt(path);
			foreach (string text in stringList)
            {
                Debug.Log(text);
            }
            yield return null;
        }
    }

}
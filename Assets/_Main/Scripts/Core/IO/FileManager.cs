using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static TextAsset readTextFile( string path)
    {
		TextAsset textFile = Resources.Load<TextAsset>(path);
		return textFile;
	}

	public static List<string> readLinesOfTxt( string path )
	{
		TextAsset textAsset = Resources.Load<TextAsset>(path);

		if (textAsset != null)
		{
			string[] fileLines = textAsset.text.Split('\n');
			List<string> lines = new List<string>(fileLines);
			return lines;
		}
		else
		{
			Debug.LogError("textAsset error!");
			return null;
		}
	}

}

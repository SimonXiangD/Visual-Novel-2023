using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMD_DB_ExtensionExamples : CMD_DB_Extension
{
    new public static void Extend(CommandDatabase cmdDb)
    {
        cmdDb.addCommand("print", new Action(PrintTest));
        cmdDb.addCommand("printMp", new Action<string[]>(PrintTest));

        cmdDb.addCommand("lambdaMp", new Action<string[]>((string[] strs)=> { foreach (string str in strs) { Debug.Log(str); }  }));


        cmdDb.addCommand("coprint", new Func<IEnumerator>(coPrintLine));
        cmdDb.addCommand("coprintMp", new Func<string[], IEnumerator>(coPrintLine));

        // unity does not support anonymous iterator block so no lambda for IEnumerator

	}

    private static void PrintTest()
    {
        Debug.Log("cmd extension test print!");
    }
	private static void PrintTest( string[] strs)
	{
        foreach (string str in strs)
        {
			Debug.Log($"cmd extension test printing: {str}!");
		}
	}

    private static IEnumerator coPrintLine()
    {
		for(int i = 0; i < 3; i++)
        {
            Debug.Log($"Count to {3 - i}...");
            yield return new WaitForSeconds(1f);
        }
	}

	private static IEnumerator coPrintLine( string[] strs)
	{
		for (int i = 0; i < strs.Length; i++)
		{
			Debug.Log($"Count to {strs.Length - i} with {strs[i]}...");
			yield return new WaitForSeconds(1f);
		}
	}

}

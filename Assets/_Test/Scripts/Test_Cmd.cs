using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Cmd : MonoBehaviour
{
    void Start()
    {
		StartCoroutine(Running());
    }

    IEnumerator Running()
    {
		yield return CommandSystem.instance.Execute("print");
		yield return CommandSystem.instance.Execute("printMp", "Hello", "Hello2");
		string[] arr = { "l1", "lnew" };
		yield return CommandSystem.instance.Execute("lambdaMp", arr);
		yield return CommandSystem.instance.Execute("coprint");
		yield return CommandSystem.instance.Execute("coprintMp", "co1", "co2", "Distrust and boredom");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Cmd : MonoBehaviour
{
    void Start()
    {
        CommandSystem.instance.Execute("print");
    }

    void Update()
    {
        
    }
}

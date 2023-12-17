using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMD_DB_ExtensionExamples : CMD_DB_Extension
{
    new public static void Extend(CommandDatabase cmdDb)
    {
        cmdDb.addCommand("print", new Action(PrintTest));
    }

    private static void PrintTest()
    {
        Debug.Log("cmd extension test print!");
    }
}

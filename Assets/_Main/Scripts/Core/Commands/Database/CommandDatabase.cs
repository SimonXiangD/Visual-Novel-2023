using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDatabase 
{
    Dictionary<string, Delegate> database = new Dictionary<string, Delegate>();
    public bool hasCommand(string command)
    {
        return database.ContainsKey(command);
    }

    public void addCommand(string commandName, Delegate command)
    {
        database.Add(commandName, command);
    }

    public Delegate getCommand(string commandName)
    {
        if(!database.ContainsKey(commandName))
        {
            Debug.LogError($"Command {commandName} not found!");
            return null;
        }
        return database[commandName];
    }

}

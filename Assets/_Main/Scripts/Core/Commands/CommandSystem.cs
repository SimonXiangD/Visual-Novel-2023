using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class CommandSystem : MonoBehaviour
{
    public static CommandSystem instance { get; private set; }
	CommandDatabase database;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			database = new CommandDatabase();
			Assembly assembly = Assembly.GetExecutingAssembly();
			Type[] extensionTypes = assembly.GetTypes().Where(t=>t.IsSubclassOf(typeof(CMD_DB_Extension))).ToArray();

			foreach(Type extension in extensionTypes)
			{
				MethodInfo extensionMethod = extension.GetMethod("Extend");
				extensionMethod.Invoke(null, new object[] { database });
			}

		}
		else
		{
			DestroyImmediate(gameObject);
		}
	}

	public void Execute(string cmdName)
	{
		Delegate command = database.getCommand(cmdName);
		if(command != null)
		{
			command.DynamicInvoke();
		}
	}
}

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
	private Coroutine process;
	public bool isRunning => process != null;

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

	public Coroutine Execute(string cmdName, params string[] args)
	{
		Delegate command = database.getCommand(cmdName);
		if (command == null) return null;
		return StartExecuteProcess(command, args);
	}

	private Coroutine StartExecuteProcess(Delegate command, params string[] args )
	{
		StopProcess();
		process = StartCoroutine(RunningExecuteProcess(command, args));
		return process;
	}

	private IEnumerator RunningExecuteProcess(Delegate command, params string[] args)
	{
		if (command is Action)
		{
			command.DynamicInvoke();
		}
		else if (command is Action<string[]>)
		{
			command.DynamicInvoke((object)args);
		}
		else if (command is Func<IEnumerator>)
		{
			yield return ((Func<IEnumerator>)command)();
		}
		else if(command is Func<string[], IEnumerator>)
		{
			yield return ((Func<string[], IEnumerator>)command)(args);
		}

		process = null;
	}

	void StopProcess()
	{
		if(process == null) return;
		StopCoroutine(process);
		process = null;
	}
}

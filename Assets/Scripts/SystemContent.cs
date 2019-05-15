using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class SystemContent : Singleton<SystemContent>
{
	public event Events.SimpleEvent OnChangeVersion;
	public KeyCode keycode;

    private void Update()
	{
		OnClickKey();
	}

	public void OnClickKey()
	{
		if (Input.GetKeyDown(keycode))
		{
			ChangeVersion();
		}
	}

	public void ChangeVersion()
	{

		LocalizationManager.instance.LoadLocalizedText(LocalizationManager.instance.FileNames[0]);
		if (OnChangeVersion != null) OnChangeVersion();
	}

}

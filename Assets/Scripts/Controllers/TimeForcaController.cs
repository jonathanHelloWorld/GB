using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeForcaController : Singleton<TimeForcaController> {

	public event Events.SimpleStringEvent OnUpdate;
	Coroutine co;

	public float timeGame;

	[HideInInspector]
	public float time;
	[HideInInspector]
	public bool timeOut;

	void Start () {

		time = timeGame;
		timeOut = false;

		//ForcaController.Instance.OnGameEnd += ResetTimeGame;
		//ForcaController.Instance.OnGameStart += OnStarTime;

	}
	
	
	public float GetCurrentTime()
	{
		return time;
	}

	public void ResetTimeGame()
	{
		StopCoroutine();
		time = timeGame;
	}

	public void OnStarTime()
	{
		co = StartCoroutine(StartTime());
	}

	public void StopCoroutine()
	{
		StopCoroutine(co);
	}

	IEnumerator StartTime()
	{
		timeOut = false;
		while (true)
		{

			if(time > 0.0f)
			{
				time -=  1*Time.deltaTime;
				if (OnUpdate != null) OnUpdate(time.ToString());
			}
			else
			{
				//Utils.ConvertRealTime();
				if (OnUpdate != null) OnUpdate(time.ToString());
				timeOut = true;
				//ForcaController.Instance.GameEnd();
				break;
			}

			yield return null;

		}

	}

}

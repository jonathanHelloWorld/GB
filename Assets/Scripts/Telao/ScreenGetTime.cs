using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenGetTime : MonoBehaviour
{
	private Text _txt;
	public ConvertTimeType TimeFormat = ConvertTimeType.ms;

	void Start()
	{

		_txt = GetComponent<Text>();
		ScreenGameManager.Instance.OnUpdateTime += UpdateData;
	}

	public void UpdateData(string value)
	{
		string nText = Utils.ConvertRealTime(float.Parse(value), TimeFormat);
		UpdateText(nText);
	}

	public void UpdateText(string value)
	{
		ScreenGameManager.Instance.timeAnswer = value;
		//_txt.text = "00:"+ value;	
		_txt.text = value;
	}
}

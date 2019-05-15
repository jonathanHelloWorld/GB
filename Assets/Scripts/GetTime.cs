using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTime : MonoBehaviour {

	private Text _txt;
	public ConvertTimeType TimeFormat = ConvertTimeType.ms;


	void Start () {

		_txt = GetComponent<Text>();
		GameManager.Instance.OnUpdateTime += UpdateData;
	}

	public void UpdateData(string value)
	{
		string nText = Utils.ConvertRealTime(float.Parse(value), TimeFormat);
		UpdateText(nText);
	}

	public void UpdateText(string value)
	{
		GameManager.Instance.timeAnswer = value;
		//_txt.text = "00:"+ value;	
		_txt.text =  value;
	}

}

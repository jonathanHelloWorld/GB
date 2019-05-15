using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour {

	public delegate void SimpleEvent();
	public delegate void SimpleIntEvent(int var);
	public delegate void SimpleStringEvent(string var);
	public delegate void RegisterEvent(string key, string value, bool updateTime);
	public delegate void ReferenceButtonEvent(ButtonAlternative objButton);
	public delegate void QuestionEvent(Question question);
}

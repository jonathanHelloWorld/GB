using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetDropDown : MonoBehaviour {

	public Dropdown Drop;

	private Button _btn;

	void Start () {
		_btn = GetComponent<Button>();
		_btn.onClick.AddListener(RefreshDrop);
	}
	
	public void RefreshDrop() {
		//Drop.RefreshShownValue();
		Drop.value = 0;
	}
	
}

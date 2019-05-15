using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRaiz : MonoBehaviour
{
	protected Button btn;

	public virtual  void OnClick()
	{
		btn = GetComponent<Button>();
		btn.onClick.AddListener(() => { AudioManagerr.Instance.PlayLetterSong(); });
	}

}

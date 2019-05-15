using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAlternative : ButtonRaiz
{

	public Text letterTxt;
	public Text alternativeTxt;
	[HideInInspector]
	public Alternative altButton;

    void Start()
    {
		btn = GetComponent<Button>();

		GameManager.Instance.OnResetColors += ResetButtonColor;
		GameManager.Instance.OnAnswer += OffAlternative;

		btn.onClick.AddListener(PressedButton);
		OnClick();
    }

	public override void OnClick()
	{
		base.OnClick();
	}

	public void OffAlternative()
	{
		btn.interactable = false;
	}

	public void PressedButton()
	{
		btn.image.color = GameManager.Instance.pressedColor;
		GameManager.Instance.PressedButtonAltertive(this);
	}

	public void ResetButtonColor()
	{
		if(GameManager.Instance._btnPressed != this)
		{
			btn.image.color = GameManager.Instance.normalColor;
		}
	}
	
}

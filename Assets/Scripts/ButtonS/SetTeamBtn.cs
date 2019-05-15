using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTeamBtn : ButtonRaiz
{
	public Dropdown dropDown;

    void Start()
    {
		btn = GetComponent<Button>();
		btn.onClick.AddListener(SetTeamName);
		OnClick();
    }

	public void SetTeamName()
	{
		TeamsController.Instance.CreateTeam(dropDown.captionText.text);
		Pagination.Instance.OpenPage(1);
	}

	public override void OnClick()
	{
		base.OnClick();
	}
}

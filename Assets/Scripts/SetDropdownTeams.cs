using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.EventSystems;

public class SetDropdownTeams : MonoBehaviour,IPointerClickHandler,ISelectHandler
{
	private Dropdown _dropdown;

    void Start()
    {
		_dropdown = GetComponent<Dropdown>();
		SetDrop();
    }

	public void SetDrop()
	{
		GameRequest.Instance.LoadNameTeam();
		StartCoroutine(WaitFillTeamNames());
	}

	IEnumerator WaitFillTeamNames()
	{

		while (TeamsController.Instance.teamsName.Count == 0)
		{
			yield return null;
		}

		_dropdown.ClearOptions();
		_dropdown.AddOptions(TeamsController.Instance.teamsName);

	}

	public void OnPointerClick(PointerEventData eventData)
	{
		AudioManagerr.Instance.PlayLetterSong();
		_dropdown.OnPointerClick(eventData);
	}

	public void OnSelect(BaseEventData eventData)
	{
		AudioManagerr.Instance.PlayLetterSong();
		_dropdown.OnSelect(eventData);
	}
}

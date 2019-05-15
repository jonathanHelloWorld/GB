using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamsController : Singleton<TeamsController>
{

	public event Events.SimpleEvent OnChangeAttributes;
	public Text txtEquipe;
	public List<string> teamsName = new List<string>();

	[HideInInspector]
	public Team myTeam = null;

	public void CreateTeam(string teamName)
	{
		Team team = new Team()
		{
			teamName = teamName,
			attribute1 = new Attribute() { attributeName = "seila", score = 0 },
			attribute2 = new Attribute() { attributeName = "seila2", score = 0 },
			attribute3 = new Attribute() { attributeName = "seila3", score = 0 },
			attribute4 = new Attribute() { attributeName = "seila4", score = 0 },
			attribute5 = new Attribute() { attributeName = "seila5", score = 0 },
			position = 1
		};

		this.myTeam = team;
		txtEquipe.text = teamName;
		OnChange();

		GameManager.Instance.RankingChange();
	}

	public void OnChange()
	{
		if(OnChangeAttributes != null) { OnChangeAttributes(); }
	}

	public void SetAttributesScore(CollectionPonts ponts)
	{
		myTeam.attribute1.score = ponts.atributo1;
		myTeam.attribute2.score = ponts.atributo2;
		myTeam.attribute3.score = ponts.atributo3;
		myTeam.attribute4.score = ponts.atributo4;
		myTeam.attribute5.score = ponts.atributo5;

		OnChange();
	}
}

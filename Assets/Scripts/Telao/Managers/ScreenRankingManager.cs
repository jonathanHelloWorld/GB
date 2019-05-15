using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRankingManager : Singleton<ScreenRankingManager>
{

	[HideInInspector]
	public List<CollectionTeam> teamRankeds;
	public List<ContainerRank> containers;
	

	public void OnSetRanks(List<CollectionTeam> teams)
	{
		teamRankeds = teams;
		SetRankings();
	}

	private void SetRankings()
	{
		for(int i = 0; i < teamRankeds.Count; i++)
		{
			containers[i].posRankTxt.text = (i + 1).ToString() + "º";
			containers[i].nameTeamTxt.text = teamRankeds[i]._id;
			containers[i].barraEffect.scoreNew = teamRankeds[i].total;
			containers[i].barraEffect.SetBarraRanking();
		}
	}
}

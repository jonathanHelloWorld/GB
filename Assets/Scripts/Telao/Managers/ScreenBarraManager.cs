using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBarraManager : MonoBehaviour
{
	public ScreenBarraEffect barraEffect1;
	public ScreenBarraEffect barraEffect2;
	public ScreenBarraEffect barraEffect3;
	public ScreenBarraEffect barraEffect4;
	public ScreenBarraEffect barraEffect5;

	public List<ScreenBarraEffect> barrasRankings;

	void Start()
	{
		//TeamsController.Instance.OnChangeAttributes += SetScoredBarras;
	}

	public void SetScoredBarras()
	{
		barraEffect1.scoreNew = (float)TeamsController.Instance.myTeam.attribute1.score;
		barraEffect2.scoreNew = (float)TeamsController.Instance.myTeam.attribute2.score;
		barraEffect3.scoreNew = (float)TeamsController.Instance.myTeam.attribute3.score;
		barraEffect4.scoreNew = (float)TeamsController.Instance.myTeam.attribute4.score;
		barraEffect5.scoreNew = (float)TeamsController.Instance.myTeam.attribute5.score;
	}

}

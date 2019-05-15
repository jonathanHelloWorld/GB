using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrasManager : MonoBehaviour
{

	public BarraEffect barraEffect1;
	public BarraEffect barraEffect2;
	public BarraEffect barraEffect3;
	public BarraEffect barraEffect4;
	public BarraEffect barraEffect5;

	void Start()
    {
		TeamsController.Instance.OnChangeAttributes += SetScoredBarras;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
	public Text txtPageRank;
	public Text txtRank;
	public Image imgRank;
	public Sprite[] spriteRanks = new Sprite[3];

    void Start()
    {
		OffImgRank();
		GameManager.Instance.OnRanking += SetTxtPageRank;
		GameManager.Instance.OnRanking += SetTxtRank;
	}

	public void OffImgRank()
	{
		imgRank.gameObject.SetActive(false);
	}

	public void OnImgRank()
	{
		imgRank.gameObject.SetActive(true);
	}

	public void ChangeSpriteRank(int index)
	{
		if(index != 0)
			imgRank.sprite = spriteRanks[index - 1];
	}

	public void SetTxtRank()
	{
		string data = GetPositionTeam().ToString() + "º" + " lugar";
		txtRank.text = data;
	}

	public void SetTxtPageRank()
	{
		int value = GetPositionTeam();

		if(value > 3)
		{
			string data = value.ToString() + "º" + "\n" + "colocado";
			txtPageRank.text = data;
			OffImgRank();
		}
		else
		{
			txtPageRank.text = "";
			OnImgRank();
			ChangeSpriteRank(value);
		}
	}

	public int GetPositionTeam()
	{
		return TeamsController.Instance.myTeam.position;
	}

}

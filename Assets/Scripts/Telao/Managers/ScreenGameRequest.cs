using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEngine.Networking;
using System.Text;

public class ScreenGameRequest : Singleton<ScreenGameRequest>
{
	public string BaseUrl = "http://10.11.1.84:9000/";

	private string urlGetMediaP = "pontuacoes/media/";
	private string urlGetTeams = "pontuacoes/group-by-team";

	public void LoadTeamsPositions()
	{
		WWW request = new WWW(BaseUrl + urlGetTeams);
		StartCoroutine(OnResponsePosition(request));
	}

	public void LoadMedias(string data)
	{
		WWW request = new WWW(BaseUrl + urlGetMediaP + data);
		StartCoroutine(OnResponseMedia(request));
	}

	private IEnumerator OnResponsePosition(WWW req)
	{
		yield return req;

		SetPositionData(req);
	}

	private IEnumerator OnResponseMedia(WWW req)
	{
		yield return req;

		SetMediasData(req);
	}

	private void SetPositionData(WWW req)
	{
		if (req != null && !string.IsNullOrEmpty(req.text))
		{
			List<CollectionTeam> equipes = Mapper.MapCreateList<CollectionTeam>(req.text, new List<CollectionTeam>());

			 equipes = (from p in equipes
						where p.posicao <= 10
						orderby p._id
						orderby p.posicao					
						select p).ToList();

			ScreenRankingManager.Instance.OnSetRanks(equipes);
			//ScreenGameManager.Instance.RankingChange();
		}
	}

	private void SetMediasData(WWW req)
	{
		if (req != null && !string.IsNullOrEmpty(req.text))
		{
			Debug.Log(req.text);
			Alt alternative = Mapper.MapCreate<Alt>(req.text, new Alt());

			ScreenMediaController.Instance.SetAlternativas(alternative);
		}
	}

}

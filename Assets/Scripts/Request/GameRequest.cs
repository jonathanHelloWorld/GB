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

public class GameRequest : Singleton<GameRequest> {

	public string baseUrl = "http://10.11.1.84:9000/";

	private  string urlGetEquipes = "equipes";
	private  string urlPostPontos = "pontuacoes/";
	private string urlGetPosition = "pontuacoes/group-by-team/";
	// private Questions questions;

	//public string NumberProject;


	public void PostPontos (Dictionary<string,string> data){
		StartCoroutine(Upload(data));
	}
	
	IEnumerator Upload(Dictionary<string, string> data) {
		//WWWForm form = new WWWForm();
		
			UnityWebRequest www = UnityWebRequest.Post(baseUrl + urlPostPontos, data);

			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log(www.error);
			}
			else {
			//Debug.Log(":\nReceived: " + www.downloadHandler.text);
			CollectionPonts pontos = Mapper.MapCreate<CollectionPonts>(www.downloadHandler.text, new CollectionPonts());
			TeamsController.Instance.SetAttributesScore(pontos);

			//Debug.Log(":\nReceived: " + www.downloadHandler.text);
			Debug.Log("Form upload complete!");
			}
	}

	public void LoadTeamPostion()
	{
		WWW request = new WWW(baseUrl + urlGetPosition);
		StartCoroutine(OnResponsePosition(request));
	}

	public void LoadNameTeam()
	{
		WWW request = new WWW(baseUrl + urlGetEquipes);
		StartCoroutine(OnResponse(request));
	}

	private IEnumerator OnResponse(WWW req)
	{
		yield return req;

		SetAllIData(req);
	}

	private IEnumerator OnResponsePosition(WWW req)
	{
		yield return req;

		SetPositionData(req);
	}

	private void SetPositionData(WWW req)
	{
		if (req != null && !string.IsNullOrEmpty(req.text))
		{
			List<CollectionTeam> equipes = Mapper.MapCreateList<CollectionTeam>(req.text, new List<CollectionTeam>());

			int pos = (from p in equipes
						where p._id == TeamsController.Instance.myTeam.teamName
						select p.posicao).SingleOrDefault();


			TeamsController.Instance.myTeam.position = pos;
			GameManager.Instance.RankingChange();
		}
	}

	private void SetAllIData(WWW req)
	{

		if(req != null && !string.IsNullOrEmpty(req.text))
		{
		
			List<NameEquipes> list = new List<NameEquipes>();
			List<NameEquipes> nameEquipes = Mapper.MapCreateList<NameEquipes>(req.text, list);
			
			for(int i = 0; i < nameEquipes.Count; i++)
			{
				TeamsController.Instance.teamsName.Add(nameEquipes[i].nome);
			}
		}
	}

}

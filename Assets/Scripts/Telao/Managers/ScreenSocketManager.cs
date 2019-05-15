using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading;
using System;
using SocketIO;

public class ScreenSocketManager : MonoBehaviour
{
	private SocketIOComponent socketIO;

	private void Start()
	{
		//  SystemContent.Instance.ChangeVersion();
		socketIO = GetComponent<SocketIOComponent>();
		OnSocket();
	}

	public void OnSocket()
	{
		#region DEFAULT ON
		socketIO.On("open", (SocketIOEvent ev) =>
		{
			Debug.Log("conectou " + ev.data);

		});

		socketIO.On("close", (SocketIOEvent ev) =>
		{
			Debug.Log("Disconnected.");
		});

		socketIO.On("error", (SocketIOEvent ev) =>
		{
			Debug.Log("error" + ev.data);
		});

		#endregion

		socketIO.On("pergunta", (SocketIOEvent ev) =>
		{
			Debug.Log("PERGUNTA");

			QuestionMap map = Mapper.MapCreate<QuestionMap>(ev.data.ToString(), new QuestionMap());
			
			#region MAP
			List<Alternative> alt = new List<Alternative>();
			for (int i = 0; i < map.alternativas.Count; i++)
			{
				Alternative newAlt = new Alternative();
				newAlt.atributo1 = new Attribute() { score = map.alternativas[i].atributo1, attributeName = "FORÇA" };
				newAlt.atributo2 = new Attribute() { score = map.alternativas[i].atributo2, attributeName = "SAÚDE" };
				newAlt.atributo3 = new Attribute() { score = map.alternativas[i].atributo3, attributeName = "HEMAN" };
				newAlt.atributo4 = new Attribute() { score = map.alternativas[i].atributo4, attributeName = "EU" };
				newAlt.atributo5 = new Attribute() { score = map.alternativas[i].atributo5, attributeName = "TENHO" };
				newAlt.texto = map.alternativas[i].texto;
				alt.Add(newAlt);
			}

			Question question = new Question()
			{
				titulo = map.titulo,
				id = map.id,
				alternativas = alt
			};

			#endregion

			ScreenGameManager.Instance.ReceiverAnswer(question);
			ScreenGameManager.Instance.GameStart();
			Pagination.Instance.OpenPage(1);
		});

		socketIO.On("home", (SocketIOEvent ev) =>
		{
			Debug.Log("HOME");
			Pagination.Instance.OpenPage(0);
		});

		socketIO.On("ranking", (SocketIOEvent ev) =>
		{
			Debug.Log("RANKING");

			ScreenGameRequest.Instance.LoadTeamsPositions();
			Pagination.Instance.OpenPage(2);
		});

		socketIO.On("media", (SocketIOEvent ev) =>
		{
			Debug.Log("Media");

			//var data = JsonConvert.DeserializeObject<string>(ev.data.ToString());

			ScreenGameRequest.Instance.LoadMedias(ev.data.ToString().Substring(1, ev.data.ToString().Length-2));
			Pagination.Instance.OpenPage(3);
		});

		socketIO.On("tempo", (SocketIOEvent ev) =>
		{
			Debug.Log("TEMPO");
			string temp = JsonConvert.DeserializeObject<string>(ev.data.ToString());
			ScreenGameManager.Instance.UpdateTime(temp);
		});

		socketIO.On("tela-instrucoes", (SocketIOEvent ev) =>
		{
			Debug.Log("INSTRUCOES");

			Pagination.Instance.OpenPage(4);
		});
	}
}

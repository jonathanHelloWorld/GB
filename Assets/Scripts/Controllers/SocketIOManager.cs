using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading;
using System;
using SocketIO;

public class SocketIOManager : MonoBehaviour
{
    private SocketIOComponent socketIO;

    private void Start()
    {
      //  SystemContent.Instance.ChangeVersion();
        socketIO = GetComponent<SocketIOComponent>();
		GameManager.Instance.OnSocketEmit += SocketEmitR;
        OnSocket();
	}

    public void SendState()
    {
		//socketIO.Emit("enviaPerguntas", "teste");
	}

	public void SockeEmitEstado(Dictionary<string, string> dir)
	{
		string jsonData = JsonConvert.SerializeObject(dir);
		JSONObject json = new JSONObject(jsonData);
		socketIO.Emit("estado", json);
	}

	public void SocketEmitR()
	{
		JSONObject json = JSONObject.CreateStringObject(TeamsController.Instance.myTeam.teamName);
		socketIO.Emit("respondeu", json);
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
			Dictionary<string, string> dir = new Dictionary<string, string>()
			{
				{"estado","pergunta" },
				{"time",TeamsController.Instance.myTeam.teamName }
			};
			SockeEmitEstado(dir);

			#region MAP
			List<Alternative> alt = new List<Alternative>();
			Debug.Log(map.alternativas.Count);
			for (int i =0; i < map.alternativas.Count; i++)
			{
				Alternative newAlt = new Alternative();
				newAlt.atributo1 = new Attribute(){score = map.alternativas[i].atributo1,attributeName = "FORÇA"};
				newAlt.atributo2 = new Attribute(){score = map.alternativas[i].atributo2,attributeName = "SAÚDE"};
				newAlt.atributo3 = new Attribute(){score = map.alternativas[i].atributo3,attributeName = "HEMAN"};
				newAlt.atributo4 = new Attribute(){score = map.alternativas[i].atributo4,attributeName = "EU"};
				newAlt.atributo5 = new Attribute(){score = map.alternativas[i].atributo5,attributeName = "TENHO"};
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

			GameManager.Instance.ReceiverAnswer(question);
			GameManager.Instance.GameStart();		
			Pagination.Instance.OpenPage(2);
        });

		socketIO.On("resposta", (SocketIOEvent ev) =>
		{
			Debug.Log("RESPOSTA");
			Dictionary<string, string> dir = new Dictionary<string, string>()
			{
				{"estado","resposta" },
				{"time",TeamsController.Instance.myTeam.teamName }
			};
			SockeEmitEstado(dir);
			GameManager.Instance.RespostaActive();
		});

		socketIO.On("home", (SocketIOEvent ev) =>
		{
			Debug.Log("HOME");
			Dictionary<string, string> dir = new Dictionary<string, string>()
			{
				{"estado","home" },
				{"time",TeamsController.Instance.myTeam.teamName }
			};
			SockeEmitEstado(dir);

			Pagination.Instance.OpenPage(1);
		});

		socketIO.On("tela-instrucoes", (SocketIOEvent ev) =>
		{
			Debug.Log("INSTRUCOES");
			Dictionary<string, string> dir = new Dictionary<string, string>()
			{
				{"estado","tela-instrucoes" },
				{"time",TeamsController.Instance.myTeam.teamName }
			};
			SockeEmitEstado(dir);

			Pagination.Instance.OpenPage(4);
		});

		socketIO.On("ranking", (SocketIOEvent ev) =>
		{
			Debug.Log("RANKING");
			Dictionary<string, string> dir = new Dictionary<string, string>()
			{
				{"estado","ranking" },
				{"time",TeamsController.Instance.myTeam.teamName }
			};
			SockeEmitEstado(dir);

			GameRequest.Instance.LoadTeamPostion();
			Pagination.Instance.OpenPage(5);
		});

		socketIO.On("pontos", (SocketIOEvent ev) =>
		{
			Debug.Log("PONTOS");
			Dictionary<string, string> dir = new Dictionary<string, string>()
			{
				{"estado","pontos" },
				{"time",TeamsController.Instance.myTeam.teamName }
			};
			SockeEmitEstado(dir);
			GameManager.Instance.ViewPontos();
		});

		socketIO.On("tempo", (SocketIOEvent ev) =>
        {
			Debug.Log("TEMPO");
			string temp = JsonConvert.DeserializeObject<string>(ev.data.ToString());
			Debug.Log(temp);
			GameManager.Instance.UpdateTime(temp);
        });
	}
}

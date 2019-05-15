using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSendAnswer : ButtonRaiz
{
	

    void Start()
    {
		btn = GetComponent<Button>();
		btn.onClick.AddListener(SendAlternative);
		btn.onClick.AddListener(OffInteractable2);

		OnClick();

		GameManager.Instance.OnGameStart += OffInteractable;
		GameManager.Instance.OnResposta += OnInteractable;
    }

	public override void OnClick()
	{
		base.OnClick();
	}

	public void OffInteractable()
	{
		btn.interactable = false;
	}

	public void OffInteractable2()
	{
		if (GameManager.Instance._btnPressed != null)
		{
			btn.interactable = false;
		}
	}

	public void OnInteractable()
	{
		btn.interactable = true;
	}

	public void SendAlternative()
	{
		ButtonAlternative refButton = GameManager.Instance._btnPressed;

		if (refButton != null)
		{
			Dictionary<string, string> dir = new Dictionary<string, string>()
			{
				{"equipe",TeamsController.Instance.myTeam.teamName },
				{"idAlternativa",refButton.letterTxt.text},
				{"pergunta", GameManager.Instance.GetIDQuestion() },
				{"atributo1", refButton.altButton.atributo1.score.ToString() },
				{"atributo2", refButton.altButton.atributo2.score.ToString() },
				{"atributo3", refButton.altButton.atributo3.score.ToString() },
				{"atributo4", refButton.altButton.atributo4.score.ToString() },
				{"atributo5", refButton.altButton.atributo5.score.ToString() },
				{"tempo", GameManager.Instance.timeAnswer.Substring(3)}
			};

			GameRequest.Instance.PostPontos(dir);
			GameManager.Instance.SocketEmit();
			GameManager.Instance.AnswerDone();
		}
	}
}

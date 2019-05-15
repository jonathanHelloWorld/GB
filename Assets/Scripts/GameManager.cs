using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	#region EVENTS
	public event Events.SimpleEvent OnGameStart, OnResetColors, OnViewPontos, OnResposta, OnAnswer,OnRanking,OnSocketEmit;
	public event Events.SimpleStringEvent OnUpdateTime;
	public event Events.QuestionEvent OnReceiverQuestion;
	public event Events.ReferenceButtonEvent OnPressedButton;
	#endregion

	public GameObject prefabButton;
	public Transform ParentButton;
	public Text txtQuestion;
	[HideInInspector]public string timeAnswer;

	#region COLOR
	[Header("Color Settings ButtonAlternatives")]
	public Color pressedColor;
	public Color normalColor = Color.white;
	#endregion

	public ButtonAlternative _btnPressed = null;
	private QuestionController _questionController;

	protected override void OnAwake()
	{
		base.OnAwake();

		_questionController = GetComponent<QuestionController>();
	}

	public void PressedButtonAltertive(ButtonAlternative buttonPressed)
	{
		_btnPressed = buttonPressed;
		if (OnPressedButton != null) OnPressedButton(buttonPressed);
		if (OnResetColors != null) OnResetColors();

	}

	public void GameStart()
	{
		SetNull();
		if (OnGameStart != null) OnGameStart();
		SetQuestion();
	}

	private void SetNull()
	{
		OnResetColors = null;
		OnAnswer = null;
		_btnPressed = null;
	}

	private void SetQuestion()
	{
		for (int i = 0; i < _questionController.GetQtdAlternatives(); i++)
		{
			GameObject obj = Instantiate(prefabButton, ParentButton);
			ButtonAlternative btn = obj.GetComponent<ButtonAlternative>();

			char letter = (char)(96 + i + 1);

			btn.altButton = _questionController.question.alternativas[i];
			btn.letterTxt.text = letter.ToString().ToUpper();
			btn.alternativeTxt.text = _questionController.question.alternativas[i].texto;
		}

		txtQuestion.text = _questionController.GetNameQuestion();
	}

	public void ReceiverAnswer(Question question)
	{
		if (OnReceiverQuestion != null) OnReceiverQuestion(question);
	}
	
	public string GetIDQuestion()
	{
		return _questionController.question.id;
	}

	public void ViewPontos()
	{
		if (OnViewPontos != null) OnViewPontos();
	}

	public void RespostaActive()
	{
		if (OnResposta != null) OnResposta();
	}

	public void AnswerDone()
	{
		if (OnAnswer != null) OnAnswer();
	}

	public void UpdateTime(string value)
	{
		if (OnUpdateTime != null) OnUpdateTime(value);
	}

	public void RankingChange()
	{
		if (OnRanking != null) OnRanking();
	}

	public void SocketEmit()
	{
		if (OnSocketEmit != null) OnSocketEmit();
	}

}

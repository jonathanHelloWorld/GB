using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenQuestionController : MonoBehaviour
{
	public Question question;

	private void Start()
	{
		ScreenGameManager.Instance.OnReceiverQuestion += ChangeCurrentQuestion;
	}

	public void ChangeCurrentQuestion(Question question)
	{
		this.question = question;
	}

	public string GetNameQuestion()
	{
		return question.titulo;
	}

	public int GetQtdAlternatives()
	{
		return question.alternativas.Count;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenBarraEffect : MonoBehaviour
{
	public Image positive;
	public Image negative;
	public Image normal;

	public Text valueTxt;
	public bool valueIsActive = true;

	public float scoreCurrent = 0;
	public float scoreNew = 0;
	public int Max = 200;

	void Start()
	{
		ScreenGameManager.Instance.OnViewPontos += SetBarraValues;
		//SetBarraValues();
	}

	public void SetBarraValues()
	{
		float value = scoreNew - scoreCurrent;
		value = value / Max;
		AudioManagerr.Instance.PlayRightSong();
		if ((normal.fillAmount + value) >= normal.fillAmount)
		{
			StartCoroutine(EffectPositve(value));
		}
		else
		{
			StartCoroutine(EffectNegative(value));
		}

		scoreCurrent = scoreNew;
	}

	public void SetBarraRanking()
	{
		float value = scoreNew - scoreCurrent;
		value = value / Max;

		normal.fillAmount += value;

		if(normal.fillAmount + value <= 0)
			normal.fillAmount = 0f;

		negative.fillAmount = normal.fillAmount;
		positive.fillAmount = normal.fillAmount;
		scoreCurrent = scoreNew;

		if (valueIsActive)
		{
			valueTxt.text = "$" + scoreCurrent.ToString() +",00";
		}
			
	}

	public void SetBarraMedia()
	{
		StartCoroutine(EffectMedia(scoreNew));

		valueTxt.text = (scoreNew * 100f).ToString("00") + "%";
		scoreCurrent = scoreNew;
	}

	#region TWEEN
	IEnumerator EffectPositve(float value)
	{
		positive.DOFillAmount(normal.fillAmount + value, 2f).SetEase(Ease.Flash);
		yield return new WaitForSeconds(2f);
		normal.fillAmount += value;
		negative.fillAmount = normal.fillAmount;
	}

	IEnumerator EffectNegative(float value)
	{
		if (normal.fillAmount + value <= 0)
		{
			normal.fillAmount = 0f;
			positive.fillAmount = normal.fillAmount;
			negative.DOFillAmount(0f, 2f).SetEase(Ease.OutFlash);
		}
		else
		{
			normal.fillAmount += value;
			positive.fillAmount = normal.fillAmount;
			negative.DOFillAmount(normal.fillAmount, 2f).SetEase(Ease.OutFlash);
		}
		yield return new WaitForSeconds(2f);
	}

	IEnumerator EffectMedia(float value)
	{
		if(value <= 0.01f)
		{
			normal.fillAmount = 0.01f;
			positive.fillAmount = normal.fillAmount;
			negative.fillAmount = normal.fillAmount;
			yield return null;
		}
		else
		{
			positive.DOFillAmount(value, 2f).SetEase(Ease.Flash);
			AudioManagerr.Instance.PlayRightSong();
			yield return new WaitForSeconds(2f);
			normal.fillAmount = value;
			negative.fillAmount = value;
		}

		//positive.DOFillAmount(value, 2f).SetEase(Ease.Flash);
		//AudioManagerr.Instance.PlayRightSong();
		//yield return new WaitForSeconds(2f);
		//normal.fillAmount = value;
		//negative.fillAmount = value;
	}
	#endregion
}

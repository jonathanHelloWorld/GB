using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BarraEffect : MonoBehaviour
{
	public Image positive;
	public Image negative;
	public Image normal;
	public Text valueTxt;

	public Color negativeColor;

	public float scoreCurrent = 0;
	public float scoreNew = 0;
	public int Max = 200; 
	
	void Start()
    {
		GameManager.Instance.OnViewPontos += SetBarraValues;
		valueTxt.color = Color.white;
		SetBarraValues();
	}

	public void SetBarraValues()
	{
		float value = scoreNew - scoreCurrent;
		value = value / Max;

		if ((normal.fillAmount + value) >= normal.fillAmount)
		{
			StartCoroutine(EffectPositve(value));
		}
		else
		{
			StartCoroutine(EffectNegative(value));
		}

		scoreCurrent = scoreNew;
		valueTxt.text = "$"+scoreCurrent.ToString()+",00";
	}

	IEnumerator EffectPositve(float value)
	{	
		if(normal.fillAmount + value > 0.05f)
		{
			valueTxt.color = Color.white;
		}
		positive.DOFillAmount(normal.fillAmount + value,2f).SetEase(Ease.Flash);
		yield return new WaitForSeconds(2f);
		normal.fillAmount += value;
		negative.fillAmount = normal.fillAmount;
	}

	IEnumerator EffectNegative(float value)
	{
		if(normal.fillAmount + value <= 0.05f)
		{
			normal.fillAmount = 0.05f;
			positive.fillAmount = normal.fillAmount;
			negative.DOFillAmount(0f, 2f).SetEase(Ease.OutFlash);
			valueTxt.color = negativeColor;
		}
		else
		{
			normal.fillAmount += value;
			positive.fillAmount = normal.fillAmount;
			negative.DOFillAmount(normal.fillAmount, 2f).SetEase(Ease.OutFlash);
		}
		yield return new WaitForSeconds(2f);
	}
}

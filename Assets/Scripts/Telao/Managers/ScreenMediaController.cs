using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScreenMediaController : Singleton<ScreenMediaController>
{
	public GameObject prefabContainerP;
	public Transform parent;
	public Alt alternativasP;
	public Color altMaxColor;
	public Text title;

	private int atributoValue;

	private void DestroyChilds()
	{
		for(int i = 0; i < parent.childCount; i++)
		{
			var child = parent.GetChild(i);
			Destroy(child.gameObject);
		}
	}

	public void SetAlternativas(Alt alt)
	{
		alternativasP = alt;
		atributoValue = GetAtributoMax();
		ShowAlternativas();
	}

	public void ShowAlternativas()
	{
		// Reset Containers
		DestroyChilds();
		Debug.Log("showOn");

		for (int i = 0; i <alternativasP.alternativas.Count; i++)
		{
			//Instatiate Container alternativa
			GameObject obj = Instantiate(prefabContainerP, parent);
			//Get ContainerRank for manipulation
			ContainerRank container = obj.GetComponent<ContainerRank>();
			// Letter Get
			char letter = (char)(97 + i);
			// Setters ContainerRank
			container.posRankTxt.text = letter.ToString().ToUpper();
			container.nameTeamTxt.text = alternativasP.alternativas[i].texto;
			container.barraEffect.scoreNew = alternativasP.alternativas[i].media;
			container.barraEffect.SetBarraMedia();
			// Check ATributto max
			bool flag = AtributoCheck(alternativasP.alternativas[i].atributo1);
			if (flag)
				container.img.color = altMaxColor;
		}

		title.text = alternativasP.titulo;
	}

	private bool AtributoCheck(int valueAtributo)
	{
		if(atributoValue == valueAtributo)
		{
			return true;
		}
		return false;
	}

	private int GetAtributoMax()
	{
		int valueMax = alternativasP.alternativas.OrderBy(p => p.atributo1).Select(p => p.atributo1).Max();

		return valueMax;
	}

}

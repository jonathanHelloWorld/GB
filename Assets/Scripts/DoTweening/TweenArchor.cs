using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TweenArchor : MonoBehaviour {

	[SerializeField]
	private Image img;
	[SerializeField]
	private float pos;
	[SerializeField]
	private int duract;

	private void Start() {
		img = GetComponent<Image>();
		MoveLado();
	}

	void MoveLado() {
		img.rectTransform.DOAnchorPosX(pos, duract, true);
	}


}

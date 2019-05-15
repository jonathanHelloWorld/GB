using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerAlt : MonoBehaviour
{

    void Start()
    {
		GameManager.Instance.OnGameStart += CheckChilds;
    }

	public void CheckChilds()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}

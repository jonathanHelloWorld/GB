
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pagination : Singleton<Pagination>
{
    public List<CanvasGroup> pages;

    public void OpenPage(int index)
    {
        for(int i = 0; i < pages.Count; i++)
        {
            if (index == i)
            {
                StartCoroutine(TogglePage(pages[i], 1f));
                pages[i].interactable = true;
                pages[i].blocksRaycasts = true;
            }
            else
            {
                StartCoroutine(TogglePage(pages[i], 0f));
                pages[i].interactable = false;
                pages[i].blocksRaycasts = false;
            }
        }
    }

    IEnumerator TogglePage(CanvasGroup cg, float to)
    {
        float time = 0f;
        float from = cg.alpha;

        while(true)
        {
            time += Time.deltaTime;

            cg.alpha = Mathf.Lerp(from, to, time / 1f);

            if (time >= 1f)
                break;

            yield return null;
        }
    }
}
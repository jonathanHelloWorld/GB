using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class LocalizedTextKey : MonoBehaviour
{
    private SocketIOComponent socketIOComponent;
    public string key;

    void Start()
    {
        socketIOComponent = SocketIOComponent.Instance;
        //OnChangeTxt();

        SystemContent.Instance.OnChangeVersion += OnChangeTxt;

       // SystemContent.Instance.ChangeVersion();
    }

    public void OnChangeTxt()
    {
        socketIOComponent.url = LocalizationManager.instance.GetLocalizedValue(key);
    }
}

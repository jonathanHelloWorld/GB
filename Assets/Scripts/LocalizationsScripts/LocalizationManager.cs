using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
using Newtonsoft.Json;

public class LocalizationManager : MonoBehaviour {

	public static LocalizationManager instance;
	public List<string> FileNames;
	public string DataPath;

	public Dictionary<string, string> LocalizedText;

	void Awake () {
		
		if(instance == null) 
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
        LoadLocalizedText(FileNames[0]);
    }

	public void LoadLocalizedText(string fileName) {

		if(LocalizedText != null)
			LocalizedText.Clear();

		LocalizedText = new Dictionary<string, string>();
		string filePath = DataPath + fileName;
		
		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);

			//LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
			LocalizationData loadedData = JsonConvert.DeserializeObject<LocalizationData>(dataAsJson);

			for (int i = 0; i < loadedData.items.Length; i++)
			{
				LocalizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
			}

			Debug.Log("Data loaded, dictionary contains: " + LocalizedText.Count + " entries");
		}
		else
		{
			Debug.LogError("Cannot find file!");
		}

	}

	public string GetLocalizedValue(string key)
	{
		string result = null;
		if (LocalizedText.ContainsKey(key))
		{
			result = LocalizedText[key];
		}

		return result;
	}

}

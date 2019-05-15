using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedImageKey : MonoBehaviour
{
	private Image _image;
	private int width = 399, height = 399;

	public string key;

    void Start()
    {
		_image = GetComponent<Image>();
		OnChanceImage();

		//SystemContent.Instance.OnChangeVersion += OnChanceImage;
    }
    
	public void OnChanceImage()
	{
		string filePath = LocalizationManager.instance.GetLocalizedValue(key);

		byte[] bytes = File.ReadAllBytes(filePath);
		Texture2D texture = new Texture2D(width, height);
		texture.filterMode = FilterMode.Trilinear;
		texture.LoadImage(bytes);
		Sprite sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.0f), 1.0f);

		_image.sprite = sprite;
	}

}

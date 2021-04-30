using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetImage : MonoBehaviour
{
	public string pathSelected = "";

	public GameObject placeImage;

	public InputField cardContentInputField;
	public string cardContentTextSave;

    public void onClickGetImage() {
		//open gallery for user to select image
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				UploadNewProfileImage(path);
			}
		}, "Select an image", "image/png");

		Debug.Log("Permission result: " + permission);
	}

	//get image path and display image
	public void UploadNewProfileImage(string newPath) {

		Texture2D texture = NativeGallery.LoadImageAtPath(newPath);
		if (texture == null)
		{
			Debug.Log("Couldn't load texture from " + newPath);
			return;
		}

		pathSelected = newPath;
		placeImage.SetActive(true);
		placeImage.GetComponent<RawImage>().texture = texture;

		cardContentTextSave = cardContentInputField.text;
		cardContentInputField.text = "";
		cardContentInputField.interactable = false;
	}

	public void onClickRemoveImage()
	{
		pathSelected = "";
		cardContentInputField.text = cardContentTextSave;
		cardContentInputField.interactable = true;
		placeImage.SetActive(false);
	}
}
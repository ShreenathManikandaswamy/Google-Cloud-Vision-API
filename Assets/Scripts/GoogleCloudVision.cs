using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GoogleCloudVision : MonoBehaviour
{

	public string url = "https://vision.googleapis.com/v1/images:annotate?key=";
	public Keys keys;
	public float captureIntervalSeconds = 5.0f;
	public int requestedWidth = 640;
	public int requestedHeight = 480;
	public FeatureType featureType = FeatureType.TEXT_DETECTION;
	public int maxResults = 10;

	public GoogleVisionManager googleVision;

	Color[] pixels;
	string apiKey = "";
	Texture2D texture2D;
	Dictionary<string, string> headers;

	[System.Serializable]
	public class AnnotateImageRequests
	{
		public List<AnnotateImageRequest> requests;
	}

	[System.Serializable]
	public class AnnotateImageRequest
	{
		public Image image;
		public List<Feature> features;
	}

	[System.Serializable]
	public class Image
	{
		public string content;
	}

	[System.Serializable]
	public class Feature
	{
		public string type;
		public int maxResults;
	}

	public enum FeatureType
	{
		TYPE_UNSPECIFIED,
		FACE_DETECTION,
		LANDMARK_DETECTION,
		LOGO_DETECTION,
		LABEL_DETECTION,
		TEXT_DETECTION,
		SAFE_SEARCH_DETECTION,
		IMAGE_PROPERTIES,
		OBJECT_LOCALIZATION
	}

	// Use this for initialization
	void Start()
	{
		apiKey = keys.googleApiKey;
		headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json; charset=UTF-8");

		if (apiKey == null || apiKey == "")
			Debug.LogError("No API key. Please set your API key into the \"Web Cam Texture To Cloud Vision(Script)\" component.");

		StartCoroutine(Capture());
	}

	void ProcessResponse(string response)
	{
		switch (featureType)
		{
			case FeatureType.LOGO_DETECTION:
				ProcessLogoDetectionResponse(response);
				break;
			case FeatureType.OBJECT_LOCALIZATION:
				ProcessMultiDetectionResponse(response);
				break;
		}
	}

	public void ProcessLogoDetectionResponse(string response)
	{
		LogoAnnotationResponse annotations = LogoAnnotationResponse.FromJson(response);
		Debug.Log(annotations.Responses[0].LogoAnnotations[0].Description);
	}

	public void ProcessMultiDetectionResponse(string response)
	{
		MultiAnnotationsResponseData multiAnnotations = JsonConvert.DeserializeObject<MultiAnnotationsResponseData>(response);
		googleVision.ShowObject(multiAnnotations);
	}

	public void SendVuforiaCameraData(Texture2D camData)
    {
		texture2D = camData;
		pixels = camData.GetPixels();
	}

	#region Google Cloud Vision API

	private IEnumerator Capture()
	{
		while (true)
		{
			if (this.apiKey == null)
				yield return null;

			yield return new WaitForSeconds(captureIntervalSeconds);

			if (texture2D != null && pixels != null)
			{
				texture2D.SetPixels(pixels);
				// texture2D.Apply(false); // Not required. Because we do not need to be uploaded it to GPU
				byte[] jpg = texture2D.EncodeToJPG();
				string base64 = System.Convert.ToBase64String(jpg);
				// #if UNITY_WEBGL	
				// 			Application.ExternalCall("post", this.gameObject.name, "OnSuccessFromBrowser", "OnErrorFromBrowser", this.url + this.apiKey, base64, this.featureType.ToString(), this.maxResults);
				// #else

				AnnotateImageRequests requests = new AnnotateImageRequests();
				requests.requests = new List<AnnotateImageRequest>();

				AnnotateImageRequest request = new AnnotateImageRequest();
				request.image = new Image();
				request.image.content = base64;
				request.features = new List<Feature>();
				Feature feature = new Feature();
				feature.type = this.featureType.ToString();
				feature.maxResults = this.maxResults;
				request.features.Add(feature);
				requests.requests.Add(request);

				string jsonData = JsonUtility.ToJson(requests, false);
				if (jsonData != string.Empty)
				{
					string url = this.url + this.apiKey;
					byte[] postData = System.Text.Encoding.Default.GetBytes(jsonData);
					using (WWW www = new WWW(url, postData, headers))
					{
						yield return www;
						if (string.IsNullOrEmpty(www.error))
						{
							string responses = www.text.Replace("\n", "").Replace(" ", "");
							Debug.Log(responses);
							ProcessResponse(responses);
						}
						else
						{
							Debug.Log("Error: " + www.error);
						}
					}
				}
			}
		}
	}

#if UNITY_WEBGL
	void OnSuccessFromBrowser(string jsonString) {
		Debug.Log(jsonString);	
	}

	void OnErrorFromBrowser(string jsonString) {
		Debug.Log(jsonString);	
	}
#endif

	#endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
using UnityEngine.Networking;

public class DownloadDataFile : MonoBehaviour {
	public Slider progressBAr;
	string url = "https://www.dropbox.com/s/axwa1v41hmjox6q/jsonData.json?dl=1";
	string data;
	JsonData jsonObj;
	string pathl;
	// Use this for initialization
	void Start () {
		pathl = Path.Combine (Application.persistentDataPath, "jsonData.json");
		StartCoroutine (DownloadJson( url));
	}

	IEnumerator DownloadJson(string url){
		WWW www = new WWW (url);
		yield return www;
		progressBAr.value = www.progress;
		if (www.error == null) {
			File.WriteAllText (pathl,www.text);
			jsonObj = JsonMapper.ToObject (www.text.Trim());
			for(int i = 0; i< jsonObj["atomData"].Count; i++){
				string name_ = jsonObj ["atomData"] [i] ["atomName"].ToString ()+".jpg";
				string url_ = jsonObj ["atomData"] [i] ["iconUrl"].ToString ();
				yield return StartCoroutine (DownloadIcon( url_,  name_));
			}
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (1);
		}
	}

	IEnumerator DownloadIcon(string url, string Name){
		
		WWW www = new WWW (url);
		yield return www;
		progressBAr.value = www.progress;
		if (www.error == null) {
			string pathl = Path.Combine (Application.persistentDataPath, Name);
			File.WriteAllBytes (pathl,www.bytes);

		}
	}


	// Update is called once per frame
	void Update () {
		
	}
}

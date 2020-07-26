using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;

public class GenrateAtomList : MonoBehaviour
{
	public PhotonView pv;
	GameObject obj;
	public Text noFile;
	public Transform content;
	public GameObject buttonPrefab;
	public static string pathl;
	string data;
	JsonData jsonObj;

	public void Awake ()
	{
		PhotonNetwork.offlineMode = true;
		pv = gameObject.GetComponent<PhotonView> ();
		pathl = Path.Combine (Application.persistentDataPath, "jsonData.json");
	}
	// Use this for initialization
	void Start ()
	{
		if (File.Exists (pathl)) {
			noFile.text = "";
			StartCoroutine (ReadFile());
		} 
		else {
			noFile.text = "No file Present Plz Turn on your internet\nAnd restart the app !!!";
		}
	}

	IEnumerator ReadFile(){
		data = File.ReadAllText (pathl);
		Debug.Log (data);
		yield return new WaitForEndOfFrame ();
		jsonObj = JsonMapper.ToObject (data.Trim());
		for(int i = 0; i < jsonObj["atomData"].Count; i++){
			yield return null;
			obj = Instantiate (buttonPrefab);
			obj.GetComponentInChildren<Text> ().text = jsonObj ["atomData"] [i] ["atomName"].ToString ();
			//obj.GetComponentInChildren<Image> ().sprite = Sprite.Create(
			int id = int.Parse (jsonObj ["atomData"] [i] ["id"].ToString ());
			Debug.Log ("id is "+ id);
			string p = Path.Combine (Application.persistentDataPath,jsonObj ["atomData"] [i] ["atomName"].ToString ()+".jpg");
			StartCoroutine (LoadIcons(obj.GetComponentInChildren<Image> (),p));
			obj.transform.SetParent (content,false);

			Button btn = obj.GetComponent<Button> ();
			btn.onClick.AddListener(delegate {ButtonClick(id); });
		}
	}

	IEnumerator LoadIcons(Image icon,string path){
		WWW www;
		#if UNITY_EDITOR
		www = new WWW("file:///"+path);
		#else
		www = new WWW("file://"+path);
		#endif
		yield return www;
		byte[] fileData = www.bytes;
		Texture2D tex = new Texture2D(2, 2);
		tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		icon.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
	}

	public void ButtonClick(int id){
		if (PhotonNetwork.offlineMode == true) {
			TaskWithParameters(id);
		} 
		else {
			Debug.Log ("rpc called !!!");
			pv.RPC ("TaskWithParameters", PhotonTargets.AllBufferedViaServer,id);
		}
	}

	[PunRPC]
	void TaskWithParameters(int buttonNo)
	{
		//Output this to console when the Button3 is clicked
		Debug.Log("Button clicked = " + buttonNo);
		PlayerPrefs.SetString ("roomName", ReturnAtomName (buttonNo));
		PlayerPrefs.SetInt ("ID",buttonNo);
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (2);
	}
	string ReturnAtomName(int id){
		int _ID;
		if(!PlayerPrefs.HasKey("ID")){
			Debug.Log ("Dont have the key !!!");
			return null;
		}
		data = File.ReadAllText (GenrateAtomList.pathl);
		jsonObj = JsonMapper.ToObject (data);
		for(int i =0; i< jsonObj["atomData"].Count; i++){
			_ID = int.Parse (jsonObj ["atomData"] [i] ["id"].ToString ());
			if (_ID == PlayerPrefs.GetInt ("ID")) {
				return jsonObj ["atomData"] [i] ["atomName"].ToString ();
			}
		}
		return null;
	}
}

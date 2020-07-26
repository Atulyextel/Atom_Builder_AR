using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class InfoUpdate : MonoBehaviour {
	string data;
	JsonData jsonObj;
	string pathl ;
	public Text infoText;
	// Use this for initialization
	void Start () {
		pathl = Path.Combine (Application.persistentDataPath, "jsonData.json");
		infoText.text = "Make a "+ ReturnAtomName(PlayerPrefs.GetInt ("ID"))+" atom. This atom contains "+ReturnAtomElectrons(PlayerPrefs.GetInt ("ID"))+" electrons. Think about how many protrons this atom should contain, and try to guess how many nutrons it can contain.";
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

	int ReturnAtomElectrons(int id){
		int _ID;
		if(!PlayerPrefs.HasKey("ID")){
			Debug.Log ("Dont have the key !!!");
			return 0;
		}
		data = File.ReadAllText (GenrateAtomList.pathl);
		jsonObj = JsonMapper.ToObject (data);
		for(int i = 0; i< jsonObj["atomData"].Count; i++){
			_ID = int.Parse (jsonObj ["atomData"] [i] ["id"].ToString ());
			if (_ID == PlayerPrefs.GetInt ("ID")) {
				return int.Parse(jsonObj ["atomData"] [i] ["noOfElectrons"].ToString ());
			}
		}
		return 0;
	}
}

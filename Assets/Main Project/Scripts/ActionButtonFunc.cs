using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class ActionButtonFunc : MonoBehaviour {
	//public Camera cam;
	public float fuseFactor = 0.2f;
	public float fuseThreshold = 0.8f;
	public static int pro;
	public static int nut;
	public static int elec;
	public GameObject fuseButton;
	public GameObject exitButton;
	public GameObject completeButton;
	public GameObject arButton;
	public Transform atom;
	public Transform nucleus;
	public Transform electron_;
	public Text infoText;
	public Button subElectron;
	public Button addElectron;
	public Button subProtron;
	public Button addProtron;
	public Button subNutron;
	public Button addNutron;

	string data;
	JsonData jsonObj;
	string pathl ;
	public void LeaveRoom()
	{
		Debug.Log ("Room left !!!");
		PhotonNetwork.LeaveRoom();
	}
	void Awake(){
		pathl = Path.Combine (Application.persistentDataPath, "jsonData.json");
	}
	[PunRPC]
	public void FuseAction(){
		if (pro != ReturnAtomProtrons (PlayerPrefs.GetInt ("ID"))) {
			infoText.text = "Wrong Config. check no of protrons";
		}
		else if (nut != ReturnAtomNutrons (PlayerPrefs.GetInt ("ID"))) {
			infoText.text = "Wrong Config. check no of nutrons";
		} 
		else {
			
			addElectron.interactable = true;
			subElectron.interactable = true;
			subProtron.interactable = false;
			addProtron.interactable = false;
			subNutron.interactable = false;
			addNutron.interactable = false;
			MyPartcleMovement dummyMyPartcleMovement;
			foreach(Transform n in nucleus){
				dummyMyPartcleMovement = n.gameObject.GetComponent<MyPartcleMovement> ();
				if(dummyMyPartcleMovement != null){
					dummyMyPartcleMovement.enabled = false;
				}
			}
			foreach(Transform x in nucleus){
				if (x.position.x > fuseThreshold) {
					x.position = new Vector3 ((x.position.x- fuseFactor),x.position.y,x.position.z);
				}
				if (x.position.y > fuseThreshold) {
					x.position = new Vector3 (x.position.x,(x.position.y - fuseFactor),x.position.z);
				}
				if (x.position.z > fuseThreshold) {
					x.position = new Vector3 (x.position.x,x.position.y,(x.position.z - fuseFactor));
				}
				if (x.position.x < -fuseThreshold) {
					x.position = new Vector3 ((x.position.x + fuseFactor),x.position.y,x.position.z);
				}
				if (x.position.y < -fuseThreshold) {
					x.position = new Vector3 (x.position.x,(x.position.y + fuseFactor),x.position.z);
				}
				if (x.position.z < -fuseThreshold) {
					x.position = new Vector3 (x.position.x,x.position.y,(x.position.z + fuseFactor));
				}
			}
			infoText.text = "Correct Now Add Electrons to the Atom";
			completeButton.SetActive (true);
		}
	}
	[PunRPC]
	public void ArView(){
		atom.localScale = new Vector3(.1f,.1f,.1f);
		atom.position = new Vector3 (1000,1000,1000);
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (3);
	}
	[PunRPC]
	public void ExitAction(){
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (1);
		Debug.Log ("Room left !!!");
		PhotonNetwork.LeaveRoom();
	}
	int ReturnAtomNutrons(int id){
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
				return int.Parse(jsonObj ["atomData"] [i] ["noOfNutrons"].ToString ());
			}
		}
		return 0;
	}
	[PunRPC]
	public void CompleteAction(){
		if (elec == ReturnAtomElectrons (PlayerPrefs.GetInt ("ID"))) {
			infoText.text = "Atom completed !!!";
			fuseButton.SetActive (false);
			completeButton.SetActive (false);
			arButton.SetActive (true);

			MyPartcleMovement1 dummyMyPartcleMovement1;
			foreach(Transform e in electron_){
				dummyMyPartcleMovement1 = e.gameObject.GetComponent<MyPartcleMovement1> ();
				if(dummyMyPartcleMovement1 != null){
					dummyMyPartcleMovement1.enabled = false;
				}
			}
			nucleus.SetParent (atom);
			electron_.SetParent (atom);
			atom.localScale = new Vector3(.8f,.8f,.8f);
			//cam.fieldOfView = 30;
		} else {
			infoText.text = "Wrong no. of electrons !!!";
		}
	}

	int ReturnAtomProtrons(int id){
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
				return int.Parse(jsonObj ["atomData"] [i] ["noOfProtrons"].ToString ());
			}
		}
		return 0;
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

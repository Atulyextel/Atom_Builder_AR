using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class EditElectrons : MonoBehaviour {

	public Transform electron_;
	public GameObject electronPrefab;
	public Text electronText;
	public Transform electronCollection;
	public Text infoText;
	int x;
	List<GameObject> nutronsDisabled;
	//public static List<GameObject> atomDisabled;
	bool foundProtron;
	GameObject obj;
//	public Button subElectron;
//	public Button addElectron;
	string data;
	JsonData jsonObj;
	string pathl ;

	void Awake(){
		//atomDisabled = new List<GameObject> ();
		nutronsDisabled = new List<GameObject> ();
		pathl = Path.Combine (Application.persistentDataPath, "jsonData.json");
	}
		
	int electron;
	// Use this for initialization
	void Start () {
		electron = 0;
	}

	[PunRPC]
	public void AddNutron () {
		if(electron >= 12){
			electron = 12;
			return;
		}
		infoText.text = "";
		++electron;
		foreach (Transform panelchild in electronCollection) {
			foreach (Transform protron in panelchild) {
				if (protron.gameObject.activeInHierarchy) {
					nutronsDisabled.Add (protron.gameObject);
					protron.gameObject.SetActive (false);
					foundProtron = true;
					obj = Instantiate (electronPrefab, protron.position, protron.rotation);
					obj.transform.SetParent (electron_);
					obj.GetComponent<MyPartcleMovement1> ().target = new Vector3 ((3+(electron * 1.5f)),0,0);
					break;
				}
			}
			if (foundProtron) {
				break;
			}
		}
		foundProtron = false;
		NutronUpdate ();
	}
	[PunRPC]
	public void SubNutron () {
		if(electron <= 0){
			electron = 0;
			return;
		}
		infoText.text = "";

		--electron;
		if(electron_.childCount >0){
			Destroy( electron_.GetChild(electron_.childCount-1).gameObject);

		}

		if(nutronsDisabled.Count > 0){
			nutronsDisabled [(nutronsDisabled.Count - 1)].SetActive (true);
			nutronsDisabled.RemoveAt ((nutronsDisabled.Count - 1));
		}
		if(EditProtrons.atomDisabled.Count > 0){
			EditProtrons.atomDisabled [(EditProtrons.atomDisabled.Count - 1)].SetActive (true);
			EditProtrons.atomDisabled.RemoveAt ((EditProtrons.atomDisabled.Count - 1));
		}
		NutronUpdate ();
	}

	void NutronUpdate () {
		ActionButtonFunc.elec = electron;
		electronText.text = electron.ToString ();
		Debug.Log ("retured value is " + ReturnAtomElectrons (PlayerPrefs.GetInt ("ID")));
//		if (ReturnAtomElectrons (PlayerPrefs.GetInt ("ID")) == electron) {
//			Debug.Log ("hi");
////			addElectron.interactable = true;
////			subElectron.interactable = true;
//
//			//infoText.text = "Atom completed !!!";
//		}
//		else {
//			Debug.Log ("bye");
//			if(addElectron.IsInteractable()){
//				addElectron.interactable = false;
//			}
//			if(subElectron.IsInteractable()){
//				subElectron.interactable = false;
//			}
//
//			infoText.text = "";
//		}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class EditNutrons : MonoBehaviour {
	float x_;
	float y;
	float z;
	public float upLm = 1.2f;
	public float lowLm = -1.2f;
	public Transform atom;
	public GameObject nutronPrefab;
	public Text noOfProtronAndNutron;
	public Text nutronText;
	public Transform nutronCollection;
	public Text infoText;
	int x;
	List<GameObject> nutronsDisabled;
	//public static List<GameObject> atomDisabled;
	bool foundProtron;
	GameObject obj;
//	public Button subElectron;
//	public Button addElectron;
	bool isAdd;
	string data;
	JsonData jsonObj;
	string pathl ;

	void Awake(){
		//atomDisabled = new List<GameObject> ();
		nutronsDisabled = new List<GameObject> ();
		pathl = Path.Combine (Application.persistentDataPath, "jsonData.json");
	}
		
	int nutron;
	// Use this for initialization
	void Start () {
		nutron = 0;
	}

	[PunRPC]
	public void AddNutron () {
		if(nutron >= 12){
			nutron = 12;
			return;
		}
		infoText.text = "";
		isAdd = true;
		++nutron;
		foreach (Transform panelchild in nutronCollection) {
			foreach (Transform protron in panelchild) {
				if (protron.gameObject.activeInHierarchy) {
					nutronsDisabled.Add (protron.gameObject);
					protron.gameObject.SetActive (false);
					foundProtron = true;
					x_ = Random.Range(lowLm,upLm);
					y = Random.Range(lowLm,upLm);
					z = Random.Range(lowLm,upLm);
					obj = Instantiate (nutronPrefab, protron.position, protron.rotation);
					obj.GetComponent<MyPartcleMovement> ().target = new Vector3(x_,y,z);
					obj.transform.SetParent (atom);
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
		if(nutron <= 0){
			nutron = 0;
			return;
		}
		infoText.text = "";
		isAdd = false;
		--nutron;
		if(atom.childCount >0){
			for(int i = (atom.childCount-1); i>=0;i--){
				if(atom.GetChild(i).gameObject.CompareTag("N1")){
					Destroy( atom.GetChild(i).gameObject);
					break;
				}
			}
		}

		if(nutronsDisabled.Count > 0){
			nutronsDisabled [(nutronsDisabled.Count - 1)].SetActive (true);
			nutronsDisabled.RemoveAt ((nutronsDisabled.Count - 1));
		}
		NutronUpdate ();
	}

	public void ResetNutron(){
		if(atom.childCount >0){
			for(int i = (atom.childCount-1); i>=0;i--){
				if(atom.GetChild(i).gameObject.CompareTag("N1")){
					Destroy( atom.GetChild(i).gameObject);
				}
			}
		}
		if(nutronsDisabled.Count > 0){
			for (int i = (nutronsDisabled.Count - 1); i >= 0; i--) {
				nutronsDisabled [i].SetActive (true);
				nutronsDisabled.RemoveAt (i);
			}
		}
		nutronText.text = "0";
	}

	void NutronUpdate () {
		ActionButtonFunc.nut = nutron;
		nutronText.text = nutron.ToString ();
		if (isAdd) {
			x = int.Parse (noOfProtronAndNutron.text) + 1;
		} else {
			x = int.Parse(noOfProtronAndNutron.text) - 1;
		}
		Debug.Log ("x is " + x);
		noOfProtronAndNutron.text = x.ToString();
		Debug.Log ("retured value is " + ReturnAtomAtomicMass (PlayerPrefs.GetInt ("ID")));
	}

	int ReturnAtomAtomicMass(int id){
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
				return int.Parse(jsonObj ["atomData"] [i] ["atomicMass"].ToString ());
			}
		}
		return 0;
	}
}

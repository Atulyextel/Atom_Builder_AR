using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class EditProtrons : MonoBehaviour
{
	float x;
	float y;
	float z;
	public float upLm = 1.2f;
	public float lowLm = -1.2f;
	GameObject obj;
	public EditNutrons editNutrons;
	public Transform atom;
	public GameObject protronPrefab;
	public InputField nameOfAtom;
	public Text noOfProtron;
	public Text noOfProtronAndNutron;
	public Text sysmbolOfAtom;
	public Text protronText;
	public Transform PrortonCollection;
	public Text infoText;
	List<GameObject> protronsDisabled;
	public static List<GameObject> atomDisabled;
	public Button subNutron;
	public Button addNutron;
	bool foundProtron;
	string data;
	JsonData jsonObj;
	string pathl;

	void Awake ()
	{
		atomDisabled = new List<GameObject> ();
		protronsDisabled = new List<GameObject> ();
		pathl = Path.Combine (Application.persistentDataPath, "jsonData.json");
	}

	public enum ProtronConfig
	{
		Empty,
		Hydrogen_H,
		Helium_He,
		Lithium_Li,
		Beryllium_Be,
		Boron_B,
		Carbon_C,
		Nitrogen_N,
		Oxygen_O,
		Fluorine_F,
		Neon_Ne,
		Sodium_Na
	}

	public static ProtronConfig protronConf;
	int protron;
	// Use this for initialization
	void Start ()
	{
		protron = 0;
	}

	[PunRPC]
	public void AddProtron ()
	{
		if (protron >= 11) {
			protron = 11;
			return;
		}
		editNutrons.ResetNutron ();
		infoText.text = "";
		++protron;
		foreach (Transform panelchild in PrortonCollection) {
			foreach (Transform protron in panelchild) {
				if (protron.gameObject.activeInHierarchy) {
					protronsDisabled.Add (protron.gameObject);
					protron.gameObject.SetActive (false);
					foundProtron = true;
					obj = Instantiate (protronPrefab, protron.position, protron.rotation);
					x = Random.Range(lowLm,upLm);
					y = Random.Range(lowLm,upLm);
					z = Random.Range(lowLm,upLm);
					obj.GetComponent<MyPartcleMovement> ().target = new Vector3 (x,y,z);
					obj.transform.SetParent (atom);
					break;
				}
			}
			if (foundProtron) {
				break;
			}
		}
		foundProtron = false;
		ProtronUpdate ();
	}
	[PunRPC]
	public void SubProtron ()
	{
		if (protron <= 0) {
			protron = 0;
			return;
		}
		editNutrons.ResetNutron ();
		infoText.text = "";
		--protron;
		if(atom.childCount >0){
			for(int i = (atom.childCount-1); i>=0;i--){
				if(atom.GetChild(i).gameObject.CompareTag("P1")){
					Destroy( atom.GetChild(i).gameObject);
					break;
				}
			}
		}

		if(protronsDisabled.Count > 0){
			protronsDisabled [(protronsDisabled.Count - 1)].SetActive (true);
			protronsDisabled.RemoveAt ((protronsDisabled.Count - 1));
		}
		ProtronUpdate ();
	}

	void ProtronUpdate ()
	{
		ActionButtonFunc.pro = protron;
		protronText.text = protron.ToString ();
		protronConf = (ProtronConfig)protron;
		switch (protronConf) {
		case ProtronConfig.Empty:
			//Debug.Log ("Empty Config");
			nameOfAtom.text = "--------------------";
			sysmbolOfAtom.text = "--";
			noOfProtron.text = "0";
			noOfProtronAndNutron.text = "0";
			break;
		case ProtronConfig.Hydrogen_H:
			//Debug.Log ("Hydrogen_H Config");
			nameOfAtom.text = "Hydrogen";
			sysmbolOfAtom.text = "H";
			noOfProtron.text = "1";
			noOfProtronAndNutron.text = "1";
			break;
		case ProtronConfig.Helium_He:
			Debug.Log ("Helium_He Config");
			nameOfAtom.text = "Helium";
			sysmbolOfAtom.text = "He";
			noOfProtron.text = "2";
			noOfProtronAndNutron.text = "2";
			break;
		case ProtronConfig.Lithium_Li:
			Debug.Log ("Lithium_Li Config");
			nameOfAtom.text = "Lithium";
			sysmbolOfAtom.text = "Li";
			noOfProtron.text = "3";
			noOfProtronAndNutron.text = "3";
			break;
		case ProtronConfig.Beryllium_Be:
			Debug.Log ("Beryllium_Be Config");
			nameOfAtom.text = "Beryllium";
			sysmbolOfAtom.text = "Be";
			noOfProtron.text = "4";
			noOfProtronAndNutron.text = "4";
			break;
		case ProtronConfig.Boron_B:
			Debug.Log ("Boron_B Config");
			nameOfAtom.text = "Boron";
			sysmbolOfAtom.text = "B";
			noOfProtron.text = "5";
			noOfProtronAndNutron.text = "5";
			break;
		case ProtronConfig.Carbon_C:
			Debug.Log ("Carbon_C Config");
			nameOfAtom.text = "Carbon";
			sysmbolOfAtom.text = "C";
			noOfProtron.text = "6";
			noOfProtronAndNutron.text = "6";
			break;
		case ProtronConfig.Nitrogen_N:
			Debug.Log ("Nitrogen_N Config");
			nameOfAtom.text = "Nitrogen";
			sysmbolOfAtom.text = "N";
			noOfProtron.text = "7";
			noOfProtronAndNutron.text = "7";
			break;
		case ProtronConfig.Oxygen_O:
			Debug.Log ("Oxygen_O Config");
			nameOfAtom.text = "Oxygen";
			sysmbolOfAtom.text = "O";
			noOfProtron.text = "8";
			noOfProtronAndNutron.text = "8";
			break;
		case ProtronConfig.Fluorine_F:
			Debug.Log ("Fluorine_F Config");
			nameOfAtom.text = "Fluorine";
			sysmbolOfAtom.text = "F";
			noOfProtron.text = "9";
			noOfProtronAndNutron.text = "9";
			break;
		case ProtronConfig.Neon_Ne:
			Debug.Log ("Neon_Ne Config");
			nameOfAtom.text = "Neon";
			sysmbolOfAtom.text = "Ne";
			noOfProtron.text = "10";
			noOfProtronAndNutron.text = "10";
			break;
		case ProtronConfig.Sodium_Na:
			Debug.Log ("Sodium_Na Config");
			nameOfAtom.text = "Sodium";
			sysmbolOfAtom.text = "Na";
			noOfProtron.text = "11";
			noOfProtronAndNutron.text = "11";
			break;
		}
	}
}

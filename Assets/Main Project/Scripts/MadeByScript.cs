using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
public class MadeByScript : MonoBehaviour {
//	string jsonData;
//	public static string pathl;
//	public void Awake(){
//		pathl = Path.Combine (Application.persistentDataPath,"jsonData.json");
//	}
//	public void Start(){
//		pathl = Path.Combine (Application.persistentDataPath,"jsonData.json");
//		AtomClass Atoms = new AtomClass (2);
//		for(int i=0; i<2;i++){
//			Atoms.atomData [i] = new DataClass ();
//		}
//		jsonData = JsonMapper.ToJson (Atoms);
//		Debug.Log (jsonData);
//		File.WriteAllText (pathl,jsonData);
//		Debug.Log ("path is "+ pathl);
//	}
	public void MadeBy(){
		Application.OpenURL ("https://www.linkedin.com/in/atul-kumar-singh-911845113/");
	}
}

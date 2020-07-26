using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCMapper1 : MonoBehaviour {
	public EditNutrons editNutrons;
	public PhotonView pv;

	void Start(){
		pv = gameObject.GetComponent<PhotonView> ();
	}

	public void AddNuton(){
		if (PhotonNetwork.offlineMode == true) {
			editNutrons.AddNutron ();
		} 
		else {
			pv.RPC ("AddNutron", PhotonTargets.AllBufferedViaServer, null);
		}
	}
	public void SubNuton(){
		if (PhotonNetwork.offlineMode == true) {
			editNutrons.SubNutron ();
		} 
		else {
			pv.RPC ("SubNutron", PhotonTargets.AllBufferedViaServer, null);
		}
	}
}

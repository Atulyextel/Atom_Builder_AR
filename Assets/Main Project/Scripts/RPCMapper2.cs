using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCMapper2 : MonoBehaviour {
	public EditElectrons editElectrons;
	public PhotonView pv;

	void Start(){
		pv = gameObject.GetComponent<PhotonView> ();
	}

	public void AddElecton(){
		if (PhotonNetwork.offlineMode == true) {
			editElectrons.AddNutron ();
		} 
		else {
			pv.RPC ("AddNutron", PhotonTargets.AllBufferedViaServer, null);
		}
	}
	public void SubElecton(){
		if (PhotonNetwork.offlineMode == true) {
			editElectrons.SubNutron ();
		} 
		else {
			pv.RPC ("SubNutron", PhotonTargets.AllBufferedViaServer, null);
		}
	}
}

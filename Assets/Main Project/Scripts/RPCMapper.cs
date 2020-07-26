using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCMapper : MonoBehaviour {
	public EditProtrons editProtrons;
	public PhotonView pv;

	void Start(){
		pv = gameObject.GetComponent<PhotonView> ();
	}

	public void AddProton(){
		if (PhotonNetwork.offlineMode == true) {
			editProtrons.AddProtron ();
		} 
		else {
			pv.RPC ("AddProtron", PhotonTargets.AllBufferedViaServer, null);
		}
	}
	public void SubProton(){
		if (PhotonNetwork.offlineMode == true) {
			editProtrons.SubProtron ();
		} 
		else {
			pv.RPC ("SubProtron", PhotonTargets.AllBufferedViaServer, null);
		}
	}
}

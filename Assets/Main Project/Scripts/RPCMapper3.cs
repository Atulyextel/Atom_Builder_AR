using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCMapper3 : MonoBehaviour {
	public ActionButtonFunc actionButtonFunc;
	public PhotonView pv;

	void Start(){
		pv = gameObject.GetComponent<PhotonView> ();
	}

	public void FuseAction(){
		if (PhotonNetwork.offlineMode == true) {
			actionButtonFunc.FuseAction ();
		} 
		else {
			pv.RPC ("FuseAction", PhotonTargets.AllBufferedViaServer, null);
		}
	}
	public void CompleteAction(){
		if (PhotonNetwork.offlineMode == true) {
			actionButtonFunc.CompleteAction ();
		} 
		else {
			pv.RPC ("CompleteAction", PhotonTargets.AllBufferedViaServer, null);
		}
	}
	public void ExitAction(){
		if (PhotonNetwork.offlineMode == true) {
			actionButtonFunc.ExitAction ();
		} 
		else {
			pv.RPC ("ExitAction", PhotonTargets.AllBufferedViaServer, null);
		}
	}

	public void ArView(){
		if (PhotonNetwork.offlineMode == true) {
			actionButtonFunc.CompleteAction ();
		} 
		else {
			pv.RPC ("ArView", PhotonTargets.AllBufferedViaServer, null);
		}
	}
}

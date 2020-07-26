using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class MyPartcleMovement1 : MonoBehaviour {
	public Transform ps;
	public Vector3 target;
	public float speed = 5f;
	void Start () {
		ps.position = target;
	}
//	void Update () {
//		if((int)ps.position.y == -8){
//			gameObject.GetComponent<MyPartcleMovement1> ().enabled = false;
//			//gameObject.GetComponent<Orbit> ().enabled = true;
//		}
//		float step = speed * Time.deltaTime;
//		ps.position = Vector3.LerpUnclamped(ps.position, target.position, step);
//	}
}

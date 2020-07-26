using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
	Transform centerMass;
	public int speed;
	// Use this for initialization
	void Start () {
		speed = Random.Range (60,120);
		centerMass = GameObject.FindGameObjectWithTag("Atom").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		OrbitAround ();
		
	}
	void OrbitAround(){
		transform.RotateAround (centerMass.position, Vector3.up, speed * Time.deltaTime);
	}
}

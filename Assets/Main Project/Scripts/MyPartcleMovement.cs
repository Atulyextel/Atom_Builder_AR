using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class MyPartcleMovement : MonoBehaviour {
	public Transform ps;
//	ParticleSystem.Particle[] m_Particles;
	public Vector3 target;
	public float speed = 5f;
	int numParticlesAlive;
	void Start () {
//		ps = GetComponent<ParticleSystem>();
//		if (!GetComponent<Transform>()){
//			GetComponent<Transform>();
//		}
	}
	void Update () {
//		m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
//		numParticlesAlive = ps.GetParticles(m_Particles);
		float step = speed * Time.deltaTime;
		ps.position = Vector3.LerpUnclamped(ps.position, target, step);
	}
}

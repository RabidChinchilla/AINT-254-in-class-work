using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour {

    private Transform m_cubeTransform;
    private Rigidbody m_cubeRigidBody;

    [SerializeField]
    [Range(0.1f, 200)]
    private float m_speed = 5.0f;


	// Use this for initialization
	void Start ()
    {
        m_cubeTransform = transform;
        m_cubeRigidBody = m_cubeTransform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //m_cubeTransform.position = m_cubeTransform.position + (m_cubeTransform.forward * m_speed * Time.deltaTime);
        m_cubeRigidBody.velocity = m_cubeTransform.forward * m_speed * Time.deltaTime;

        //m_cubeRigidBody.AddForce(m_cubeTransform.forward * m_speed * Time.deltaTime);
	}
}

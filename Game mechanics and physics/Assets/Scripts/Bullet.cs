using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody m_bulletRigid;

	// Use this for initialization
	void Start () {

        m_bulletRigid = GetComponent<Rigidbody>();

        m_bulletRigid.AddForce(transform.forward * 50.0f, ForceMode.Impulse);

        //Invoke("Destroy", 100.0f);
	}
	
	// Update is called once per frame
	void Update () {
        //Destroy(gameObject);
	}
}

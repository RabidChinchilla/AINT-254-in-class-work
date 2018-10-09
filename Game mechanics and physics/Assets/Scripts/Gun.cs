using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, transform.position + transform.forward * 3.0f, transform.rotation);
        }
	}
}

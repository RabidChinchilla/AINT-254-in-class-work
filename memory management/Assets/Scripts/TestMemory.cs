using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMemory : MonoBehaviour {

    private Transform m_transform;
    public GameObject prefab;
    private GameObject[] prefabs;

	// Use this for initialization
	void Start ()
    {
        m_transform = transform;

        prefabs = new GameObject[1000];

        for (int i = 0; i < 1000; i++)
        {
            prefabs[i] = Instantiate(prefab);
            prefabs[i].SetActive(false);
        }

        string stringOne = "Kenji";
        string stringTwo = "Absolute unit";
        Debug.Log(compareStrings(stringOne, stringTwo));
    }
	
	// Update is called once per frame
	void Update ()
    {
        //TestTransform();
        //TestTransformOptimised();
        //ObjectAllocation();
        //InstantiateTest();
        InstantiateTestOptimised();
    }

    private void InstantiateTest()
    {
        for (int i = 0; i < 1000; i++)
        {
            GameObject tempObject1 = Instantiate(prefab);
            tempObject1.transform.position = Vector3.zero;
            Destroy(tempObject1);
        }
    }

    private void InstantiateTestOptimised()
    {
        for (int i = 0; i < 1000; i++)
        {
            prefabs[i].SetActive(true);
            prefabs[i].transform.position = Vector3.zero;
            prefabs[i].SetActive(false);
        }
    }

    private void TestTransform()
    {
        for (int i = 0; i < 10000; i++)
        {
            this.transform.position = Vector3.zero;
        }
    }

    private void TestTransformOptimised()
    {
        for (int i = 0; i < 10000; i++)
        {
            m_transform.position = Vector3.zero;
        }
    }

    private void ObjectAllocation()
    {
        int testInt = 1;
        testInt++;

        int testJ = testInt;
        testJ++;

        for (int i = 0; i < 50000; i++)
        {
            TestObject tempObject = new TestObject();
        }
    }

    private bool compareStrings(string stringOne, string stringTwo)
    {
        if (stringOne == stringTwo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

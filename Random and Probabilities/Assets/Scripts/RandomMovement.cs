using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

    private int randomNumber;

    private Transform m_transform;

    [SerializeField]
    private AnimationCurve m_curve;

	// Use this for initialization
	void Start () {
        m_transform = transform;

        
	}
	
	// Update is called once per frame
	void Update () {
        //StepPercentage();

        //StepArray();

        //StepCurve();

        StepVector();

    }

    private void StepPercentage()
    {
        //int randomNumber = Random.Range(0, 4);
        float randomNumber = Random.Range(0.0f, 1.0f);

        float stepX = 0;
        float stepZ = 0;

        if (randomNumber <= 0.25)
        {
            stepX = -0.3f;
        }
        else if (randomNumber <= 0.5)
        {
            stepX = 0.3f;
        }
        else if (randomNumber <= 0.75)
        {
            stepZ = -0.3f;
        }
        else if (randomNumber <= 1)
        {
            stepZ = 0.3f;
        }

        m_transform.position += new Vector3(stepX, 0, stepZ);
    }

    private void StepArray()
    {
        float[] array = new float[5];

        array[0] = 0;
        array[1] = 1;
        array[2] = 2;
        array[3] = 3;
        array[4] = 0;

        //int randomIndex = Random.Range(0, 5);
        float randomIndex = Random.Range(0.0f, 5.0f);
        //float randomNumber = array[randomIndex];

        float stepX = 0;
        float stepZ = 0;

        if (randomNumber == 0)
        {
            stepX = -0.5f;
        }
        else if (randomNumber == 1)
        {
            stepX = 0.5f;
        }
        else if (randomNumber == 2)
        {
            stepZ = -0.5f;
        }
        else if (randomNumber == 3)
        {
            stepZ = 0.5f;
        }

        m_transform.position += new Vector3(stepX, 0, stepZ);
    }

    private void StepCurve()
    {
        float randomCurveValue = Random.Range(0.0f, 1.0f);
        float randomNumber = m_curve.Evaluate(randomCurveValue);

        float stepX = 0;
        float stepZ = 0;

        if (randomNumber <= 0.25)
        {
            stepX = -0.3f;
        }
        else if (randomNumber <= 0.5)
        {
            stepX = 0.3f;
        }
        else if (randomNumber <= 0.75)
        {
            stepZ = -0.3f;
        }
        else
        {
            stepZ = 0.3f;
        }

        m_transform.position += new Vector3(stepX, 0, stepZ);
    }

    private void StepVector()
    {
        float randomX = Random.Range(-0.1f, 0.01f);
        float randomZ = Random.Range(-0.01f, 0.01f);

        m_transform.position += new Vector3(randomX, 0.0f, randomZ);
    }
}

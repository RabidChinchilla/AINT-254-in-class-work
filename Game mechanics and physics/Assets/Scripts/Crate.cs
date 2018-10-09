using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISS
{
    public class Crate : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] m_ArrayDots;

        public Transform m_target;
        public Camera m_mainCam;
        public GameObject m_dotPrefab;

        private Vector3 m_direction;


        public float force = 10.0f;
        public float gravity = -9.81f;
        //public float m_magnitude;

        // Use this for initialization
        void Start()
        {
            //m_mainCam = GetComponent<Camera>();

            m_ArrayDots = new GameObject[10];

            for (int i = 0; i < m_ArrayDots.Length; i++)
            {
                GameObject tempObject = Instantiate(m_dotPrefab);
                m_ArrayDots[i] = tempObject;
                m_ArrayDots[i].SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0)) //left mouse button
            {
                Vector3 screenPos = m_mainCam.WorldToScreenPoint(m_target.position);
                screenPos.z = 0;
                m_direction = (Input.mousePosition - screenPos).normalized;

                //m_magnitude = 

                Aim();
            }

            if (Input.GetMouseButtonUp(0))
            {
                float Sx = -m_direction.x * force;
                float Sy = -m_direction.y * force;

                for (int i = 0; i < m_ArrayDots.Length; i++)
                {
                    float t = i * 0.1f;

                    float Dx = m_target.position.x + Sx * t;
                    float Dy = (m_target.transform.position.y + Sy * t) - (0.5f * gravity * (t * t));
                    float Dz = 0;

                    m_ArrayDots[i].transform.position = new Vector3(Dx, Dy, Dz);

                    m_ArrayDots[i].SetActive(false);


                }
            }
        }

        private void Aim()
        {
            float Sx = -m_direction.x * force;
            float Sy = -m_direction.y * force;

            for (int i = 0; i < m_ArrayDots.Length; i++)
            {
                float t = i * 0.1f;

                float Dx = m_target.position.x + Sx * t;
                float Dy = (m_target.transform.position.y + Sy * t) - (0.5f * gravity * (t * t));
                float Dz = 0;

                m_ArrayDots[i].transform.position = new Vector3(Dx, Dy, Dz);

                m_ArrayDots[i].SetActive(true);
            }
        }
    }
}
    

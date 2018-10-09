using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ISS
{
    public class CustomInput : MonoBehaviour
    {

        private void OnGUI()
        {
            GUI.Label(new Rect(5, 5, 200, 30), "Horizontal Axes: ");
            GUI.Label(new Rect(150, 5, 200, 30), Input.GetAxis("Horizontal").ToString());

            GUI.Label(new Rect(5, 30, 200, 30), "Vertical Axes: ");
            GUI.Label(new Rect(150, 30, 200, 30), Input.GetAxis("Vertical").ToString());
        }
    }
}

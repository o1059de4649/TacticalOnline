using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{

    public class CameraSenceSlider : MonoBehaviour
    {
        Slider slider;
        public TPSCameraControll tpsCameraControll;
        public TPSControll_y tpsControll_Y;


        // Use this for initialization
        void Start()
        {
            
            slider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            tpsCameraControll.speed = slider.value;
            tpsControll_Y.speed = slider.value;
        }
    }
}

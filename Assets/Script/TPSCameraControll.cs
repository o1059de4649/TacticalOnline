using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UnityStandardAssets.CrossPlatformInput
{
    public class TPSCameraControll : MonoBehaviour

    {
       
        public float speed = 1;

        public CameraRotate cameraRotate;
        public float x;
        // Use this for initialization
        void Start()
        {
            cameraRotate = GameObject.Find("DualTouchControls/TurnAndLookTouchpad").GetComponent<CameraRotate>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
           

            if (cameraRotate.m_Dragging == false)
            {
                return;
            }


               
                
                x = cameraRotate.newAngle_x.y;
                    transform.Rotate(0,-x * speed, 0);
 


        }


    }
}

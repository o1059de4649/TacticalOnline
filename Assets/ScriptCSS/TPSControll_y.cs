using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UnityStandardAssets.CrossPlatformInput
{
    public class TPSControll_y : MonoBehaviour

    {
        public float speed = 1;
        public GameObject y_CameraTarget;

     
       public CameraRotate cameraRotate;

     
       public GameObject cameraRoate_obj;
        public float y,x,camera_Distance;
       
        // Use this for initialization
        void Start()
        {
            cameraRoate_obj = GameObject.Find("DualTouchControls");
            cameraRotate = cameraRoate_obj.GetComponentInChildren<CameraRotate>();

            camera_Distance = -7;
        }

        // Update is called once per frame
        public void FixedUpdate()
        {

           //以下のコメントアウトプログラムは必須事項


            if (!cameraRotate)
            {
               
            }

            if (cameraRotate.m_Dragging == false){
            //    return;
            }



            y_CameraTarget.transform.localEulerAngles = new Vector3(y_CameraTarget.transform.localEulerAngles.x, y_CameraTarget.transform.localEulerAngles.y, 0);
          //  x = cameraRotate.newAngle_x.y;
                y_CameraTarget.transform.Rotate(0, -x * speed * 0.5f, 0);
        //    y = cameraRotate.newAngle_y.x;
                y_CameraTarget.transform.Rotate(-y * speed,0, 0);


            if (this.transform.position.y <= 0 && this.transform.localPosition.z <= -2)//カメラが地中、かつ、カメラ距離が２より離れているとき
            {

                this.transform.localPosition += new Vector3(0, 0, Time.deltaTime * 4);//接近
                
            }

            if (this.transform.position.y > 0 && this.transform.localPosition.z > camera_Distance)//カメラが地上、かつ、カメラ距離が-7より近い時
            {
                this.transform.localPosition += new Vector3(0, 0, -Time.deltaTime * 4);
            }



        }

     
          




    }

    

}
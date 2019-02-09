using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.CrossPlatformInput
{

    public class CameraStork : MonoBehaviour
    {
        public GameObject p_player;

        float eulerAngleX;
       
        // Use this f

        void Start()
        {
            this.transform.parent = null;
        }

        // Update is called once per frame
        void Update()
        {
           

           
            if(transform.localEulerAngles.z == 180 && transform.localEulerAngles.x < 90){
                transform.localEulerAngles = new Vector3(80, 0, 0);
            }

            if (transform.localEulerAngles.z == 180 && transform.localEulerAngles.x > 270)
            {
                transform.localEulerAngles = new Vector3(280, 0, 0);
            }

            if(transform.localEulerAngles.x > 80 && transform.localEulerAngles.x < 180){
                transform.localEulerAngles = new Vector3(80,0,0);
            }

            if (transform.localEulerAngles.x < 280 && transform.localEulerAngles.x >= 180)
            {
                transform.localEulerAngles = new Vector3(280, 0, 0);
            }
        
          
       
                
            this.transform.position = p_player.transform.position + new Vector3(0,2f,0);



          


        }



    }
}

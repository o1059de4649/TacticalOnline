using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.CrossPlatformInput{
public class CameraControll : MonoBehaviour
{
        
    public GameObject camera_player;
    GameObject maincamera;
    Vector3 playerPos;
    Vector3 cameraPos;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
           
           // this.transform.localPosition = new Vector3(0.5f, 0, -7f);


        



            /*
            camera_player = GameObject.FindWithTag("Player").transform.Find("CameraStork").gameObject;
            playerPos = camera_player.transform.position;
*/
            this.transform.parent = camera_player.transform;

            if (maincamera == null)
            {
                maincamera = this.gameObject;
                maincamera.GetComponent<TPSControll_y>().enabled = true;
            }
        


    }
}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.CrossPlatformInput
{
    public class CamerPosition : MonoBehaviour
    {
        RaycastHit hit;
        Ray ray;
        Vector3 hit_pos;
        Vector3 default_Pos_local;
        public GameObject ray_target;
        public float distance = 4.0f;
        SphereCollider sphereCollider;
        bool col_On = false;
        public GameObject default_Pos;
        // Use this for initialization
        void Start()
        {
            transform.localPosition = new Vector3(0.5f, 1.2f, -2.0f);
            default_Pos_local = new Vector3(0.5f, 1.2f, -2.0f);
            sphereCollider = GetComponent<SphereCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            ray = new Ray(ray_target.transform.position, ray_target.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.blue);


            if (Physics.Raycast(ray, out hit, distance))
            {
                if (hit.collider)
                {
                    this.transform.position = hit_pos;
                    hit_pos = hit.point;
                   
                    Debug.Log(hit_pos);

                }


            }

           
   

                
            if (!hit.collider)
            {
                this.transform.position = default_Pos.transform.position;
            }

          
                 

        }


      
    }
}

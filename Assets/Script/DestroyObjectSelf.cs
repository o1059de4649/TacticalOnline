using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyObjectSelf : MonoBehaviour
{
    public float _destoy_time = 8;
    // Start is called before the first frame update
    void Start()
    {
       // Destroy(this.gameObject, _destoy_time);
        GetComponent<PhotonView>().RPC("OnDestroySelf",RpcTarget.AllViaServer,_destoy_time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void OnDestroySelf(float _destroy_Time)
    {
        Destroy(this.gameObject,_destoy_time);
    }
}

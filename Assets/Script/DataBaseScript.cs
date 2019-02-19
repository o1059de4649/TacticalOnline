using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DataBaseScript : MonoBehaviourPunCallbacks,IPunObservable
{

    public int _TeamNumber,_team1Point,_team2Point;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_team1Point);
            stream.SendNext(_team2Point);
        }
        else
        {
            _team1Point = (int)stream.ReceiveNext();
            _team2Point = (int)stream.ReceiveNext();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.CrossPlatformInput;

public class SwordControl : MonoBehaviourPunCallbacks, IPunObservable
{
    // Start is called before the first frame update
    public float _power;

    public bool isStrongSkill = false, isGroundSkill = false;
    public float _up_Power;

    public bool isEffect = false;
    public float onTime, offTime;
    public BoxCollider boxCollider;

    public int _teamNumber;
    public bool isDragon = false;
    public PhotonView photonView;

    DataBaseScript data;
    void Start()
    {
        data = GameObject.Find("DataBase").GetComponent<DataBaseScript>();
        this.gameObject.name = this.gameObject.name.Replace("(Clone)", "");

        _power++;

        if (isEffect)
        {
            Invoke("ColliderOnSwich", onTime);
            Invoke("ColliderOffSwich", offTime);
        }

        if (this.gameObject.name == "SH_Sword_A")
        {
            return;
        }

        if (this.gameObject.GetComponent<DestroyObjectSelf>())
        {
            if (isDragon)
            {
                photonView = GetComponentInParent<PhotonView>();
            }
            else
            {
                photonView = GetComponent<PhotonView>();
            }

        }
        /*
        if (photonView.IsMine)
        {
            photonView.RPC("DesideTeamNumber", RpcTarget.AllBuffered);
        }*/
    }

    [PunRPC]
    public void DesideTeamNumber()
    {
        _teamNumber = data._TeamNumber;
    }

    // Update is called once per frame
    void Update()
    {
        //_teamNumber = data._TeamNumber;
    }

    //他プレイヤーとの判別子

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_teamNumber);
            stream.SendNext(_power);
        }
        else
        {
            _teamNumber = (int)stream.ReceiveNext();
            _power = (float)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Team" + _teamNumber.ToString() + "Body") return;

       

        if (other.gameObject.tag == "Team1Body" || other.gameObject.tag == "Team2Body")
        {
            if (other.gameObject.GetComponentInParent<MovePlayer>()._animatorStateInfo.IsName("Base Layer.Arrive") || other.gameObject.GetComponentInParent<MovePlayer>()._animatorStateInfo.IsName("Base Layer.PushDash"))
            {
                return;
            }
            
                other.gameObject.GetComponentInParent<MovePlayer>().Damage(_power);
                
            
            Vector3 pre_gravity = other.GetComponentInParent<Rigidbody>().velocity;
            other.GetComponentInParent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            
            other.gameObject.GetComponentInParent<MovePlayer>().DamageAnimation(isStrongSkill, _up_Power, isGroundSkill);
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void ColliderOnSwich()
    {


       

        boxCollider.enabled = true;

    }

    void ColliderOffSwich()
    {
        

            boxCollider.enabled = false;
        
       
          
    }
}

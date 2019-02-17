using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.CrossPlatformInput;

public class SwordControl : MonoBehaviourPunCallbacks,IPunObservable
{
    // Start is called before the first frame update
    public float _power;

    public bool isStrongSkill = false,isGroundSkill = false;
    public float _up_Power;

    public bool isEffect = false;
    public float onTime,offTime;
    public BoxCollider boxCollider;

    public int _teamNumber;

 
    void Start()
    {
        _teamNumber = GameObject.Find("DataBase").GetComponent<DataBaseScript>()._TeamNumber;

        _power++;

        if (isEffect)
        {
            Invoke("ColliderSwich",onTime);
            Invoke("ColliderSwich", offTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //他プレイヤーとの判別子
   public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_teamNumber);
        }
        else
        {
            _teamNumber = (int)stream.ReceiveNext();
        }
    }

            private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Team" + _teamNumber.ToString() + "Body")
        {
            return;
        }

        if (other.gameObject.tag == "Team1Body"|| other.gameObject.tag == "Team2Body")
        {
            if (other.gameObject.GetComponentInParent<MovePlayer>()._animatorStateInfo.IsName("Base Layer.Arrive") || other.gameObject.GetComponentInParent<MovePlayer>()._animatorStateInfo.IsName("Base Layer.PushDash"))
            {
                return;
            }
            Vector3 pre_gravity = other.GetComponentInParent<Rigidbody>().velocity;
            other.GetComponentInParent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            other.gameObject.GetComponentInParent<MovePlayer>().Damage(_power);
            other.gameObject.GetComponentInParent<MovePlayer>().DamageAnimation(isStrongSkill,_up_Power,isGroundSkill);
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void ColliderSwich()
    {
        if (boxCollider.enabled == true)
        {
            
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled =true;
        }
    }
}

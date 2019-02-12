using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SwordControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float _power;

    public bool isStrongSkill = false,isGroundSkill = false;
    public float _up_Power;
    void Start()
    {
        _power++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
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
}

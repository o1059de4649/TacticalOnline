using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CommandPlayer : MonoBehaviour
{
    public SkillData[] skilldata;

    public Animator animator;
   AnimatorStateInfo _animatorStateInfo;
    public MovePlayer movePlayer;
    float _recast_time,dash_power,_smash_recast_time,_upperCutSmash_recast_time,_sting_recast_time,_groundAttack_recast_time;

    public float _skill_up_addForce_Power;
    public BoxCollider _sword_col;
    public SwordControl swordControl;
    Vector3 sword_col_vector,sword_position;

    public bool isPush = false;

    public GameObject[] effekseer_obj;

    [System.Serializable]
    public class SkillData
    {
        public string skill_name_forAnim;
       public bool isStrongSkill;
       public bool isGroundSkill;
       public float up_power;
       public float recast_time;
        public float sword_OnTime;
        public float sword_OffTime;

        public float push_power;
        public float push_time;


        public Vector3 sword_size;
        public Vector3 sword_pos;

        public float _mp;


    }
    // Start is called before the first frame update
    void Start()
    {
        movePlayer = GetComponent<MovePlayer>();
        animator = GetComponentInChildren<Animator>();
        dash_power = 7;

        swordControl = this.transform.GetComponentInChildren<SwordControl>();
       _sword_col = swordControl.gameObject.GetComponent<BoxCollider>();
        sword_col_vector = _sword_col.size;
        sword_position = _sword_col.center;
    }

    // Update is called once per frame
    void Update()
    {
        _recast_time += Time.deltaTime;
        _smash_recast_time += Time.deltaTime;
        _upperCutSmash_recast_time += Time.deltaTime;
        _sting_recast_time += Time.deltaTime;
        _groundAttack_recast_time += Time.deltaTime;

        _animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

    }

    //回避
    public void PushDash()
    {
        if (_animatorStateInfo.IsTag("Damage") || _animatorStateInfo.IsTag("AttackSkill")||movePlayer.isSkillMoving) 
        {
            return;
        }

        if (_recast_time < 1)
        {
            return;
        }
        _recast_time = 0;
        animator.SetTrigger("PushDash");
        movePlayer._push_power = 20;
        movePlayer._push_time = 0.2f;
        //  movePlayer.isPushing = true;

        //スキル硬直

    }

    public void SkillAttack(int skillnumber)
    {
        //スキルナンバー選定、スキルの特定
        for (int i = 0;i < skilldata.Length ;i++)
        {
            if (this.gameObject.name == skilldata[i] + "Button")
            {
                skillnumber = i;
            }
        }
        
        //スキル使用制限
        if (_animatorStateInfo.IsTag("Damage")  || movePlayer.isSkillMoving || _animatorStateInfo.IsName("Base Layer.PushDash"))
        {
            return;
        }

        //打ち上げ威力と打ち上げ属性
        swordControl._up_Power = skilldata[skillnumber].up_power;
        swordControl.isStrongSkill = skilldata[skillnumber].isStrongSkill;
        swordControl.isGroundSkill = skilldata[skillnumber].isGroundSkill;

        if (_smash_recast_time < skilldata[skillnumber].recast_time)
        {
            return;
        }
        _sword_col.size += skilldata[skillnumber].sword_size;
        _smash_recast_time = 0;
        animator.SetTrigger(skilldata[skillnumber].skill_name_forAnim);
        Invoke("SwordColOn", skilldata[skillnumber].sword_OnTime);
        Invoke("SwordColOff", skilldata[skillnumber].sword_OffTime);

        movePlayer._mp -= skilldata[skillnumber]._mp;

        movePlayer._push_power = skilldata[skillnumber].push_power;
        movePlayer._push_time = skilldata[skillnumber].push_time;

    }

    public void UpperCutSmash()
    {
        if (_animatorStateInfo.IsTag("Damage") || movePlayer.isSkillMoving || _animatorStateInfo.IsName("Base Layer.PushDash"))
        {
            return;
        }
        //打ち上げ威力と打ち上げ属性
        swordControl._up_Power = 500;
        swordControl.isStrongSkill = true;
        swordControl.isGroundSkill = true;

        if (_upperCutSmash_recast_time < 1)
        {
            return;
        }
        _upperCutSmash_recast_time = 0;
        animator.SetTrigger("UpperCut");
        _sword_col.enabled = true;
        _sword_col.size *= 2;
       
        
        Invoke("SwordColOff", 0.7f);

        movePlayer.FallOn(400);

        movePlayer._mp -= 100;
        Instantiate(effekseer_obj[0],this.transform.position,Quaternion.identity);
    }

    public void Stinger()
    {
        if (_animatorStateInfo.IsTag("Damage") || movePlayer.isSkillMoving || _animatorStateInfo.IsName("Base Layer.PushDash"))
        {
            return;
        }
        //打ち上げ威力と打ち上げ属性
        swordControl._up_Power = 150;
        swordControl.isStrongSkill = false;
        swordControl.isGroundSkill = false;

        if (_sting_recast_time < 1)
        {
            return;
        }

        //推進力
         movePlayer._push_power = 40;
         movePlayer._push_time = 0.1f;
       // movePlayer.ForwardPushPosition(4);

        _sting_recast_time = 0;
        animator.SetTrigger("Sting");
        _sword_col.enabled = true;
        _sword_col.size *= 2.0f;

        Invoke("SwordColOff", 0.7f);

        movePlayer.SkillMovingOn(0.5f);

        movePlayer._mp -= 40;

    }

    public void GroundAttack()
    {
        if (_animatorStateInfo.IsTag("Damage") || movePlayer.isSkillMoving || _animatorStateInfo.IsName("Base Layer.PushDash"))
        {
            return;
        }
        //打ち上げ威力と打ち上げ属性
        swordControl._up_Power = 400;
        swordControl.isStrongSkill = true;
        swordControl.isGroundSkill = true;

        if (_groundAttack_recast_time < 1)
        {
            return;
        }

        //推進力
        movePlayer._push_power = 20;
        movePlayer._push_time = 0.1f;
        // movePlayer.ForwardPushPosition(4);

        _groundAttack_recast_time = 0;
        animator.SetTrigger("GroundAttack");
        Invoke("SwordColOn", 0.4f);

        _sword_col.size += new Vector3(6, 0, 0);
        _sword_col.size *= 1.5f;
        _sword_col.center += new Vector3(-6,0,0);

        Invoke("SwordColOff", 0.7f);

        movePlayer.SkillMovingOn(0.5f);

        movePlayer._mp -= 80;
       
    }

    public void SwordColOff()
    {
        _sword_col.center = sword_position; 
        _sword_col.size = sword_col_vector;
        _sword_col.enabled = false;

    }

    public void SwordColOn()
    {
        _sword_col.enabled = true;
    }

}

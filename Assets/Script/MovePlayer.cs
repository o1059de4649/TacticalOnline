using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using Photon.Realtime;
using Photon;
using UnityEngine.EventSystems;
using Photon.Pun;

public class MovePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public float _maxLife = 14500;
    public float _life = 14500;

    public float _maxMp = 4500;
    public float _mp = 4500;

    public float v;
    public float h;
    public float _runSpeed, animSpeed = 1.5f, _moveTime_PushDashing, _up_Power_save, ground_time = 0, _gravity;
    public bool isPushing = false, isStun = false, isSkillMoving = false, isFallStartToEnd = false, isGroundDown = false, isGround = true;
    public Rigidbody rb;


    Vector3 v_velocity, h_velocity;
    public JoystickWalk j_walk;
    bool isDrag;
    public Animator anim;
    public Camera player_camera;
    public Vector3 moveForward;

    int _state_walk, _state_fall;
    public AnimatorStateInfo _animatorStateInfo;

    public float _push_power, _push_time;

    public Slider hpSlider, mpSlider;

    //Tagの選定
    public PhotonView photonView;
    public GameObject player_body;

    public int _teamNumber = 0;

   public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
           
            stream.SendNext(_maxLife);
            stream.SendNext(_life);
            stream.SendNext(_maxMp);
            stream.SendNext(_mp);
            stream.SendNext(_teamNumber);
        }
        else
        {
            
            _maxLife = (float)stream.ReceiveNext();
            _life = (float)stream.ReceiveNext();
            _maxMp = (float)stream.ReceiveNext();
            _mp = (float)stream.ReceiveNext();
            _teamNumber = (int)stream.ReceiveNext();

        }
    }

    private void Awake()
    {
        player_camera = GetComponentInChildren<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.name = this.gameObject.name.Replace("(Clone)", "");
        anim = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody>();
        j_walk = GameObject.Find("DualTouchControls").GetComponentInChildren<JoystickWalk>();
        _runSpeed = 5;
        _moveTime_PushDashing = 0;

        _life = _maxLife;
        _mp = _maxMp;
        _gravity = Physics.gravity.y;

        if (photonView.IsMine)
        {
            this.gameObject.name = "MyPlayer";
           

            player_camera.enabled = true;
            this.gameObject.GetComponentInChildren<Canvas>().enabled = true;

            
        }


    }

   

    private void FixedUpdate()
    {
       
    }

    // Update is called once per frame
    void Update()
        {

        this.gameObject.tag = "Team" + _teamNumber.ToString() + "Player";
        player_body.gameObject.tag = "Team" + _teamNumber.ToString() + "Body";
        //他のプレイヤー
        if (!photonView.IsMine) return;
        


        //State管理
        _animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
        _mp += Time.deltaTime * _maxMp * 0.02f;
        //mp
        if (_mp <= 0)
        {
            _mp = 0;
        }

        if (_mp > _maxMp)
        {
            _mp = _maxMp;
        }

        hpSlider.maxValue = _maxLife;
        hpSlider.value = _life;
        mpSlider.maxValue = _maxMp;
        mpSlider.value = _mp;

        //起き上がり処理
        if (ground_time <= 0)
        {

            anim.SetBool("Arrive", false);
        }

       //接地判定と落下処理
        if (_animatorStateInfo.IsName("Base Layer.Fall")|| !isGround)
        {
            rb.velocity += new Vector3(0,_gravity/20 ,0);
           
        }
        else
        {
            isGroundDown = false;
        }


        //移動スキル全般の処理
        if (_animatorStateInfo.IsName("Base Layer.Sting") || _animatorStateInfo.IsName("Base Layer.GroundAttack") || _animatorStateInfo.IsName("Base Layer.PushDash")||
            _animatorStateInfo.IsName("Base Layer.GroundFire") || _animatorStateInfo.IsName("Base Layer.IceSpike") || _animatorStateInfo.IsName("Base Layer.GroundMana"))
        {
            
            _push_time -= Time.deltaTime;
            if (_push_time < 0)
            {
                _push_time = 0;
                rb.velocity = new Vector3(0,rb.velocity.y,0);
                return;
            }
            ForwardPushRB(_push_power);

        }

        //特定の時、動けないreturn
        if (isSkillMoving|| isStun|| _animatorStateInfo.IsName("Base Layer.Fall") || _animatorStateInfo.IsTag("AttackSkill"))
        {
            if (_animatorStateInfo.IsName("Base Layer.Sting") || _animatorStateInfo.IsName("Base Layer.GroundAttack") ||
                _animatorStateInfo.IsName("Base Layer.GroundFire") || _animatorStateInfo.IsName("Base Layer.IceSpike")||
                _animatorStateInfo.IsName("Base Layer.GroundMana"))
            {
                return;
            }

           rb.velocity = new Vector3(0, rb.velocity.y, 0);
            return;
        }



       

        //自由移動処理
        if (_animatorStateInfo.IsName("Base Layer.Walk"))
        {
            PlayerMove();
        }
      
        //回避処理
        if (isPushing)
        {
            MovePushDash();
        }
    }

    //自由移動処理
        private void PlayerMove()
        {
            v = CrossPlatformInputManager.GetAxis("Vertical");
            h = CrossPlatformInputManager.GetAxis("Horizontal");


            if (v <= -1)
            {
                v = -1;
            }

            if (v >= 1)
            {
                v = 1;
            }

        anim.SetFloat("Speed", v);                          // Animator側で設定している"Speed"パラメタにvを渡す
        anim.SetFloat("Direction", h);                      // Animator側で設定している"Direction"パラメタにhを渡す
        anim.speed = animSpeed;
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(player_camera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        moveForward = cameraForward * v + player_camera.transform.right * h;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * _runSpeed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

  

    }

    //回避処理
    public void MovePushDash()
    {

        _moveTime_PushDashing += Time.deltaTime;
        if (_moveTime_PushDashing > 0.28)
        {
          
            _moveTime_PushDashing = 0;
            isPushing = false;
            return;
        }

       
        rb.velocity = transform.forward * _runSpeed * 2.4f;

    }

    //硬直処理
    /*
    public void SkillMovingTime(float skill_time)
    {
        Invoke("SkillMovingOn", skill_time);
    }
    */


    public void SkillMovingOn(float skill_lengh)
    {
        isSkillMoving = true;
        Invoke("SkillMovingOff", skill_lengh);
    }

    public void SkillMovingOff()
    {
        isSkillMoving = false;
    }

    //ダメージ計算
    public void Damage(float power)
    {
        _life -= power;
    }

    //ダメージアニメーション、物理挙動
    public void DamageAnimation(bool isStrongSkill,float _up_Power,bool isGroundSkill)
    {

        //up攻撃を受ける
        if (isStrongSkill)
        {
            if (!isGroundDown)//up攻撃　または　地面攻撃　ではない攻撃は、ダウン状態をどうにもできない
            {
                
                anim.SetTrigger("Fall");
                _up_Power_save = _up_Power;
                FallOn(_up_Power_save);
            }

          
        }
       else//弱弱攻撃を受ける
        {
            if (!isGroundSkill) {
                if (!isGroundDown)//弱攻撃はダウン状態をどうにもできない
                {



                    if (_animatorStateInfo.IsName("Base Layer.Fall"))//空中ダウン状態であるか
                    {
                        _up_Power_save = _up_Power;
                        FallOn(_up_Power_save);
                        anim.SetTrigger("Fall");
                    }
                    else
                    {
                        if (_animatorStateInfo.IsName("Base Layer.Walk"))
                        {
                            anim.SetTrigger("Hit");
                        }

                    }
                }
            }
        }

       

        if (isGroundSkill && isGroundDown)//地面ダウン状態で、地面スキルを受ける
        {
            anim.SetTrigger("Fall");
            _up_Power_save = _up_Power;

           

            FallOn(_up_Power_save);
          
        }

        if (isGroundSkill && !isGroundDown)//地面ダウン状態でないときに、地面スキルを受ける
        {
            anim.SetTrigger("Fall");
            _up_Power_save = _up_Power;
            if (!isStrongSkill)
            {
                FallOn(_up_Power_save);
            }
          
        }
    }

    void DamageAnimationGround()
    {

    }

    public void FallOn(float _up)
    {
      
        rb.AddForce(this.transform.up * _up);
    
    }

    public void FallOff()
    {
        isFallStartToEnd = false;
    }

    public void OnCollisionStay(Collision collision)
    {


        if (collision.gameObject.tag == "Plane")
        {
            isGround = true;
            if (_animatorStateInfo.IsName("Base Layer.Fall"))
            {
                isGroundDown = true;
            }
          
            if(_animatorStateInfo.IsName("Base Layer.Fall") && ground_time >= 1)//空中ダウンからの着地
            {
              
                ground_time = 0;
                anim.SetBool("Arrive",true);
             
               
            }
            ground_time += Time.deltaTime;


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")//空中ダウンからの着地
        {

            ground_time = 0;
            isGroundDown = false;
            isGround = false;
        }
    }

    public void ForwardPushRB(float _push_power)
    {


        //rb.AddForce(transform.forward * _push_power , ForceMode.Force);
       
        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = transform.forward * _push_power;

       
    }

    public void ForwardPushPosition(float _push_power)
    {


        transform.Translate(0, 0, _push_power);

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        // rb.velocity = transform.forward * _push_power;


    }


}
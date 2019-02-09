using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


    public class MovePlayer : MonoBehaviour
    {
        public float v;
        public float h;
        public float _runSpeed,animSpeed = 1.5f;
       
        public Rigidbody rb;


        Vector3 v_velocity,h_velocity;
        public JoystickWalk j_walk;
        bool isDrag;
       public Animator anim;
   public Camera player_camera;
        // Start is called before the first frame update
        void Start()
        {
        anim = GetComponentInChildren<Animator>();
        player_camera = GetComponentInChildren<Camera>();
            rb = GetComponent<Rigidbody>();
            j_walk = GameObject.Find("DualTouchControls").GetComponentInChildren<JoystickWalk>();
        _runSpeed = 3;
        }

        // Update is called once per frame
        void Update()
        {
      
            PlayerMove();

        }

        private void PlayerMove()
        {
            v = CrossPlatformInputManager.GetAxis("Vertical");
            h = CrossPlatformInputManager.GetAxis("Horizontal");

            /*
            isDrag = j_walk.drag;
            if (!isDrag)
            {
                v = 0;
            }
            */
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
        Vector3 moveForward = cameraForward * v + player_camera.transform.right * h;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * _runSpeed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        /*
        v_velocity = new Vector3(0, 0, v);
        h_velocity = new Vector3(h, 0, 0);

        anim.SetFloat("Speed", v);                          // Animator側で設定している"Speed"パラメタにvを渡す
        anim.SetFloat("Direction", h);                      // Animator側で設定している"Direction"パラメタにhを渡す
        anim.speed = animSpeed;                             // Animatorのモーション再生速度に animSpeedを設定する

        v_velocity = transform.TransformDirection(v_velocity);
        if (v > 0.1)
        {
            v_velocity *= _runSpeed;       // 移動速度を掛ける
        }
        else if (v < -0.1)
        {
            v_velocity *= _runSpeed;  // 移動速度を掛ける
        }

        h_velocity = transform.TransformDirection(h_velocity);
        if (h > 0.1)
        {
            h_velocity *= _runSpeed;       // 移動速度を掛ける
        }
        else if (h < -0.1)
        {
            h_velocity *= _runSpeed;  // 移動速度を掛ける
        }

    transform.localPosition += v_velocity * Time.fixedDeltaTime;
    transform.localPosition += h_velocity * Time.fixedDeltaTime;
    */
    }


    }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class CameraRotate : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler
    {
        // カメラオブジェクトを格納する変数
        public GameObject mainCamera;
        // カメラオブジェクトを格納する変数
        public GameObject camera_Stork;
        // カメラの回転速度を格納する変数
        public Vector2 rotationSpeed;
        // マウス移動方向とカメラ回転方向を反転する判定フラグ
        public bool reverse;
        // マウス座標を格納する変数
        public Vector2 lastMousePosition;
        // カメラの角度を格納する変数（初期値に0,0を代入）
        public Vector2 newAngle_x =new Vector2(0, 0);
        public Vector2 newAngle_y = new Vector2(0, 0);

        public GameObject player;


        int m_Id;
        public bool m_Dragging;
        // Use this for initialization
        void Start()
        {
           lastMousePosition =new Vector2(0,0);
        } 

        // Update is called once per frame
        void FixedUpdate()
        {
          
            if(!player){
//                player = GameObject.Find("PhotonController").GetComponent<PhotonController>().player;
            }
          
            if (!m_Dragging)
            {
                return;
            }
          
            if (Input.touchCount >= m_Id + 1 && m_Id != -1)
            {
                
                //カメラ回転方向の判定フラグが"true"の場合
                if (!reverse)
                {

                    // Y軸の回転：マウスドラッグ方向に視点回転
                    // マウスの水平移動値に変数"rotationSpeed"を掛ける
                    //（クリック時の座標とマウス座標の現在値の差分値）
                    newAngle_x.y = (lastMousePosition.x - Input.touches[m_Id].position.x) * rotationSpeed.y;
                    // X軸の回転：マウスドラッグ方向に視点回転
                    // マウスの垂直移動値に変数"rotationSpeed"を掛ける
                    //（クリック時の座標とマウス座標の現在値の差分値）
                    newAngle_y.x = (Input.touches[m_Id].position.y - lastMousePosition.y) * rotationSpeed.x;
                    // "newAngle"の角度をカメラ角度に格納
                  
                    // マウス座標を変数"lastMousePosition"に格納
                    lastMousePosition = Input.touches[m_Id].position;
                }
                // カメラ回転方向の判定フラグが"reverse"の場合
                else if (reverse)
                {
                    // Y軸の回転：マウスドラッグと逆方向に視点回転
                    newAngle_x.y = (Input.touches[m_Id].position.x - lastMousePosition.x) * rotationSpeed.y;
                    // X軸の回転：マウスドラッグと逆方向に視点回転
                    newAngle_y.x = (lastMousePosition.y - Input.touches[m_Id].position.y) * rotationSpeed.x;
                    // "newAngle"の角度をカメラ角度に格納
                  
                    // マウス座標を変数"lastMousePosition"に格納
                    lastMousePosition = Input.touches[m_Id].position;
                }
            }


        }

        public void OnPointerDown(PointerEventData data)
        {

          //  mainCamera = player.GetComponent<UnityChanControlScriptWithRgidBody>().player_camera;
           // camera_Stork = player.GetComponent<UnityChanControlScriptWithRgidBody>().p_camera_Stork;

            m_Dragging = true;
            m_Id = data.pointerId;

            // カメラの角度を変数"newAngle"に格納
            //newAngle_x = mainCamera.transform.localEulerAngles;
           //newAngle_y = camera_Stork.transform.localEulerAngles;
            // マウス座標を変数"lastMousePosition"に格納
            lastMousePosition = Input.touches[m_Id].position;
           
        }

        public void OnPointerUp(PointerEventData data)
        {
            m_Dragging = false;
            m_Id = -1;

        }

        public void DirectionChange()
        {
            // 判定フラグ変数"reverse"が"false"であれば
            if (!reverse)
            {
                // 判定フラグ変数"reverse"に"true"を代入
                reverse = true;
            }
            // でなければ（判定フラグ変数"reverse"が"true"であれば）
            else
            {
                // 判定フラグ変数"reverse"に"false"を代入
                reverse = false;
            }
        }

    }
}
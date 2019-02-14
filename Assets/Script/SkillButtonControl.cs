using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButtonControl : MonoBehaviour,IPointerClickHandler
{
   public CommandPlayer commandPlayer;
    public MovePlayer movePlayer;

    public float _button_hideTime;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.name = this.gameObject.name.Replace("(Clone)", "");
        commandPlayer = GetComponentInParent<CommandPlayer>();
        movePlayer = GetComponentInParent<MovePlayer>();
        _button_hideTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (movePlayer._mp <= 0)
        {
            return;
        }

        if (this.gameObject.name == "SmashButton")
        {
            commandPlayer.SkillAttack(0);
            Invoke("SetActiveOff", _button_hideTime);
            Invoke("SetActiveOn",5);
        }

        if (this.gameObject.name == "UpperCutButton")
        {
            commandPlayer.UpperCutSmash();
            Invoke("SetActiveOff", _button_hideTime);
            Invoke("SetActiveOn", 5);
        }

        if (this.gameObject.name == "StingButton")
        {
            commandPlayer.SkillAttack(1);
            Invoke("SetActiveOff", _button_hideTime);
            Invoke("SetActiveOn", 5);
        }

        if (this.gameObject.name == "GroundButton")
        {
            commandPlayer.SkillAttack(2);
            Invoke("SetActiveOff", _button_hideTime);
            Invoke("SetActiveOn", 5);
        }

        if (this.gameObject.name == "FireWaveButton")
        {
            commandPlayer.SkillAttack(3);
            Invoke("SetActiveOff", _button_hideTime);
            Invoke("SetActiveOn", 5);
        }

    }

    void SetActiveOff()
    {
        this.gameObject.SetActive(false);
    }

    void SetActiveOn()
    {
        this.gameObject.SetActive(true);
    }
}

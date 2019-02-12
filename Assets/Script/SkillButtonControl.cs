using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButtonControl : MonoBehaviour,IPointerClickHandler
{
   public CommandPlayer commandPlayer;
    public MovePlayer movePlayer;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.name = this.gameObject.name.Replace("(Clone)", "");
        commandPlayer = GetComponentInParent<CommandPlayer>();
        movePlayer = GetComponentInParent<MovePlayer>();
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
            commandPlayer.Smash();
           this.gameObject.SetActive(false);
            Invoke("SetAcyiveOn",5);
        }

        if (this.gameObject.name == "UpperCutButton")
        {
            commandPlayer.UpperCutSmash();
            this.gameObject.SetActive(false);
            Invoke("SetAcyiveOn", 5);
        }

        if (this.gameObject.name == "StingButton")
        {
            commandPlayer.Stinger();
            this.gameObject.SetActive(false);
            Invoke("SetAcyiveOn", 5);
        }

        if (this.gameObject.name == "GroundButton")
        {
            commandPlayer.GroundAttack();
            this.gameObject.SetActive(false);
            Invoke("SetAcyiveOn", 5);
        }

    }

    void SetAcyiveOn()
    {
        this.gameObject.SetActive(true);
    }
}

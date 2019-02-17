using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using System;



public class DropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    private Sprite nowSprite;
    public string skill_name;

    public string number_string;
    public int number;
    public string[] splitSkill;

    
    void Start()
    {
        nowSprite = iconImage.sprite;

       
        if (!PlayerPrefs.HasKey("SkillNote0")|| !PlayerPrefs.HasKey("SkillNote1")|| !PlayerPrefs.HasKey("SkillNote2")|| !PlayerPrefs.HasKey("SkillNote3"))
        {
            return;
        }

        string save_data_all = PlayerPrefs.GetString("SkillNote0") + "/" + PlayerPrefs.GetString("SkillNote1") + "/" + PlayerPrefs.GetString("SkillNote2") + "/" + PlayerPrefs.GetString("SkillNote3");
        splitSkill = save_data_all.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

      
        if (splitSkill.Length < 16)
        {
            return;
        }

        number_string = this.gameObject.name.Replace("SkillSetPanel","");
      Int32.TryParse(number_string, out number);
        skill_name = splitSkill[number];



        iconImage.sprite = Resources.Load<Sprite>("SkillButton/" + splitSkill[number]);
    }

    void Update()
    {
        skill_name = iconImage.sprite.name;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        nowSprite = iconImage.sprite;

        if (pointerEventData.pointerDrag == null) return;
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        iconImage.color = Vector4.one * 0.6f;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(pointerEventData.pointerDrag == null)
        {
            return;
        }
            
        iconImage.sprite = nowSprite;
        if(nowSprite == null)
            iconImage.color = Vector4.zero;
        else
            iconImage.color = Vector4.one;
    }
    public void OnDrop(PointerEventData pointerEventData)
    {
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        nowSprite = droppedImage.sprite;
        iconImage.color = Vector4.one;

        skill_name = iconImage.sprite.name;
      
    }
}

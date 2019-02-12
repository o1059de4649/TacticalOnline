using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class SkillSlotSetSkill : MonoBehaviour
{
    public GameObject[] skill_slot;
    public string save_data_All;
    public string[] splitSkill;

    public OptionControl option;

    public GameObject[] _pre_prefab;
    // Start is called before the first frame update
    void Start()
    {



        SkillSetUpStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SkillSetUpStart()
    {
        save_data_All = PlayerPrefs.GetString("SkillNote0") + "/" + PlayerPrefs.GetString("SkillNote1") + "/" + PlayerPrefs.GetString("SkillNote2") + "/" + PlayerPrefs.GetString("SkillNote3");
        splitSkill = save_data_All.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);


        if (splitSkill.Length < 16 || splitSkill.Length == 0)
        {

            return;
        }
        if (!PlayerPrefs.HasKey("SkillNote0") ||
splitSkill[0] == null || splitSkill[1] == null || splitSkill[2] == null || splitSkill[3] == null ||
    splitSkill[4] == null || splitSkill[5] == null || splitSkill[6] == null || splitSkill[7] == null ||
     splitSkill[8] == null || splitSkill[9] == null || splitSkill[10] == null || splitSkill[11] == null ||
     splitSkill[12] == null || splitSkill[13] == null || splitSkill[14] == null || splitSkill[15] == null
    )
        {
            return;
        }


        for (int i = 0; i < 16; i++)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefab/" + splitSkill[i] + "Button");

            if (0 <= i && i <= 3)
            {
                GameObject _prefab_button = Instantiate(prefab, skill_slot[0].transform.position, Quaternion.identity);
                _prefab_button.transform.parent = skill_slot[0].transform;
                _prefab_button.transform.localScale *= 2.3f;
            }
            if (4 <= i && i <= 7)
            {

                GameObject _prefab_button = Instantiate(prefab, skill_slot[1].transform.position, Quaternion.identity);
                _prefab_button.transform.parent = skill_slot[1].transform;
                _prefab_button.transform.localScale *= 2.3f;
            }
            if (8 <= i && i <= 11)
            {

                GameObject _prefab_button = Instantiate(prefab, skill_slot[2].transform.position, Quaternion.identity);
                _prefab_button.transform.parent = skill_slot[2].transform;
                _prefab_button.transform.localScale *= 2.3f;
            }
            if (12 <= i && i <= 16)
            {

                GameObject _prefab_button = Instantiate(prefab, skill_slot[3].transform.position, Quaternion.identity);
                _prefab_button.transform.parent = skill_slot[3].transform;
                _prefab_button.transform.localScale *= 2.3f;
            }
        }
    }

    public void SkillSetUp()
    {
        option.Save();

        save_data_All = PlayerPrefs.GetString("SkillNote0") + "/" + PlayerPrefs.GetString("SkillNote1") + "/" + PlayerPrefs.GetString("SkillNote2") + "/" + PlayerPrefs.GetString("SkillNote3");
        splitSkill = save_data_All.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

       
        if (splitSkill.Length < 16 || splitSkill.Length == 0)
        {

            return;
        }
                if (!PlayerPrefs.HasKey("SkillNote0") || 
        splitSkill[0] == null||splitSkill[1] == null ||splitSkill[2] == null ||splitSkill[3] == null ||
            splitSkill[4] == null ||splitSkill[5] == null ||splitSkill[6] == null ||splitSkill[7] == null ||
             splitSkill[8] == null || splitSkill[9] == null || splitSkill[10] == null || splitSkill[11] == null ||
             splitSkill[12] == null || splitSkill[13] == null || splitSkill[14] == null || splitSkill[15] == null 
            )
        {
            return;
        }

                //既存オブジェクト
        for (int i = 0; i < 4; i++)
        {
            _pre_prefab[i * 4  + 0] = skill_slot[i].transform.GetChild(0).gameObject;
            _pre_prefab[i * 4 + 1] = skill_slot[i].transform.GetChild(1).gameObject;
            _pre_prefab[i * 4 + 2] = skill_slot[i].transform.GetChild(2).gameObject;
            _pre_prefab[i * 4 + 3] = skill_slot[i].transform.GetChild(3).gameObject;

        }

        //既存オブジェクト削除
        for (int p = 0; p < 16; p++)
        {
            Destroy(_pre_prefab[p]);
        }

        //新規オブジェクト作成
        for (int i = 0; i < 16;i++)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefab/" + splitSkill[i] + "Button");
           
            if (0 <= i && i <= 3)
            {
               GameObject _prefab_button = Instantiate(prefab, skill_slot[0].transform.position, Quaternion.identity);
                _prefab_button.transform.parent = skill_slot[0].transform;
                _prefab_button.transform.localScale *= 2.3f;

                _pre_prefab[i] = _prefab_button;
            }
            if (4 <= i && i <= 7)
            {

                GameObject _prefab_button = Instantiate(prefab, skill_slot[1].transform.position, Quaternion.identity);
                _prefab_button.transform.parent = skill_slot[1].transform;
                _prefab_button.transform.localScale *= 2.3f;

                _pre_prefab[i] = _prefab_button;
            }
            if (8 <= i && i <= 11)
            {

                GameObject _prefab_button = Instantiate(prefab, skill_slot[2].transform.position, Quaternion.identity);
                _prefab_button.transform.parent = skill_slot[2].transform;
                _prefab_button.transform.localScale *= 2.3f;

                _pre_prefab[i] = _prefab_button;
            }
            if (12 <= i && i <= 16)
            {

                GameObject _prefab_button = Instantiate(prefab, skill_slot[3].transform.position, Quaternion.identity);
                _prefab_button.transform.parent = skill_slot[3].transform;
                _prefab_button.transform.localScale *= 2.3f;

                _pre_prefab[i] = _prefab_button;
            }

        }

       

    }

}

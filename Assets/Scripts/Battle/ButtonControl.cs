using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonControl : BattleUI
{

    private BattleControl battleControl;

    private void Start()
    {
        battleControl = FindObjectOfType<BattleControl>();
        if (battleControl == null)
        {
            Debug.LogError("Could not find BattleControl in the scene.");
        }
    }
    public void ClickSkill()
    {
        Debug.Log("Skill");
        Skill.SetActive(true);
        List.SetActive(false);

    }

    public void ClickBag()
    {
        Bag.SetActive(true);

    }

    public void ClickAbility()
    {
        Ability.SetActive(true);
    }

    public void Clickrun()
    {
        GameObject.Find("Loading").GetComponent<LoadingController>().SwitchScene("GrassLandScene",true,true);

    }

    public void ClickCancel()
    {
        List.SetActive(true);
        Skill.SetActive(false);
    }

    public void ChooseSkill()
    {
        List.SetActive(true);
        Skill.SetActive(false);
        var textComponent = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (battleControl != null)
        {
            battleControl.CanMove();
            battleControl.SkillName(textComponent.text);
        }
       

    }

    public void BacktoMainScene()
    {
        GameObject.Find("Loading").GetComponent<LoadingController>().SwitchScene("GrassLandScene",true,true);
    }

    public void GameOver()
    {
        GameObject.Find("Loading").GetComponent<LoadingController>().SwitchScene("TitleScene");

    }
}

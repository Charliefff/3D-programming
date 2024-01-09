using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ButtonControl : BattleUI
{
    
    private BattleControl battleControl;
    public GameObject Player2;

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
        GameObject.Find("Loading").GetComponent<LoadingController>().SwitchScene(Base_seneriao, true,true);

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

        //Debug.Log(textComponent.text);
        if (battleControl != null)
        {
            battleControl.CanMove();
            battleControl.SkillName(textComponent.text);
        } 

    }

    public void BacktoMainScene()
    {
        GameObject.Find("Loading").GetComponent<LoadingController>().SwitchScene(Base_seneriao,true,true);
    }

    public void GameOver()
    {
        GameObject.Find("Loading").GetComponent<LoadingController>().SwitchScene("TitleScene");

    }

    public void ClickCumsumable()
    {
        Bag.SetActive(false);

        TextMeshProUGUI countText = transform.Find("Count").GetComponent<TextMeshProUGUI>();

        if (int.TryParse(countText.text, out int countNumber))
        {
            countNumber -= 1;
            countText.text = countNumber.ToString();
        }

        Player2.transform.GetChild(0).GetComponent<CharAbility>().HP += 15;
    }

}

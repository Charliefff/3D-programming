using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class BattleUI : BattleBase
{
    public GameObject List;
    public GameObject Skill;
    public GameObject Bag;
    public GameObject Ability;
    
    private float _lerpSpeed = 3;

    public void Update()
    {
        UpdateBloodUI();
        UpdateAbility();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    private void CloseUI()
    {

        Bag.SetActive(false);
        Ability.SetActive(false);
        //Skill.SetActive(false);

    }


    private void UpdateBloodUI()
    {
        foreach (var hpItem in HPList)
        {
            if (hpItem == null)
            {
                continue;
            }

            UpdateHealthBar(hpItem);
        }
    }

    private void UpdateHealthBar(GameObject hpItem)
    {
        var name = hpItem.name;
        var bar = hpItem.GetComponent<Image>();
        if (bar == null) return;

        GameObject targetPlayer = name switch
        {
            "Player1HP" => Player1,
            "Player2HP" => Player2,
            "Player3HP" => Player3,
            "Player4HP" => Player4,
            _ => Monster,
        };

        if (targetPlayer != null)
        {
            var charAbility = targetPlayer.GetComponent<CharAbility>();
            if (charAbility != null)
            {
                int health = charAbility.HP;
                int maxHealth = charAbility.MaxHP;
                bar.fillAmount = Mathf.Lerp(bar.fillAmount, (float)health / maxHealth, _lerpSpeed * Time.deltaTime);
                UpdateText(health, maxHealth, hpItem);

            }
        }
    }

    private void UpdateText(int health, int maxHealth, GameObject hpItem)
    {
        if (hpItem.transform.childCount > 0)
        {
            var textComponent = hpItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = $"HP {health}/{maxHealth}";
            }
        }

    }

    private void UpdateAbility()
    {

        
        foreach (var item in AbilityUIList)
        {
            if (item.name == StateObj)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }


    }


}


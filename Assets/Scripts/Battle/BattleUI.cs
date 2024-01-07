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
    public GameObject ExBar1;
    public GameObject ExBar2;
    public GameObject ExBar3;
    public GameObject ExBar4;


    private float _lerpSpeed = 3;

    public void Start()
    {
        SetExBar();
    }
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
                int maxHealth = charAbility.HPMax;
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

    private void SetExBar()
    {
        // 獲取 Image 組件
        var ExBar1_Image = ExBar1.GetComponent<Image>();
        var ExBar2_Image = ExBar2.GetComponent<Image>();
        var ExBar3_Image = ExBar3.GetComponent<Image>();
        var ExBar4_Image = ExBar4.GetComponent<Image>();

        // 設置每個經驗值條的填充量
        // 假設每個角色的經驗值條對應一個玩家
        ExBar1_Image.fillAmount = (float)Base.player[0].Exp / Base.player[0].ExpMax;
        ExBar2_Image.fillAmount = (float)Base.player[1].Exp / Base.player[1].ExpMax;
        ExBar3_Image.fillAmount = (float)Base.player[2].Exp / Base.player[2].ExpMax;
        ExBar4_Image.fillAmount = (float)Base.player[3].Exp / Base.player[3].ExpMax;
    }


}


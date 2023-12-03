using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 


public class DashBoardNormalController : MonoBehaviour
{
    public TextMeshProUGUI[] nameText = new TextMeshProUGUI[4]; 
    public TextMeshProUGUI[] stateText = new TextMeshProUGUI[4]; 
    public GameObject[] hpBar = new GameObject[4]; 
    public GameObject[] mpBar = new GameObject[4]; 
    public GameObject[] expBar = new GameObject[4]; 

    private Image[] hpBarImage = new Image[4];
    private Image[] mpBarImage = new Image[4];
    private Image[] expBarImage = new Image[4];
    
    
    void Awake()
    {
        for(int i=0;i<4;i++){
            hpBarImage[i] = hpBar[i].GetComponent<Image>();
            mpBarImage[i] = mpBar[i].GetComponent<Image>();
            expBarImage[i] = expBar[i].GetComponent<Image>();
        }
    }

    void Start()
    {      
        UpdateNormal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNormal() {
        for(int i=0;i<4;i++){
            nameText[i].text = Base.player[i].Name + "\nLV " + Base.player[i].Level;
            stateText[i].text = "HP\t" + Base.player[i].HP + "/" + Base.player[i].HPMax + "\nMP\t" + Base.player[i].MP + "/" + Base.player[i].MPMax + "\nExp\t" + Base.player[i].Exp + "/" + Base.player[i].ExpMax;
            hpBarImage[i].fillAmount = (float)Base.player[i].HP / (float)Base.player[i].HPMax;
            mpBarImage[i].fillAmount = (float)Base.player[i].MP / (float)Base.player[i].MPMax;
            expBarImage[i].fillAmount = (float)Base.player[i].Exp / (float)Base.player[i].ExpMax;
        }
    }
}

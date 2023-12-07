using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System.Linq;
public class DashBoardSkillController : MonoBehaviour
{
    public Transform skillParent;
    public GameObject skillButton;
    public List<GameObject> skillButtonList = new List<GameObject>();
    public TextMeshProUGUI skillTitleText;
    public GameObject[] skillLearnedButton = new GameObject[4]; 
    public GameObject[] actorImage = new GameObject[4];

    private  int selectActor;

    void Start()
    {
        selectActor = 0;
        UpdateSkill();
    }

    void Update()
    {
        
    }

    public void UpdateSkill(){
        skillTitleText.text = "已裝備技能\n("+Base.player[selectActor].SkillList.Count+"/4)";

        foreach(var temp in skillButtonList){
            Destroy(temp);
        }
        skillButtonList.Clear();

        for(int i=0;i<4;i++){
            actorImage[i].SetActive(false);
        }
        actorImage[selectActor].SetActive(true);

        Base.player[selectActor].SkillHistoryList.Sort();
        for(int i = 0; i < Base.player[selectActor].SkillHistoryList.Count; i++){
            string skill = Base.player[selectActor].SkillHistoryList[i];

            GameObject newButtonObj = Instantiate(skillButton, skillParent);

            // newButtonObj.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(Base.dicConsumable[key].Image);
            newButtonObj.transform.Find("Count").GetComponent<TextMeshProUGUI>().text = "<";
            newButtonObj.transform.Find("Detail").GetComponent<TextMeshProUGUI>().text = Base.dicSkill[skill].SkillName;

            string kiy = skill;
            int id = i;
            newButtonObj.GetComponent<Button>().onClick.AddListener(() => OnButtonItemClicked(id, kiy));

            newButtonObj.SetActive(true);
            skillButtonList.Add(newButtonObj);
        }

        for(int i=0; i<4; i++){
            if(i < Base.player[selectActor].SkillList.Count){
                skillLearnedButton[i].SetActive(true);
                // skillLearnedButton[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(Base.dicConsumable[key].Image);
                skillLearnedButton[i].transform.Find("Count").GetComponent<TextMeshProUGUI>().text = ">";
                skillLearnedButton[i].transform.Find("Detail").GetComponent<TextMeshProUGUI>().text = Base.dicSkill[Base.player[selectActor].SkillList[i]].SkillName;
            }else{
                skillLearnedButton[i].SetActive(false);
            }            
        }
    }

    private void OnButtonItemClicked(int id, string key)
    {
        if(Base.player[selectActor].SkillList.Count < 4){
            Base.player[selectActor].SkillList.Add(key);
            Base.player[selectActor].SkillHistoryList.RemoveAt(id);
            UpdateSkill();
        }
    }

    public void SelectActor(int id){
        selectActor = id;
        UpdateSkill();
    }

    public void skillLearnedButtonPress(int id){
        Base.player[selectActor].SkillHistoryList.Add(Base.player[selectActor].SkillList[id]);
        Base.player[selectActor].SkillList.RemoveAt(id);
        UpdateSkill();
    }
}

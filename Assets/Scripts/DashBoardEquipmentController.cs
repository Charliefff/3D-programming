using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System.Linq;

public class DashBoardEquipmentController : MonoBehaviour
{
    public GameObject[] equipmentButton = new GameObject[7];
    public TextMeshProUGUI equipmentDetailText;
    public TextMeshProUGUI actorText;
    public GameObject[] actorImage = new GameObject[4];
    public GameObject selectPage;
    public GameObject selectPageButton;
    public Transform selectPageParent;
    public TextMeshProUGUI selectPageText;
    public Sprite backgroundSprite;
    

    private string selectType;
    private List<GameObject> selectPageButtonList = new List<GameObject>();
    private int selectActor;
    private int equipmentFlag;
    private string equipmentFlagKey;
    private string actorEquipmentKey;
    private int[] tempStatus = new int[6];
    private string weaponType;

    void Start()
    {
        selectPage.SetActive(false);
        selectActor = 0;
        weaponType = "Weapon";
        UpdateEquipment();
    }

    void Update()
    {
        
    }

    public void UpdateEquipment()
    {
        for(int i=0;i<4;i++){
            actorImage[i].SetActive(false);
        }
        
        for(int i=0;i<7;i++){
            equipmentButton[i].transform.Find("name").GetComponent<TextMeshProUGUI>().text = "";
            equipmentButton[i].transform.Find("Image").gameObject.SetActive(false);
        }

        actorImage[selectActor].SetActive(true);
        actorText.text = Base.player[selectActor].Name + "\nLV " + Base.player[selectActor].Level;
        equipmentDetailText.text = "";
        equipmentDetailText.text += "生命\t\t" + Base.player[selectActor].HPMax + "\t>\t" + "\n";
        equipmentDetailText.text += "魔力\t\t" + Base.player[selectActor].MPMax + "\t>\t" + "\n";
        equipmentDetailText.text += "物攻\t\t" + Base.player[selectActor].Attack + "\t>\t" + "\n";
        equipmentDetailText.text += "魔攻\t\t" + Base.player[selectActor].Magic + "\t>\t" + "\n";
        equipmentDetailText.text += "防禦\t\t" + Base.player[selectActor].Defense + "\t>\t" + "\n";
        equipmentDetailText.text += "靈巧\t\t" + Base.player[selectActor].Speed + "\t>\t" + "\n";

        foreach(var temp in Base.player[selectActor].WeaponList){
            if(Base.dicWeapon[temp].Category == "Weapon"){
                equipmentButton[0].transform.Find("Image").gameObject.SetActive(true);
                equipmentButton[0].transform.Find("name").GetComponent<TextMeshProUGUI>().text = Base.dicWeapon[temp].Name;
                equipmentButton[0].transform.Find("Image").GetComponent<Image>().sprite =  Resources.Load<Sprite>(Base.dicWeapon[temp].Image);
            }else if(Base.dicWeapon[temp].Category == "Head"){
                equipmentButton[1].transform.Find("Image").gameObject.SetActive(true);
                equipmentButton[1].transform.Find("name").GetComponent<TextMeshProUGUI>().text = Base.dicWeapon[temp].Name;
                equipmentButton[1].transform.Find("Image").GetComponent<Image>().sprite =  Resources.Load<Sprite>(Base.dicWeapon[temp].Image);
            }else if(Base.dicWeapon[temp].Category == "Body"){
                equipmentButton[2].transform.Find("Image").gameObject.SetActive(true);
                equipmentButton[2].transform.Find("name").GetComponent<TextMeshProUGUI>().text = Base.dicWeapon[temp].Name;
                equipmentButton[2].transform.Find("Image").GetComponent<Image>().sprite =  Resources.Load<Sprite>(Base.dicWeapon[temp].Image);
            }else if(Base.dicWeapon[temp].Category == "Pants"){
                equipmentButton[3].transform.Find("Image").gameObject.SetActive(true);
                equipmentButton[3].transform.Find("name").GetComponent<TextMeshProUGUI>().text = Base.dicWeapon[temp].Name;
                equipmentButton[3].transform.Find("Image").GetComponent<Image>().sprite =  Resources.Load<Sprite>(Base.dicWeapon[temp].Image);
            }else if(Base.dicWeapon[temp].Category == "Boots"){
                equipmentButton[4].transform.Find("Image").gameObject.SetActive(true);
                equipmentButton[4].transform.Find("name").GetComponent<TextMeshProUGUI>().text = Base.dicWeapon[temp].Name;
                equipmentButton[4].transform.Find("Image").GetComponent<Image>().sprite =  Resources.Load<Sprite>(Base.dicWeapon[temp].Image);
            }else if(Base.dicWeapon[temp].Category == "Accessory"){
                equipmentButton[5].transform.Find("Image").gameObject.SetActive(true);
                equipmentButton[5].transform.Find("name").GetComponent<TextMeshProUGUI>().text = Base.dicWeapon[temp].Name;
                equipmentButton[5].transform.Find("Image").GetComponent<Image>().sprite =  Resources.Load<Sprite>(Base.dicWeapon[temp].Image);
            }else if(Base.dicWeapon[temp].Category == "Ring"){
                equipmentButton[6].transform.Find("Image").gameObject.SetActive(true);
                equipmentButton[6].transform.Find("name").GetComponent<TextMeshProUGUI>().text = Base.dicWeapon[temp].Name;
                equipmentButton[6].transform.Find("Image").GetComponent<Image>().sprite =  Resources.Load<Sprite>(Base.dicWeapon[temp].Image);
            }
        }
    }

    public void UpdateSelectPage(string _weaponType = "Weapon"){
        weaponType = _weaponType;
        equipmentFlag = 0;
        actorEquipmentKey = "";
        selectPageText.text = weaponType;

        foreach(var temp in selectPageButtonList){
            Destroy(temp);
        }
        selectPageButtonList.Clear();
        
        foreach(var temp in Base.player[selectActor].WeaponList){
            if(Base.dicWeapon[temp].Category == weaponType){
                actorEquipmentKey = temp;
                equipmentFlagKey = temp;
                CreateEquipmentButton(temp, "*", true);
                break;
            }
        }

        var bagWeaponsSort = Base.bagWeapons.OrderBy(pair => int.Parse(pair.Key));

        foreach (var weapon in bagWeaponsSort){
            if(Base.dicWeapon[weapon.Key].Category != weaponType) continue;

            CreateEquipmentButton("" + weapon.Key, "" + weapon.Value);
        }
    }

    public void CreateEquipmentButton(string key, string val, bool sel = false){
            GameObject newButtonObj = Instantiate(selectPageButton, selectPageParent);

            if(sel){
                newButtonObj.transform.Find("switch").gameObject.SetActive(true);
            }

            newButtonObj.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(Base.dicWeapon[key].Image);
            newButtonObj.transform.Find("Count").GetComponent<TextMeshProUGUI>().text = "" + val;
            newButtonObj.transform.Find("Detail").GetComponent<TextMeshProUGUI>().text = Base.dicWeapon[key].Name;

            int listSize = selectPageButtonList.Count;
            newButtonObj.GetComponent<Button>().onClick.AddListener(() => OnButtonWeaponClicked(listSize, key));

            newButtonObj.SetActive(true);
            selectPageButtonList.Add(newButtonObj);
    }

    public void OnButtonWeaponClicked(int index, string key){
        equipmentFlagKey = key;
        bool shouldSubtract;
        bool isActive = selectPageButtonList[index].transform.Find("switch").gameObject.activeSelf;

        equipmentDetailText.text = "";

        shouldSubtract = actorEquipmentKey == "" ? false : true ;

        tempStatus[0] = Base.player[selectActor].HPMax - (shouldSubtract ? Base.dicWeapon[actorEquipmentKey].HP : 0) + (isActive ? 0 : Base.dicWeapon[key].HP);
        equipmentDetailText.text += "生命\t\t" + Base.player[selectActor].HPMax + "\t>\t" + GetColorTag(tempStatus[0], Base.player[selectActor].HPMax) + tempStatus[0] + "</color>\n";

        tempStatus[1] = Base.player[selectActor].MPMax - (shouldSubtract ? Base.dicWeapon[actorEquipmentKey].MP : 0) + (isActive ? 0 : Base.dicWeapon[key].MP);
        equipmentDetailText.text += "魔力\t\t" + Base.player[selectActor].MPMax + "\t>\t" + GetColorTag(tempStatus[1], Base.player[selectActor].MPMax) + tempStatus[1] + "</color>\n";

        tempStatus[2] = Base.player[selectActor].Attack - (shouldSubtract ? Base.dicWeapon[actorEquipmentKey].ATK : 0) + (isActive ? 0 : Base.dicWeapon[key].ATK);
        equipmentDetailText.text += "物攻\t\t" + Base.player[selectActor].Attack + "\t>\t" + GetColorTag(tempStatus[2], Base.player[selectActor].Attack) + tempStatus[2] + "</color>\n";

        tempStatus[3] = Base.player[selectActor].Magic - (shouldSubtract ? Base.dicWeapon[actorEquipmentKey].MAG : 0) + (isActive ? 0 : Base.dicWeapon[key].MAG);
        equipmentDetailText.text += "魔攻\t\t" + Base.player[selectActor].Magic + "\t>\t" + GetColorTag(tempStatus[3], Base.player[selectActor].Magic) + tempStatus[3] + "</color>\n";

        tempStatus[4] = Base.player[selectActor].Defense - (shouldSubtract ? Base.dicWeapon[actorEquipmentKey].DEF : 0) + (isActive ? 0 : Base.dicWeapon[key].DEF);
        equipmentDetailText.text += "防禦\t\t" + Base.player[selectActor].Defense + "\t>\t" + GetColorTag(tempStatus[4], Base.player[selectActor].Defense) + tempStatus[4] + "</color>\n";

        tempStatus[5] = Base.player[selectActor].Speed - (shouldSubtract ? Base.dicWeapon[actorEquipmentKey].AGI : 0) + (isActive ? 0 : Base.dicWeapon[key].AGI);
        equipmentDetailText.text += "靈巧\t\t" + Base.player[selectActor].Speed + "\t>\t" + GetColorTag(tempStatus[5], Base.player[selectActor].Speed) + tempStatus[5] + "</color>\n";

        if(equipmentFlag == index){
            selectPageButtonList[index].transform.Find("switch").gameObject.SetActive(!isActive);
        }
        else{
            selectPageButtonList[equipmentFlag].transform.Find("switch").gameObject.SetActive(false);
            selectPageButtonList[index].transform.Find("switch").gameObject.SetActive(true);
            equipmentFlag = index;
        }
    }

    private string GetColorTag(int newValue, int originalValue)
    {
        if (newValue < originalValue)
        {
            return "<color=#ff0000>"; 
        }
        else if (newValue > originalValue)
        {
            return "<color=green>"; 
        }
        else
        {
            return "<color=white>";
        }
    }

    public void OpenSelectPage(string _selectType){
        tempStatus[0] = Base.player[selectActor].HPMax;
        tempStatus[1] = Base.player[selectActor].MPMax;
        tempStatus[2] = Base.player[selectActor].Attack;
        tempStatus[3] = Base.player[selectActor].Magic ;
        tempStatus[4] = Base.player[selectActor].Defense;
        tempStatus[5] = Base.player[selectActor].Speed;

        selectType = _selectType;
        selectPage.SetActive(true);
        UpdateSelectPage(selectType);
    }

    public void CloseSelectPage(){
        selectPage.SetActive(false);
    }

    public void FinishSelectPage(){
        Base.player[selectActor].HPMax = tempStatus[0];
        Base.player[selectActor].MPMax = tempStatus[1];
        Base.player[selectActor].Attack = tempStatus[2];
        Base.player[selectActor].Magic = tempStatus[3];
        Base.player[selectActor].Defense = tempStatus[4];
        Base.player[selectActor].Speed = tempStatus[5];

        for (int i = 0; i<Base.player[selectActor].WeaponList.Count ; i++){
            var temp = Base.player[selectActor].WeaponList[i];
            if (Base.dicWeapon[temp].Category == weaponType){
                if (Base.bagWeapons.ContainsKey(temp)){
                    Base.bagWeapons[temp] += 1;
                }else{
                    Base.bagWeapons[temp] = 1;
                }
                Base.player[selectActor].WeaponList.RemoveAt(i);
                break;
            }
        }


        if(selectPageButtonList.Count != 0 && selectPageButtonList[equipmentFlag].transform.Find("switch").gameObject.activeSelf){
            Base.player[selectActor].WeaponList.Add(equipmentFlagKey);
            Base.bagWeapons[equipmentFlagKey] -= 1;
            if(Base.bagWeapons[equipmentFlagKey] == 0){
                Base.bagWeapons.Remove(equipmentFlagKey);
            }
        }

        UpdateEquipment();
        
        CloseSelectPage();
    }

    public void SelectActor(int id){
        selectActor = id;
        UpdateEquipment();
        CloseSelectPage();
    }
}

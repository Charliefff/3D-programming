using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System.Linq;

public class ShopController : MonoBehaviour
{
    public List<string> itemButtonList = new List<string>();
    public List<string> weaponButtonList = new List<string>();
    public List<string> skillButtonList = new List<string>();
    public List<string> bagButtonList = new List<string>();
    public List<int> bagCountList = new List<int>();


    public Transform itemParent;
    public GameObject itemButton;
    public Image itemImage; 
    public TextMeshProUGUI itemText; 
    public Button selectButton;
    public GameObject selectPage;
    public TextMeshProUGUI selectPageCount; 
    public TextMeshProUGUI selectPageCaculate; 
    public GameObject detailPage;
    public GameObject alertPage;
    public TextMeshProUGUI moneyCount; 
    public GameObject shop;





    private List<GameObject> shopButtonList = new List<GameObject>();
    private bool isFirst;
    private string state;
    private int state_id;
    private int state_money;

    void Start()
    {
        UpdateShop();
    }

    void Update()
    {
        
    }

    public void UpdateShop(){
        isFirst = true;
        state = "item";
        UpdateShopItem();
        selectPage.SetActive(false);        
        alertPage.SetActive(false);
        moneyCount.text = "" + Base.money + " G";
    }

    public void ShopMenuButton(string type){
        state = type;
        state_id = 0;
        isFirst = true;
        ShopButtonListClean();
        if(type == "item"){
            UpdateShopItem();
        }else if(type == "weapon"){
            UpdateShopWeapon();
        }else if(type == "skill"){
            UpdateShopSkill();
        }else if(type == "bag"){
            UpdateShopBag();
        }
        selectPage.SetActive(false);
        alertPage.SetActive(false);        
    }

    public void SelectButton(bool open=false){
        if(!open){
            selectPage.SetActive(false);
        }else{
            selectPage.SetActive(true);
            selectPageCount.text = "1";
            UpdateSelectPage();
        }
    }

    public int UpdateSelectPage(){
        int count;
        if(state == "bag"){
            count = Base.money + int.Parse(selectPageCount.text) * state_money; 
        }else{
            count = Base.money - int.Parse(selectPageCount.text) * state_money; 
        }

        selectPageCaculate.text = "" + Base.money + "G  ->  " + count + "G";
        return count;
    }

    public void SelectPageButton(string input){
        if(input == "alertPage") alertPage.SetActive(false);
        if(input == "-10") selectPageCount.text = "" + (int.Parse(selectPageCount.text) - 10);
        if(input == "-1") selectPageCount.text = "" + (int.Parse(selectPageCount.text) - 1);
        if(input == "+1") selectPageCount.text = "" + (int.Parse(selectPageCount.text) + 1);
        if(input == "+10") selectPageCount.text = "" + (int.Parse(selectPageCount.text) + 10);

        if(state != "bag" && int.Parse(selectPageCount.text) > 99){
            selectPageCount.text = "99";
        }else if(state == "bag" && int.Parse(selectPageCount.text) > bagCountList[state_id]){
            selectPageCount.text = "" + bagCountList[state_id];
        }else if(int.Parse(selectPageCount.text) < 1){
            selectPageCount.text = "1";
        }        
        
        int count = UpdateSelectPage();
        if(input == "false") SelectButton();
        if(input == "true"){
            if(count < 0){
                alertPage.SetActive(true);
            }else{
                Base.money = count;
                moneyCount.text = "" + Base.money + " G";

                if(state == "item"){
                    if(Base.bagConsumable.ContainsKey("" + itemButtonList[state_id])){
                        Base.bagConsumable["" + itemButtonList[state_id]] += int.Parse(selectPageCount.text);
                    }else{
                        Base.bagConsumable["" + itemButtonList[state_id]] = int.Parse(selectPageCount.text);
                    }
                }else if(state == "weapon"){
                    if(Base.bagWeapons.ContainsKey("" + weaponButtonList[state_id])){
                        Base.bagWeapons["" + weaponButtonList[state_id]] += int.Parse(selectPageCount.text);
                    }else{
                        Base.bagWeapons["" + weaponButtonList[state_id]] = int.Parse(selectPageCount.text);
                    }
                }else if(state == "skill"){

                }else{
                    if(state_id<Base.bagConsumable.Count){
                        Base.bagConsumable["" + bagButtonList[state_id]] -= int.Parse(selectPageCount.text);
                        if(Base.bagConsumable["" + bagButtonList[state_id]] == 0){
                            Base.bagConsumable.Remove("" + bagButtonList[state_id]);
                        }
                    }else{
                        Base.bagWeapons["" + bagButtonList[state_id]] -= int.Parse(selectPageCount.text);
                        if(Base.bagWeapons["" + bagButtonList[state_id]] == 0){
                            Base.bagWeapons.Remove("" + bagButtonList[state_id]);
                        }
                    }
                    ShopMenuButton("bag");
                }
                SelectButton();
            }
        }
    }

    public void ShopButtonListClean(){
        foreach(var temp in shopButtonList){
            Destroy(temp);
        }
        shopButtonList.Clear();
    }

    public void UpdateShopItem(){
        int id=0;
        foreach(var temp in itemButtonList){
            if(isFirst){
                isFirst = false;
                state_money = Base.dicConsumable[temp].BuyPrice;

                string detail = "";          
                detail += Base.dicConsumable[temp].Name + "\n\n";
                detail += "Type : " + Base.dicConsumable[temp].Type + "\n";
                detail += "Quality : " + Base.dicConsumable[temp].Quality + "\n";
                detail += "Description : " + Base.dicConsumable[temp].Description + "\n";
                
                SetShopDetail(Base.dicConsumable[temp].Image,detail);
            }

            CreatButton(Base.dicConsumable[temp].Image,Base.dicConsumable[temp].BuyPrice,Base.dicConsumable[temp].Name,id,""+temp);
            id++;
        }
        if(isFirst) SetShopDetail("","");
    }

    public void UpdateShopWeapon(){
        int id=0;
        foreach(var temp in weaponButtonList){
            if(isFirst){
                isFirst = false;
                state_money = Base.dicWeapon[temp].BuyPrice;

                string detail = "";  
                detail += Base.dicWeapon[temp].Name + "\n\n";
                detail += "Category : " + Base.dicWeapon[temp].Category + "\n";
                detail += "Quality : " + Base.dicWeapon[temp].Quality + "\n";
                detail += "Description : " + Base.dicWeapon[temp].Description + "\n";
                
                SetShopDetail(Base.dicWeapon[temp].Image,detail);
            }

            CreatButton(Base.dicWeapon[temp].Image,Base.dicWeapon[temp].BuyPrice,Base.dicWeapon[temp].Name,id,""+temp);
            id++;
        }
        if(isFirst) SetShopDetail("","");
    }

    public void UpdateShopSkill(){
        int id=0;
        foreach(var temp in skillButtonList){
            if(isFirst){
                isFirst = false;
                state_money = 100;
                SetShopDetail("book","123");
            }

            CreatButton("book",100,Base.dicSkill[temp].SkillName,id);
            id++;
        }
        if(isFirst) SetShopDetail("","");
    }

    public void UpdateShopBag(){
        bagCountList.Clear();
        bagButtonList.Clear();
        int id=0;
        foreach(var temp in Base.bagConsumable){
            if(isFirst){
                isFirst = false;
                state_money = Base.dicConsumable[temp.Key].SellPrice;

                string detail = "";  
                detail += Base.dicConsumable[temp.Key].Name + "\n\n";
                detail += "Type : " + Base.dicConsumable[temp.Key].Type + "\n";
                detail += "Quality : " + Base.dicConsumable[temp.Key].Quality + "\n";
                detail += "Description : " + Base.dicConsumable[temp.Key].Description + "\n";

                SetShopDetail(Base.dicConsumable[temp.Key].Image,detail);
            }

            CreatButton(Base.dicConsumable[temp.Key].Image,Base.dicConsumable[temp.Key].SellPrice,Base.dicConsumable[temp.Key].Name,id,temp.Key);
            bagCountList.Add(temp.Value);
            bagButtonList.Add(temp.Key);
            id++;
        }
        foreach(var temp in Base.bagWeapons){
            if(isFirst){
                isFirst = false;
                state_money = Base.dicWeapon[temp.Key].SellPrice;

                string detail = "";  
                detail += Base.dicWeapon[temp.Key].Name + "\n\n";
                detail += "Category : " + Base.dicWeapon[temp.Key].Category + "\n";
                detail += "Quality : " + Base.dicWeapon[temp.Key].Quality + "\n";
                detail += "Description : " + Base.dicWeapon[temp.Key].Description + "\n";

                SetShopDetail(Base.dicWeapon[temp.Key].Image,detail);
            }

            CreatButton(Base.dicWeapon[temp.Key].Image,Base.dicWeapon[temp.Key].SellPrice,Base.dicWeapon[temp.Key].Name,id,temp.Key);
            bagCountList.Add(temp.Value);
            bagButtonList.Add(temp.Key);
            id++;
        }

        if(isFirst) SetShopDetail("","");
    }

    private void CreatButton(string img, int val, string name, int index, string key = ""){
        GameObject newButtonObj = Instantiate(itemButton, itemParent);
        newButtonObj.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(img);
        newButtonObj.transform.Find("Count").GetComponent<TextMeshProUGUI>().text = "" + val;
        newButtonObj.transform.Find("Detail").GetComponent<TextMeshProUGUI>().text = name;

        string detail = "";
        if(state == "item"){
            detail += Base.dicConsumable[key].Name + "\n\n";
            detail += "Type : " + Base.dicConsumable[key].Type + "\n";
            detail += "Quality : " + Base.dicConsumable[key].Quality + "\n";
            detail += "Description : " + Base.dicConsumable[key].Description + "\n";
        }else if(state == "weapon"){
            detail += Base.dicWeapon[key].Name + "\n\n";
            detail += "Category : " + Base.dicWeapon[key].Category + "\n";
            detail += "Quality : " + Base.dicWeapon[key].Quality + "\n";
            detail += "Description : " + Base.dicWeapon[key].Description + "\n";
        }else if(state == "skill"){

        }else if(state == "bag"){
            if(index < Base.bagConsumable.Count){
                detail += Base.dicConsumable[key].Name + "\n\n";
                detail += "Type : " + Base.dicConsumable[key].Type + "\n";
                detail += "Quality : " + Base.dicConsumable[key].Quality + "\n";
                detail += "Description : " + Base.dicConsumable[key].Description + "\n";
            }else{
                detail += Base.dicWeapon[key].Name + "\n\n";
                detail += "Category : " + Base.dicWeapon[key].Category + "\n";
                detail += "Quality : " + Base.dicWeapon[key].Quality + "\n";
                detail += "Description : " + Base.dicWeapon[key].Description + "\n";
            }
        }

        newButtonObj.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked(index,val,img,detail));

        newButtonObj.SetActive(true);
        shopButtonList.Add(newButtonObj);

        selectPage.SetActive(false);
        alertPage.SetActive(false);
    }

    private void OnButtonClicked(int index, int val, string img, string detail){
        SetShopDetail(img,detail);
        state_id = index;
        state_money = val;
    }

    private void SetShopDetail(string img, string detail){
        if(img==""){
            detailPage.SetActive(false);
        }else{
            detailPage.SetActive(true);
            itemImage.sprite = Resources.Load<Sprite>(img);
        }

        if(state == "bag"){
            selectButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "販賣";
        }else if(state == "skill"){
            selectButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "學習";
        }else{
            selectButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "購買";
        }
        itemText.text = detail;
    }

    public void LeaveShop(){
        shop.SetActive(false);
    }
}

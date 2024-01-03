using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System.Linq;

public class DashBoardItemController : MonoBehaviour
{
    public Transform itemParent;
    public GameObject itemButton;
    public List<GameObject> itemButtonList = new List<GameObject>();
    public Image itemImage; 
    public TextMeshProUGUI itemText; 


    // Start is called before the first frame update
    void Start()
    {
        UpdateItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateItem(){
        foreach(var temp in itemButtonList){
            Destroy(temp);
        }
        itemButtonList.Clear();

        bool isFirst = true;
        var bagConsumableSort = Base.bagConsumable.OrderBy(pair => int.Parse(pair.Key));
        var bagWeaponsSort = Base.bagWeapons.OrderBy(pair => int.Parse(pair.Key));

        foreach (var consumable in bagConsumableSort){
            string key = "" + consumable.Key;
            int val = consumable.Value;

            if(isFirst){
                OnButtonItemClicked(key, val, "consumable");
                isFirst = false;
            }

            GameObject newButtonObj = Instantiate(itemButton, itemParent);

            newButtonObj.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(Base.dicConsumable[key].Image);
            newButtonObj.transform.Find("Count").GetComponent<TextMeshProUGUI>().text = "" + val;
            newButtonObj.transform.Find("Detail").GetComponent<TextMeshProUGUI>().text = Base.dicConsumable[key].Name;

            newButtonObj.GetComponent<Button>().onClick.AddListener(() => OnButtonItemClicked(key, val, "consumable"));

            newButtonObj.SetActive(true);
            itemButtonList.Add(newButtonObj);
        }

        foreach (var weapon in bagWeaponsSort){
            string key = "" + weapon.Key;
            int val = weapon.Value;

            if(isFirst){
                OnButtonItemClicked(key, val, "weapon");
                isFirst = false;
            }

            GameObject newButtonObj = Instantiate(itemButton, itemParent);

            newButtonObj.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(Base.dicWeapon[key].Image);
            newButtonObj.transform.Find("Count").GetComponent<TextMeshProUGUI>().text = "" + val;
            newButtonObj.transform.Find("Detail").GetComponent<TextMeshProUGUI>().text = Base.dicWeapon[key].Name;

            newButtonObj.GetComponent<Button>().onClick.AddListener(() => OnButtonItemClicked(key, val, "weapon"));

            newButtonObj.SetActive(true);
            itemButtonList.Add(newButtonObj);
        }
    }

    private void OnButtonItemClicked(string key, int count, string type)
    {
        itemText.text = "";

        if(type == "consumable"){
            itemText.text += Base.dicConsumable[key].Name + "\n\n";
            itemText.text += "數量 : " +  count + "\n";
            itemText.text += "分類 : " +  Base.dicConsumable[key].Type + "\n";
            itemText.text += "品質 : " +  Base.dicConsumable[key].Quality + "\n";
            itemText.text += "描述 : " +  Base.dicConsumable[key].Description + "\n";        

            itemImage.sprite = Resources.Load<Sprite>(Base.dicConsumable[key].Image);
        }else if(type == "weapon"){
            itemText.text += Base.dicWeapon[key].Name + "\n\n";
            itemText.text += "數量 : " +  count + "\n";
            itemText.text += "分類 : " +  Base.dicWeapon[key].Category + "\n";
            itemText.text += "品質 : " +  Base.dicWeapon[key].Quality + "\n";
            itemText.text += "描述 : " +  Base.dicWeapon[key].Description + "\n";        

            itemImage.sprite = Resources.Load<Sprite>(Base.dicWeapon[key].Image);
        }
        
    }
}

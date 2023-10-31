using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


[System.Serializable]
public class ItemInfo
{
    public string name;
    public string type;
    public string quantity;
    public string description;
    public int sellprice;
    public int buyprice;
    public int hp;
    public int mp;
}

public class BattleSceneMgr : MonoBehaviour
{
    public Text itemDicText; 

    public void HandleBloodButton()
    {
        int itemID = 1;
        int itemQuantity = 1;
        string itemName = "Blood potion";

        if (Base.serializedItemDic.ItemDic.ContainsKey(itemID))
        {
            Base.serializedItemDic.ItemDic[itemID] += itemQuantity;
        }
        else
        {
            Base.serializedItemDic.ItemDic[itemID] = itemQuantity;
        }
        Debug.Log("Added " + itemName + " to the inventory.");
    }

    public void HandleManaButton()
    {
        int itemID = 4;
        int itemQuantity = 1;
        string itemName = "Mana potion";

        if (Base.serializedItemDic.ItemDic.ContainsKey(itemID))
        {
            Base.serializedItemDic.ItemDic[itemID] += itemQuantity;
        }
        else
        {
            Base.serializedItemDic.ItemDic[itemID] = itemQuantity;
        }
        Debug.Log("Added " + itemName + " to the inventory.");
    }

    public void HandleBagButton()
    {
        if (itemDicText != null)
        {
            itemDicText.text = "Item Dictionary:\n";

            foreach (var kvp in Base.serializedItemDic.ItemDic)
            {
                itemDicText.text += "Item ID: " + kvp.Key + ", Quantity: " + kvp.Value + "\n";
                Debug.Log("Item ID: " + kvp.Key + ", Quantity: " + kvp.Value);
            }
        }

        foreach (var kvp in Base.serializedItemDic.ItemDic)
        {
           
            Debug.Log("Item ID: " + kvp.Key + ", Quantity: " + kvp.Value);
        }
    }


    public void ItemInfo()
    {

        string itemID = "1";
        string filePath = "Assets/Json/Item.json";

        if (File.Exists(filePath))
        {

            string json = File.ReadAllText(filePath);

            if (!string.IsNullOrEmpty(json))
            {

                Dictionary<string, ItemInfo> itemInfoDict = JsonUtility.FromJson<Dictionary<string, ItemInfo>>(json);

                if (itemInfoDict.ContainsKey(itemID))
                {
                    ItemInfo itemInfo = itemInfoDict[itemID];
                    Debug.Log("Item ID: " + itemID);
                    Debug.Log("Item Name: " + itemInfo.name);
                    Debug.Log("Item Description: " + itemInfo.type);
                    Debug.Log("Item Value: " + itemInfo.quantity);
                }
                else
                {
                    Debug.Log("Item with ID " + itemID + " not found in itemInfoDict.");
                }
            }
            else
            {
                Debug.Log("JSON file is empty.");
            }
        }

        else
        {
            Debug.Log("JSON file not found at " + filePath);
        }

    }

}

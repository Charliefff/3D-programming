using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBoardItemController : MonoBehaviour
{
    public Transform itemParent;
    public GameObject itemButton;
    public List<GameObject> itemButtonList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        ItemUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemUpdate(){
        for(int i=0;i<100;i++){
            itemButtonList.Add(Instantiate(itemButton,itemParent));
            itemButtonList[itemButtonList.Count - 1].SetActive(true);
        }
    }
}

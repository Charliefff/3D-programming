using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public int id;
    public int type;
    public string name;
    public string content;
    public int next_id;
    public string effect;
    public string target;

    public Dialog(int _id,int _type,string _name,string _content,int _next_id,string _effect = "",string _target = ""){
        id = _id;
        type = _type;
        name = _name;
        content = _content;
        next_id = _next_id;
        effect = _effect;
        target = _target;
    }

}

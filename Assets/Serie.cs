using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serie : MonoBehaviour
{
    public string name;
    public bool inList { get; private set; }
    void Start()
    {
        inList = true;
    }
    public void InList() => inList = !inList;
}

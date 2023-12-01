using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OnePanelData")]
public class OnePanelData : ScriptableObject
{
    public List<ObjectData> data = new List<ObjectData>();
    public ObjType type;
}
[Serializable]
public class ObjectData
{
    public string name;
    public Sprite icon;
    public GameObject prefab;
}
public enum ObjType
{
    Pet,
    Food,
}
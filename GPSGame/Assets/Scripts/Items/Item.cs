using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable/Item")]
public class Item : ScriptableObject{
    public string ID;
    public string Name;
    public string Description;
    public bool Enabled;
}
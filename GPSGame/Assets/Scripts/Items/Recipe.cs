using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Scriptable/Recipe")]
public class Recipe : ScriptableObject{
    public string ID;
    public string Name;
    public string Description;
    public bool Enabled;
    public List<Item> InputItems;
    public List<int> InputAmounts;
    public List<Item> OutputItems;
    public List<int> OutputAmounts;
}
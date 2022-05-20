using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Building", menuName = "Scriptable/Building")]
public class Building : ScriptableObject{
    public string ID;
    public string Name;
    public string Description;
    public bool Enabled;
    public Building Parent;
}
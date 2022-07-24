using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Skill", menuName = "Scriptable/Skill")]
public class Skill : ScriptableObject{
    public string ID;
    public string Name;
    public string Description;
    public bool Enabled;
    public Skill Parent;
    public float Difficulty = 1f;
}
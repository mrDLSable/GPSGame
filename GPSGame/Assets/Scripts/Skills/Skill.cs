using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Skill", menuName = "Scriptable/Skill")]
public class Skill : ScriptableObject{
    public string ID;
    public string Name;
    public string Description;
    public bool Enabled;
    public Skill Parent;
    public float Difficulty = 1f;
    public List<RecipeList> RecipeLists;
}
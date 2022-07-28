using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe List", menuName = "Scriptable/Recipe List")]
public class RecipeList : ScriptableObject{
    public string ID;
    public string Name;
    public string Description;
    public bool Enabled;
    public List<Recipe> Recipes;
}
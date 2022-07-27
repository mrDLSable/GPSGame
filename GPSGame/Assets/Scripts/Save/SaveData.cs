using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<WorldData> worlds;
    public string currentSkill;
    public SaveData(){
        worlds = new List<WorldData>();
        currentSkill = SaveManager._saveManager.DefaultSkill.ID;
        if(!worldExists(currentSkill)) CreateWorld(currentSkill);
    }

    public bool worldExists(string skillID){
        foreach(WorldData world in worlds){
            if(world.SkillID == skillID){
                return true;
            }
        }
        return false;
    }

    public void CreateWorld(string skillID){
        WorldData world = new WorldData(skillID);
        worlds.Add(world);
    }

    public WorldData GetCurrentWorld(){
        foreach(WorldData world in worlds){
            if(world.SkillID == currentSkill){
                return world;
            }
        }
        CreateWorld(currentSkill);
        return GetCurrentWorld();
    }
}

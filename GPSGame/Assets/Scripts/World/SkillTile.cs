using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SkillTile
{
    public string skillID;
    public int x;
    public int y;
    public DateTime lastGather;
    public int level;

    public bool IsGatherable(){
        if(lastGather.AddHours(Math.Pow(2, level)) <= DateTime.Now){
            return true;
        }
        return false;
    }
}

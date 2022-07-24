using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTile
{
    public string skillID;
    public int x;
    public int y;
    public DateTime lastGather;
    public int level;

    public bool IsGatherable(){
        DateTime epoch = lastGather.AddHours(Math.Pow(2, level));
        if(epoch <= DateTime.Now){
            if(lastGather != new DateTime()){
                Debug.Log(lastGather);
            }
            return true;
        }
        return false;
    }

    public void Gather(){
        if(IsGatherable()){
            lastGather = DateTime.Now;
            if(level < GetLevelCap()){
                level++;
            }
        }
    }

    public int GetLevelCap(){
        return 5;
    }
}

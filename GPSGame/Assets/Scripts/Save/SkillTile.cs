using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillTile
{
    public string skillID;
    public int x;
    public int y;
    public DateTime lastGather;
    public int level;

    public bool IsGatherable(){
        DateTime epoch = lastGather.AddSeconds(GPSManager._gpsManager.zoneDeltaTime * Math.Pow(2, level));
        if(epoch <= DateTime.Now){
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

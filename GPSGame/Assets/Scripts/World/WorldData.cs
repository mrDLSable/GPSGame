using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData
{
    public string SkillID;
    public List<SkillTile> Tiles;

    public WorldData(string skillID){
        this.SkillID = skillID;
        Tiles = new List<SkillTile>();
    }

    public SkillTile GetTile(int x, int y){
        foreach (SkillTile tile in Tiles)
        {
            if(tile.x == x && tile.y == y){
                return tile;
            }
        }
        return CreateTile(x, y);
    }

    private SkillTile CreateTile(int x, int y){
        SkillTile skillTile = new SkillTile();
        skillTile.skillID = SkillID;
        skillTile.x = x;
        skillTile.y = y;
        Tiles.Add(skillTile);
        return skillTile;
    }
}

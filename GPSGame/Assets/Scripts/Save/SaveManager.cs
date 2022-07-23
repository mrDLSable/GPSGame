using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private SaveData saveData;
    public static SaveManager _saveManager;

    public Skill DefaultSkill;

    // Start is called before the first frame update
    void Start()
    {
        _saveManager = this;
        if(true){
            saveData = new SaveData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SkillTile GetSkillTile(TileCoords coords){
        return saveData.GetCurrentWorld().GetTile(coords.x, coords.y);
    }
}

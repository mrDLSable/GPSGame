using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private SaveData saveData;
    public static SaveManager _saveManager;

    public Skill DefaultSkill;
    DateTime latestSave;

    private bool saveopen = false;

    // Start is called before the first frame update
    void Start()
    {
        _saveManager = this;
        SaveData tempSaveData = Load("latest");
        if(tempSaveData != null){
            saveData = tempSaveData;
        }else{
            saveData = new SaveData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(latestSave.AddSeconds(5) <= DateTime.Now){
            Save(saveData, "latest");
        }
    }

    public SkillTile GetSkillTile(TileCoords coords){
        return saveData.GetCurrentWorld().GetTile(coords.x, coords.y);
    }

    public  void Save(SaveData saveData, string saveName){
        if(!saveopen){
            saveopen = true;
            Debug.Log("Saving");
            if(!Directory.Exists($"{Application.persistentDataPath}/saves/")){
                Directory.CreateDirectory($"{Application.persistentDataPath}/saves/");
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Create($"{Application.persistentDataPath}/saves/{saveName}.save");
            binaryFormatter.Serialize(file, saveData);
            file.Close();
            latestSave = DateTime.Now;
            saveopen = false;
        }
        
    }

    public SaveData Load(string saveName){
        Debug.Log("Loading");
        if(File.Exists($"{Application.persistentDataPath}/saves/{saveName}.save")){
            saveopen = true;
            Debug.Log($"Save Found: {Application.persistentDataPath}/saves/{saveName}.save");
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open($"{Application.persistentDataPath}/saves/{saveName}.save", FileMode.Open);
            SaveData tempSaveData = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();
            saveopen = false;
            return tempSaveData;
        }
        return null;
    }
}

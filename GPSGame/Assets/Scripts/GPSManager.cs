using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSManager : MonoBehaviour
{
    private Vector2 GPSCoords;
    private Vector2 DebugCoords = new Vector2(-0.12754661804138964f, 51.50726853453877f); //London

    public GameObject TilePrefab;

    public Dictionary<TileCoords, TileData> WorldTiles = new Dictionary<TileCoords, TileData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetGPSCoords();
        TileCoords tileCoords = GetOpenMapsCoords(GPSCoords);
        if(!WorldTiles.ContainsKey(tileCoords)){
            Debug.Log(tileCoords);
            InitiateTile(tileCoords);
        }
    }

    private void InitiateTile(TileCoords coords)
    {
        GameObject temp = GameObject.Instantiate(TilePrefab);
        TileData data = new TileData(coords);
        temp.GetComponent<TileManager>().tileData = data;
        WorldTiles.Add(coords, data);
    }

    /// <summary>
    /// Set the GPS coords
    /// </summary>
    private void SetGPSCoords(){
        switch(Application.platform){
            case RuntimePlatform.WindowsEditor:
                GPSCoords = DebugCoords;
                break;
        }
    }

    /// <summary>
    /// Convert the GPS coordinates to Open Maps tile coordinates.
    /// </summary>
    /// <param name="GPSCoords"></param>
    /// <returns></returns>
    private TileCoords GetOpenMapsCoords(Vector2 GPSCoords){
        TileCoords tileCoords = new TileCoords(GPSCoords);
        return tileCoords;
    }
}

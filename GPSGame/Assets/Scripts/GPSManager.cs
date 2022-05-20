using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSManager : MonoBehaviour
{
    public static Vector2 GPSCoords;
    private Vector2 DebugCoords = new Vector2(-0.12754661804138964f, 51.50726853453877f); //London

    public GameObject TilePrefab;

    public Dictionary<TileCoords, TileData> WorldTiles;

    public static TileCoords centerTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetGPSCoords();
        if(WorldTiles == null){
            InitiateWorld();
        }

        if(Input.GetKeyDown(KeyCode.W)){
            GPSCoords.y += 0.005f;
        }
    }

    /// <summary>
    /// Initiates the world
    /// </summary>
    private void InitiateWorld(){
        WorldTiles = new Dictionary<TileCoords, TileData>();
        TileCoords tileCoords = GetOpenMapsCoords(GPSCoords);
        centerTile = tileCoords;
        InitiateTile(tileCoords);
        List<TileCoords> surrounding = tileCoords.GetSurrounding(3);
        Debug.Log(surrounding.Count);
        foreach(TileCoords tc in surrounding){
            InitiateTile(tc);
        }
    }

    /// <summary>
    /// Initiates a single tile
    /// </summary>
    /// <param name="coords"></param>
    private void InitiateTile(TileCoords coords)
    {
        Debug.Log($"Initiating tile {coords}");
        if(WorldTiles.ContainsKey(coords)){

        }else{
            GameObject temp = GameObject.Instantiate(TilePrefab);
            float xoffset = centerTile.x - coords.x;
            float yoffset = coords.y - centerTile.y;
            temp.transform.position = new Vector3(xoffset, yoffset, 0);
            TileData data = new TileData(coords);
            temp.GetComponent<TileManager>().tileData = data;
            WorldTiles.Add(coords, data);
        }
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

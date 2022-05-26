using System;
using System.Collections.Generic;
using UnityEngine;

public class GPSManager : MonoBehaviour
{
    public static Vector2 GPSCoords;
    private Vector2 DebugCoords = new Vector2(-0.12754661804138964f, 51.50726853453877f); //London

    public GameObject TilePrefab;

    public Dictionary<TileCoords, TileData> WorldTiles;

    public static TileCoords centerTile;
    public const int TileRadius = 6;

    // Update is called once per frame
    void Update()
    {
        if(GPSCoords == new Vector2()){
            SetGPSCoords();
        }
        
        if(WorldTiles == null){
            InitiateWorld();
        }

        switch(Application.platform){
            case RuntimePlatform.Android:
                if(Input.location.status == LocationServiceStatus.Running){
                    GPSManager_android.Update();
                }else{
                    StartCoroutine(GPSManager_android.Start());
                }
                break;
            default:
                UpdateDebug();
                break;
        }

        TileCoords newCoords = new TileCoords(GPSCoords);

        if(!(newCoords.x == centerTile.x && newCoords.y == centerTile.y)){
            centerTile = newCoords;
            UpdateTilePositions();
            LoadWorldAroundCenter(TileRadius);
        }
    }

    /// <summary>
    /// Update the GPS position when in editor
    /// </summary>
    private void UpdateDebug(){
        float moveDelta = 0.0001f;
        if(Input.GetKeyDown(KeyCode.W)) GPSCoords.y += moveDelta;
        if(Input.GetKeyDown(KeyCode.S)) GPSCoords.y -= moveDelta;
        if(Input.GetKeyDown(KeyCode.A)) GPSCoords.x -= moveDelta;
        if(Input.GetKeyDown(KeyCode.D)) GPSCoords.x += moveDelta;
    }

    /// <summary>
    /// Create tiles of a certain radius around the player position
    /// </summary>
    /// <param name="radius">The radius to be generated</param>
    private void LoadWorldAroundCenter(int radius){
        if(!WorldTiles.ContainsKey(centerTile)) InitiateTile(centerTile);
        List<TileCoords> surrounding = centerTile.GetSurrounding(radius);
        foreach(TileCoords tc in surrounding){
            InitiateTile(tc);
        }

        foreach (TileData tile in WorldTiles.Values)
        {
            TileCoords coords = tile.GetCoords();
            if(Math.Abs(coords.x - centerTile.x) > TileRadius || Math.Abs(coords.y - centerTile.y) > TileRadius){
                tile.DestroyGameObject();
            }else{
                if(!tile.HasGameObject()){
                    CreateTileGameObject(tile);
                }
            }
        }
    }

    /// <summary>
    /// Reposition the tiles around the center tile
    /// </summary>
    private void UpdateTilePositions(){
        foreach(TileData data in WorldTiles.Values){
            data.PositionGameObject();            
        }
    }

    /// <summary>
    /// Initiates the world
    /// </summary>
    private void InitiateWorld(){
        WorldTiles = new Dictionary<TileCoords, TileData>();
        TileCoords tileCoords = new TileCoords(GPSCoords);
        centerTile = tileCoords;
        LoadWorldAroundCenter(TileRadius);
    }

    /// <summary>
    /// Initiates a single tile
    /// </summary>
    /// <param name="tileCoords"></param>
    private void InitiateTile(TileCoords tileCoords)
    {
        if(WorldTiles.ContainsKey(tileCoords)){

        }else{
            TileData tileData = new TileData(tileCoords);
            CreateTileGameObject(tileData);
            WorldTiles.Add(tileCoords, tileData);
        }
    }

    /// <summary>
    /// Create a gameobject for a given tiledata object
    /// </summary>
    /// <param name="tileData"></param>
    private void CreateTileGameObject(TileData tileData){
        GameObject tileGameObject = GameObject.Instantiate(TilePrefab);
        tileData.SetGameObject(tileGameObject);
        tileGameObject.GetComponent<TileManager>().tileData = tileData;
        tileData.PositionGameObject();
    }

    /// <summary>
    /// Set the GPS coords
    /// </summary>
    private void SetGPSCoords(){
        switch(Application.platform){
            case RuntimePlatform.Android:
                break;
            default:
                GPSCoords = DebugCoords;
                break;
        }
    }
}

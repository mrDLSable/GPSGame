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
    public const int TileRadius = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        TileCoords tileCoords = GetOpenMapsCoords(GPSCoords);
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
            default:
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

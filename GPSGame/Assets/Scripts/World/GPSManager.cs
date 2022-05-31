using System;
using System.Collections.Generic;
using UnityEngine;

public class GPSManager : MonoBehaviour
{
    public static Vector2 GPSCoords;
    private Vector2 DebugCoords = new Vector2(-0.12754661804138964f, 51.50726853453877f); //London

    public GameObject TilePrefab;

    public Dictionary<TileCoords, TileData> WorldTiles;
    public List<GPSPathPoint> pathPoints;

    public static TileCoords centerTile;
    public static TileCoords currentTile;
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

        if(!(newCoords.x == currentTile.x && newCoords.y == currentTile.y)){
            currentTile = newCoords;
            UpdateTilePositions();
            LoadWorldAroundCurrent(TileRadius);
        }

        UpdateGPSPath();
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
    private void LoadWorldAroundCurrent(int radius){
        if(!WorldTiles.ContainsKey(currentTile)) InitiateTile(currentTile);
        List<TileCoords> surrounding = currentTile.GetSurrounding(radius);
        foreach(TileCoords tc in surrounding){
            InitiateTile(tc);
        }

        foreach (TileData tile in WorldTiles.Values)
        {
            TileCoords coords = tile.GetCoords();
            if(Math.Abs(coords.x - currentTile.x) > TileRadius || Math.Abs(coords.y - currentTile.y) > TileRadius){
                tile.DestroyGameObject();
            }else{
                if(!tile.HasGameObject()){
                    CreateTileGameObject(tile);
                }
            }
        }
    }

    private void UpdateGPSPath(){
        if(currentTile != null){
            if(pathPoints.Count == 0){
                pathPoints.Add(CreatePointAtCurrentPosition());
            }else{
                GPSPathPoint tempPathPoint = CreatePointAtCurrentPosition();
                if(pathPoints[pathPoints.Count - 1].DistanceToPoint(tempPathPoint) > 50f){
                    pathPoints.Add(tempPathPoint);
                    Debug.Log(pathPoints[pathPoints.Count - 1].DistanceToPoint(tempPathPoint));
                }
            }
        }
    }

    private GPSPathPoint CreatePointAtCurrentPosition(){
        GPSPathPoint pathPoint = new GPSPathPoint(GPSCoords);
        return pathPoint;
    }

    /// <summary>
    /// Reposition the tiles around the center tile
    /// </summary>
    private void UpdateTilePositions(){
        int deltaX = Math.Abs(centerTile.x - currentTile.x);
        int deltaY = Math.Abs(centerTile.y - currentTile.y);
        if(deltaX >= 1000 || deltaY >= 1000){
            centerTile = currentTile;
        }
        foreach(TileData data in WorldTiles.Values){
            data.PositionGameObject();            
        }
    }

    /// <summary>
    /// Initiates the world
    /// </summary>
    private void InitiateWorld(){
        pathPoints = new List<GPSPathPoint>();
        WorldTiles = new Dictionary<TileCoords, TileData>();
        TileCoords tileCoords = new TileCoords(GPSCoords);
        centerTile = tileCoords;
        currentTile = tileCoords;
        LoadWorldAroundCurrent(TileRadius);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoords
{
    internal int x{get; private set;}
    internal int y{get; private set;}

    private const int zoom = 18;

    public TileCoords(Vector2 GPSCoords){
        CalculateFromGPS(GPSCoords);
    }

    public TileCoords(int x, int y){
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Convert the GPS Coordinates to Open Maps tile coordinates
    /// </summary>
    /// <param name="GPSCoords"></param>
    private void CalculateFromGPS(Vector2 GPSCoords){
        x = (int)Mathf.Floor((GPSCoords.x + 180.0f) / 360.0f * (1 << zoom));
        y = (int)Mathf.Floor((1 - Mathf.Log(Mathf.Tan(Mathf.Deg2Rad * GPSCoords.y) + 1 / Mathf.Cos(Mathf.Deg2Rad * GPSCoords.y)) / Mathf.PI) / 2 * (1 << zoom));
    }

    /// <summary>
    /// Converts the internal x and y position to GPS coordinates
    /// </summary>
    /// <returns></returns>
    public Vector2 GetGPS(){
        Vector2 GPS = new Vector2();
        GPS.x = x / (float)(1 << zoom) * 360.0f - 180;
        float n = Mathf.PI - 2.0f * Mathf.PI * y / (float)(1 << zoom);
        GPS.y = 180.0f / Mathf.PI * Mathf.Atan(0.5f * (Mathf.Exp(n) - Mathf.Exp(-n)));
        return GPS;
    }

    /// <summary>
    /// Make a nice string from the x and y position of the tile
    /// </summary>
    /// <returns></returns>
    public override string ToString(){
        return $"{x}, {y}";
    }

    /// <summary>
    /// Get the URL to download the tile image from OSM
    /// </summary>
    /// <returns></returns>
    public string GetURL(){
        return "http://tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png";
    }

    /// <summary>
    /// Returns the path to the image in the save.
    /// </summary>
    /// <returns></returns>
    public string GetImagePath()
    {
        return Application.persistentDataPath + "/zones/" + x + "_" + y + ".png";
    }

    /// <summary>
    /// Custom Hash code for dictionary comparisons
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    /// <summary>
    /// Custom comparison for dictionary purposes
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if(obj.GetType() == this.GetType()){
            TileCoords tc = (TileCoords)obj;
            if(tc.x == this.x && tc.y == this.y){
                return true;
            } 
        }
        return false;
    }

    /// <summary>
    /// Get the surrounding tile coordinates
    /// </summary>
    /// <returns></returns>
    public List<TileCoords> GetSurrounding(int layers = 1){
        List<TileCoords> surrounding = new List<TileCoords>();

        for(int x = this.x - layers; x <= this.x + layers; x++){
            for(int y = this.y - layers; y <= this.y + layers; y++){
                if(x == this.x && y == this.y){
                    
                }else{
                    TileCoords temp = new TileCoords(x, y);
                    surrounding.Add(temp);
                }
            }
        }

        return surrounding;
    }
}

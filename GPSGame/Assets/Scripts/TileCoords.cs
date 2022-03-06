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

    /// <summary>
    /// Convert the GPS Coordinates to Open Maps tile coordinates
    /// </summary>
    /// <param name="GPSCoords"></param>
    private void CalculateFromGPS(Vector2 GPSCoords){
        x = (int)Mathf.Floor((GPSCoords.x + 180.0f) / 360.0f * (1 << zoom));
        y = (int)Mathf.Floor((1 - Mathf.Log(Mathf.Tan(Mathf.Deg2Rad * GPSCoords.y) + 1 / Mathf.Cos(Mathf.Deg2Rad * GPSCoords.y)) / Mathf.PI) / 2 * (1 << zoom));
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
}

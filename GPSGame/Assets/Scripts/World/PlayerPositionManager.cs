using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    private Vector2 previousCoords;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(previousCoords == new Vector2()){
            if(GPSManager.GPSCoords != new Vector2()){
                previousCoords = GPSManager.GPSCoords;
            }
        }else{
            if(previousCoords != GPSManager.GPSCoords){
                UpdatePosition();
            }
        }
    }

    private void UpdatePosition(){
        previousCoords = GPSManager.GPSCoords;
        float deltax = GPSManager.centerTile.GetGPS().x - GPSManager.GPSCoords.x;
        float deltay = GPSManager.centerTile.GetGPS().y - GPSManager.GPSCoords.y;
        float multiplierX = GPSManager.centerTile.GetGPS().x - new TileCoords(GPSManager.centerTile.x - 1, GPSManager.centerTile.y).GetGPS().x;
        float multiplierY = GPSManager.centerTile.GetGPS().y - new TileCoords(GPSManager.centerTile.x, GPSManager.centerTile.y - 1).GetGPS().y;
        transform.position = new Vector3(deltax / multiplierX, deltay / multiplierY, transform.position.z);
    }
}

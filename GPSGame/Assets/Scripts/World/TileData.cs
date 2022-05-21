using UnityEngine;

public class TileData
{
    private GameObject tileGameObject;
    private TileCoords tileCoords;
    public TileData(TileCoords tileCoords){
        this.tileCoords = tileCoords;
    }

    public void SetGameObject(GameObject temp){
        tileGameObject = temp;
    }

    public TileCoords GetCoords(){
        return tileCoords;
    }

    public void PositionGameObject(){
        float xoffset = GPSManager.centerTile.x - tileCoords.x;
        float yoffset = tileCoords.y - GPSManager.centerTile.y;
        tileGameObject.transform.position = new Vector3(xoffset, yoffset, 0);
    }
}
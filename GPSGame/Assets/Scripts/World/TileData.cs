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
        tileGameObject.transform.position = new Vector3(tileCoords.GetGamespaceCoords().x, tileCoords.GetGamespaceCoords().y, 0);
    }

    public bool IsCenterTile(){
        if(tileCoords.x == GPSManager.centerTile.x && tileCoords.y == GPSManager.centerTile.y) return true;
        return false;
    }
}
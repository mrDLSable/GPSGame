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
        if(tileGameObject == null) return;
        tileGameObject.transform.position = new Vector3(tileCoords.GetGamespaceCoords().x, tileCoords.GetGamespaceCoords().y, 0);
    }

    public bool isCurrentTile(){
        if(tileCoords.x == GPSManager.currentTile.x && tileCoords.y == GPSManager.currentTile.y) return true;
        return false;
    }

    public void DestroyGameObject(){
        if(tileGameObject != null){
            tileGameObject.GetComponent<TileManager>().DestroyGameObject();
            tileGameObject = null;
        }
    }

    public bool HasGameObject(){
        if(tileGameObject == null) return false;
        return true;
    }
}
using UnityEngine;

public class TileData
{
    private GameObject gameObject;
    private TileCoords coords;
    public TileData(TileCoords coords){
        this.coords = coords;
    }

    public void SetGameObject(GameObject temp){
        gameObject = temp;
    }

    public TileCoords GetCoords(){
        return coords;
    }
}
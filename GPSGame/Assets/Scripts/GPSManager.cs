using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSManager : MonoBehaviour
{
    private Vector2 GPSCoords;
    private Vector2 DebugCoords = new Vector2(-0.12754661804138964f, 51.50726853453877f); //London

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetGPSCoords();
        TileCoords temp = GetOpenMapsCoords(GPSCoords);
        Debug.Log(temp.GetURL());
    }

    /// <summary>
    /// Set the GPS coords
    /// </summary>
    private void SetGPSCoords(){
        switch(Application.platform){
            case RuntimePlatform.WindowsEditor:
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

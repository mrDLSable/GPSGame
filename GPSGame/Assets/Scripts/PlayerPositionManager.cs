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
        //throw new NotImplementedException();
    }
}
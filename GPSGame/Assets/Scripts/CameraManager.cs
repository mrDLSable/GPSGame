using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector2 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(previousPosition == new Vector2()){
            if(GPSManager.GPSCoords != new Vector2()){
                previousPosition = GPSManager.GPSCoords;
            }
        }else{
            if(previousPosition != GPSManager.GPSCoords){
                UpdatePosition();
            }
        }
    }

    private void UpdatePosition(){
        previousPosition = GPSManager.GPSCoords;
        transform.position = new Vector3(0, 0, transform.position.z);
    }
}

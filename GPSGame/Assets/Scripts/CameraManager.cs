using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPositionManager playerPositionManager = FindObjectOfType<PlayerPositionManager>();
        if(playerPositionManager != null){
            transform.position = new Vector3(playerPositionManager.transform.position.x, playerPositionManager.transform.position.y, transform.position.z);
        }
    }
}

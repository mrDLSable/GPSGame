using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class GPSManager_android{

    /// <summary>
    /// Start the GPS location service and ask for permission if it's not already given.
    /// </summary>
    /// <returns></returns>
    public static IEnumerator Start(){
        if(!Input.location.isEnabledByUser){
            Permission.RequestUserPermission(Permission.FineLocation);
            int permissionWaiter = 120;
            while (!Input.location.isEnabledByUser && permissionWaiter >0){
                yield return new WaitForSeconds(1);
                permissionWaiter--;
            }
        }else{
            if(Input.location.status == LocationServiceStatus.Stopped){
                Input.location.Start();
                int waitForStartup = 120;
                while(Input.location.status == LocationServiceStatus.Initializing && waitForStartup > 0){
                    yield return new WaitForSeconds(1);
                }
            }
        }
    }

    /// <summary>
    /// Update the GPS position from the input location service
    /// </summary>
    public static void Update(){
        if(Input.location.status == LocationServiceStatus.Running){
            GPSManager.GPSCoords = new Vector2(Input.location.lastData.longitude, Input.location.lastData.latitude);
        }
    }
}
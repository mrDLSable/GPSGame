using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSPathPoint
{
    public Vector2 gpsCoords;
    public DateTime dateTime;

    public GPSPathPoint(Vector2 gpsCoords, DateTime dateTime){
        this.gpsCoords = gpsCoords;
        this.dateTime = dateTime;
    }

    public GPSPathPoint(Vector2 gpsCoords){
        this.gpsCoords = gpsCoords;
        dateTime = DateTime.Now;
    }

    public float DistanceToPoint(GPSPathPoint point){
        float d1 = gpsCoords.y * (Mathf.PI / 180.0f);
        float num1 = gpsCoords.x * (Mathf.PI / 180.0f);
        float d2 = point.gpsCoords.y * (Mathf.PI / 180.0f);
        float num2 = point.gpsCoords.x * (Mathf.PI / 180.0f) - num1;
        float d3 = Mathf.Pow(Mathf.Sin((d2 - d1) / 2.0f), 2.0f) + Mathf.Cos(d1) * Mathf.Cos(d2) * Mathf.Pow(Mathf.Sin(num2 / 2.0f), 2.0f);

        return 6376500.0f * (2.0f * Mathf.Atan2(Mathf.Sqrt(d3), Mathf.Sqrt(1.0f - d3)));
    }
}

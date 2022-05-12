using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class TileManager : MonoBehaviour
{
    public TileData tileData;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadImage(tileData.GetCoords().GetURL(), tileData.GetCoords().GetImagePath()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// The actual texture downloader
    /// </summary>
    /// <param name="MediaUrl">The URL to download the image from</param>
    /// <param name="zoneID">The zone ID to apply the texture to</param>
    /// <returns></returns>
    IEnumerator DownloadImage(string MediaUrl, string path)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log(request.error);
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Color[] pix = texture.GetPixels(0, 0, 256, 256);
            System.Array.Reverse(pix, 0, pix.Length);
            texture.SetPixels(0, 0, 256, 256, pix);
            texture.Apply();
            SaveTexture(texture, path);
            this.gameObject.GetComponent<Renderer>().material.mainTexture = texture;
        }
    }

    /// <summary>
    /// Saves the texture in the zone data to be saved.
    /// </summary>
    /// <param name="texture">The texture</param>
    public void SaveTexture(Texture2D texture, string path)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/zones/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/zones/");
        }

        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, bytes);
    }
}

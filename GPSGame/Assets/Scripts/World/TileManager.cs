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
        string path = tileData.GetCoords().GetImagePath();
        if(File.Exists(path)){
            ImageFromFile(path);
        }else{
            StartCoroutine(ImageFromWeb(tileData.GetCoords().GetURL(), path));
        }
    }

    // Update is called once per frame
    void Update()
    {
        ColorTile();
    }

    private void ColorTile(){
        if(GetSkillTile().IsGatherable()){
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        }else{
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private SkillTile GetSkillTile(){
        return SaveManager._saveManager.GetSkillTile(tileData.GetCoords());
    }

    /// <summary>
    /// Download the image and save it to a file, then apply it to the zone
    /// </summary>
    /// <param name="MediaUrl">The URL to download the image from</param>
    /// <param name="zoneID">The zone ID to apply the texture to</param>
    /// <returns></returns>
    IEnumerator ImageFromWeb(string MediaUrl, string path)
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
            SaveImage(texture, path);
            this.gameObject.GetComponent<Renderer>().material.mainTexture = texture;
        }
    }

    /// <summary>
    /// Apply an image to the zone from a file
    /// </summary>
    /// <param name="path">The path to the image</param>
    public void ImageFromFile(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(256, 256);
        texture.LoadImage(fileData);
        gameObject.GetComponent<Renderer>().material.mainTexture = texture;
    }

    /// <summary>
    /// Saves the texture in the zone data to be saved.
    /// </summary>
    /// <param name="texture">The texture</param>
    public void SaveImage(Texture2D texture, string path)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/zones/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/zones/");
        }

        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, bytes);
    }

    public void DestroyGameObject(){
        Destroy(gameObject);
    }
}

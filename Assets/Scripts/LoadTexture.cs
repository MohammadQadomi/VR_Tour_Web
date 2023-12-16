using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Text;

public class LoadTexture : MonoBehaviour
{

    public Material skyboxMaterial;
    //public string addressWeb = "https://images.pexels.com/photos/17821306/pexels-photo-17821306/free-photo-of-landscape-of-hills-and-mountains.jpeg";
    public string addressWeb = "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";

    // Use this for initialization
    void Start()
    {
        StartCoroutine(IsDownloading(addressWeb));
    }

    [ContextMenu("Load Image")]
    public void LoadImage()
    {
        print("Loading image...");
        StartCoroutine(IsDownloading(addressWeb));
    }

    IEnumerator IsDownloading(string url)
    {
        yield return new WaitForSeconds(1); // wait for one sec, without it you will have a compiler error


        var www = new WWW(url); // start a download of the given URL
        //var unityWebRequest = UnityWebRequestTexture.GetTexture(url);
        yield return www;       // wait until the download is done

        print("Image loaded");
        //Texture2D texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.ASTC_10x10, false);// create a texture in DXT1/ASTC_10x10 format

        byte[] decodedBytes = Convert.FromBase64String("/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAgGBgcGBQgHBwcJCQgKDBQNDAsLDBkSEw8UHRofHh0aHBwgJC4nICIsIxwcKDcpLDAxNDQ0Hyc5PTgyPC4zNDL/2wBDAQkJCQwLDBgNDRgyIRwhMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjL/wAARCABAAEADASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwDpUbjov/fIqZXPoP8AvkVAg4qYCvprHxxMH+n/AHyKydZ8XaVoJ2Xc26bGRDEgZj/hWhJIIoZJCcBELH8BXA+B9PtNUjOo6hGJ7i4diWkGcDPQVw4/FfVoJpXbPRy3ArFzabska8XxO0xnAewvETu5RTj8K6zSdZstatPtNhOkqZwwAAKn0I7VFL4V0K6iCTWce0egxXMW2l2/hbx7ZR2Eriz1JHjeMkkBgCR/KuHDZi6k1Ca3O/G5PClSdSm9juwx9B/3yKCx9B/3yKSkr17HzxjIal3cVWVqfu4rdIZBqZeSwuIkIDSRsgJPTIxXF2h1XTLC1j01QrIux22BsEfXgV2N4f3ZrDs777HHdjZv2scgDJGe+K8TOacrRn02PochnD3qb0e9/wCv61Na417Wrfw/Z3cUMUk8jMr5TI49sjr9acon1W60e+ubbyZoJjIwUdtpAx7Ekfkaq6XrrLbxW7lNqkspWNiXz65GAK6K3uPtEjvgAZxj0ry8HSlOvFR9T2MxqQpUJOfaxezS5pgNLmvqrHwhz4cU7zBjrXO634gt9Ds1nmR5GdtsaJ1Y4z17CuGv/H+sXIK2witEPdBub8z/AIUquJp0naW510cHVrK8Voenaje21lavPdzJDEo5Zj/L1rIttNvNU0aHxHpsRkgkLCSMckBWIBI+grySe5uLyXzbmeSZz/FIxY/rXuHwovrlfhze/Y2Vbq1mm2tINygFQw479a8rGYj6xHkSsj2sDhFhpc17sg06C6uSsFrY7JpuAQpya6GMQ6bqc2iPIou7ZUZkLDLhlB3D2zkfhTvB+qateams8CWm3YHuf3IGQegBHIJP4cdK87+Lk7j4k3UsZeNlggKEHDD5P51zYGToz5+515nRdePs2/M9PD08NXimneO9dsiA1yLlP7lwu79Rz+td5oPjzTtVZLe6/wBDu2OFVjlHPoG9fY17UMRTnpsfNVcDWpK9rryPOPHV95t/a2oP+qQu31bp+grlVOc1Z1m6+161dzA5UyFV+g4/pVJG+Zq8qvPnquR9Bh6fs6UYkyDr9a9f+FV68HgbxMiY3JIpQ+hZQP6V4+jfNj2r0r4aTH/hHvFNuM8xQSf+PkVi9johrNHoPgK9+x6g9kB+7n2AH0Zen6Zrz/4yHHxLvP8Ar3h/9BrodGna31a1lAIxMmfxOK534zgj4k3WVxm2hP14NTSZvjI2nfucAWw4+lPWYpIjjqh3D8KhJ+YetMQknntxW1zksf/Z");
        string decodedText = Encoding.UTF8.GetString(decodedBytes);
        print($"texture:{decodedText}");

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(decodedBytes);
        //var texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.ASTC_6x6, false);// create a texture in DXT1/ASTC_10x10 format

        www.LoadImageIntoTexture(texture); // load data into a texture
        print($"Texture: {texture}");
        skyboxMaterial.SetTexture("_MainTex", texture);

        www.Dispose();

        www = null;

    }
}
using SimpleJSON;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GistHandler : MonoBehaviour
{
    [SerializeField] TextAsset dataFile;
    [SerializeField] private string apiUrl = "https://api.github.com/gists";
    [SerializeField] private string accessToken = "YOUR_ACCESS_TOKEN"; // Replace with your GitHub personal access token
    public APITest aPITest;
    public LoadLocationsUI loadLocationsUI;

    string _content;

    public void CreateGist(string filename, string content)
    {
        StartCoroutine(PostGist(filename, content));
    }

    public void GetGist(TMPro.TMP_InputField id)
    {
        StartCoroutine(GetGistCoroutine("ea15ee944c3f78e91a5bfd7a1d12f169", int.Parse(id.text)));
    }
    public void GetGist(int id)
    {
        StartCoroutine(GetGistCoroutine("ea15ee944c3f78e91a5bfd7a1d12f169", id));
    }

    [ContextMenu("Get Gist Content")]
    public void GetGistContent(int id)
    {
        //GetGist(id);
    }

    public void EditGist(string newContent)
    {
        StartCoroutine(EditGistCoroutine("ea15ee944c3f78e91a5bfd7a1d12f169", "test.json", newContent));
    }

    [ContextMenu("Edit Gist Content")]
    public void EditGistContent()
    {
        EditGist(dataFile.text);
    }

    IEnumerator PostGist(string filename, string content)
    {
        string json = "{\"files\":{\"" + filename + "\":{\"content\":\"" + content + "\"}}}";

        UnityWebRequest www = UnityWebRequest.Post(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Authorization", "token " + accessToken);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            Debug.Log("Gist created successfully. Gist ID: " + www.downloadHandler.text);
        }
    }

    IEnumerator GetGistCoroutine(string gistId, int id =0)
    {
        UnityWebRequest www = UnityWebRequest.Get(apiUrl + "/" + gistId);
        www.SetRequestHeader("Authorization", "token " + accessToken);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            var temp = www.downloadHandler.text;
            temp = temp.Replace(@"^^", @"""");
            var body = JSON.Parse(temp)["files"]["test.json"]["content"];
            Debug.Log("Gist content: " + body);
            _content = body;
            aPITest.GetData(id, body);
        }
    }

    public IEnumerator SendAllGistLocationsToLoadLocationsUICoroutine(string gistId = "ea15ee944c3f78e91a5bfd7a1d12f169")
    {
        UnityWebRequest www = UnityWebRequest.Get(apiUrl + "/" + gistId);
        www.SetRequestHeader("Authorization", "token " + accessToken);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            var temp = www.downloadHandler.text;
            //temp = temp.Replace(@"^^", @"""");
            var body = JSON.Parse(temp)["files"]["test.json"]["content"];
            Debug.Log("Gist content: " + body);
            _content = body;

            var locations = aPITest.GetAllData(body);
            foreach (var location in locations.locations)
            {
                StartCoroutine(loadLocationsUI.ApplyWebImage(location.imagePath, location.id));
            }
        }
    }

    IEnumerator EditGistCoroutine(string gistId, string filename, string newContent)
    {
        newContent = newContent.Replace(@"""", @"^^");
        print($"New Content: {newContent}");
        string json = "{\"files\":{\"" + filename + "\":{\"content\":\"" + newContent + "\"}}}";
        //string json = "{description:asd,files:{test.json:{content:Test Content}}}";

        UnityWebRequest www = UnityWebRequest.Post(apiUrl + "/" + gistId, "PATCH");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Authorization", "token " + accessToken);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            Debug.Log("Gist edited successfully. Gist ID: " + gistId);
        }
    }
}

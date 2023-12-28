using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLocationsUI : MonoBehaviour
{
    [SerializeField] GameObject contentPrefab;
    [SerializeField] GameObject contentParent;
    [SerializeField] ToggleGroup toggleGroup;
    [SerializeField] GistHandler gistHandler;

    [SerializeField] List<GameObject> createdContents;

    private void OnEnable()
    {
        ResetLoadedLocations();
        StartCoroutine(gistHandler.SendAllGistLocationsToLoadLocationsUICoroutine());
    }

    void CreateContent(Texture2D imagePath, int id)
    {
        var content = Instantiate(contentPrefab, contentParent.transform);
        createdContents.Add(content);
        if(content.TryGetComponent<Toggle>(out Toggle toggle))
        {
            toggle.group = toggleGroup;
            print("Changing toggle group!");
        }

        if(content.TryGetComponent<ToggleContent>(out ToggleContent toggleContent))
        {
            toggleContent.SetImage(imagePath, id);
        }
    }

    // Testing
    [ContextMenu("Test Creation")]
    void TestCreation()
    {
        for (int i = 0; i < 10; i++)
        {
            var content = Instantiate(contentPrefab, contentParent.transform);
            createdContents.Add(content);
            if (content.TryGetComponent<Toggle>(out Toggle toggle))
            {
                toggle.group = toggleGroup;
                print("Changing toggle group!");
            }
        }
    }

    public IEnumerator ApplyWebImage(string url, int id)
    {
        var loader = new WWW(url);
        yield return loader;
        Texture2D texture = loader.texture;
        CreateContent(texture, id);
    }

    public void ResetLoadedLocations()
    {
        foreach (var item in createdContents)
        {
            Destroy(item.gameObject);
        }
        createdContents.Clear();
    }

    public void LoadLocationByToggles()
    {
        print("Start Load Location By Toggle");
        List<Toggle> toggles = new List<Toggle>();
        foreach (var item in createdContents)
        {
            if(item.TryGetComponent<Toggle>(out Toggle toggle))
            {
                toggles.Add(toggle);
            }
        }

        foreach (var toggle in toggles)
        {
            if (toggle.isOn)
            {
                if(toggle.TryGetComponent<ToggleContent>(out ToggleContent toggleContent))
                {
                    print("Load location by toggle");
                    gistHandler.GetGist(toggleContent.GetID());
                }
            }
        }
        print("End Load Location By Toggle");
    }
}

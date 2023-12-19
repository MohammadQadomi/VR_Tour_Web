using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

public class APITest : MonoBehaviour
{
    public CreateLocationTest createLocationTest;
    public TextAsset textFile;
    string path = "Assets/Data/test.json";
    //string path = "file:///C:/Unity%20Projects/Temp%20Project/Assets/Data/test.json";
    string textFileURL = "https://gist.githubusercontent.com/MohammadQadomi/ea15ee944c3f78e91a5bfd7a1d12f169/raw/427ed025354c104f5f97f594b80b7fa1b529cc11/test.json";

    async void Start()
    {
        string temp;
        temp = await GetContents(textFileURL);
        Debug.Log(temp);
    }

    [ContextMenu("Send Data")]
    public void SendData()
    {
        //WriteFile(JsonConvert.SerializeObject(createLocationTest.GetCurrentLocationData()));

        Locations locations = new Locations();
        locations = GetAllData();
        locations.Add(createLocationTest.GetCurrentLocationData());
        WriteFile(JsonConvert.SerializeObject(locations));
    }

    public void WriteFile(string data)
    {
        //File.WriteAllText(path, data);

        // Make the file readable
        var body = JsonConvert.DeserializeObject(data);
        File.WriteAllText(path, body.ToString());
    }

    [ContextMenu("Read file")]
    public void ReadFile()
    {
        var temp = File.ReadAllText(path);
        var body = JsonConvert.DeserializeObject(temp);
        WriteFile(body.ToString());
    }

    [ContextMenu("Get Data")]
    public void GetData(int id)
    {
        Locations locations = GetAllData();
        createLocationTest.NewLocation(locations.Find(id)); 
    }

    [ContextMenu("Get all data")]
    public Locations GetAllData()
    {
        // Read the file
        StreamReader reader = new StreamReader(path);
        var temp = reader.ReadToEnd();

        if (temp.Length <= 0) return null;

        var body = JSON.Parse(temp)["locations"];

        Debug.Log(body);

        reader.Close();

        Locations locations = new Locations();

        for (int i = 0; i<body.Count; i++)
        {
            List<Arrow> arrows = new List<Arrow>();
            for (int j = 0; j < body[i]["arrows"].Count; j++)
            {
                arrows.Add(new Arrow(body[i]["arrows"][j]["text"], new Vector3(body[i]["arrows"][j]["positionX"], body[i]["arrows"][j]["positionY"], body[i]["arrows"][j]["positionZ"]), new Quaternion(body[i]["arrows"][j]["rotationX"], body[i]["arrows"][j]["rotationY"], body[i]["arrows"][j]["rotationZ"], body[i]["arrows"][j]["rotationW"]), new Vector3(body[i]["arrows"][j]["scaleX"], body[i]["arrows"][j]["scaleY"], body[i]["arrows"][j]["scaleZ"]), body[i]["arrows"][j]["destinationId"], body[i]["arrows"][j]["isHidden"], body[i]["arrows"][j]["color"]));
            }

            List<Hotspot> hotspots = new List<Hotspot>();
            for (int j = 0; j < body[i]["hotspots"].Count; j++)
            {
                hotspots.Add(new Hotspot(body[i]["hotspots"][j]["text"], new Vector3(body[i]["hotspots"][j]["positionX"], body[i]["hotspots"][j]["positionY"], body[i]["hotspots"][j]["positionZ"]), new Quaternion(body[i]["hotspots"][j]["rotationX"], body[i]["hotspots"][j]["rotationY"], body[i]["hotspots"][j]["rotationZ"], body[i]["hotspots"][j]["rotationW"]), new Vector3(body[i]["hotspots"][j]["scaleX"], body[i]["hotspots"][j]["scaleY"], body[i]["hotspots"][j]["scaleZ"]), body[i]["hotspots"][j]["isHidden"]));
            }

            List<Desk> desks = new List<Desk>();
            for (int j = 0; j < body[i]["hotspots"].Count; j++)
            {
                desks.Add(new Desk(body[i]["desks"][j]["employeeName"], new Vector3(body[i]["desks"][j]["positionX"], body[i]["desks"][j]["positionY"], body[i]["desks"][j]["positionZ"]), new Quaternion(body[i]["desks"][j]["rotationX"], body[i]["desks"][j]["rotationY"], body[i]["desks"][j]["rotationZ"], body[i]["desks"][j]["rotationW"]), new Vector3(body[i]["desks"][j]["scaleX"], body[i]["desks"][j]["scaleY"], body[i]["desks"][j]["scaleZ"]), body[i]["desks"][j]["isHidden"], body[i]["desks"][j]["employeeDepartment"], body[i]["desks"][j]["employeeDescription"]));
            }

            Location location = new Location(body[i]["id"], body[i]["imagePath"], body[i]["voiceOverPath"], arrows, hotspots, desks);

            locations.Add(location);
        }

        return locations;
    }

    static async Task<string> GetContents(string url)
    {
        using(var client = new HttpClient())
        {
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }

}

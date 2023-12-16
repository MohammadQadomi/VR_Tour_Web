using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;

public class APIController : MonoBehaviour
{

    [SerializeField] List<TMP_Text> orientationText;
    [SerializeField] List<TMP_Text> descriptionText;
    [SerializeField] List<EmployeeInfo> employeeInfo;
    [SerializeField] List<TMP_Text> policyText;
    [SerializeField] List<AudioClip> voiceOver;

    private readonly string baseURL = "http://10.170.8.132:3002/api/getImageInfo";
    //private readonly string deskURL = "http://10.170.8.132:3002/api/getDeskById";
    private readonly string deskURL = "http://10.170.8.132:3002/api/getALLDeskInfoById";

    void Start()
    {
        
    }

    public void NewLocation(int id, List<TMP_Text> orientationText, List<TMP_Text> descriptionText, List<EmployeeInfo> employeeInfo, List<TMP_Text> policyText, List<AudioClip> voiceOver)
    {
        this.orientationText = orientationText;
        this.descriptionText = descriptionText;
        this.employeeInfo = employeeInfo;
        this.policyText = policyText;
        this.voiceOver = voiceOver;

        ResetValues();// Reset all values

        GetDataAtIndex(id);
    }

    void ResetValues()
    {
        foreach (var text in orientationText)
        {
            text.text = "";
        }
        // Reset description text values
        foreach (var text in descriptionText)
        {
            text.text = "";
        }
        // Reset employeeInfo text values
        foreach (var text in employeeInfo)
        {
            text.SetEmployeeData();
        }
        // Reset policy text values
        foreach (var text in policyText)
        {
            text.text = "";
        }
    }

    [ContextMenu("Test Get Data")]
    public void TestGetData()
    {
        GetDataAtIndex(3);
    }

    public void GetDataAtIndex(int index)
    {
        StartCoroutine(GetData(index));
    }

    IEnumerator GetData(int index)
    {
        UnityWebRequest VRTourRequest = UnityWebRequest.Get(baseURL);
        VRTourRequest.SetRequestHeader("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6Im1vaGFtbWFkLnFhZG9taSIsImlhdCI6MTY5NjUwODY1MH0.aSmy5o1kGKbzVfUuCpVATYVBxhEgdETxYk-6I2XJjrM");
        VRTourRequest.SetRequestHeader("image_id", $"{index}");

        yield return VRTourRequest.SendWebRequest();

        if (VRTourRequest.result == UnityWebRequest.Result.ConnectionError || VRTourRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(VRTourRequest.error);
            yield break;
        }

        JSONNode locationInfo = JSON.Parse(VRTourRequest.downloadHandler.text);
        //print($"Data: {locationInfo["orientation_array"][0].GetType()}");

        // Fill the orientation texts
        for (int i = 0; i < orientationText.Count; i++)
        {
            for (int j = 0; j < locationInfo["orientation_array"].Count; j++)
            {
                var temp = JSON.Parse(locationInfo["orientation_array"][j].ToString());
                if (temp["key"] == $"OR{i+1}")
                {
                    orientationText[i].text = temp["value"];
                    break;
                }
            }
        }

        // Fill employees info texts
        for (int j = 0; j < locationInfo["desk_array"].Count; j++)
        {
            var temp = JSON.Parse(locationInfo["desk_array"][j].ToString());
            print((temp["input1"]));

            UnityWebRequest EmployeeRequest = UnityWebRequest.Get(deskURL);
            EmployeeRequest.SetRequestHeader("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6Im1vaGFtbWFkLnFhZG9taSIsImlhdCI6MTY5NjUwODY1MH0.aSmy5o1kGKbzVfUuCpVATYVBxhEgdETxYk-6I2XJjrM");
            EmployeeRequest.SetRequestHeader("desk_id", $"{temp["input1"]}");

            yield return EmployeeRequest.SendWebRequest();

            if (EmployeeRequest.result == UnityWebRequest.Result.ConnectionError || EmployeeRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(EmployeeRequest.error);
                yield break;
            }

            var _employeeInfo = JSON.Parse(EmployeeRequest.downloadHandler.text);
            print($"employee data: {_employeeInfo["employeeInfo"]["name"]}");

            if(j < employeeInfo.Count) employeeInfo[j].SetEmployeeData(_employeeInfo["employeeInfo"]["name"], _employeeInfo["employeeInfo"]["departement"], _employeeInfo["message"]["employee_main_responsibility"], _employeeInfo["employeeInfo"]["userPicture"]);
        }
    }
}

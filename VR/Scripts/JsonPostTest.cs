using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class JsonPostTest : MonoBehaviour
{
    // Start is called before the first frame update
    string output;
    async void Start()
    {
        PostData p = new PostData(
            privateKey: "fb57cdb52c16a26a9f54d37ce8f106bc4a334772d5c376c08f009e042cb0a7fe",
            bidAmount: 0.15
        );

        output = JsonConvert.SerializeObject(p);

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://coinexgift.onrender.com/api/BidNFT/0"),
            Content = new StringContent("{\t\n\t\"privatekey\":\"fb57cdb52c16a26a9f54d37ce8f106bc4a334772d5c376c08f009e042cb0a7fe\",\n  \"BidPrice\":0.30\n}")
            {
                Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
            }
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Debug.Log(body);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator PodaDei()
    {
        string testurl = "https://coinexgift.onrender.com/api/BidNFT/3";

        using UnityWebRequest webrq = new UnityWebRequest(testurl, "POST");


        byte[] rawdata = Encoding.UTF8.GetBytes(output);

        Debug.Log(rawdata.Length);

        webrq.uploadHandler = new UploadHandlerRaw(rawdata);

        webrq.downloadHandler = new DownloadHandlerBuffer();

        webrq.SetRequestHeader("Content-Type", "application/json");

        yield return webrq.SendWebRequest();

        switch (webrq.result)
        {
            case UnityWebRequest.Result.Success:
                string response = webrq.downloadHandler.text;
                Debug.Log(response);
                break;
        }

        yield break;
    }
}
public class PostData
{
    public string privateKey;

    public double bidAmount;

    public PostData(string privateKey, double bidAmount)
    {
        this.privateKey = privateKey;
        this.bidAmount = bidAmount;
    }
}
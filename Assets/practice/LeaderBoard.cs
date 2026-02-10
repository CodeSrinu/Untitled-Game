using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public Transform entriesContainer;
    public Transform entryTemplate;
    private List<LeaderboardEntryData> leaderboardEntryDataList = new List<LeaderboardEntryData>();
    private List<Transform> leaderboardEntryTransformList = new List<Transform>();


    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        AddNewEntry("Bobby", 20, 3, 2300, 4500);

        string jsonString = PlayerPrefs.GetString("leaderboardEntries");
        LeaderboardEntries leaderboard = JsonUtility.FromJson<LeaderboardEntries>(jsonString);

        leaderboardEntryDataList = leaderboard.leaderboardEntryDataList;

        //sorting based on score
        leaderboardEntryDataList.Sort((a,b) => b.score.CompareTo(a.score));

        foreach(LeaderboardEntryData entry in leaderboardEntryDataList)
        {
            CreateLeaderboardEntryTransform(entry, entriesContainer, leaderboardEntryTransformList);
        }

    }

    private void AddNewEntry(string name,int cargoDelivered, int collisions, int netEarnings, int score)
    {
        //create entry
        LeaderboardEntryData entry = new LeaderboardEntryData { playerName = name, cargoDelivered = cargoDelivered, collisions = collisions, netEarnings = netEarnings, score = score };

        //load current entries data
        string jsonString = PlayerPrefs.GetString("leaderboardEntries");
        LeaderboardEntries entries = JsonUtility.FromJson<LeaderboardEntries>(jsonString);

        //update and save
        entries.leaderboardEntryDataList.Add(entry);
        string json = JsonUtility.ToJson(entries);
        PlayerPrefs.SetString("leaderboardEntries", json);
        PlayerPrefs.Save();

    }


    private void CreateLeaderboardEntryTransform(LeaderboardEntryData leaderboardEntryData, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;

        Transform entryTransform = Instantiate(entryTemplate, container);

        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -transformList.Count * templateHeight);

        entryRectTransform.gameObject.SetActive(true);


        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            case 1: rankString = "1st"; break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd"; break;
            default: rankString = rank + "th"; break;
        }
        entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rankString;

        string name = leaderboardEntryData.playerName;
        entryTransform.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = name;

        int cargoDelivered = leaderboardEntryData.cargoDelivered;
        entryTransform.Find("CargoDelivered").GetComponent<TextMeshProUGUI>().text = cargoDelivered.ToString();

        int collisions = leaderboardEntryData.collisions;
        entryTransform.Find("Collisions").GetComponent<TextMeshProUGUI>().text = collisions.ToString();

        int netEarnings = cargoDelivered * 100;
        entryTransform.Find("NetEarnings").GetComponent<TextMeshProUGUI>().text = netEarnings.ToString();

        int score = (cargoDelivered * 100) - (collisions * 20);
        entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();





        transformList.Add(entryTransform);
    }


    //created this separate class to make our list of entries to an obj, so we convert them into string using jsonUtility and save in playerPrefs
    private class LeaderboardEntries
    {
        public List<LeaderboardEntryData> leaderboardEntryDataList;
    }
    

    [System.Serializable] //added this to make it able to pass in functions, in our case setString
    public class LeaderboardEntryData
    {
        public string playerName;
        public int cargoDelivered;
        public int collisions;
        public int netEarnings;
        public int score;
    }
}

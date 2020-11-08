using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankingTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> playerMetadataTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("rankingEntryContainer");
        entryTemplate = entryContainer.Find("rankingEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("ranking");
        List<PlayerMetadata> rankings = JsonConvert.DeserializeObject<List<PlayerMetadata>>(jsonString) as List<PlayerMetadata>;


        // Sort entry list by Score
        for (int i = 0; i < rankings.Count; i++)
        {
            for (int j = i + 1; j < rankings.Count; j++)
            {
                if (rankings[j].score > rankings[i].score)
                {
                    // Swap
                    PlayerMetadata tmp = rankings[i];
                    rankings[i] = rankings[j];
                    rankings[j] = tmp;
                }
            }
        }

        // only 10 best scores
        rankings = rankings.Take(8).ToList();

        playerMetadataTransformList = new List<Transform>();

        foreach (PlayerMetadata playerMetadata in rankings)
        {
            CreateHighscoreEntryTransform(playerMetadata, entryContainer, playerMetadataTransformList);
        }

    }

    private void CreateHighscoreEntryTransform(PlayerMetadata playerMetadata, Transform container, List<Transform> transformList)
    {
        float templateHeight = 31f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        float score = playerMetadata.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = playerMetadata.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        // Set background visible odds and evens, easier to read
        // entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        // Highlight First
        if (rank == 1)
        {
            entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
        }

        // Set tropy
        /*
        switch (rank)
        {
            default:
                entryTransform.Find("trophy").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("FFD200");
                break;
            case 2:
                entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("C6C6C6");
                break;
            case 3:
                entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("B76F56");
                break;

        }
        */

        transformList.Add(entryTransform);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovieListProgram : MonoBehaviour
{
    [SerializeField] GameObject serieButtonTemplate;
    [SerializeField] SeriesList seriesList;
    [SerializeField] TMP_InputField serieIF;
    [SerializeField] RectTransform listRT;
    [SerializeField] Button rollButton;
    [SerializeField] TextMeshProUGUI resultText;

    [SerializeField] List<Serie> series = new();
    List<Serie> lessChancesSeires = new();


    void Start()
    {
        rollButton.onClick.AddListener(OnRollButtonClick);

        foreach(string s in seriesList.SeriesNames){
            AddSerie(s, true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (serieIF.text != "")
            {
                AddSerie(serieIF.text);
            }
        }
    }

    void AddSerie(string serieName, bool init = false)
    {        
        foreach (Serie se in series)
        {
            if(se.name == serieName) return;
        }

        GameObject newSerie = Instantiate(serieButtonTemplate, listRT);
        Button remove = null;
        foreach (Transform child in newSerie.transform) if (child.GetComponent<Button>()) remove = child.GetComponent<Button>();
        Serie s = newSerie.AddComponent<Serie>();
        TextMeshProUGUI name = newSerie.GetComponentInChildren<TextMeshProUGUI>();
        s.name = serieName;
        name.text = s.name;

        newSerie.GetComponent<Button>().onClick.AddListener(() => OnSerieButtonClick(s));
        remove.onClick.AddListener(() => OnRemoveSerieButtonClick(s));
        newSerie.transform.SetParent(listRT);
        series.Add(s);
        if(!init) seriesList.Add(serieName);

        serieIF.text = "";
    }

    void OnSerieButtonClick(Serie s)
    {
        s.InList();
        s.GetComponent<Image>().color = s.inList ? Color.white : Color.red;
    }
    void OnRemoveSerieButtonClick(Serie s)
    {
        series.Remove(s);
        if (lessChancesSeires.Contains(s)) lessChancesSeires.Remove(s);
        Destroy(s.gameObject);
        seriesList.Remove(s.name);
    }

    void OnRollButtonClick()
    {

        foreach (Serie s in series.ToArray())
        {
            if (s.inList) continue;

            series.Remove(s);
            lessChancesSeires.Add(s);
        }

        float rand = Random.Range(0f, 1f);
        int randId = 0;
        if (lessChancesSeires.Count > 0)
        {
            if (rand > 0.2f)
            {
                randId = Random.Range(0, series.Count);
                resultText.text = series[randId].name;
            }
            else
            {
                randId = Random.Range(0, lessChancesSeires.Count);
                resultText.text = lessChancesSeires[randId].name;
            }
        }
        else
        {
            randId = Random.Range(0, series.Count);
            resultText.text = series[randId].name;

        }
    }
}
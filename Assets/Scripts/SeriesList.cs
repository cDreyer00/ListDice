using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "SeriesList", fileName = "New SeriesList")]
public class SeriesList : ScriptableObject
{
    [SerializeField] List<string> seriesNames;
    public List<string> SeriesNames  => seriesNames;
    public void Add(string name) => SeriesNames.Add(name);

    public void Remove(string name)
    {
        if (SeriesNames.Contains(name)) SeriesNames.Remove(name);
    }
}

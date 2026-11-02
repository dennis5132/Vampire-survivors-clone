using System.IO;
using System.Text.Json;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private string saveData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        saveData = Application.persistentDataPath+"/data.txt";
        if (!File.Exists(saveData))
        {
            File.Create(saveData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Data GetData()
    {
        string json = File.ReadAllText(saveData);
        if (string.IsNullOrEmpty(json))
        {
            return new Data();
        }
        Data d = JsonSerializer.Deserialize<Data>(json);
        if (d == null)
        {
            d = new Data();
            SetData(d);
        }
        return d;
    }
    private void SetData(Data d)
    {
        string json = JsonSerializer.Serialize(d);
        using (StreamWriter writer = new StreamWriter(saveData))
        {
            writer.WriteLine(json);
        }
        Debug.Log(json);
    }
    public void IncreaseCurrency()
    {
        Data d = GetData();
        d.upgradeCurrency++;
        SetData(d);
    }
    public Data SendData()
    {
        return GetData();
    }
    public void Receive(Data d)
    {
        SetData(d);
    }
}
public class Data
{
    public int upgradeCurrency {  get; set; }
    public int XPLevel { get; set; }
    public int HealthLevel { get; set; }
}

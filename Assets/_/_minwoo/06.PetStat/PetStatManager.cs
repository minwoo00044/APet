using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PetStatManager : MonoBehaviour
{
    public static PetStatManager Instance { get; private set; }

    [SerializeField] OnePanelData petData;
    private Dictionary<string, PetStat> _nameStatPair = new Dictionary<string, PetStat>();

    public Dictionary<string, PetStat> NameStatPair { get => _nameStatPair; set => _nameStatPair = value; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        LoadStatData();
    }

    private void OnApplicationQuit()
    {
        SaveStatData();
    }

    private void LoadStatData()
    {
        TextAsset data = Resources.Load("StatData") as TextAsset;
        string[] lines = data.text.Split('\n');
        NameStatPair.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');

            if (parts.Length < 5) continue; // 라인의 부분이 충분하지 않으면 건너뜀

            var nameKeyPart = parts[0];
            var ageKeyPart = parts[1];
            var hungerKeyPart = parts[2];
            var loveKeyPart = parts[3];
            var cleanKeyPart = parts[4];

            var tempPetStat = new PetStat(int.Parse(ageKeyPart), int.Parse(hungerKeyPart), int.Parse(loveKeyPart), int.Parse(cleanKeyPart));

            NameStatPair.Add(nameKeyPart, tempPetStat);
            print("!");
        }
    }
    public void SaveStatData()
    {
        using (StreamWriter sw = new StreamWriter(Application.dataPath + "/Resources/StatData.csv"))
        {
            // 헤더를 작성합니다.
            sw.WriteLine("Name,Age,Hunger,Love,Clean");

            // 각각의 PetStat을 CSV 형식으로 작성합니다.
            foreach (KeyValuePair<string, PetStat> entry in _nameStatPair)
            {
                sw.WriteLine($"{entry.Key},{entry.Value.Age},{entry.Value.Hunger},{entry.Value.Love},{entry.Value.Clean}");
            }
        }
    }

}

public class PetStat
{
    private int _age;
    private int _hunger;
    private int _love;
    private int _clean;

    public PetStat(int age, int hunger, int love, int clean)
    {
        _age = age;
        _hunger = hunger;
        _love = love;
        _clean = clean;
    }

    public int Hunger { get => _hunger; set => _hunger = value; }
    public int Love { get => _love; set => _love = value; }
    public int Clean { get => _clean; set => _clean = value; }
    public int Age { get => _age; set => _age = value; }


}
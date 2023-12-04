using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PetStatManager : MonoBehaviour
{
    public static PetStatManager Instance { get; private set; }

    [SerializeField] OnePanelData petData;
    private Dictionary<string, PetStat> _nameStatPair = new Dictionary<string, PetStat>();

    public Dictionary<string, PetStat> NameStatPair { get => _nameStatPair; set => _nameStatPair = value; }

    private Tuple<string, GameObject> _petWithTag = new Tuple<string, GameObject>("", null);

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
            var healthKeyPart = parts[5];

            var tempPetStat = new PetStat(int.Parse(ageKeyPart), int.Parse(hungerKeyPart), int.Parse(loveKeyPart), int.Parse(cleanKeyPart), int.Parse(healthKeyPart));

            NameStatPair.Add(nameKeyPart, tempPetStat);
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
                sw.WriteLine($"{entry.Key},{entry.Value.Age},{entry.Value.Hunger},{entry.Value.Love},{entry.Value.Clean},{entry.Value.Health}");
            }
        }
    }

    public void ChangePet(string name, GameObject prefab)

    {
        GameObject instance;
        if (_petWithTag.Item1 != string.Empty)
        {
            if (name == _petWithTag.Item1)
                return;
            instance = Instantiate(prefab, _petWithTag.Item2.transform.position, Quaternion.identity);
        }
        else
        {
            instance = Instantiate(prefab);
        }
        Destroy(_petWithTag.Item2);
        _petWithTag = new Tuple<string, GameObject>(name, instance);
        LogController.onPetChange(name);
    }
    public GameObject GetCurrentPet()
    {
        if(_petWithTag.Item1 != string.Empty && _petWithTag.Item2 is not null)
            return _petWithTag.Item2;
        else
        {
            LogController.onError("You are not place your pet on Game");
            return null;
        }
    }
}

public class PetStat
{
    private int _age;
    private int _hunger;
    private int _love;
    private int _clean;
    private int _health;

    public PetStat(int age, int hunger, int love, int clean, int health)
    {
        _age = age;
        _hunger = hunger;
        _love = love;
        _clean = clean;
        _health = health;
    }

    public int Hunger { get => _hunger; set => _hunger = value; }
    public int Love { get => _love; set => _love = value; }
    public int Clean { get => _clean; set => _clean = value; }
    public int Age { get => _age; set => _age = value; }
    public int Health { get => _health; set => _health = value; }
}
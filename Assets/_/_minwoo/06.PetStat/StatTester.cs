using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTester : MonoBehaviour
{
    public string testName;
    public AudioClip test;
    PetStat currentStat;
    [ContextMenu("Debug")]
    public void DebugStat()
    {
        currentStat = PetStatManager.Instance.NameStatPair[testName];

        Debug.Log(currentStat.Age);
        Debug.Log(currentStat.Hunger);
        Debug.Log(currentStat.Love);
        Debug.Log(currentStat.Clean);
    }
    [ContextMenu("TestFeed")]
    public void Test1()
    {
        currentStat.Hunger++;
    }
    [ContextMenu("TestSound")]
    public void test2()
    {
        SoundManager.PlayOneShot(test);
    }
}

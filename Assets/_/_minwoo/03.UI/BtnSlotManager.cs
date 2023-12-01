using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnSlotManager : MonoBehaviour
{
    [SerializeField] List<OnePanelData> panelDatas = new List<OnePanelData>();
    [SerializeField] List<Button> panelChangeBtns = new List<Button>();
    [SerializeField] GameObject btnMom;
    Dictionary<Button, OnePanelData> btnDataPair = new Dictionary<Button, OnePanelData>();
    List<Button> buttons = new List<Button>();
    private void Start()
    {
        Button[] btns = btnMom.GetComponentsInChildren<Button>(true);
        for (int i = 0; i < btns.Length; i++)
        {
            buttons.Add(btns[i]);
        }
        for (int i = 0; i < panelChangeBtns.Count; i++)
        {
            int idx = i;
            if (i > panelDatas.Count)
            {
                break;
            }
            btnDataPair.Add(panelChangeBtns[i], panelDatas[i]);
            panelChangeBtns[i].onClick.AddListener(() => SetUI(panelChangeBtns[idx]));
            panelChangeBtns[i].GetComponentInChildren<TMP_Text>().text = panelDatas[i].type.ToString();
        }
    }
    public void SetUI(Button currentClicked)
    {
        foreach(var item in buttons)
        {
            item.gameObject.SetActive(false);
        }
        var currentData = btnDataPair[currentClicked];

        for(int i = 0; i< currentData.data.Count; i++)
        {
            print(buttons.Count);
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentsInChildren<Image>()[1].sprite = currentData.data[i].icon;
        }
    }
}

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
    [SerializeField] FolderUIController folderUIController;
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
            panelChangeBtns[i].onClick.AddListener(() => SetUI(panelChangeBtns[idx], panelDatas[idx].type == ObjType.Pet));
            panelChangeBtns[i].GetComponentInChildren<TMP_Text>().text = panelDatas[i].type.ToString();
        }
        folderUIController = GetComponent<FolderUIController>();
    }
    public void SetUI(Button currentClicked, bool isPet)
    {
        foreach (var item in buttons)
        {
            item.gameObject.SetActive(false);
        }
        var currentData = btnDataPair[currentClicked];

        for (int i = 0; i < currentData.data.Count; i++)
        {
            int index = i;
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentsInChildren<Image>()[1].sprite = currentData.data[i].icon;
            if (isPet)
            {
                buttons[i].onClick.AddListener(() => PetStatManager.Instance.ChangePet(currentData.data[index].name, currentData.data[index].prefab));
            }
            else
            {
                buttons[i].onClick.AddListener(() => PlaceOnIndicator.placePrefab = currentData.data[index].prefab);
                LogController.onObjectChange(currentData.data[index].name);
            }

            buttons[i].onClick.AddListener(folderUIController.ToggleFolder);
        }
    }
}

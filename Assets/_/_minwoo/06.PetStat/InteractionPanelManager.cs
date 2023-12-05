using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPanelManager : MonoBehaviour
{
    [SerializeField] GameObject touchP;
    [SerializeField] TMP_Text statTxt;
    [SerializeField] Button loveBtn;
    [SerializeField] Button cleanBtn;


    public void StatPanelSetUp()
    {
        PetStat current = PetStatManager.Instance.NameStatPair[PetStatManager.Instance.GetCurrentName()];
        statTxt.text =
            $"<b><color=black>Name : {PetStatManager.Instance.GetCurrentName()}\r\n</color></b>" +
            $"<b><color=orange>Age : {current.Age}\r\n</color></b>" +
            $"<b><color=yellow>Hunger : {current.Hunger}\r\n</color></b>" +
            $"<b><color=red>Love : {current.Love}\r\n</color></b>" +
            $"<b><color=blue>Clean : {current.Clean}\r\n</color></b>" +
            $"<b><color=green>Health : {current.Health}</color></b>";
    }
    public void PetInteractionBtnSetUp()
    {
        loveBtn.onClick.RemoveAllListeners();
        cleanBtn.onClick.RemoveAllListeners();
        //PetController current = PetStatManager.Instance.GetCurrentPet().GetComponent<PetController>();
        PetController current = FindAnyObjectByType<PetController>();
        cleanBtn.onClick.AddListener(current.PetShower);
        cleanBtn.onClick.AddListener(() => touchP.SetActive(false));
    }
}

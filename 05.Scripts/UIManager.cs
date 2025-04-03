using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UIManager>();
            
            return instance;
        }
    }

    

    public GameObject[] UI;
    public Slider healthSlider; //체력을 표시할 UI 슬라이더
    [SerializeField] private GameObject Player;
    [SerializeField] private Image[] Quests;
    [SerializeField] private Image[] ItemIcons;
    int batteryCount = 0;

    private void Awake()
    {
        if(instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        foreach(var item in ItemIcons)
        {
            item.gameObject.SetActive(false);
        }
        batteryCount = 0;
    }

    public void ToggleHelpUI(int idx, bool isShow)
    {
        UI[idx].SetActive(isShow);
    }

    public void ResetPlayerHpBar()
    {
        healthSlider.maxValue = Player.GetComponent<PlayerHealth>().startingHealth;

        healthSlider.value = Player.GetComponent<PlayerHealth>().health;

    }

    public void UpdateItemIcons(ItemData item)
    {
        int itemID = int.Parse(item.itemID.ToString());
        ItemIcons[itemID].gameObject.SetActive(true);
    }

    public void UpdateQuest(ItemData item)
    {
        int itemID = int.Parse(item.itemID.ToString());
        if (itemID == 0)
        {
            Quests[2].gameObject.SetActive(false);
        }
        else if(itemID >= 1 && itemID <= 4)
        {
            Quests[1].GetComponentInChildren<TextMeshProUGUI>().text = $"Find spaceship batteries ({++batteryCount}/4)";
            if (batteryCount == 4) Quests[1].gameObject.SetActive(false);
        }
    }
}

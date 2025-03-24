using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

    public UnityEvent SetQuestComplete;

    public GameObject[] UI;
    public Slider healthSlider; //체력을 표시할 UI 슬라이더
    [SerializeField] private GameObject Player;
    [SerializeField] private Image[] Quests;
    [SerializeField] private Image[] ItemIcons;

    private void Awake()
    {
        if(instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        foreach(var item in ItemIcons)
        {
            item.gameObject.SetActive(false);
        }
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
        int itemID = int.Parse(item.itemID);
        ItemIcons[itemID].gameObject.SetActive(true);
    }
}

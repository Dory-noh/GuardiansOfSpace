using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null) FindObjectOfType<UIManager>();
            return instance;
        }
    }

    [SerializeField] GameObject[] UI;

    private void Awake()
    {
        if(instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public void ToggleHelpUI(int idx, bool isShow)
    {
        UI[idx].SetActive(isShow);
    }

    private void Update()
    {

    }
}

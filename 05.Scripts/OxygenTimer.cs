using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenTimer : MonoBehaviour
{
    public int startingTime = 600;
    public int remainingTime;
    private bool timeStop = false;

    void Start()
    {
        remainingTime = startingTime;
        UIManager.Instance.ResetOxygenBar();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while(remainingTime > 0 && timeStop == false)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
            UIManager.Instance.oxygenSlider.value = remainingTime;
            if (remainingTime < 120) UIManager.Instance.oxygenSlider.transform.Find("Fill Area/Fill").GetComponent<Image>().color = new Color(0xDC, 00, 0xFF, 0xff); 
        }
        timeStop = true;
        UIManager.Instance.Player.GetComponent<PlayerHealth>().Die();
    }
}

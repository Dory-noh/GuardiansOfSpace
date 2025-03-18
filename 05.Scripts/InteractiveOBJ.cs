using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveOBJ : MonoBehaviour, IItem
{
    public void Use(GameObject target)
    {
        gameObject.SetActive(false);
        UIManager.Instance.SetQuestComplete?.Invoke();
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ToggleHelpUI(0, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.ToggleHelpUI(0, false);
    }
}

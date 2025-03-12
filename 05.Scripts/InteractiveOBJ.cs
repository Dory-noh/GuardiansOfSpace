using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveOBJ : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{transform.name}°ú ºÎµúÈû");
        UIManager.Instance.ToggleHelpUI(0, true);
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.ToggleHelpUI(0, false);
    }
}

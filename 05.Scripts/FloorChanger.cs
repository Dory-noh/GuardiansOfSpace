using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChanger : MonoBehaviour
{
    public int floor;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IPlayer>() is not null)
            UIManager.Instance.ChangeFloorInfo(floor);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.IsGameover) return;
        LivingEntity otherEntity = null;
        LivingEntity parentEntity = null;
        if(other.transform.parent != null)
        otherEntity = (LivingEntity)other.transform.parent.GetComponent<IDamageable>();
        if(transform.parent != null)
        parentEntity = (LivingEntity)transform.parent.GetComponent<IDamageable>();
        if (otherEntity is not null && parentEntity is not null)
        {
            Debug.Log($"{gameObject.name}이 {other.name} 을 공격하였음.");
            otherEntity.OnDamage(parentEntity.power);
        }
        else
        {
            Debug.Log("LivingEntity를 찾을 수 없습니다.");
        }
    }
}

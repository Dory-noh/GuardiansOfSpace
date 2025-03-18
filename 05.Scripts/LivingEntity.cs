using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100f; //시작 체력
    public float health {  get; set; } //현재 체력
    public bool dead { get; private set; } //사망 상태
    public event Action onDeath; //사망시 발동할 이벤트

    //생명체가 활성화될 때 상태 리셋
    protected virtual void OnEnable()
    {
        //사망하지 않은 상태로 시작
        dead = false;
        //체력을 시작 체력으로 초기화
        health = startingHealth;
    }

    //데미지를 잃는 메서드
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0f, startingHealth);
        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    //사망 처리
    public virtual void Die()
    {
        //onDeath 이벤트에 등록된 메서드가 있다면 실행한다.
        if(onDeath != null)
        {
            onDeath();
        }

        //사망 상태를 참으로 변경한다.
        dead = true;
    } 
}

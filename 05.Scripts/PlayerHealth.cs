using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

//플레이어 캐릭터의 생명체로서의 동작을 담당한다.
public class PlayerHealth : LivingEntity
{
    public Vector3 originPos;
    

    private PlayerInput PlayerInput;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        originPos = transform.position;
        playerMovement = GetComponent<PlayerMovement>();
        PlayerInput = GetComponent<PlayerInput>();
    }

    protected override void OnEnable()
    {
        base.OnEnable(); //dead를 false로, hp값을 100으로 만듦.

        UIManager.Instance.ResetPlayerHpBar();

        //플레이어의 조작과 동작을 받는 컴포넌트들 활성화
        playerMovement.enabled = true;
        PlayerInput.enabled = true;
    }


    //데미지 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (!dead) //데미지 입었을 때 작동 코드
        {
            
        }
        //Living Entity의 OnDamage() 실행
        base.OnDamage(damage, hitPoint, hitDirection);
        //갱신된 체력을 체력 슬라이더에 반영한다.
        UIManager.Instance.healthSlider.value = health;
    }

    public override void Die()
    {
        base.Die();

        //사망 애니 재생

        //플레이어의 조작을 받는 컴포넌트, 입력 스크립트 비활성화
        PlayerInput.enabled = false;
        playerMovement.enabled=false;

        gameObject.SetActive(false);
        //3초 후 다시 리스폰된다.
        Invoke("Respawn", 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //아이템과 충돌한 경우 해당 아이템을 가방에 넣는다.
        IItem item = other.GetComponent<IItem>();

        //item로 부터 IItem 가저오는데 성공했다면(item이 null이 아니라면)
        if(item != null)
        {
            item.Use(gameObject);
        }
        //소리 재생
    }

    public void Respawn()
    {
        transform.position = originPos;
        gameObject.SetActive(true);
    }
}

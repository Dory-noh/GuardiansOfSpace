using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItemBox : InteractiveOBJ
{
    [SerializeField] AudioClip openSoundClip;
    [SerializeField] AudioClip closeSoundClip;
    AudioSource audioSource;

    [SerializeField] string openAnimName = "itemBoxAnimationOpen";
    [SerializeField] string closeAnimName = "itemBoxAnimationClose";
    Animation itemBoxAnimation;

    [SerializeField] BoxCollider childCollider;
    [SerializeField] BoxCollider itemBoxCollider;

    bool isOpen = false;
    bool isAnimating = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        itemBoxAnimation = GetComponent<Animation>();
        itemBoxCollider = GetComponent<BoxCollider>();
        itemBoxCollider.enabled = true;
        if(childCollider != null ) childCollider.enabled = false;
    }

    override public void Use(GameObject target)
    {
        //애니메이션 중복 재생 방지
        if (isAnimating == true) return;
        StartCoroutine(ToggleItemBox());

    }

    IEnumerator ToggleItemBox()
    {
        isAnimating = true;   
        if (!isOpen)
        {
            //아이템 상자가 비어있지 않으면
            if (childCollider != null)
            {
                itemBoxCollider.enabled = false;
                childCollider.enabled = true;
            }
                audioSource.clip = openSoundClip;
            itemBoxAnimation.Play(openAnimName);
        }
        else
        {
            audioSource.clip = closeSoundClip;
            itemBoxAnimation.Play(closeAnimName);
        }
        audioSource.Play();
        isOpen = !isOpen;

        //중복 재생 방지
        yield return new WaitForSeconds(1f);
        isAnimating = false;
    }
}

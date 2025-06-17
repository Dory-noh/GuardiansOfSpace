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

    bool isOpen = false;
    bool isAnimating = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        itemBoxAnimation = GetComponent<Animation>();
    }

    override public void Use(GameObject target)
    {
        if (isAnimating == true) return;
        StartCoroutine(ToggleItemBox());

    }

    IEnumerator ToggleItemBox()
    {
        isAnimating = true;   
        if (!isOpen)
        {
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

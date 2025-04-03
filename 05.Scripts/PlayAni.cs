using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayAni: MonoBehaviour
{
    private Animator[] target = new Animator[3];
    [SerializeField] private GameObject[] gameObjects;
    int idx = -1;
    private void Awake()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            target[i] = gameObjects[i].GetComponentInChildren<Animator>();
        }
    }

    public void Call(int index)
    {
        idx = index;
        target[index].SetTrigger("IsPlay");
    }
}
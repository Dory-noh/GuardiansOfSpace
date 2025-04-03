using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] Animator spaceShipAni;
    readonly int hashIsLobby = Animator.StringToHash("IsLobby");
    readonly int hashStart = Animator.StringToHash("Start");
    private void OnEnable()
    {
        spaceShipAni.SetBool(hashIsLobby, true);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            spaceShipAni.SetTrigger(hashStart);
            Invoke("LoadMainScene", 0.35f);
        }

    }

    private void LoadMainScene()
    {
        spaceShipAni.SetBool(hashIsLobby, false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        
    }
}

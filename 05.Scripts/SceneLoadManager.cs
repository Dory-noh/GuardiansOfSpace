using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] Animator spaceShipAni;
    [SerializeField] TextMeshProUGUI waitText;
    readonly int hashIsLobby = Animator.StringToHash("IsLobby");
    readonly int hashStart = Animator.StringToHash("Start");

    float timer = 0f;
    int dotCount = 0;
    bool isAnimating = false;
    string[] dotArr = { "", ".", "..", "...", "....", ".....", "......" };

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            spaceShipAni.SetBool(hashIsLobby, true);
        }
        //waitText.gameObject.SetActive(false);
        isAnimating = false;
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name=="StartScene" && Input.GetKey(KeyCode.Space) && isAnimating == false)
        {
            spaceShipAni.SetTrigger(hashStart);
            StartCoroutine(LoadMainSceneWithLoading());
        }
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            spaceShipAni.SetBool(hashIsLobby, false);
        }
        if (isAnimating)
        {
            AnimatingLoadingText();
        }
    }

    IEnumerator LoadMainSceneWithLoading()
    {
        //우주선 애니메이션 시간
        yield return new WaitForSeconds(0.2f);
        ShowWaitText();
        //최소 1초 동안 로딩 텍스트 보여줌.
        yield return new WaitForSeconds(1f);
        //비동기식 씬 전환 : 씬 전환 중 UI 멈추지 않도록 함.(Loading 텍스트 애니메이션 보여주기 위함)
        AsyncOperation op = SceneManager.LoadSceneAsync("MainScene");
        while (!op.isDone) yield return null;
    }

    private void ShowWaitText()
    {
        waitText.gameObject.SetActive(true);
        isAnimating = true;
    }

    void AnimatingLoadingText()
    {
        timer += Time.deltaTime;
        if(timer >= 0.2f)
        {
            dotCount = (dotCount + 1) % dotArr.Length;
            waitText.text = "Wait" + dotArr[dotCount];
            timer = 0;
        }
    }

    public void LoadMainScene()
    {
        Debug.Log("메인씬 로드 버튼 누름");
        spaceShipAni.SetBool(hashIsLobby, false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        
    }

    public void ExitGame()
    {
        Debug.Log("종료 버튼 누름");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ShowLobby()
    {
        SceneManager.LoadScene("StartScene");
    }
    private void OnDisable()
    {
        isAnimating = false;
    }
}

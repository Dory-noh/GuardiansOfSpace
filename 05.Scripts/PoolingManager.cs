using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager instance;
    public static PoolingManager Instance
    {
        get
        {
            if(instance == null) instance = FindObjectOfType<PoolingManager>();
            return instance;
        }
    }
    [SerializeField] private GameObject CreepPrefab;
    [SerializeField] private List<GameObject> CreepList = new List<GameObject>();
    [SerializeField] private Transform[] SpawnPoints;

    [SerializeField] private GameObject SpiderPrefab;
    [SerializeField] private List<GameObject> SpiderList = new List<GameObject>();


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        StartCoroutine(CreateMonster(CreepPrefab, CreepList));
        StartCoroutine(CreateMonster(SpiderPrefab, SpiderList));
        StartCoroutine(RespawnMonster(CreepList, 10));
        //StartCoroutine(RespawnMonster(SpiderList));
    }

    IEnumerator CreateMonster(GameObject monsterPrefab, List<GameObject> monsterList)
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 10; i++)
        {
            GameObject monster = Instantiate(monsterPrefab, transform);
            monster.name = $"{i}¹øÂ° {monsterPrefab.name}";
            monster.SetActive(false);
            monsterList.Add(monster);
        }
    }

    public void RespawnSpider()
    {
        StartCoroutine(RespawnMonster(SpiderList, 1));
    }

   IEnumerator RespawnMonster(List<GameObject> monsterList, int time)
    {
        if (GameManager.Instance.IsGameover) yield break;
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < time; i++)
        {
            foreach (var monster in monsterList)
            {
                if (monster.activeSelf == false)
                {
                    monster.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;
                    monster.transform.rotation = Quaternion.identity;
                    monster.gameObject.SetActive(true);
                    yield return new WaitForSeconds(10f);
                }
            }
        }
    }

    void Update()
    {
        if (GameManager.Instance.IsGameover)
        {
            StopAllCoroutines();
            return;
        }
    }
}

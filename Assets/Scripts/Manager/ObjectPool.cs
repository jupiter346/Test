using UnityEngine;
using System.Collections.Generic;


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance = null; // { get; private set; }

    [SerializeField] private GameObject MissilePrefab;
    [SerializeField] private int InitialPoolSize = 30; // 초기 풀 크기

    private Queue<Missile> missilePool = new Queue<Missile>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 가비지 컬렉터에 메모리를 반납
            return;
        }

        DontDestroyOnLoad(gameObject); // 다른 씬에서도 사용(다른 씬으로 가도 메모리가 안 사라짐)
        //InitializePool();
    }

    public void InitializePool()
    {
        missilePool.Clear();
        for (int i = 0; i < InitialPoolSize; i++)
        {
            CreateNewMissile();
        }
    }

    private void CreateNewMissile()
    {
        GameObject missileObject = Instantiate(MissilePrefab);
        Missile missile = missileObject.GetComponent<Missile>();
        missile.Deactive();
        missilePool.Enqueue(missile);
    }

    public Missile GetMissile(Vector3 position)
    {
        if (missilePool.Count == 0)
        {
            CreateNewMissile();
        }

        Missile missile = missilePool.Dequeue();
        if (missile == null || missile.gameObject == null)
        {
            // 파괴된 미사일일 경우 새로운 미사일을 생성
            CreateNewMissile();
            missile = missilePool.Dequeue();
        }
        missile.Initalize(position);
        return missile;
    }

    public void ReturnMissile(Missile missile)
    {
        missile.Deactive();
        missilePool.Enqueue(missile);
    }
    public void ResetPool()
    {
        foreach (var missile in missilePool)
        {
            missile.Deactive();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

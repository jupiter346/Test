using UnityEngine;
using System.Collections.Generic;


public class EnemyMissilePool : MonoBehaviour
{
    public static EnemyMissilePool Instance = null; // { get; private set; }

    [SerializeField] private GameObject MissilePrefab;
    [SerializeField] private int InitialPoolSize = 30; // 초기 풀 크기

    private Queue<EnemyMissile> E_missilePool = new Queue<EnemyMissile>();

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
        E_missilePool.Clear();

        for (int i = 0; i < InitialPoolSize; i++)
        {
            CreateNewMissile();
        }
    }

    private void CreateNewMissile()
    {
        GameObject missileObject = Instantiate(MissilePrefab);
        EnemyMissile e_missile = missileObject.GetComponent<EnemyMissile>();
        e_missile.Deactive();
        E_missilePool.Enqueue(e_missile);
    }

    public EnemyMissile GetMissile(Vector3 position)
    {
        if (E_missilePool.Count == 0)
        {
            CreateNewMissile();
        }

        EnemyMissile e_missile = E_missilePool.Dequeue();
        e_missile.Initalize(position);
        return e_missile;
    }

    public void ReturnMissile(EnemyMissile e_missile)
    {
        e_missile.Deactive();
        E_missilePool.Enqueue(e_missile);
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

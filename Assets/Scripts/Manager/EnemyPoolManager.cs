using UnityEngine;
using System.Collections.Generic;

public class EnemyPoolManager : MonoBehaviour
{
    // C# - 가비지 컬렉터 - 메모리 단편화
    // 설정해 놓은 범위 내에서 메모리를 사용하겠다 - 메모리 풀
    // Instanciate()
    // Destroy()
    
    // 싱글톤 - 게임 내에서 1개만 존재해야 하는 객체
    // 이러면 GameManager등 다른 데에서 EnemyPoolManager.instance.함수 이런식으로 바로 호출할 수 있다.
    public static EnemyPoolManager Instance {  get; private set; }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyFollowPrefab;
    [SerializeField] private int InitialPoolSize = 10; // 초기 풀 크기
    // tool에서만 보이고 코드상에선 안보이고(private)

    private Queue<Enemy> enemyPool = new Queue<Enemy>();
    private Queue<EnemyFollowType> enemyFollowPool = new Queue<EnemyFollowType>();

    private void Awake()
    {
        if(Instance == null)
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
        enemyPool.Clear();
        enemyFollowPool.Clear();

        for (int i = 0; i < InitialPoolSize; i++)
        {
            CreateNewEnemy();
            CreateNewEnemyFollow();
        }
    }

    private void CreateNewEnemy()
    {
        GameObject enemyObject = Instantiate(enemyPrefab); // 새로운 메모리를 만든다.
        Enemy enemy = enemyObject.GetComponent<Enemy>(); // Enemy 스크립틀을 가져온다
        enemy.Deactive();
        enemyPool.Enqueue(enemy);
    }

    private void CreateNewEnemyFollow()
    {
        GameObject enemyFollowObject = Instantiate(enemyFollowPrefab);
        EnemyFollowType enemyFollow = enemyFollowObject.GetComponent<EnemyFollowType>();
        enemyFollow.Deactive();
        enemyFollowPool.Enqueue(enemyFollow);
    } 
    
    public Enemy GetEnemy(Vector3 position)
    {
        if(enemyPool.Count == 0)    
        {
            CreateNewEnemy();
        }

        Enemy enemy = enemyPool.Dequeue();
        enemy.Initalize(position);
        FireMissileEnemy missileEnemy = enemy.GetComponent<FireMissileEnemy>();
        if (missileEnemy != null)
        {
            missileEnemy.Initialize();  // FireMissileEnemy 초기화
        }
        return enemy;
    }

    public EnemyFollowType GetEnemyFollow(Vector3 position)
    {
        if (enemyFollowPool.Count == 0)
        {
            CreateNewEnemyFollow();
        }

        EnemyFollowType enemyFollow = enemyFollowPool.Dequeue();
        enemyFollow.Initalize(position);
        return enemyFollow;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.Deactive();
        enemyPool.Enqueue(enemy);
    }

    public void ReturnEnemyFollow(EnemyFollowType enemyFollow)
    {
        enemyFollow.Deactive();
        enemyFollowPool.Enqueue(enemyFollow);
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

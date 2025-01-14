using UnityEngine;
using System.Collections.Generic;

public class EnemyPoolManager : MonoBehaviour
{
    // C# - ������ �÷��� - �޸� ����ȭ
    // ������ ���� ���� ������ �޸𸮸� ����ϰڴ� - �޸� Ǯ
    // Instanciate()
    // Destroy()
    
    // �̱��� - ���� ������ 1���� �����ؾ� �ϴ� ��ü
    // �̷��� GameManager�� �ٸ� ������ EnemyPoolManager.instance.�Լ� �̷������� �ٷ� ȣ���� �� �ִ�.
    public static EnemyPoolManager Instance {  get; private set; }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyFollowPrefab;
    [SerializeField] private int InitialPoolSize = 10; // �ʱ� Ǯ ũ��
    // tool������ ���̰� �ڵ�󿡼� �Ⱥ��̰�(private)

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
            Destroy(gameObject); // ������ �÷��Ϳ� �޸𸮸� �ݳ�
            return;
        }

        DontDestroyOnLoad(gameObject); // �ٸ� �������� ���(�ٸ� ������ ���� �޸𸮰� �� �����)
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
        GameObject enemyObject = Instantiate(enemyPrefab); // ���ο� �޸𸮸� �����.
        Enemy enemy = enemyObject.GetComponent<Enemy>(); // Enemy ��ũ��Ʋ�� �����´�
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
            missileEnemy.Initialize();  // FireMissileEnemy �ʱ�ȭ
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

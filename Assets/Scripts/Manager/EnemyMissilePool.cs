using UnityEngine;
using System.Collections.Generic;


public class EnemyMissilePool : MonoBehaviour
{
    public static EnemyMissilePool Instance = null; // { get; private set; }

    [SerializeField] private GameObject MissilePrefab;
    [SerializeField] private int InitialPoolSize = 30; // �ʱ� Ǯ ũ��

    private Queue<EnemyMissile> E_missilePool = new Queue<EnemyMissile>();

    private void Awake()
    {
        if (Instance == null)
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

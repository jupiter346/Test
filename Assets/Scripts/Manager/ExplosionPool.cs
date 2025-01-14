using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Instance = null; // { get; private set; }

    [SerializeField] private Explosion explosionPrefab = null;
    [SerializeField] private int InitialPoolSize = 10; // �ʱ� Ǯ ũ��

    private Queue<Explosion> explosionPool = new Queue<Explosion>();

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
        explosionPool.Clear();
        for (int i = 0; i < InitialPoolSize; i++)
        {
            CreateNewExplosion();
        }
    }

    private void CreateNewExplosion()
    {
        Explosion explosion = Instantiate(explosionPrefab);
        explosion.Deactive();
        explosionPool.Enqueue(explosion);
    }

    public Explosion GetExplosion(Vector3 position)
    {
        if (explosionPool.Count == 0)
        {
            CreateNewExplosion();
        }

        Explosion explosion = explosionPool.Dequeue();
        explosion.Initalize(position);
        return explosion;
    }
    public void ReturnExplosion(Explosion explosion)
    {
        explosion.Deactive();
        explosionPool.Enqueue(explosion);
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

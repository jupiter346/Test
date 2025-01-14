using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Instance = null; // { get; private set; }

    [SerializeField] private Explosion explosionPrefab = null;
    [SerializeField] private int InitialPoolSize = 10; // 초기 풀 크기

    private Queue<Explosion> explosionPool = new Queue<Explosion>();

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

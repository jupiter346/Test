using UnityEngine;
using System.Collections.Generic;


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance = null; // { get; private set; }

    [SerializeField] private GameObject MissilePrefab;
    [SerializeField] private int InitialPoolSize = 30; // �ʱ� Ǯ ũ��

    private Queue<Missile> missilePool = new Queue<Missile>();

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
            // �ı��� �̻����� ��� ���ο� �̻����� ����
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

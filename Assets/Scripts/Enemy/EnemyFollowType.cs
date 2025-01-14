using UnityEngine;

public class EnemyFollowType : MonoBehaviour, ICharacter
{
    public float hp { get; private set; } = 100.0f;
    public Vector3 position { get; private set; } = Vector3.zero;

    [SerializeField] private float moveSpeed = 0.5f;

    private Transform playerTransform = null;

    private Vector2 moveDir = Vector2.down;

    public void Initalize(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }
    public void Deactive()
    {
        gameObject.SetActive(false);
    }


    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        moveDir.x = (playerTransform.position.x > transform.position.x) ? 1.0f : -1.0f;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
    }
    private void Die()
    {
        Explosion explosion = ExplosionPool.Instance.GetExplosion(position);
        ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missile"))
        {
            hp -= 30.0f;

            if (hp <= 0.0f)
            {
                Die();
                Deactive();
                GameManager.gameManager.SetScore(10);
                hp = 100.0f; // �ݳ��Ҷ� ü�� �ʱ�ȭ
            }
        }

        if (other.CompareTag("Player"))
        {
            
            GameManager.gameManager._PlayerInstance.Damage(30.0f);  // ���� ���°� �ƴϸ� ������ ����
            
            Die();
            Deactive();
        }
    }
    private void ReturnToPool()
    {
        EnemyPoolManager.Instance.ReturnEnemyFollow(this);
    }
}

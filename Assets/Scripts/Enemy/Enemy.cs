using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    public float hp { get; private set; } = 100.0f;
    public Vector3 position { get; private set; } = Vector3.zero;

    [SerializeField] private float HorMoveSpeed = 1.0f;

    [SerializeField] private float VirMoveSpeed = 0.5f;

    private Transform playerTransform = null;

    private Vector3 moveDir = Vector3.zero;

    private bool isOnPosition = true;

    private Vector3 location = Vector3.zero;

    //[SerializeField] private float spawnYPosition = 10.0f;  // 화면 밖에서 생성될 y 좌표
    [SerializeField] private float targetYPosition = 5.0f; // y좌표가 이 위치에 도달하면 원래 이동 시

    private bool isMovingToTargetY = true;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }


    public void Initalize(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;

        hp = 100.0f; // 다시 생성될때 체력 초기화

        //transform.position = new Vector3(transform.position.x, spawnYPosition, transform.position.z);
        isMovingToTargetY = true;
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }
     
    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        if (isMovingToTargetY)
        {
            MoveVertically();
        }
        else
        {
            MoveToPosition();  // 원래의 x축으로 이동
        }

        //transform.Translate(Vector3.down * VirMoveSpeed * Time.deltaTime, Space.World);
    }
    private void Initialize()
    {
        StartCoroutine(EnemyMoveTo());
    }

    private IEnumerator EnemyMoveTo()
    {
        yield return new WaitUntil(() => isOnPosition);
        location = new Vector3(Random.Range(-3.0f, 3.0f), 0.0f, 0.0f);
        isOnPosition = false;
    }

    private void MoveVertically()
    {
        // y 좌표가 targetYPosition에 도달할 때까지 세로로 이동
        transform.Translate(Vector3.down * VirMoveSpeed * Time.deltaTime, Space.World);

        // 특정 y좌표에 도달하면 원래의 움직임 시작
        if (transform.position.y <= targetYPosition)
        {
            isMovingToTargetY = false;
        }
    }

    private void MoveToPosition()
    {
        moveDir.x = (location.x > transform.position.x) ? 1.0f : -1.0f;
        transform.Translate(moveDir * HorMoveSpeed * Time.deltaTime, Space.World);

        if (Mathf.Abs(transform.position.x - location.x) < 0.1f)
        {
            isOnPosition = true;
            StartCoroutine(EnemyMoveTo());
        }
    }


    private void Die()
    {
        Explosion explosion = ExplosionPool.Instance.GetExplosion(position);
        ReturnToPool();
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Missile"))
        {
            hp -= 30.0f;

            if (hp <= 0.0f)
            {
                Die();
                Deactive();
                GameManager.gameManager.SetScore(10);
            }
        }

        if(other.CompareTag("Player"))
        {
            GameManager.gameManager._PlayerInstance.Damage(30.0f);
            Die();
            Deactive();
        }
    }
    private void ReturnToPool()
    {
        EnemyPoolManager.Instance.ReturnEnemy(this);
    }
}

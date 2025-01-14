using System.Collections;
using UnityEngine;

public class PlayerInstance : MonoBehaviour, ICharacter
{
    // �÷��̾� hp ������Ƽ
    public float hp { get; private set; } = 100.0f;

    public Vector3 position { get; private set; } = Vector3.zero;

    // �÷��̾ �������� ���� �� �ִ� ��������
    public bool _Invincibility = false;

    private SpriteRenderer _Sprite = null;

    private Camera mainCamera;


    //public TableTest tableTest;

    private void Awake()
    {
        _Sprite = transform.Find("PlayerImage").GetComponent<SpriteRenderer>();
        GameManager.gameManager._PlayerInstance = this;
        mainCamera = Camera.main;
    }

    //private void OnEnable()
    //{
    //    Debug.Log("TableTest c : " + tableTest.c);
    //}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        ClampToScreen();  // ȭ�� ������ ������ �ʵ��� ����
    }
    private void ClampToScreen()
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);

        // Viewport �������� ��ġ ���� (0~1 ����)
        screenPos.x = Mathf.Clamp(screenPos.x, 0.05f, 0.95f);
        screenPos.y = Mathf.Clamp(screenPos.y, 0.05f, 0.95f);

        // Viewport���� ���� ��ǥ�� ��ȯ
        transform.position = mainCamera.ViewportToWorldPoint(screenPos);
    }

    //private void Die()
    //{
    //    Explosion explosion = ExplosionPool.Instance.GetExplosion(position);
    //    GameManager.gameManager.GameOver();
    //}

    private IEnumerator PlayerDie()
    {
        Explosion explosion = ExplosionPool.Instance.GetExplosion(position);

        //transform.position = new Vector3(1000, 1000, 0);  // ȭ�� ������ �̵�

        yield return new WaitForSeconds(0.8f);

        GameManager.gameManager.GameOver();

        gameObject.SetActive(false);
    }

    public void Damage(float damage)
    {
        if(!_Invincibility)
        {
            hp -= damage;
            StartCoroutine(StartInvincibilityState());

            if(hp <= 0)
            {

                StartCoroutine(PlayerDie());

            }
        }
    }
        
    private IEnumerator StartInvincibilityState()
    {
        bool visible = true;

        Color DisableColor = _Sprite.color,
            EnableColor = _Sprite.color;

        DisableColor.a = 0.2f;

        _Invincibility = true;

        for (int i = 0; i < 10; i++)
        {
            visible = !visible;
            _Sprite.color = (visible) ? EnableColor : DisableColor;
            yield return new WaitForSeconds(0.1f);
        }
        _Invincibility = false;
    }

    public void SetHp(float Hp)
    {
        hp = Hp;
    }
}

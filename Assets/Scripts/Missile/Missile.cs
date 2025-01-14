using UnityEngine;

public class Missile : MonoBehaviour
{
    public void Initalize(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Camera.main.orthographicSize + 1f)  // 화면 위로 나가면 제거
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            ReturnToPool();
        }
    }

    //private void OnBecameInvisible()
    //{
    //    ReturnToPool();
    //}

    private void ReturnToPool()
    {
        ObjectPool.Instance.ReturnMissile(this);
    }
}

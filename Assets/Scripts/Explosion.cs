using UnityEngine;

public class Explosion : MonoBehaviour
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
        
    }
    public void EffectEnd()
    {
        gameObject.SetActive(false);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        ExplosionPool.Instance.ReturnExplosion(this);
    }
}

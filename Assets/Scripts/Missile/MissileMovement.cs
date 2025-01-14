using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    public Vector2 m_MoveDirection;

    public float m_Speed;

    //public bool m_HorizontalLimit= false;
    //public bool m_VerticalLimit= false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MissileMove();
    }

    private void MissileMove()
    {
        transform.Translate(m_MoveDirection * m_Speed * Time.deltaTime, Space.World);
        //gameObject.SetActive(GameStatics.IsInScreen(transform, m_HorizontalLimit, m_VerticalLimit));

    }
}

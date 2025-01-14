using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _InputHorizontal = 0.0f;
    private float _InputVertical = 0.0f;

    public VariableJoystick variableJoystick;

    [SerializeField] private float _moveSpeed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
        PlayerMove();

        if(variableJoystick.Direction.x != 0
        || variableJoystick.Direction.y != 0)
        {
            //Debug.Log("Current Value : " + variableJoystick.Direction);
            Vector3 pos  = transform.position;
            pos.x = pos.x + variableJoystick.Direction.x * _moveSpeed * Time.deltaTime;
            pos.y = pos.y + variableJoystick.Direction.y * _moveSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }

    private void InputKey()
    {
        _InputHorizontal = Input.GetAxis("Horizontal");
        _InputVertical = Input.GetAxis("Vertical");
    }

    private void PlayerMove()
    {
        transform.Translate(Vector2.right * _InputHorizontal * _moveSpeed * Time.deltaTime, Space.World);
        transform.Translate(Vector2.up * _InputVertical * _moveSpeed * Time.deltaTime, Space.World);
    }
}

using UnityEngine;

public class JoystickTest : MonoBehaviour
{
    public VariableJoystick variableJoystick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(variableJoystick.Direction.x != 0 || variableJoystick.Direction.y != 0)
        {
            Debug.Log("Current Value: " + variableJoystick.Direction);
        }

    }
}

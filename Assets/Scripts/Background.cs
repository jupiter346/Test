using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public GameObject[] bg;

    //float orignal_Y = 0;
    float scrollSpeed = 3.0f;


    private Vector2 _dir = Vector2.down;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 3; i++)
        {
            Vector3 pos = bg[i].transform.position;
            pos.y = pos.y - scrollSpeed * Time.deltaTime;
            bg[i].transform.position = pos;

            if (bg[i].transform.position.y < -41.96)
            {
                Vector3 pos2 = bg[i].transform.position;
                pos2.y = pos2.y + 122.88f;
                bg[i].transform.position = pos2;
            }
        }
    }

    void moveBackground()
    {
    }

    void BackgroundImage()
    {
        
    }
}

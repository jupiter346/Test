using UnityEngine;

public class CallbackAndLambda : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // PerformCalculations(10, 20, OnPlayCallBack); // 콜백함수
        PerformCalculations(10, 20, (res, res2) => { return 0.0f; }); // 람다코드
    }

    void PerformCalculations(int a, int b, System.Func<int,int,float> callback)
    {
        int result = a + b;

        //callback?.Invoke(7,8); // 매개 변수로 넘어온 콜백함수를 호출할 수 있다

        float c = callback?.Invoke(7, 8) ?? 0f;
    }

    float OnPlayCallBack(int res, int res2)
    {
        Debug.Log("OnPlayCallBack 호출");
        return 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

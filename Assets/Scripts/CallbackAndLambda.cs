using UnityEngine;

public class CallbackAndLambda : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // PerformCalculations(10, 20, OnPlayCallBack); // �ݹ��Լ�
        PerformCalculations(10, 20, (res, res2) => { return 0.0f; }); // �����ڵ�
    }

    void PerformCalculations(int a, int b, System.Func<int,int,float> callback)
    {
        int result = a + b;

        //callback?.Invoke(7,8); // �Ű� ������ �Ѿ�� �ݹ��Լ��� ȣ���� �� �ִ�

        float c = callback?.Invoke(7, 8) ?? 0f;
    }

    float OnPlayCallBack(int res, int res2)
    {
        Debug.Log("OnPlayCallBack ȣ��");
        return 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

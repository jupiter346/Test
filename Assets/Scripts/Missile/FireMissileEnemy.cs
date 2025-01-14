using System.Collections;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;

public class FireMissileEnemy : MonoBehaviour
{
    public Transform _MissileLoc;

    private bool _MissileLaunchable = false;

    private float _MissileLaunchDelay = 1.5f;

    private float _initialFireDelayMin = 0.5f; // ���� �߻� ���� �ּ� �ð�
    private float _initialFireDelayMax = 2.0f; // ���� �߻� ���� �ִ� �ð�


    private void Awake()
    {
        Initialize();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnMissile();
    }

    public void Initialize()
    {
        float initialDelay = Random.Range(_initialFireDelayMin, _initialFireDelayMax);
        StartCoroutine(FirstMissileLaunch(initialDelay));
    }

    private IEnumerator FirstMissileLaunch(float delay)
    {
        yield return new WaitForSeconds(delay);  // ���� ����
        _MissileLaunchable = true;  // ù �߻� ���� ���·� ��ȯ
        StartCoroutine(MissileLaunchDelay());  // ���Ĵ� ������ �������� �߻�
    }

    private IEnumerator MissileLaunchDelay()
    {
        while (true)
        {
            yield return new WaitWhile(() => _MissileLaunchable);
            yield return new WaitForSeconds(_MissileLaunchDelay);
            _MissileLaunchable = true;
        }
    }

    private void SpawnMissile()
    {
        if(_MissileLaunchable)
        {
            EnemyMissile missile = EnemyMissilePool.Instance.GetMissile(_MissileLoc.transform.position);

            _MissileLaunchable = false;
        }
    }
}

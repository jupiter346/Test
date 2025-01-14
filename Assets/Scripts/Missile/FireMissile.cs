using System.Collections;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireMissile : MonoBehaviour
{
    public Transform _MissileLoc_Left, _MissileLoc_Right;

    private bool _MissileLoc = false;

    private bool _MissileLaunchable = false;

    private float _MissileLaunchDelay = 0.07f;

    private bool _fireButtonPressed = false;

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

    private void Initialize()
    {
        StartCoroutine(MissileLaunchDelay());
    }

    private IEnumerator MissileLaunchDelay()
    {
        while (true)
        {
            if (!_MissileLaunchable)
            {
                yield return new WaitForSeconds(_MissileLaunchDelay);
                _MissileLaunchable = true;
            }
            yield return null;
        }
    }

    private void SpawnMissile()
    {
        if (_MissileLaunchable && (Input.GetKey(KeyCode.Space) || _fireButtonPressed))
        {
            Missile missile = ObjectPool.Instance.GetMissile(_MissileLoc == false ? _MissileLoc_Left.transform.position : _MissileLoc_Right.transform.position);

            _MissileLoc = !_MissileLoc;
            _MissileLaunchable = false;
        }
    }

    // 버튼 누를 때 (발사 시작)
    public void OnPointerDown()
    {
        _fireButtonPressed = true;
    }

    // 버튼 뗄 때 (발사 중지)
    public void OnPointerUp()
    {
        _fireButtonPressed = false;
    }
}

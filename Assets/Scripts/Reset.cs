using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class Reset : MonoBehaviour
{
    private EnemyPoolManager _enemyPoolManager;
    private EnemyMissilePool _enemyMissilePool;
    private ExplosionPool _explosionPool;
    private ObjectPool _objectPool;
    private GameManager _gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("fdsf");
        _enemyPoolManager = GameObject.Find("EnemyPoolManager").GetComponent<EnemyPoolManager>();
        _enemyMissilePool = GameObject.Find("EnemyMissilePoolManager").GetComponent<EnemyMissilePool>();
        _explosionPool = GameObject.Find("ExplosionPoolManager").GetComponent<ExplosionPool>();
        _objectPool = GameObject.Find("ObjectPoolManager").GetComponent<ObjectPool>();
        _gameManager = GameManager.gameManager;

        _enemyMissilePool.InitializePool();
        _enemyPoolManager.InitializePool();
        _explosionPool.InitializePool();
        _objectPool.InitializePool();
        //_gameManager.
    }

    public void RestartGame()
    {
        _gameManager.ResetGame();
        _gameManager.Initialize();
        _gameManager.DontPlayDialogue();
        // ���� �ٽ� �ε�� �� �ʱ�ȭ ���� ȣ��
        //SceneManager.sceneLoaded += OnSceneLoaded;
    } 
    public void GotoMain()
    {
        _gameManager.ResetGame();
        _gameManager.Initialize();
        _gameManager.DontPlayDialogue();

        SceneManager.LoadScene("MainScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (this != null)  // Reset ������Ʈ�� �ı��Ǿ����� üũ
        {
            StartCoroutine(InitializeAfterDelay());
        }
        else
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    private IEnumerator InitializeAfterDelay()
    {
        yield return new WaitForEndOfFrame();  // ���� ������ �ε�� ���� ����
        _gameManager.Initialize();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

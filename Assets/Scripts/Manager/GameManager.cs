using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // ���� ��ġ 
    [SerializeField] private GameObject spawnPointPrefab;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private PlayerprefsExample playerprefsExample;

    private static GameManager _GameManagerInstance = null;

    private int Score = 0;

    [SerializeField] private float spawnInterval = 2.0f; // �� ���� ���� (��)

    public static GameManager gameManager
    {
        get
        {   // GameManagerInstance�� null�� �ƴ� ��� ���� ��ȯ, null�� ��� ������ ���� �� ��ȯ
            return _GameManagerInstance ?? (_GameManagerInstance = GameObject.Find("GameManager").GetComponent<GameManager>());
        }
    }

    // �÷��̾� ĳ���� �ν��Ͻ��� ���� ������Ƽ
    public PlayerInstance _PlayerInstance { get; set; } = null;


    private void Awake()
    {
        //if (_GameManagerInstance == null)
        //{
        //    _GameManagerInstance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject); 
        
        //if (dialoguePanel != null)
        //    dialoguePanel.SetActive(true);
        //RectTransform DiapanelRect = dialoguePanel.GetComponent<RectTransform>();
        //DiapanelRect.anchoredPosition = new Vector2(360, 0);

        //dialoguePanel.SetActive(false);

        //if (gameOverPanel != null)
        //    gameOverPanel.SetActive(true); 
        //RectTransform panelRect = gameOverPanel.GetComponent<RectTransform>();
        //panelRect.anchoredPosition = new Vector2(0, 0);

        //LoadSpawnPointsFromCSV();

        Initialize();


        //if (spawnPoints == null || spawnPoints.Length == 0)
        //{
        //    LoadSpawnPointsFromCSV();
        //}
        //if (gameOverPanel == null)
        //    gameOverPanel = GameObject.Find("GameOverPanel");
        //if (finalScoreText == null)
        //    finalScoreText = GameObject.Find("FinalScoreText").GetComponent<TextMeshProUGUI>();
        //if (highScoreText == null)
        //    highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        //if (dialoguePanel == null)
        //    dialoguePanel = GameObject.Find("DialoguePanel");


    }


    public void Initialize()
    {
        if (_GameManagerInstance == null)
        {
            _GameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);
        RectTransform DiapanelRect = dialoguePanel.GetComponent<RectTransform>();
        DiapanelRect.anchoredPosition = new Vector2(360, 0);

        dialoguePanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        RectTransform panelRect = gameOverPanel.GetComponent<RectTransform>();
        panelRect.anchoredPosition = new Vector2(0, 0);

        // CSV���� ���� ����Ʈ �ٽ� �ҷ�����
        LoadSpawnPointsFromCSV();

        // UI �翬��
        //if (dialoguePanel == null)
        //    dialoguePanel = GameObject.Find("DialoguePanel");
        ////dialoguePanel.SetActive(true);
        ////RectTransform DiapanelRect = dialoguePanel.GetComponent<RectTransform>();
        ////DiapanelRect.anchoredPosition = new Vector2(360, 0);
        //Debug.Log("DialoguePanel");

        //if (gameOverPanel == null)
        //    gameOverPanel = GameObject.Find("GameOverPanel");
        //Debug.Log("GameOverPanel");
        //gameOverPanel.SetActive(true);
        //RectTransform panelRect = gameOverPanel.GetComponent<RectTransform>();
        //panelRect.anchoredPosition = new Vector2(0, 0);

        //if (finalScoreText == null)
        //    finalScoreText = GameObject.Find("FinalScoreText").GetComponent<TextMeshProUGUI>();
        //Debug.Log("FinalScoreText");

        //if (highScoreText == null)
        //    highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        //Debug.Log("HighScoreText");

        //gameOverPanel.SetActive(false);
        //Debug.Log("gameOverPanel.SetActive(false)");
        //dialoguePanel.SetActive(false);
        //Debug.Log("dialoguePanel.SetActive(false)");
        //DiapanelRect.anchoredPosition = new Vector2(360, 0);

        gameOverPanel.SetActive(false);
        dialoguePanel.SetActive(true);

    }

    private void LoadSpawnPointsFromCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("example");
        if (csvFile != null)
        {
            List<Transform> spawnList = new List<Transform>();
            string[] rows = csvFile.text.Split('\n');

            foreach (string row in rows)
            {
                if (string.IsNullOrWhiteSpace(row)) continue; // �ش� ���� ����ְų� ������ ��� true�� ��ȯ�� �ǳʶݴϴ�.
                                                              // �̴� CSV ���Ͽ� �� ���� ���ԵǾ� ���� �� ����� ������ �����մϴ�

                string[] values = row.Split(',');
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);

                // �� ������Ʈ�� ����� ��ġ ����
                GameObject point = Instantiate(spawnPointPrefab, new Vector3(x, y, 0), Quaternion.identity); //Instantiate�� spawnPointPrefab(�� ������Ʈ ������)�� �����ϰ�,
                                                                                                             //CSV���� ���� ��ǥ(x, y, 0)�� ��ġ�մϴ�.
                                                                                                             //Quaternion.identity�� ȸ������ ���� ����(0,0,0)�� �ǹ��մϴ�.
                                                                                                             //��, �������� Ư�� ��ġ�� ȸ�� ���� �����մϴ�.
                                                                                                             //spawnPointPrefab�� Inspector���� ������ �� ������Ʈ �������Դϴ�.

                point.name = $"SpawnPoint_{x}_{y}";

                spawnList.Add(point.transform);
            }

            spawnPoints = spawnList.ToArray();
            //Debug.Log($"Loaded {spawnPoints.Length} spawn points from CSV.");
        }
        else
        {
            Debug.LogError("Spawn points CSV ������ ã�� �� �����ϴ�!");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //EnemyPoolManager.Instance.ReturnEnemy()
        StartCoroutine(SpawnEnemyPeriodically());
    }
    private IEnumerator SpawnEnemyPeriodically()
    {
        while (true)  // ����ؼ� ����
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // ���� �ð� �������� ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnInterval(Score);
    }

    private void SpawnEnemy()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Spawn points�� �����ϴ�!");
            return;
        }

        // ���� ���� ��ġ ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // ���� ����Ʈ�� �ı��� ��� ����
        if (spawnPoint == null)
            return;

        // �� Ǯ���� �� ��������
        if (Random.Range(0, 2) == 0)  // 50% Ȯ���� �� Ÿ�� ����
        {
            Enemy enemy = EnemyPoolManager.Instance.GetEnemy(spawnPoint.position);
        }
        else
        {
            EnemyFollowType enemyFollowType = EnemyPoolManager.Instance.GetEnemyFollow(spawnPoint.position);
        }
    }

    public void SetScore(int score)
    {
        Score += score;
    }

    public int GetScore()
    {
        return Score;
    }

    public void GameOver()
    {
        if (playerprefsExample != null)
            playerprefsExample.UpdateHighScore(Score);

        Time.timeScale = 0.0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = $"Score : {Score}";
            
        if (highScoreText != null && playerprefsExample != null)
        {
            int highscore = playerprefsExample.GetHighScore();
            highScoreText.text = $"High Score : {highscore}";
        }
    }

    public void ResetGame()
    {
        Score = 0;
        gameManager._PlayerInstance.SetHp(100.0f);
        Time.timeScale = 1.0f;

        ObjectPool.Instance.ResetPool();
        SceneManager.LoadScene("GameScene");
        Invoke(nameof(InitializeAfterSceneLoad), 0.1f);
    }

    public void DontPlayDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    // �� �ε� �� �ʱ�ȭ �޼���
    private void InitializeAfterSceneLoad()
    {
        Initialize();
    }

    private void UpdateSpawnInterval(int currentScore)
    {
        int threshold = 100;  // 100�� ����
        float decrement = 0.1f;  // ���� ���� (0.1��)
        float minInterval = 0.5f;  // �ּ� ���� ����

        // ������ ���� ���� ���� ����
        spawnInterval = Mathf.Max(1.0f - (currentScore / threshold) * decrement, minInterval);
    }
}

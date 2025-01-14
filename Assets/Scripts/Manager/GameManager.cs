using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // 등장 위치 
    [SerializeField] private GameObject spawnPointPrefab;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private PlayerprefsExample playerprefsExample;

    private static GameManager _GameManagerInstance = null;

    private int Score = 0;

    [SerializeField] private float spawnInterval = 2.0f; // 적 생성 간격 (초)

    public static GameManager gameManager
    {
        get
        {   // GameManagerInstance가 null이 아닌 경우 왼쪽 반환, null인 경우 오른쪽 실행 후 반환
            return _GameManagerInstance ?? (_GameManagerInstance = GameObject.Find("GameManager").GetComponent<GameManager>());
        }
    }

    // 플레이어 캐릭터 인스턴스에 대한 프로퍼티
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

        // CSV에서 스폰 포인트 다시 불러오기
        LoadSpawnPointsFromCSV();

        // UI 재연결
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
                if (string.IsNullOrWhiteSpace(row)) continue; // 해당 줄이 비어있거나 공백인 경우 true를 반환해 건너뜁니다.
                                                              // 이는 CSV 파일에 빈 줄이 포함되어 있을 때 생기는 오류를 방지합니다

                string[] values = row.Split(',');
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);

                // 빈 오브젝트를 만들어 위치 지정
                GameObject point = Instantiate(spawnPointPrefab, new Vector3(x, y, 0), Quaternion.identity); //Instantiate로 spawnPointPrefab(빈 오브젝트 프리팹)을 생성하고,
                                                                                                             //CSV에서 읽은 좌표(x, y, 0)에 배치합니다.
                                                                                                             //Quaternion.identity는 회전값이 없는 상태(0,0,0)를 의미합니다.
                                                                                                             //즉, 프리팹을 특정 위치에 회전 없이 생성합니다.
                                                                                                             //spawnPointPrefab은 Inspector에서 연결한 빈 오브젝트 프리팹입니다.

                point.name = $"SpawnPoint_{x}_{y}";

                spawnList.Add(point.transform);
            }

            spawnPoints = spawnList.ToArray();
            //Debug.Log($"Loaded {spawnPoints.Length} spawn points from CSV.");
        }
        else
        {
            Debug.LogError("Spawn points CSV 파일을 찾을 수 없습니다!");
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
        while (true)  // 계속해서 생성
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // 일정 시간 간격으로 생성
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
            Debug.LogWarning("Spawn points가 없습니다!");
            return;
        }

        // 랜덤 스폰 위치 선택
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 스폰 포인트가 파괴된 경우 무시
        if (spawnPoint == null)
            return;

        // 적 풀에서 적 가져오기
        if (Random.Range(0, 2) == 0)  // 50% 확률로 적 타입 선택
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

    // 씬 로드 후 초기화 메서드
    private void InitializeAfterSceneLoad()
    {
        Initialize();
    }

    private void UpdateSpawnInterval(int currentScore)
    {
        int threshold = 100;  // 100점 간격
        float decrement = 0.1f;  // 감소 간격 (0.1초)
        float minInterval = 0.5f;  // 최소 스폰 간격

        // 점수에 따라 스폰 간격 조정
        spawnInterval = Mathf.Max(1.0f - (currentScore / threshold) * decrement, minInterval);
    }
}

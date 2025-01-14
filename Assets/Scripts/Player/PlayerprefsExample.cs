using UnityEngine;
using UnityEngine.UIElements;

public class PlayerprefsExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string path = string.Empty;

        //DontDestroyOnLoad(gameObject);
        // path = $"{Application.persistentDataPath}/Unity/{Application.companyName}/{Application.productName}/prefs";

        //Debug.Log("PlayerPrefs path : " + Application.persistentDataPath);

        //SaveData("PlayerName", "UnityPlayer");
        //SaveData("HighScore", 10);

        //string PlayerName = LoadData("PlayerName", "DefaultName");
        //int highScore = LoadData("HighScore", 0);

        //Debug.Log($"Player Name : {PlayerName}");
    }

    public void SaveData(string key, object value)
    {
        if(value is int)
        {
            PlayerPrefs.SetInt(key, (int)value);
        }
        else if(value is float)
        {
            PlayerPrefs.SetFloat(key, (float)value);
        }
        else if(value is string)
        {
            PlayerPrefs.SetString(key, (string)value);
        }
        else
        {
            Debug.LogError("지원되지 않는 데이터 타입입니다.");
            return;
        }

        PlayerPrefs.Save();
        //Debug.Log($"Data saved : {key} = {value}");
    }

    public int LoadData(string key, int defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public float LoadData(string key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public string LoadData(string key, string defaltValue)
    {
        return PlayerPrefs.GetString(key, defaltValue);
    }

    // 특정 키에 해당하는 데이터를 삭제하는 함수
    public void DeletData(string key)
    {
        if(PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
            Debug.Log($"Data deleted : {key}");
        }
        else
        {
            Debug.Log($"No data found for key : {key}");
        }
    }

    // 모든 데이터를 삭제하는 함수
    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void UpdateHighScore(int currentScore)
    {
        int highScore = LoadData("HighScore", 0);

        if (currentScore > highScore)
        {
            SaveData("HighScore", currentScore);  // 새로운 하이 스코어 저장
            Debug.Log($"New High Score : {currentScore}");
        }
        else
        {
            Debug.Log($"Current Score: {currentScore}, High Score remains : {highScore}");
        }
    }

    public int GetHighScore()
    {
        int highScore = LoadData("HighScore", 0);

        return highScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            ResetAllData();

            Debug.Log("ResetAllData");
        }

    }
}

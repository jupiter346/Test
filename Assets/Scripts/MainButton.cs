using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Dialog Dialog;

    public void OnStartBtnClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnMainBtnClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnOptionBtnClicked()
    {
        optionPanel.SetActive(true);
    }

    public void OnExitBtnClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OnMenuBtnClicked()
    {
        optionPanel.SetActive(false);
    }

    public void OnPauseBtnClicked()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnCountinueBtnClicked()
    {
        if(Dialog.OnDialogue())
        {
            pausePanel.SetActive(false);
            Time.timeScale = 0.0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void OnPlayaginBtnClicked()
    {

        
    }
    public void OnAttackBtnClicked()
    {
        

    }
}

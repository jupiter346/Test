using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    //private List<string> csvData = new List<string>();

    public Text dialogueText;

    public Button nextButton;

    public float typingSpeed = 0.05f;

    private string[] dialogueLines;  // CSV에서 가져온 대사들
    private int currentLine = 0;  // 현재 대사 줄 번호
    private bool isTyping = false;
    private bool onDialogue = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
        onDialogue = true;

        TextAsset csvFile = Resources.Load<TextAsset>("dialogue");
        if (csvFile != null)
        {
            //Debug.Log("파일있음");
            // '\n' --> 줄바꿈
            dialogueLines = csvFile.text.Split('\n'); // Split : \n 을 기준으로 나눠(분리해)준다

            nextButton.onClick.AddListener(NextDialogue);  // 버튼 클릭시 NextDialogue 호출

            StartDialogue();
        }
        else
        {
            Debug.Log("파일 없다요");
        }

        //currentDialogue = "안녕하세요";

        //StartCoroutine(TypeDialogue(currentDialogue));
    }

    public void StartDialogue()
    {
        Time.timeScale = 0;  // 게임 일시정지
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        if (currentLine < dialogueLines.Length)
        {
            StartCoroutine(TypeDialogue(dialogueLines[currentLine]));
        }
        else
        {
            //Debug.Log("대사 끝!");
            CloseBtn();
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;

        Time.timeScale = 0;
        dialogueText.text = "";
        foreach( char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }
    private void NextDialogue()
    {
        if (isTyping)  // 대사가 타이핑 중일 때는 버튼 클릭 시 바로 타이핑 끝내고 표시
        {
            StopAllCoroutines();  // 진행 중인 코루틴 중지
            dialogueText.text = dialogueLines[currentLine];  // 대사를 바로 표시
            isTyping = false;  // 타이핑 완료 상태로 설정
            return;  // 더 이상 진행하지 않음
        }

        currentLine++;  // 다음 대사로 이동
        ShowDialogue();  // 대사 표시 함수 호출
    }

    public void CloseBtn()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        onDialogue = false;
    }

    public bool OnDialogue()
    {
        return onDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

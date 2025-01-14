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

    private string[] dialogueLines;  // CSV���� ������ ����
    private int currentLine = 0;  // ���� ��� �� ��ȣ
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
            //Debug.Log("��������");
            // '\n' --> �ٹٲ�
            dialogueLines = csvFile.text.Split('\n'); // Split : \n �� �������� ����(�и���)�ش�

            nextButton.onClick.AddListener(NextDialogue);  // ��ư Ŭ���� NextDialogue ȣ��

            StartDialogue();
        }
        else
        {
            Debug.Log("���� ���ٿ�");
        }

        //currentDialogue = "�ȳ��ϼ���";

        //StartCoroutine(TypeDialogue(currentDialogue));
    }

    public void StartDialogue()
    {
        Time.timeScale = 0;  // ���� �Ͻ�����
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
            //Debug.Log("��� ��!");
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
        if (isTyping)  // ��簡 Ÿ���� ���� ���� ��ư Ŭ�� �� �ٷ� Ÿ���� ������ ǥ��
        {
            StopAllCoroutines();  // ���� ���� �ڷ�ƾ ����
            dialogueText.text = dialogueLines[currentLine];  // ��縦 �ٷ� ǥ��
            isTyping = false;  // Ÿ���� �Ϸ� ���·� ����
            return;  // �� �̻� �������� ����
        }

        currentLine++;  // ���� ���� �̵�
        ShowDialogue();  // ��� ǥ�� �Լ� ȣ��
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

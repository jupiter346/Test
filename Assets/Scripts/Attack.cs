using UnityEngine;

public class Attack : MonoBehaviour
{
    public AudioClip soundClip; // AudioClip : (����)
    private AudioSource audioSource = null; // AudioSource : (������Ʈ)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // ��ư�� ������Ʈ�� Audio Source�� �߰��߱� ������ GetComponent�� ��������

        audioSource.clip = soundClip;

        //audioSource.Play();
    }

    public void playSound()
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class Attack : MonoBehaviour
{
    public AudioClip soundClip; // AudioClip : (파일)
    private AudioSource audioSource = null; // AudioSource : (컴포넌트)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 버튼에 컴포넌트로 Audio Source를 추가했기 때문에 GetComponent로 가져오기

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

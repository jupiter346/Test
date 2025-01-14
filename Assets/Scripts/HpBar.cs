using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HpBar : MonoBehaviour
{
    public ICharacter owner;

    //public Image HpBarImage = null;
    public Slider hpbar = null;

    private Camera mainCamera;
    public RectTransform ui_hp_bar;


    private void Initialize()
    {
        IEnumerator UpdateHpBar()
        {
            float prevHp = owner.hp;
            while (true)
            {
                yield return new WaitWhile(() => Mathf.Approximately(prevHp, owner.hp));

                prevHp = owner.hp;

                hpbar.value = prevHp * 0.01f;

                //HpBarImage.fillAmount = prevHp * 0.01f;
            }
        }

        owner = gameObject.GetComponentInParent<ICharacter>();
        //HpBarImage = transform.Find("HpBar").GetComponent<Image>();
        StartCoroutine(UpdateHpBar());
    }

    private void Awake()
    {
        Initialize();

        mainCamera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 내 비행기의 위치가 UI 좌표 어딘가로 위치한다.
        Vector3 screenPos = mainCamera.WorldToScreenPoint(owner.position);
        screenPos.y += 60.0f;
        ui_hp_bar.position = screenPos;
    }
}

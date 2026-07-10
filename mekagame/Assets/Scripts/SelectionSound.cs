using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SelectionSound : MonoBehaviour
{
    [SerializeField] private AudioClip selectSE;

    private AudioSource audioSource;
    private GameObject previous;

    private bool isFirstSelect = true;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log(audioSource);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject current = EventSystem.current.currentSelectedGameObject;

        if (current != previous)
        {
            previous = current;

            if (current == null)
                return;

            Debug.Log(current);

            // 最初の選択時だけSEを鳴らさない
            if (isFirstSelect)
            {
                isFirstSelect = false;
                return;
            }

            audioSource.PlayOneShot(selectSE);

            // マウス操作中は鳴らさない
            /*
            if (Mouse.current != null && Mouse.current.wasUpdatedThisFrame)
                return;
            */
        }
    }
}

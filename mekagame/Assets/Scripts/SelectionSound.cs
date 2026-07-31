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

            // Å‰‚Ì‘I‘ğ‚¾‚¯SE‚ğ–Â‚ç‚³‚È‚¢
            if (isFirstSelect)
            {
                isFirstSelect = false;
                return;
            }

            audioSource.PlayOneShot(selectSE);

        }
    }
}

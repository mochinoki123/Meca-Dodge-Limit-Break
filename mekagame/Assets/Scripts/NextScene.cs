//using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    [SerializeField, Scene] private int _sceneIndex;
    private Button _button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => GetPresenter.Instance.LoadScene(_sceneIndex = null));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

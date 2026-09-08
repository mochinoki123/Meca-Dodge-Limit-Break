using UnityEngine;
using UnityEngine.UI;

public class GaugeColorChange : MonoBehaviour
{
    [SerializeField] private Sprite fillColor1;
    [SerializeField] private Sprite fillColor2;
    [SerializeField] private Sprite fillColor3;

    [SerializeField] private Image gauge1;
    [SerializeField] private Image gauge2;
    [SerializeField] private Image gauge3;

    public void ChangeColor1()
    {
        gauge1.sprite = fillColor1;
        gauge2.sprite = fillColor1;
        gauge3.sprite = fillColor1;
    }
    public void ChangeColor2()
    {
        gauge1.sprite = fillColor2;
        gauge2.sprite = fillColor2;
        gauge3.sprite = fillColor2;
    }
    public void ChangeColor3()
    {
        gauge1.sprite= fillColor3;
        gauge2.sprite= fillColor3;
        gauge3.sprite= fillColor3;
    }
}

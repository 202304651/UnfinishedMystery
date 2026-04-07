using UnityEngine;
using UnityEngine.UI;

public class ToggleKnobMove : MonoBehaviour
{
    public Toggle toggle;
    public RectTransform knob;
    public Image background;

    public Vector2 onPos;
    public Vector2 offPos;

    public Color onColor;
    public Color offColor;

    void Start()
    {
        toggle.onValueChanged.AddListener(UpdateToggle);
        UpdateToggle(toggle.isOn);
    }

    void UpdateToggle(bool isOn)
    {
        knob.anchoredPosition = isOn ? onPos : offPos;
        background.color = isOn ? onColor : offColor;
    }
}
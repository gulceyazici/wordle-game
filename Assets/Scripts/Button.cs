using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public class State
    {
        public Color fillColor;
        public Color outlineColor;
    }
    public State state { get; private set; }
    public static string letter { get; private set; }

    private TextMeshProUGUI text;
    private Image fill;
    private Outline outline;
    private static bool check = false;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
    }
    public void SetState(State state)
    {
        // when we set the state we need to update those colors on our components(image and outline), so we need references of image and outline components
        this.state = state;
        fill.color = state.fillColor;
        outline.effectColor = state.outlineColor;
    }
    public void GetLetter()
    {
        Button button = GetComponent<Button>();
        letter = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        check = true;
    }
    public static string ReturnLetter()
    {
        return letter;
    }
    public static bool CheckIfButtonClicked()
    {
        return check;
    }
    public static void setFalse()
    {
        check = false;
    }
}

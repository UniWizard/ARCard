// ./Assets/Scripts/HelpText.cs

using System.Collections;

using UnityEngine;

using TMPro;


/// <summary>
/// Interface to start animation of HelpText DialogBox
/// </summary>
public class HelpText : MonoBehaviour
{
    // Declaring Variables
    private GameObject button, textArea;
    private Animator aniController;

    /// <summary>
    /// Initialize Variables
    /// </summary>
    private void Awake()
    {
        button = transform.GetChild(0).gameObject;
        textArea = transform.GetChild(0).GetChild(0).gameObject;
        aniController = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Update DialogBox with <c>width</c> and <c>text</c>, then
    /// Start Animation after <c>delay</c> second of delay
    /// </summary>
    public void StartAnimation(float delay, float width, string text)
    {
        StartCoroutine(AnimationCoroutine(delay, width, text));
    }

    /// <summary>
    /// Coroutine for Help Text Animtation
    /// </summary>
    private IEnumerator AnimationCoroutine(float delay, float width, string text)
    {
        yield return new WaitForSeconds(delay);

        button.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 30);
        textArea.GetComponent<TextMeshProUGUI>().text = text;
        aniController.Play("Base Layer.HelpText");
    }
}

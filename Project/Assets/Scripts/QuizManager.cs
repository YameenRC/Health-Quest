using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public Button[] answerButtons; // Assign answer buttons
    public Button correctAnswerButton; // Assign correct answer button 
    public Color correctColor = Color.green; // Color for correct answer
    public Color wrongColor = Color.red; // Color for wrong answer
    public float delayBeforeLoading = 5f; // Time delay for loading scenes
    private bool hasAnswered = false; // To check if an answer has been given

    void Start() // Start is called before the first frame update.
    // Used for component initialization.
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        foreach (Button btn in answerButtons)
        {
            btn.onClick.AddListener(() => Answer(btn));
        }
    }

    private void Answer(Button btn)
    {
        if (hasAnswered) return; // If an answer has already been given, don't do anything
        hasAnswered = true; // Set to true to prevent further answers

        SetButtonColor(btn, btn == correctAnswerButton ? correctColor : wrongColor);

        // Disable all buttons to prevent further interaction
        DisableAllButtons();

        // Start coroutine to handle post-answer behavior
        StartCoroutine(PostAnswerCoroutine(btn == correctAnswerButton));
    }

    private IEnumerator PostAnswerCoroutine(bool isCorrect)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeLoading);

        // Load the next scene if correct, otherwise reload the current scene
        if (isCorrect)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void SetButtonColor(Button btn, Color color)
    {
        var colors = btn.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.pressedColor = color;
        colors.selectedColor = color;
        colors.disabledColor = color; // Ensure the disabled color is also set
        btn.colors = colors;

        // change the color of the button's image component, if it has one:
        Image btnImage = btn.GetComponent<Image>();
        if (btnImage != null)
        {
            btnImage.color = color; // Change the button's image to the any color
        }
    }

    private void DisableAllButtons()
    {
        foreach (Button btn in answerButtons)
        {
            btn.interactable = false; // Make button non-interactable
        }
    }
}

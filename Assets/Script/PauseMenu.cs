using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public Button deleteSaveButton;
    public Button exitButton;

    private void Start()
    {
        panel.SetActive(false);

        deleteSaveButton.onClick.AddListener(OnDeleteSave);
        exitButton.onClick.AddListener(OnExit);
    }

    private void Update()
    {
        if (EscapePressed())
        {
            Toggle();
        }
    }

    private bool EscapePressed()
    {
        bool pressed = Input.GetKeyDown(KeyCode.Escape);

        return pressed;
    }

    public void Toggle()
    {
        bool newState = !panel.activeSelf;
        panel.SetActive(newState);
        GameManager.Instance.SetPaused(newState);
    }

    private void OnDeleteSave()
    {
        GameManager.Instance.DeleteSaveAndResetProgress();
        Toggle();
    }

    private void OnExit()
    {
        GameManager.Instance.SaveGame();

        Application.Quit();
    }
}
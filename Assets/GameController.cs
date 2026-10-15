
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;

    [Header("Game Over UI")]
    public Canvas GameOverCanvas;
    public TMP_Text TimerText;

    [Header("Background")]
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private Sprite[] backgrounds;

    private const string BackgroundIndexKey = "BackgroundIndex";

    private void Awake()
    {
        // Atur background sesuai urutan yang tersimpan
        if (backgroundRenderer != null &&
            backgrounds != null &&
            backgrounds.Length > 0)
        {
            int index = PlayerPrefs.GetInt(BackgroundIndexKey, 0);
            index = Mathf.Clamp(index, 0, backgrounds.Length - 1);

            backgroundRenderer.sprite = backgrounds[index];
        }

        // Pertahankan mekanik Player
        if (PlayerController != null)
        {
            PlayerController.PlayerDied += WhenPlayerDies;
        }

        if (GameOverCanvas != null &&
            GameOverCanvas.gameObject.activeSelf)
        {
            GameOverCanvas.gameObject.SetActive(false);
        }
    }

    private void WhenPlayerDies()
    {
        if (GameOverCanvas != null)
        {
            GameOverCanvas.gameObject.SetActive(true);
        }

        if (TimerText != null)
        {
            TimerText.text =
                "You Lasted: " +
                Math.Round(Time.timeSinceLevelLoad, 2);
        }

        if (PlayerController != null)
        {
            PlayerController.PlayerDied -= WhenPlayerDies;
        }
    }

    public void RetryClicked()
    {
        // Pindah ke background selanjutnya
        if (backgrounds != null && backgrounds.Length > 0)
        {
            int currentIndex =
                PlayerPrefs.GetInt(BackgroundIndexKey, 0);

            int nextIndex =
                (currentIndex + 1) % backgrounds.Length;

            PlayerPrefs.SetInt(BackgroundIndexKey, nextIndex);
            PlayerPrefs.Save();
        }

        // Muat ulang scene seperti sebelumnya
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIGameManager : MonoBehaviour
{
    [Header("Button Setup")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;

    [Header("Movement Buttons")]
    [SerializeField] private Button moveForwardButton;
    [SerializeField] private Button moveBackwardButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;


    [Header("UI Setup")]
    [SerializeField] private TMP_Text greetingText;
    [SerializeField] private PlayerController player;


    public static event Action OnStartButtonPressed;
    public static event Action OnRestartButtonPressed;
    public float speed = 5.0f;


    void Start()
    {
        startButton.onClick.AddListener(OnUIStartButtonPressed);
        restartButton.onClick.AddListener(OnUIRestartButtonPressed);

        SetMovementButtonsActive(false);
        restartButton.gameObject.SetActive(false);
        moveForwardButton.onClick.AddListener(player.MoveForward);
        moveBackwardButton.onClick.AddListener(player.MoveBackward);
        moveLeftButton.onClick.AddListener(player.MoveLeft);
        moveRightButton.onClick.AddListener(player.MoveRight);

    }


    void Update()
    {
        

    }
    void OnUIStartButtonPressed()
    {
        OnStartButtonPressed?.Invoke();
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        SetMovementButtonsActive(true);
        greetingText.gameObject.SetActive(false);
    }
    void OnUIRestartButtonPressed()
    {
        OnRestartButtonPressed?.Invoke();

        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        SetMovementButtonsActive(false);
        greetingText.gameObject.SetActive(true);
    }
    void SetMovementButtonsActive(bool isActive)
    {
        moveForwardButton.gameObject.SetActive(isActive);
        moveBackwardButton.gameObject.SetActive(isActive);
        moveLeftButton.gameObject.SetActive(isActive);
        moveRightButton.gameObject.SetActive(isActive);
    }

}

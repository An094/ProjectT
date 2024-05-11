using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    [SerializeField] private Image IncorrectBackground;
    [SerializeField] private Image CorrectBackground;
    [SerializeField] private Button Button;
    private Image Icon;

    public delegate void CustomButtonClickEvent(CustomButton InButton);
    public event CustomButtonClickEvent OnCustomButtonClickEvent;
    private string animalData;

    private void OnEnable()
    {
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveAllListeners();
    }

    private void Awake()
    {
        Icon = Button.GetComponent<Image>();
    }

    private void Start()
    {
        SetDefautState();
    }
    public void SetIcon(Sprite InSprite)
    {
        Icon.sprite = InSprite;
        Icon.SetNativeSize();        
    }

    public void SetResult(bool IsCorrect)
    {
        IncorrectBackground.enabled = !IsCorrect;
        CorrectBackground.enabled = IsCorrect;
    }
    
    public void SetDefautState()
    {
        IncorrectBackground.enabled = false;
        CorrectBackground.enabled = false;
        Button.interactable = false;
    }
    public void SetAnimalData(string InAnimalData)
    {
        this.animalData = InAnimalData;
    }

    public string GetAnimalData() => this.animalData;

    private void OnButtonClicked()
    {
        OnCustomButtonClickEvent?.Invoke(this);
    }

    public void SetEnable()
    {
        Button.interactable = true;
    }
}

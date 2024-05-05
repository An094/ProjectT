using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TabGroup : MonoBehaviour
{
    public delegate void TabGroupDelegate(int index);
    public event TabGroupDelegate OnTabChanged;

    public List<TabButton> TabButtons;
    private TabButton SelectedTab;
    public Sprite TabIdle;
    public Sprite TabActive;

    private int CurrentTab = 0;

    private void Start()
    {
        if (TabButtons.Count > 0)
        {
            CurrentTab = 0;
            SelectedTab = TabButtons[0];
            SelectedTab.Background.sprite = TabActive;
            SelectedTab.Background.transform.localScale = new Vector2(0.5f, 0.5f);
        }
    }

    public void Subscribe(TabButton button)
    {
        if (TabButtons == null)
        {
            TabButtons = new List<TabButton>();
        }

        TabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(SelectedTab == null || button != SelectedTab)
        {
            button.Background.sprite = TabActive;
            button.Background.transform.localScale = new Vector2(0.45f, 0.45f);
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        SelectedTab = button;
        ResetTabs();
        button.Background.sprite = TabActive;
        button.Background.transform.localScale = new Vector2(0.5f, 0.5f);

        OnTabChanged?.Invoke(button.Id);
    }

    private void ResetTabs()
    {
        foreach (TabButton button in TabButtons)
        {
            if(SelectedTab != null && button == SelectedTab)
            {
                continue;
            }
            button.Background.sprite = TabIdle;
            button.Background.transform.localScale = new Vector2(0.4f, 0.4f);
        }
    }
}

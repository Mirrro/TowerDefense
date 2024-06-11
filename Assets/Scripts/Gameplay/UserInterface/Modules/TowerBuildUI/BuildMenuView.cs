using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuView : MonoBehaviour
{
    [SerializeField] private BuildMenuButton buttonPrefab;
    public event Action<BuildMenuButtonData> ButtonClicked;
    private List<BuildMenuButton> buttons = new ();

    public void ClearButtons()
    {
        foreach (var button in buttons)
        {
            button.Button.onClick.RemoveAllListeners();
            Destroy(button);
        }
    }
    
    public void CreatButton(BuildMenuButtonData data)
    {
        BuildMenuButton btn = Instantiate(buttonPrefab, transform);
        btn.Button.onClick.AddListener(() => HandleButtonClicked(data));
        btn.SetSprite(data.icon);
        btn.SetName(data.name);
        btn.SetPrice(data.price);
        buttons.Add(btn);
    }

    private void HandleButtonClicked(BuildMenuButtonData buildMenuButtonData)
    {
        ButtonClicked?.Invoke(buildMenuButtonData);
    }
}

[Serializable]
public struct BuildMenuButtonData
{
    public Sprite icon;
    public string name;
    public string price;
}
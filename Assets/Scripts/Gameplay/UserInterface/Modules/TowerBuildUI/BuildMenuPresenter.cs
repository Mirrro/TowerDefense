using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class BuildMenuPresenter
{
    private readonly BuildMenuModel model;
    private readonly BuildMenuView view;
    
    public event Action<BuildMenuButtonData> ItemSelected;

    public BuildMenuPresenter(BuildMenuModel model, BuildMenuView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        view.ButtonClicked += HandleButtonClicked;
    }

    private void HandleButtonClicked(BuildMenuButtonData buttonData)
    {
        ItemSelected?.Invoke(buttonData);
    }

    public void SetItems(List<BuildMenuButtonData> buildMenuItems)
    {
        view.ClearButtons();
        model.Items = buildMenuItems;
        foreach (var data in model.Items)
        {
            view.CreatButton(data);
        }
    }
}

public struct BuildMenuItemData
{
    public BuildMenuButtonData ButtonData;
}
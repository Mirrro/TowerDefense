using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BuildMenuView : MonoBehaviour
{
    [SerializeField] private BuildMenuButton buttonPrefab;
    public event Action<BuildMenuButtonData> ButtonClicked;
    private Dictionary<BuildMenuButtonData , BuildMenuButton> dataButtonMap = new ();

    public void ClearButtons()
    {
        foreach (var button in dataButtonMap)
        {
            button.Value.Button.onClick.RemoveAllListeners();
            Destroy(button.Value.gameObject);
        }

        dataButtonMap.Clear();
    }

    public void CreatButton(BuildMenuButtonData data)
    {
        BuildMenuButton btn = Instantiate(buttonPrefab, transform);
        btn.Button.onClick.AddListener(() => HandleButtonClicked(data));
        btn.SetSprite(data.icon);
        btn.SetName(data.name);
        dataButtonMap.Add(data, btn);
    }

    public void RemoveButton(BuildMenuButtonData data)
    {
        if (dataButtonMap.TryGetValue(data, out BuildMenuButton btn))
        {
            Destroy(btn);
            dataButtonMap.Remove(data);
        }
    }

    private void HandleButtonClicked(BuildMenuButtonData buildMenuButtonData)
    {
        ButtonClicked?.Invoke(buildMenuButtonData);
    }
}

[Serializable]
public class BuildMenuButtonData
{
    public Sprite icon;
    public string name;

    public override bool Equals(object obj)
    {
        if (obj is BuildMenuButtonData other)
        {
            return icon == other.icon && name == other.name;
        }
        return false;
    }

    // Override GetHashCode to generate a hash code based on Id and Name
    public override int GetHashCode()
    {
        return HashCode.Combine(Time.realtimeSinceStartup, name, UnityEngine.Random.Range(0,1000));
    }
}
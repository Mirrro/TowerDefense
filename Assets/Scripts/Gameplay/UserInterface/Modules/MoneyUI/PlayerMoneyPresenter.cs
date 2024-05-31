using UnityEngine;

public class PlayerMoneyPresenter
{
    private readonly PlayerBank playerBank;
    private readonly PlayerMoneyView view;
    private readonly PlayerMoneyModel model;

    public PlayerMoneyPresenter(PlayerBank playerBank, PlayerMoneyView view, PlayerMoneyModel model)
    {
        this.playerBank = playerBank;
        this.view = view;
        this.model = model;
    }

    public void Initialize()
    {
        playerBank.CoinsChanged += HandleCoinsChanged;
        HandleCoinsChanged(playerBank.Coins);
    }

    private void HandleCoinsChanged(int obj)
    {
        model.MoneyCount = obj;
        view.UpdateMoneyTextfield(model.MoneyCount.ToString());
    }
}
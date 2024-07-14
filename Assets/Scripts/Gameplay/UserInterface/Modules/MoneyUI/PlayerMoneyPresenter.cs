
public class PlayerMoneyPresenter
{
    private readonly PlayerMoneyView view;
    private readonly PlayerMoneyModel model;
    private readonly PlayerBank playerBank;

    public PlayerMoneyPresenter(PlayerMoneyView view, PlayerMoneyModel model, PlayerBank playerBank)
    {
        this.view = view;
        this.model = model;
        this.playerBank = playerBank;
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
public class UIManager
{
    private readonly UIViewReferences uiViewReferences;
    private readonly PlayerBank playerBank;

    private PlayerMoneyPresenter playerMoneyPresenter;
    
    public UIManager(UIViewReferences uiViewReferences, PlayerBank playerBank)
    {
        this.uiViewReferences = uiViewReferences;
        this.playerBank = playerBank;
    }

    public void Initialize()
    {
        playerMoneyPresenter =
            new PlayerMoneyPresenter(playerBank, uiViewReferences.PlayerMoneyView, new PlayerMoneyModel());
        playerMoneyPresenter.Initialize();
    }
}

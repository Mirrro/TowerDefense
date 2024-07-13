using Zenject;

public class UIManager : IInitializable
{
    [Inject] private readonly PlayerBank playerBank;
    
    private readonly UIViewReferences uiViewReferences;
    private PlayerMoneyPresenter playerMoneyPresenter;
    private EndTurnPresenter endTurnPresenter;
    private BuildMenuPresenter buildMenuPresenter;

    public EndTurnPresenter EndTurnPresenter => endTurnPresenter;
    public BuildMenuPresenter BuildMenuPresenter => buildMenuPresenter;
    
    public UIManager(UIViewReferences uiViewReferences)
    {
        this.uiViewReferences = uiViewReferences;
    }

    public void Initialize()
    {
        playerMoneyPresenter = new PlayerMoneyPresenter(playerBank, uiViewReferences.PlayerMoneyView, new PlayerMoneyModel());
        endTurnPresenter = new EndTurnPresenter(new EndTurnModel(), uiViewReferences.EndTurnView);
        buildMenuPresenter = new BuildMenuPresenter(new BuildMenuModel(), uiViewReferences.BuildMenuView);
        
        playerMoneyPresenter.Initialize();
        endTurnPresenter.Initialize();
        buildMenuPresenter.Initialize();
    }
}

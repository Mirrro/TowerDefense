using Zenject;

public class UIManager : IInitializable
{
    [Inject] private readonly PlayerBank playerBank;
    [Inject] private readonly PlayerHealth playerHealth;
    
    private readonly UIViewReferences uiViewReferences;
    private PlayerMoneyPresenter playerMoneyPresenter;
    private EndTurnPresenter endTurnPresenter;
    private BuildMenuPresenter buildMenuPresenter;
    private PlayerHealthPresenter playerHealthPresenter;

    public EndTurnPresenter EndTurnPresenter => endTurnPresenter;
    public BuildMenuPresenter BuildMenuPresenter => buildMenuPresenter;
    
    public UIManager(UIViewReferences uiViewReferences)
    {
        this.uiViewReferences = uiViewReferences;
    }

    public void Initialize()
    {
        playerMoneyPresenter = new PlayerMoneyPresenter(uiViewReferences.PlayerMoneyView, new PlayerMoneyModel(), playerBank);
        playerHealthPresenter =
            new PlayerHealthPresenter(uiViewReferences.PlayerHealthView, new PlayerHealthModel(), playerHealth);
        endTurnPresenter = new EndTurnPresenter(new EndTurnModel(), uiViewReferences.EndTurnView);
        buildMenuPresenter = new BuildMenuPresenter(new BuildMenuModel(), uiViewReferences.BuildMenuView);

        playerHealthPresenter.Initialize();
        playerMoneyPresenter.Initialize();
        endTurnPresenter.Initialize();
        buildMenuPresenter.Initialize();
    }
}

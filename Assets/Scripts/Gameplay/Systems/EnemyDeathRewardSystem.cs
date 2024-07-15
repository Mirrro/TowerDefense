public class EnemyDeathRewardSystem
{
    private readonly PlayerBank playerBank;
    private readonly EnemyManager enemyManager;

    public EnemyDeathRewardSystem(PlayerBank playerBank, EnemyManager enemyManager)
    {
        this.playerBank = playerBank;
        this.enemyManager = enemyManager;
    }

    public void Activate()
    {
        enemyManager.EnemyDied += HandleEnemyDeath;
    }

    public void Deactivate()
    {
        enemyManager.EnemyDied -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(EnemyPresenter obj)
    {
        playerBank.AddMoney(25);
    }
}
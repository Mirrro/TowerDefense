using System.Threading;
using Cysharp.Threading.Tasks;

public interface IGameplayTask
{
    public UniTask Execute(CancellationToken cancellationToken);
}
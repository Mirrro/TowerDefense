using System;
using System.Collections.Concurrent;
using Cysharp.Threading.Tasks;

namespace Gameplay.Systems
{
    public class TaskQueue
    {
        private ConcurrentQueue<(Func<UniTask>, Action)> taskQueue = new ();
        private bool isProcessing = false;

        public void EnqueueTask(Func<UniTask> task, Action callback = null)
        {
            taskQueue.Enqueue((task, callback));
            if (!isProcessing)
            {
                ProcessNextTask().Forget();
            }
        }

        private async UniTaskVoid ProcessNextTask()
        {
            isProcessing = true;
            while (taskQueue.TryDequeue(out var taskWithCallback))
            {
                var (task, callback) = taskWithCallback;
                await task();
                callback?.Invoke();
            }
            isProcessing = false;
        }
    }
}
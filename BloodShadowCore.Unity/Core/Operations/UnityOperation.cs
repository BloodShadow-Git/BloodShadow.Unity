using BloodShadow.Core.Operations;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace BloodShadow.Unity.Core.Operations
{
    public class UnityOperation : Operation
    {
        public override float Progress => Operation.progress;
        public override bool AllowSceneActivation
        {
            get => Operation.allowSceneActivation;
            set => Operation.allowSceneActivation = value;
        }
        public override bool IsDone => Operation.isDone;
        public override int Priority { get => Operation.priority; set => Operation.priority = value; }

        public AsyncOperation Operation { get; private set; }
        private CancellationTokenSource _source;

        public UnityOperation(AsyncOperation operation)
        {
            Operation = operation;
            _source = new CancellationTokenSource();

            _ = WaitingForDone();
        }

        private async Task WaitingForDone()
        {
            while (!_source.IsCancellationRequested)
            {
                if (Operation.isDone)
                {
                    OnCompletedAction?.Invoke();
                    return;
                }
                else { await Task.Yield(); }
            }
        }

        public override void Dispose() { _source.Cancel(); }

        public static implicit operator UnityOperation(AsyncOperation operation) => new UnityOperation(operation);
        public static implicit operator AsyncOperation(UnityOperation operation) => operation.Operation;
    }
}

using System;
using System.Threading.Tasks;

namespace ATH
{
    public class DelegateCommand : Command
    {
        private readonly Action _action;

        public override bool IsRunning { get; protected set; }

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public override Task Run()
        {
            _action?.Invoke();
            return Task.CompletedTask;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATH
{
    public class ParallelCommand : Command
    {
        private List<Command> _commands = new();

        public override bool IsRunning { get; protected set; }

        public override async Task Run()
        {
            var runningCommands = new List<Task>();

            IsRunning = true;
            foreach (var command in _commands)
            {
                runningCommands.Add(command.Run());
            }
            await Task.WhenAll(runningCommands);
            IsRunning = false;
        }

        public ParallelCommand Add(Command command)
        {
            _commands.Add(command);
            return this;
        }
    }
}

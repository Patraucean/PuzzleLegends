using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ATH
{
    public class SequentialCommand : Command
    {
        private List<Command> _commands = new();

        public override bool IsRunning { get; protected set; }

        public override async Task Run()
        {
            IsRunning = true;
            
            try
            {
                foreach (var command in _commands)
                {
                    await command.Run();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            IsRunning = false;
        }

        public SequentialCommand Add(Command command)
        {
            if (command != null)
            {
                _commands.Add(command);
            }

            return this;
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            for (int i = 0; i < _commands.Count; i++)
            {
                str.Append($"[{i}] {_commands[i].GetType()}\n");
            }

            return str.ToString();
        }
    }
}

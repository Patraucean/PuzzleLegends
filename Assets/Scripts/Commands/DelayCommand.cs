using System.Threading.Tasks;
using UnityEngine;

namespace ATH
{
    public class DelayCommand : Command
    {
        private readonly float _delay;

        public override bool IsRunning { get; protected set; }

        public DelayCommand(float delay)
        {
            _delay = delay;
        }

        public override async Task Run()
        {
            await Awaitable.WaitForSecondsAsync(_delay);
        }
    }
}

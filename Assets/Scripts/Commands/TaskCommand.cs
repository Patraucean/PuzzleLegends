using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ATH
{
    public class TaskCommand : Command
    {
        private readonly Func<Task> _task;

        public override bool IsRunning { get; protected set; }

        public TaskCommand(Func<Task> task)
        {
            _task = task;
        }

        public override async Task Run()
        {
            try
            {
                await _task();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}

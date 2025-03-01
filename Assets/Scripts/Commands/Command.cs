using System.Threading.Tasks;

namespace ATH
{
    public abstract class Command
    {
        public abstract bool IsRunning { get; protected set; }
        public abstract Task Run();
    }
}

namespace Runtime.Timer
{
    public interface IGameTimerService
    {
        float ElapsedTime { get; }
        bool IsRunning { get; }

        void Start();
        void Stop();
        void Reset();
        void Tick(float deltaTime);
    }
}
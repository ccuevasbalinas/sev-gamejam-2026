namespace Runtime.Timer
{
    public class GameTimerService : IGameTimerService
    {
        public float ElapsedTime { get; private set; }
        public bool IsRunning { get; private set; }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Reset()
        {
            ElapsedTime = 0f;
            IsRunning = false;
        }

        public void Tick(float deltaTime)
        {
            if (!IsRunning)
                return;

            ElapsedTime += deltaTime;
        }
    }
}
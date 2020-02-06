namespace Simfluencer.FSM {
    public interface IState {
        void Start();
        void Run();
        void Complete();
    }
}

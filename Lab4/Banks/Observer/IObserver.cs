namespace Banks.Observer
{
    public interface IObserver
    {
        void Update(IObservable observable);
    }
}
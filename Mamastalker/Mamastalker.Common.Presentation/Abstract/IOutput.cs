namespace PingPong.Common.Presentation.Abstract
{
    public interface IOutput<TOut>
    {
        void Output(TOut output);
    }
}

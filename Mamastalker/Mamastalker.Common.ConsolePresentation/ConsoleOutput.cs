using PingPong.Common.Presentation.Abstract;
using System;

namespace PingPong.Common.ConsolePresentation
{
    public class ConsoleOutput<TOut> : IOutput<TOut>
    {
        public void Output(TOut output)
        {
            Console.WriteLine(output);
        }
    }
}

using Mamastalker.Common.Presentation.Abstract;
using System;

namespace Mamastalker.Common.ConsolePresentation
{
    public class ConsoleOutput<TOut> : IOutput<TOut>
    {
        public void Output(TOut output)
        {
            Console.WriteLine(output);
        }
    }
}

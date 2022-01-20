namespace Mamastalker.Server.Logic.DataParsers.Abstract
{
    public interface IDataParser<TParseFrom, TParseTo>
    {
        TParseTo Parse(TParseFrom data);
    }
}

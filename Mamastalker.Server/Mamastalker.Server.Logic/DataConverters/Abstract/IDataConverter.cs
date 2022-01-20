namespace Mamastalker.Server.Logic.DataConverters.Abstract
{
    public interface IDataConverter<TParseFrom, TParseTo>
    {
        TParseTo Parse(TParseFrom data);
    }
}

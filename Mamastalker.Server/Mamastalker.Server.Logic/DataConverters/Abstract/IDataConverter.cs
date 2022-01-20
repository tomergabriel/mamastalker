namespace Mamastalker.Server.Logic.DataConverter.Abstract
{
    public interface IDataConverter<TParseFrom, TParseTo>
    {
        TParseTo Parse(TParseFrom data);
    }
}

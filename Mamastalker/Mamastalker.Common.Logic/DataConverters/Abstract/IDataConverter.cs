namespace PingPong.Common.Logic.DataConverters.Abstract
{
    public interface IDataConverter<TConvertFrom, TConvertTo>
    {
        TConvertTo Parse(TConvertFrom data);
    }
}

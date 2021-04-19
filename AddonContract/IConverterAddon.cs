namespace AddonContract
{
    public interface IConverterAddon
    {
        bool CanConvert(string fromTypeAsString);
        decimal Convert(decimal fromValue);
    }
}

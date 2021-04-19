using System.ComponentModel.Composition;
using AddonContract;

namespace FrancsToEuro
{
    [Export(typeof(IConverterAddon))]
    public class FrancsToEur : ICurrencyToEurConverter
    {
        private const decimal ConversionRate = 0.9068m;
        public bool CanConvert(string fromTypeAsString)
        {
            if (fromTypeAsString.Equals("F"))
            {
                return true;
            }

            return false;
        }


        public decimal Convert(decimal fromValue)
        {
            return fromValue * ConversionRate;
        }
    }
}

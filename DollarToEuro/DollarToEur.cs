using System.ComponentModel.Composition;
using AddonContract;

namespace DollarToEuro
{
    [Export(typeof(IConverterAddon))]
    public class DollarToEur : ICurrencyToEurConverter
    {
        private const decimal ConversionRate = 0.8343m;
        public bool CanConvert(string fromTypeAsString)
        {
            if (fromTypeAsString.Equals("D"))
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

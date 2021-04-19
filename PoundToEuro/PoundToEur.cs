using System.ComponentModel.Composition;
using AddonContract;

namespace PoundToEuro
{
    [Export(typeof(IConverterAddon))]
    public class PoundToEur : ICurrencyToEurConverter
    {
        private const decimal ConversionRate = 1.1622421m;
        public bool CanConvert(string fromTypeAsString)
        {
            if (fromTypeAsString.Equals("P"))
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

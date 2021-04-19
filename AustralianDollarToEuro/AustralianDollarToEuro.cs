using System.ComponentModel.Composition;
using AddonContract;

namespace AustralianDollarToEuro
{
    [Export(typeof(IConverterAddon))]
    public class AustralianDollarToEuro : ICurrencyToEurConverter
    {
        private const decimal ConversionRate = 0.6448m;
        public bool CanConvert(string fromTypeAsString)
        {
            if (fromTypeAsString.Equals("A"))
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

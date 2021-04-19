using System.ComponentModel.Composition;
using AddonContract;

namespace FootToCentimeter
{
    [Export(typeof(IConverterAddon))]
    public class FootToCentimeter : ILengthConverter
    {
        private const decimal ConversionRate = 30.48m;
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

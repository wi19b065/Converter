using System.ComponentModel.Composition;
using AddonContract;

namespace InchToCentimeter
{
    [Export(typeof(IConverterAddon))]
    public class InchToCentimeter : ILengthConverter
    {
        private const decimal ConversionRate = 2.54m;
        public bool CanConvert(string fromTypeAsString)
        {
            if (fromTypeAsString.Equals("I"))
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

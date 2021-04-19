using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using AddonContract;

namespace TheConverter
{
    class Program
    {
        private const string AddonDirName = "./addons";

        [ImportMany(typeof(IConverterAddon))]
        private List<Lazy<IConverterAddon>> _converterAddons;

        static void Main(string[] args)
        {
            var program = new Program();
            Console.WriteLine("Load Plugins:");
            program.LoadAddons();
            program.ReadInput();
            
        }

        void LoadAddons()
        {
            if (!Directory.Exists(AddonDirName))
            {
                Console.WriteLine("Addon directory does not exist. Aborting.");
                return;
            }
            
            var directoryCatalog = new DirectoryCatalog(AddonDirName);
            var compositionContainer = new CompositionContainer(directoryCatalog);
            compositionContainer.ComposeParts(this);

            if (_converterAddons == null)
            {
                Console.WriteLine("Addon directory is empty. Aborting.");
                return;
            }

            foreach (Lazy<IConverterAddon> converterAddon in _converterAddons)
            {
                Console.WriteLine($"Loaded {converterAddon.Value.ToString().Split('.')[0]}.");
            }

            Console.WriteLine("\n");

        }

        void ReadInput()
        {
            string input = string.Empty;

            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("This Program converts...\n");
                Console.WriteLine("Usage: <ConversionType> <Value> <ConversionUnit>\n" +
                                  "<ConversionType> => G - for LengthConversion\n" +
                                  "                 => W - for CurrencyConversion\n" +
                                  "<Value> => decimal in your input format (use . or , as comma)\n" +
                                  "<ConversionUnit> First character of loaded plugin.\"" +
                                  "Example: 'W 500 P' without '' converts 500 pound to EUR\n" );

                Console.Write("Please Input Conversion command: ");


                input = Console.ReadLine();
                if(string.IsNullOrEmpty(input)) continue;

                string[] inputArray = input?.Split(' ');
                if (inputArray.Length != 3)
                {
                    input = string.Empty;
                    continue;
                }
                else
                {
                    var addonTypeString = inputArray[0];
                    Type addonType;
                    decimal valueToConvert;

                    decimal.TryParse(inputArray[1], out valueToConvert); 

                    switch (addonTypeString)
                    {
                        case "W":
                            addonType = typeof(ICurrencyToEurConverter);
                            break;
                        case "G":
                            addonType = typeof(ILengthConverter);
                            break;
                        default:
                            return;
                    }

                    bool converted = false;
                    foreach (Lazy<IConverterAddon> converterAddon in _converterAddons)  
                    {
                        if (converterAddon.Value.GetType().GetInterfaces().Contains(addonType)
                        && converterAddon.Value.CanConvert(inputArray[2]))
                        {
                            Console.WriteLine($"Converted Value: {converterAddon.Value.Convert(valueToConvert)}");
                            converted = true;
                        }
                    }
                    if(!converted)
                        Console.WriteLine("\nConversion missing please send a mail to waller@technikum-wien.at - press any key to restart.\n");
                    Console.ReadKey();

                    input = null;
                }

            }
        }
    }
}

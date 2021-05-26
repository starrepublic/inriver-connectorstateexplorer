using System;
using System.Globalization;
using System.Windows.Data;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.Converter
{
    public class TextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (TryGetJson(value, out var result, true))
                return result;

            if (TryGetXml(value, out result, true))
                return result;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (TryGetJson(value, out var result, false))
                return result;

            if (TryGetXml(value, out result, false))
                return result;

            return value;
        }

        private bool TryGetJson(object value, out string result, bool prettyPrint)
        {
            result = string.Empty;

            try
            {
                dynamic parsedJson = JsonConvert.DeserializeObject((string)value);
                if (prettyPrint)
                    result = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                else
                    result = JsonConvert.SerializeObject(parsedJson, Formatting.None);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryGetXml(object value, out string result, bool prettyPrint)
        {
            result = string.Empty;

            try
            {
                var document = XDocument.Parse((string)value);
                if (prettyPrint)
                    result = document.ToString();
                else
                    result = document.ToString(SaveOptions.DisableFormatting);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    internal  static class Tools
    {
        public static string ToStringProperty(object obj)
        {
            var properties = obj.GetType().GetProperties();
            var stringBuilder = new StringBuilder();

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                if (value is not null)
                {
                    if (value is IEnumerable enumerable)
                    {
                        stringBuilder.Append($"{property.Name}: {enumerable.ToString()}\n");
                    }
                    else
                    {
                        stringBuilder.Append($"{property.Name}: {value}\n");
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}

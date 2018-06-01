using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGamesLibrary
{
    public class Serialization
    {
        public static string Serialize<TInput>(TInput input)
        {
            string output;
            try
            {
                output = JsonConvert.SerializeObject(input);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                output = null;
            }
            return output;
        }

        public static T Deserialize<T> (string input)
        {
            T output;
            try
            {
                output = JsonConvert.DeserializeObject<T>(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                output = default(T);
            }
            return output;
        }
    }
}

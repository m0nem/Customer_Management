using Newtonsoft.Json.Linq;

namespace Customer_Management.MVC.Utilities
{
    public static class Utility
    {
       public static string ExtractErrors(string responseString)
        {
            string errorString = "";

            // Find the start and end index of JSON content
            int startIndex = responseString.IndexOf('{');
            int endIndex = responseString.LastIndexOf('}');

            // Extract JSON content
            string jsonContent = responseString.Substring(startIndex, endIndex - startIndex + 1);

            // Parse the JSON content
            var jsonObject = JObject.Parse(jsonContent);

            // Extract errors
            if (jsonObject["errors"] != null)
            {
                var errorObject = jsonObject["errors"] as JObject;
                foreach (var error in errorObject)
                {
                    foreach (var errorMessage in error.Value)
                    {
                        errorString += errorMessage + "\n";
                    }
                }
            }

            return errorString;
        }

    }
}

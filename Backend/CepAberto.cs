using Newtonsoft.Json.Linq;
using SharpControls.APIHandler;
using System.Text.RegularExpressions;

namespace SharpControls.AutoAddress.Backend
{
    public partial class CepAberto
    {
        public static string? Token { private get; set; }

        public async static Task<Response> fromCep(string cep, string? token = null)
        {
            Regex OnlyNumbers = OnlyNumberRegex();
            cep = OnlyNumbers.Replace(cep, "");
            if (Utils.General.CountNumbers(int.Parse(cep)) != 8)
            {
                return new() { Content = null, StatusText = "invalid" };
            }
            token ??= Token;
            if (token == null)
            {
                return new() { Content = null, StatusText = "token_not_set" };
            }
            APIHandler.REST.RestConnection.Init(false);
            APIHandler.REST.RestConnection.SetDefaultHeader("Authorization", "Token token=" + token);
            APIHandler.REST.RestConnection.SetDefaultHeader("Accept", "application/json");
            APIHandler.REST.RestConnection.SetDefaultHeader("Content-Type", "application/json");
            try
            {
                JObject response = await APIHandler.REST.RestConnection.SendRequest(HttpMethod.Post, "https://www.cepaberto.com/api/v3/cep?cep=" + cep, new RequestContent());
                if (!response.ContainsKey("cidade"))
                {
                    return new() { Content = null, StatusText = "invalid" };
                }
                return new() { Content = response, StatusText = "ok" };
            }
            catch (Exception ex)
            {
                //TODO: Add more specific exception handling
                throw ex;
            }
        }

        [GeneratedRegex(@"[^0-9]", RegexOptions.IgnoreCase)]
        private static partial Regex OnlyNumberRegex();
    }
}

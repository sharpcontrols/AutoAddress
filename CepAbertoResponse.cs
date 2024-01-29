using SharpControls.AutoAddress.Backend;

namespace SharpControls.AutoAddress
{
    public class CepAbertoResponse
    {
        public string Status { get; private set; }
        public string Cep { get; private set; }
        public string? Ibge { get; private set; }
        public string? Ddd {  get; private set; }
        public string? Street {  get; private set; }
        public string? Complement {  get; private set; }
        public string? Area {  get; private set; }
        public string? City { get; private set; }
        public string? State {  get; private set; }
        public string? Country { get; private set; }
        public string? Longitude {  get; private set; }
        public string? Latitude {  get; private set; }
        public string? Altitude {  get; private set; }

        public event EventHandler<EventArgs> OnDataFetched;

        public CepAbertoResponse(string cep, string? token = null)
        {
            Fetch(cep, token);
        }

        private async void Fetch(string cep, string? token = null)
        {
            var response = await CepAberto.fromCep(cep, token);
            var status = response.StatusText;
            if (status != "ok")
            {
                throw new Exception("Status from Response is \"" + status + "\"");
            }
            var content = response.Content!;
            if (content["cidade"] != null)
            {
                Ibge = content["cidade"]!.Value<string>("ibge");
                Ddd = content["cidade"]!.Value<string>("ddd");
                City = content["cidade"]!.Value<string>("nome");
            }
            Street = content.Value<string>("logradouro");
            Complement = content.Value<string>("complemento");
            Area = content.Value<string>("bairro");
            if (content["estado"] != null)
            {
                State = content["estado"]!.Value<string>("sigla");
            }
            Longitude = content.Value<string>("longitude");
            Latitude = content.Value<string>("latitude");
            Altitude = content.Value<string>("altitude");
            Cep = cep;

            OnDataFetched?.Invoke(this, new());
        }

        public bool IsValid()
        {
            return Status == "ok";
        }
    }
}

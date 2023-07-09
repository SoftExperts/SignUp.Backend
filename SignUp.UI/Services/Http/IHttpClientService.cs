namespace SignUp.UI.Services
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> PostAsync<T>(T model, string path) where T : class;
        Task<T> DeserializeAsync<T>(HttpResponseMessage response);
        string Serialize<T>(T model);
    }
}
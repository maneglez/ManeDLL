namespace Mane.BD.BaseDeDatos.Executors.WebApiExecutor
{
    public class WebApiToken
    {
        public WebApiToken(string connectionName, string token)
        {
            ConnectionName = connectionName;
            Token = token;
        }

        public string ConnectionName { get; set; }
        public string Token { get; set; }

    }
}

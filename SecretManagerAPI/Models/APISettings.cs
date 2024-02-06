namespace SecretManagerAPI.Models
{
    public interface IAPISettings
    {
        public Dictionary<string, string> DBConnections { get; set; }
        public Settings Settings { get; set; }
        public string SandboxDB { get; }
    }
    public class APISettings : IAPISettings
    {
        public Dictionary<string, string> DBConnections { get; set; }

        public Settings Settings { get; set; }

        public string SandboxDB
        {
            get
            {
                return DBConnections.FirstOrDefault(x => x.Key.ToLower() == "sandboxdb").Value;
            }
        }
    }

    public class Settings
    {
        public int Timeout { get; set; }

        public string? TopPlayers { get; set; }
    }
}

namespace DeletedUserTool;

public class MySqlSettings
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public uint Port { get; set; } = 3306;
    public string Database { get; set; }
}
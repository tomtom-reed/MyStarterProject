using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Config;

public class DatabaseSettings
{
    public string Host { get; set; } = "localhost";
    public string Port { get; set; } = "5432";
    public string UserName { get; set; } = "postgres";
    public string Password { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = "starterproject";
    public bool UseSSL { get; set; } = false;

    public string GetConnectionString()
    {
        var connectionString = $"Host={Host};Port={Port};Username={UserName};Database=\"{DatabaseName}\";";
        // Could use FullDatabaseName 
        return connectionString;
    }
}
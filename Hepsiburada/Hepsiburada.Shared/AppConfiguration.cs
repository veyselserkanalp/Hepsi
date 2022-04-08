namespace Hepsiburada.Shared
{
    public class AppConfiguration
    {
        public RabbitMQSettings RabbitMQSetting { get; set; }
        public LogServerSettings LogServerSetting { get; set; }
        public RedisSettings RedisSetting { get; set; }
        public AppSettings App { get; set; }
        public string MailList { get; set; }
        public string InfoMailList { get; set; }
        public PressClipperSettings PressClipperSettings { get; set; }
        public PushNotificationSettings PushNotificationSettings { get; set; }

        public SqlServerSettings SqlServerSettings { get; set; }
        public MongoServerSettings MongoServerSettings { get; set; }
        public MailSettings MailSettings { get; set; }
    }

    public class MailSettings
    {
        public string MailUserName { get; set; }
        public string MailPassword { get; set; }
        public string PathUrl { get; set; }
        public string ServiceUrl { get; set; }
        public string MailAddress { get; set; }
        public string TemplateFile { get; set; }
        public string ReportFile { get; set; }
        public string WeeklyReportFile { get; set; }
        public string WeeklyMailAddress { get; set; }
    }

    public class RedisSettings
    {
        public string HostUrl { get; set; }
        public string InstanceName { get; set; }
        public int DbName { get; set; }
        public int ExpireDate { get; set; }
    }
    public class SqlServerSettings
    {
        public string ConnectionString { get; set; }

        public string LocalConnectionString { get; set; }
    }

    public class MongoServerSettings
    {
        public string ConnectionString { get; set; }

        public string LocalConnectionString { get; set; }
    }


    public class LogServerSettings
    {
        public SolrLogServerSettings SolrLogServer { get; set; }

        public class SolrLogServerSettings
        {
            public string SolrUrl { get; set; }
        }
    }


    public class RabbitMQSettings
    {
        public string ConnectionString { get; set; }
    }
    public class SignalrSettings
    {
        public string ConnectionString { get; set; }
        public string LocalConnectionString { get; set; }
    }

    public class AppSettings
    {
        public string ServerRootAddress { get; set; }
        public string ClientRootAddress { get; set; }
    }

    public class PressClipperSettings
    {
        public string Uri { get; set; }
        public string SesId { get; set; }
    }

    public class PushNotificationSettings
    {
        public string Server_Api_Key { get; set; }
        public string Sender_ID { get; set; }
    }
    public class DocumentSettings
    {
        public string Path { get; set; }

        public string ProfilePicturePath { get; set; }
    }



    public class SessionSettings
    {
        public int Timer { get; set; }
    }

    public class SecuredContactSetting
    {
        public string SignutureKey { get; set; }

    }
}

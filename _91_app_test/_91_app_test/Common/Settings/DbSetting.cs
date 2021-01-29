using System;
namespace _91_app_test.Common.Settings
{
    public class DbSetting
    {
        public string HOSTNAME { get; set; }
        public string PORT { get; set; }
        public string DB_NAME { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public int MAX_CONN { get; set; }
        public int MIN_CONN { get; set; }
    }
}

using System;
namespace _91_app_test.Models
{
    public class DatabaseObject
    {
        public DatabaseObject(string connStr, string sqlStatement, object data)
        {
            this.connStr = connStr;
            this.sqlStatement = sqlStatement;
            this.data = data;
        }
        public string connStr { get; set; }
        public string sqlStatement { get; set; }
        public object data { get; set; }

    }
}

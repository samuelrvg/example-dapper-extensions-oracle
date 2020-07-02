using System;
using Dapper;
using DapperExtensions.Oracle;
using Oracle.ManagedDataAccess.Client;

namespace ExampleDapperExtensionsOracle
{
    //[Table(TableName = "dbo.People", KeyName = "PeopleId", SequenceName = "SEQ_PEOPLE", IsIdentity = false)]
    [Table(TableName = "People", KeyName = "PeopleId", SequenceName = "SEQ_PEOPLE", IsIdentity = false)]
    public class People
    {
        public int PeopleId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var people = new People();
            people.Name = "Samuel";
            people.Age = 30;

            using (var con = new OracleConnection("your connection string"))
            {
                con.Insert(people);
                con.Update(people);
                var result = con.Query("select * from people where peopleid = 1");
                con.Delete<People>(1);

                //......
            }
            Console.WriteLine("Hello World!");
        }
    }
}

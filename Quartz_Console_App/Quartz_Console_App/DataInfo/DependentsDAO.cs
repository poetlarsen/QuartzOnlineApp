using Dapper;
using Quartz_Console_App.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Quartz_Console_App.DataInfo
{
    class DependentsDAO
    {

        public IList<DependentsInfo> GetDepedents(string Customer_ID)
        {
            IList<DependentsInfo> Dependents = new List<DependentsInfo>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quartz_Console_App"].ConnectionString))
            {
                connection.Open();

                string sql = @"SELECT [Dependent_ID]
                                     ,[SSN]
                                     ,[FirstName]
                                     ,[Customer_ID]
                                     ,[LastName]
                                     ,[Zip]
                                     ,[DOB]
                                     ,[Dependent_Type]
                                 FROM [2018FA422_TeamB].[dbo].[Dependents]
                                 WHERE Customer_ID LIKE @Customer_ID;";
                Dependents = connection.Query<DependentsInfo>(sql, new { Customer_ID }).AsList();

                connection.Close();
            }
            return Dependents;
        }
        public int AddNewDependent(DependentsInfo New_Dependent)
        {
            int complete = 0;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quartz_Console_App"].ConnectionString))
            {
                connection.Open();
                string sql = @"   INSERT INTO Dependents 
                                      (Customer_ID, SSN, FirstName,LastName,Zip,DOB,Dependent_Type)
                                      VALUES(@Customer_ID, @SSN, @FirstName, @LastName,@Zip, @DOB, @Dependent_Type);
                                    
                                       SELECT CONVERT(int, SCOPE_IDENTITY());";
                complete = (int)connection.ExecuteScalar(sql, New_Dependent);
                connection.Close();
            }
            return complete;
        }


    }
}

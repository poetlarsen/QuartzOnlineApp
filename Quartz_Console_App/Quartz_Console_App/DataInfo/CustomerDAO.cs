using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

//using System.Collections.Generic;
//using System.Configuration;
using System.Data.SqlClient;
using Quartz_Console_App.Classes;

namespace Quartz_Console_App.DataInfo
{
    class CustomerDAO
    {
       // public object ConfigurationManager { get; private set; }

        public IList<CustomerInfo> getCustomersInfo()
        {
            IList<CustomerInfo> customers;
            using (var connection = new SqlConnection( ConfigurationManager.ConnectionStrings["Quartz_Console_App"].ConnectionString))
            {
                connection.Open();
                string sql = @"SELECT [Customer_ID]
                                     ,[SSN]
                                     ,[FirstName]
                                     ,[LastName]
                                     ,[Address]
                                     ,[City]
                                     ,[State]
                                     ,[Zip]
                                     ,[DOB]
                                 FROM [2018FA422_TeamB].[dbo].[Customers]";
                customers = connection.Query<CustomerInfo>(sql).AsList();
                connection.Close();
                return customers;
            }
        }
        public IList<DependentsInfo> getDependents()
        {
            IList<DependentsInfo> dependents;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quartz_Console_App"].ConnectionString))
            {
                connection.Open();
                string sql = @"SELECT [Dependent_ID]
                                     ,[Customer_ID]
                                     ,[SSN]
                                     ,[FirstName]
                                     ,[LastName]
                                     ,[Zip]
                                     ,[DOB]
                                     ,[Dependent_Type]
                                 FROM [2018FA422_TeamB].[dbo].[Dependents]";
                dependents = connection.Query<DependentsInfo>(sql).AsList();
                connection.Close();
                return dependents;
            }
            //return dependents;
        }

        public IList<CustomerInfo> GetCustomer(string Customer_ID)
        {
            IList<CustomerInfo> customers;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quartz_Console_App"].ConnectionString))
            {
                connection.Open();
                string sql = @"SELECT [Customer_ID]
                                     ,[SSN]
                                     ,[FirstName]
                                     ,[LastName]
                                     ,[Address]
                                     ,[City]
                                     ,[State]
                                     ,[Zip]
                                     ,[DOB]
                                 FROM [2018FA422_TeamB].[dbo].[Customers]
                                    WHERE Customer_ID LIKE @Customer_ID";
                customers = connection.Query<CustomerInfo>(sql, new {Customer_ID }).AsList();
                connection.Close();
                return customers;
            }
        }

        public int AddNewCustomer(CustomerInfo New_Cust)
        {
            int complete = 0;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quartz_Console_App"].ConnectionString))
            {
                connection.Open();
                string sql = @" INSERT INTO Customers ([SSN]
                                  ,[FirstName]
                                  ,[LastName]
                                  ,[Address]
                                  ,[City]
                                  ,[State]
                                  ,[Zip]
                                  ,[DOB])
	                              VALUES (@SSN, @FirstName, @LastName, @Address, @City, @State, @Zip, @DOB);
                                    
                                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
                complete = (int)connection.ExecuteScalar(sql, New_Cust);
                connection.Close();
            }
            return complete;
        }

    }
}

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
    class CustomerInvoiceDAO
    {
        public int NewPurchase(CustomerInfo customer, InsurancePlansInfo Chosen_Plan, double cost)
        {
            CustomerInvoiceInfo InsertingCustInfo = new CustomerInvoiceInfo();
           
            DateTime today = DateTime.Today;
           // DateTime dateOnly;
            var nextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);
            var nextYear = new DateTime(nextMonth.Year, nextMonth.Month, 1).AddYears(1);

            InsertingCustInfo.Customer_ID = customer.Customer_ID;
            InsertingCustInfo.Paid_Amount = cost;
            InsertingCustInfo.Coverage_Start_Date = nextMonth;
            InsertingCustInfo.Coverage_End_Date = nextYear;
            InsertingCustInfo.Paid_Date_Time = today;
            InsertingCustInfo.Plan_ID = Chosen_Plan.Plan_ID;
            InsertingCustInfo.Plan_Rating_Area = Chosen_Plan.Plan_Rating_Area;
            InsertingCustInfo.Plan_Age = Chosen_Plan.Plan_Age;
            int complete = 0;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quartz_Console_App"].ConnectionString))
            {
                connection.Open();
                string sql = @"        INSERT INTO [2018FA422_TeamB].[dbo].[Customer_Invoice]
                                        (Customer_ID,Paid_Amount,Coverage_Start_Date,Coverage_End_Date,Paid_Date_Time, Plan_ID,Plan_Age,Plan_Rating_Area)
                                         VALUES(@Customer_ID,@Paid_Amount,@Coverage_Start_Date,@Coverage_End_Date,@Paid_Date_Time, @Plan_ID,@Plan_Age,@Plan_Rating_Area);

                                    SELECT CONVERT(int, SCOPE_IDENTITY());";

                complete = (Int32)connection.ExecuteScalar(sql, InsertingCustInfo);
                //connection.Execute(sql, InsertingCustInfo);
                connection.Close();
                //complete = "complete";
            }
            if(complete > 0)
            {
                return complete;
            }
            else
            {
                return 0;
            }
            
        }
        public IList<CustomerInvoiceInfo> GetInvoiceHistory(string Customer_ID)
        {
            IList<CustomerInvoiceInfo> All_Invoices = new List<CustomerInvoiceInfo>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quartz_Console_App"].ConnectionString))
            {
                connection.Open();
                string sql = @"        SELECT         [Invoice_ID]
                                                     ,[Customer_ID]
                                                     ,[Paid_Amount]
                                                     ,[Coverage_Start_Date]
                                                     ,[Coverage_End_Date]
                                                     ,[Paid_Date_Time]
                                                     ,[Plan_ID]
                                                     ,[Plan_Age]
                                                     ,[Plan_Rating_Area]
                                                 FROM [2018FA422_TeamB].[dbo].[Customer_Invoice]
                                                 WHERE Customer_ID LIKE @Customer_ID;";

                //complete = (Int32)connection.ExecuteScalar(sql, Customer_ID);
                All_Invoices = connection.Query<CustomerInvoiceInfo>(sql, new { Customer_ID }).AsList();
                connection.Close();
            }
            return All_Invoices;
        }
    }
}

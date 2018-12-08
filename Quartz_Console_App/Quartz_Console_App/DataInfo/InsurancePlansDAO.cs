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
using System.Data;

namespace Quartz_Console_App.DataInfo
{
    class InsurancesPlansDAO
    {
        // public object ConfigurationManager { get; private set; }

        public IList<InsurancePlansInfo> getAllPlanInfo(int rating_area)
        {
            IList<InsurancePlansInfo> Insurance_Plans;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quartz_Console_App"].ConnectionString))
            {
                connection.Open();
                string sql = @"
                                    SELECT [Plan_ID]
                                    ,[Plan_Age]
                                    ,[Plan_Rating_Area]
                                    ,[Metal_Tier]
                                    ,[Co_Insurance]
                                    ,[Premium]
                                    ,[Plan_type]
                                    ,[Deductible]
                                    ,[Max_Out_of_Pocket]
                                    ,[Office_Visit]
                                    ,[Specialist_Visit]
                                    ,[Urgent_care]
                                    ,[Emergency_Room]
                                    ,[Inpatient_Services]
                                    ,[Outpatient_Services]
                                    ,[Lab]
                                    ,[X_Ray]
                                    ,[MRI]
                                    ,[Dental_Coverage]
                                    ,[Pharmacy_Co_Pay]
                                    FROM [2018FA422_TeamB].[dbo].[Insurance_Plans]
                                    WHERE Plan_Rating_Area = @rating_area";
                Insurance_Plans = connection.Query<InsurancePlansInfo>(sql, new { rating_area }).AsList();
                connection.Close();
                return Insurance_Plans;
            }
        }
        public IList<InsurancePlansInfo> getSpecificPlans(int age, int rating_area)
        {
            IList<InsurancePlansInfo> Insurance_Plans;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Quartz_Console_App"].ConnectionString))
            {
                connection.Open();
                string sql = @"
                                    SELECT [Plan_ID]
                                    ,[Plan_Age]
                                    ,[Plan_Rating_Area]
                                    ,[Metal_Tier]
                                    ,[Co_Insurance]
                                    ,[Premium]
                                    ,[Plan_type]
                                    ,[Deductible]
                                    ,[Max_Out_of_Pocket]
                                    ,[Office_Visit]
                                    ,[Specialist_Visit]
                                    ,[Urgent_care]
                                    ,[Emergency_Room]
                                    ,[Inpatient_Services]
                                    ,[Outpatient_Services]
                                    ,[Lab]
                                    ,[X_Ray]
                                    ,[MRI]
                                    ,[Dental_Coverage]
                                    ,[Pharmacy_Co_Pay]
                                    FROM [2018FA422_TeamB].[dbo].[Insurance_Plans]
                                    WHERE Plan_Age = @age AND Plan_Rating_Area = @rating_area";
                Insurance_Plans = connection.Query<InsurancePlansInfo>(sql, new { age, rating_area }).AsList();
                connection.Close();
                return Insurance_Plans;
            }
        }

       

    }
}

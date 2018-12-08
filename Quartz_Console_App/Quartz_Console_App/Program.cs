using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Quartz_Console_App.Classes;
using Quartz_Console_App.DataInfo;
//using Dapper;

namespace Quartz_Console_App
{
    class Program
    {
        private static CustomerDAO _customerViewer;
        private static InsurancesPlansDAO _InsuranceViewer;
        private static CustomerInvoiceDAO _CustInvoiceViewer;
        private static DependentsDAO _DependentsViewer;


        private static CustomerInfo Current_Customer;
        private static IList<DependentsInfo> Dependents_List;
        //private static CustomerInfo _customerList;

        static void Main(string[] args)
        {
            MainMenu();
         
        }
        public static string MainMenu()
        {

            _DependentsViewer = new DependentsDAO();
            Dependents_List = new List<DependentsInfo>();
            int going = 0;
            while (going == 0)
            {


                cleanHeader();
                Console.WriteLine("MAIN MENU:");
                Console.WriteLine();
                Console.WriteLine("1. Customer View");
                Console.WriteLine("2. Managerial View");

                Console.WriteLine("9. Exit");


                CommandPrompt("Enter menu option");
                string selection = Console.ReadLine();
                if (selection.Equals("1"))
                {
                    int going1 = 0;
                    while (going1 == 0)
                    {
                        //10032
                        cleanHeader();
                        CommandPrompt("Are you a new Customer? (Y/N)");
                        string new_customer = Console.ReadLine();
                        new_customer = new_customer.ToUpper();
                        if (new_customer.Equals("Y"))
                        {
                            Current_Customer = CreateNewCustomer();
                            int going2 = 0;
                            while (going2 == 0)
                            {
                                cleanHeader();
                                CommandPrompt("Would you like to add any dependents? (Y/N)");
                                string new_dependents_answer = Console.ReadLine();
                                new_dependents_answer = new_dependents_answer.ToUpper();

                                new_dependents_answer = new_dependents_answer.ToUpper();
                                if (new_dependents_answer.Equals("Y"))
                                {
                                    int going3 = 0;
                                    int num_dep = 0;
                                    while (going3 == 0)
                                    {
                                        cleanHeader();
                                        CommandPrompt("How many dependents would you like to add? (0-9)");
                                        string num_dependents = Console.ReadLine();
                                       
                                        if (!Int32.TryParse(num_dependents, out num_dep))
                                        {
                                            going3 = 0;
                                        }
                                        else
                                        {
                                            Int32.TryParse(num_dependents, out num_dep);
                                            going3 = 1;
                                        }
                                         
                                    }
                                    
                                    Dependents_List = CreateNewDependents(num_dep, Current_Customer.Customer_ID);
                                    going2 = 1;


                                }else if (new_dependents_answer.Equals("N"))
                                {
                                    going2 = 1;
                                }
                                else
                                {
                                    going2 = 0;
                                }
                            }

                            going1 = 1;
                        }
                        else
                        {
                            Current_Customer = Login();
                            Dependents_List = new List<DependentsInfo>();
                            
                            Dependents_List = _DependentsViewer.GetDepedents(Current_Customer.Customer_ID);
                            going1 = 1;
                        }
                        if (Current_Customer == null)
                        {
                            going1 = 0;
                        }
                    }

                    

                    string choice = "";
                    int chosen = 1;
                    //While loop to go through menu options until a customer wants to exit
                    while (chosen != 0 && chosen != 9)
                    {
                        choice = DisplayCustomerOptions(Current_Customer);
                        chosen = CustomerChoice(choice);
                    }
                    Current_Customer = new CustomerInfo();
                    Dependents_List = new List<DependentsInfo>();
                    going = 0;
                }
                //Manager choice
                else if (selection.Equals("2"))
                {
                    string choice = "";
                    int chosen = 1;
                    while(chosen != 0 && chosen != 9)
                    {
                        choice = DisplayManagerOptions();
                        chosen = ManagerChoice(choice);
                    }

                    going = 0;
                }
                else if (selection != "9")
                {
                    cleanHeader();
                    CommandPrompt("Not a valid choice, please choose a proper value!");
                    Console.ReadKey();
                    going = 0;
                    
                }
                else if (selection.Equals("9"))
                {
                    going = 1;
                }
            }

            return "done";

        }
       
        //Customer Menus
        private static string DisplayCustomerOptions(CustomerInfo Customer)
        {
            cleanHeader();
            Console.WriteLine("Welcome " +Customer.FirstName +"!");
            Console.WriteLine();
            Console.WriteLine("1. Your Information");
            Console.WriteLine("2. Purchase Plan");
            Console.WriteLine("3. Purchase History");
            Console.WriteLine("4. Add Dependents");
            Console.WriteLine("5. Support Line");
            Console.WriteLine("9. Log Out");
            CommandPrompt("Enter menu option");
            string selection = Console.ReadLine();
            return selection;
        }
        private static int CustomerChoice(string choice)
        {
            if (choice.Equals("1"))
            {
                DisplayCustomerInfo(Current_Customer,Dependents_List);
                return 1;

            }
            else if (choice.Equals("2"))
            {
                int complete = ChoosePlanOptions();
                return 2;
            }
            else if (choice.Equals("3"))
            {
                IList<CustomerInvoiceInfo> Invoice_Hist = new List<CustomerInvoiceInfo>();
                Invoice_Hist = getCustInvoiceHist(Current_Customer.Customer_ID);
                DisplayPlans(Invoice_Hist);
                CommandPrompt("Press any key to return to menu");
                Console.ReadLine();
                return 3;
            }
            else if (choice.Equals("4"))
            {
                int going = 0;
                int num_depend = 0;
                while(going == 0)
                {
                    cleanHeader();
                    CommandPrompt("How many Dependents would you like to add? (1-9)");
                    string depend = Console.ReadLine();
                    if(!Int32.TryParse(depend,out num_depend))
                    {
                        cleanHeader();
                        CommandPrompt("Not a valid number, please choose again");
                        Console.ReadLine();
                    }
                    else
                    {
                        Int32.TryParse(depend, out num_depend);
                        going = 1;
                    }
                }

                IList<DependentsInfo> temp_list = new List<DependentsInfo>();
                temp_list = CreateNewDependents(num_depend, Current_Customer.Customer_ID);
                for(int i = 0; i < temp_list.Count; i++)
                {
                    Dependents_List.Add(temp_list[i]);
                }

                return 4;
            }else if (choice.Equals("5"))
            {
                string url = "https://quartzbenefits.com/about-quartz-brand-health-insurance-plans-wisconsin/contact-us";
                System.Diagnostics.Process.Start(url);
                return 5;
            }
            else if (choice.Equals("9"))
            {
                CommandPrompt("Logging you out... Press any key to return to the initial menu");
                Console.ReadLine();
                return 9;
            }
            else
            {
                CommandPrompt("Please choose an option!");
                Console.ReadKey();
                return 10;
            }
            
            
        }


        //Manager Menus
        private static string DisplayManagerOptions()
        {
            cleanHeader();
            Console.WriteLine();
            Console.WriteLine("1. Get Customer List");
            Console.WriteLine("2. Customer Plan Options");
            Console.WriteLine("3. Quartz Invoice History");
            Console.WriteLine("4. Aggregations");
            Console.WriteLine("5. Plans Update History");
            Console.WriteLine("6. Customer Invoice History");
            Console.WriteLine("9. Exit to Main Menu");
            CommandPrompt("Enter menu option");
            string selection = Console.ReadLine();
            return selection;
        }

        private static int ManagerChoice(string choice)
        {
            if (choice.Equals("1"))
            {
                return 1;
            }
            else if (choice.Equals("2"))
            {
                return 2;
            }
            else if (choice.Equals("3"))
            {
                return 3;
            }
            else if (choice.Equals("4"))
            {
                return 4;

            }
            else if (choice.Equals("5"))
            {
                return 5;
            }
            else if (choice.Equals("6"))
            {
                return 6;
            }
            else if (choice.Equals("9"))
            {
                return 9;
            }
            else
            {
                cleanHeader();
                CommandPrompt("Option not recognized, please choose again");
                Console.ReadLine();
                return 0;
            }
            //return 0;
        }
       

        private static CustomerInfo Login()
        {
            _customerViewer = new CustomerDAO();
            IList<CustomerInfo> valid_Customer;
            int going = 0;
            CustomerInfo customer = new CustomerInfo();
            while (going == 0)
            {
                cleanHeader();
                CommandPrompt("What is your Customer ID?");
                string str_Age_Chosen = Console.ReadLine();
                //Console.WriteLine(str_Age_Chosen.ToString().Substring(5));
                valid_Customer = _customerViewer.GetCustomer(str_Age_Chosen);
                if (!valid_Customer.Any())
                {
                    Console.WriteLine("ID not found in the system");
                    Console.ReadKey();
                    going = 0;
                }
                else
                {
                    
                    customer = valid_Customer[0];
                    going = 1;
                }
            }
            return customer;

        }

        //Display methods
        private static void DisplayCustomerInfo(CustomerInfo Cust, IList<DependentsInfo> Dependents)
        {
            Console.Clear();
            WriteHeader();
            Console.WriteLine("Your Information");
            Console.WriteLine("Your ID: " +Cust.Customer_ID);
            Console.WriteLine("First Name: " + Cust.FirstName);
            Console.WriteLine("Last Name: " + Cust.LastName);
            Console.WriteLine("Date of Birth: " + String.Format("{0:M/d/yyyy}", Cust.DOB));

            Console.WriteLine("Address: " + Cust.Address);
            Console.WriteLine("City: " + Cust.City);
            Console.WriteLine("State: " + Cust.State);
            Console.WriteLine("Address: " + Cust.Zip);

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Your Dependents Information");
            Console.WriteLine("-------------------------------------");
            for(int i = 0; i < Dependents.Count; i++)
            {
                Console.WriteLine(Dependents[i].FirstName + " " + Dependents[i].LastName);
                Console.WriteLine("Dependent ID: " + Dependents[i].Dependent_ID);
                Console.WriteLine("Zip Code: " + Dependents[i].Zip);
                Console.WriteLine("Date of Birth: " + String.Format("{0:M/d/yyyy}",Dependents[i].DOB));
                Console.WriteLine("Dependent Type: " + Dependents[i].Dependent_Type);
                Console.WriteLine();
            }

            Console.WriteLine();
            CommandPrompt("Return to menu");
            string read = Console.ReadLine();
            return;

        }
        private static void DisplayInsurancePlans(IList<InsurancePlansInfo> Plans, int age_specific)
        {
            cleanHeader();
            if (age_specific == 0)
            {
                Console.WriteLine("\t Age Range  Plan Rating Area  Metal Tier  Co Insurance  Deductible  Premium  Plan Type");
                for (var i = 0; i < Plans.Count; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, "\t  " + Plans[i].Plan_Age + "\t \t" + Plans[i].Plan_Rating_Area + "\t\t"
                        + Plans[i].Metal_Tier + "\t \t" + Math.Round(Plans[i].Co_Insurance, 2) + " \t" + Plans[i].Deductible + "\t \t"
                        + Math.Round(Plans[i].Premium, 2) + "\t " + Plans[i].Plan_type);
                    //plan_Count++;
                }
            }
            else
            {
                Console.WriteLine("\t Age Range  Plan Rating Area  Metal Tier       Co Insurance  Deductible  Premium  Plan Type");
                for (int i = 0; i < Plans.Count; i++)
                {
                    if (!Plans[i].Metal_Tier.Equals("Platinum"))
                    {
                        Console.WriteLine("{0}. {1}", i + 1, "\t  " + Plans[i].Plan_Age + "\t \t"
                          + Plans[i].Plan_Rating_Area + "\t\t"
                          + Plans[i].Metal_Tier + "\t \t" + Math.Round(Plans[i].Co_Insurance, 2) +
                          "\t \t" + Plans[i].Deductible + " \t"
                          + Math.Round(Plans[i].Premium, 2) + "\t " + Plans[i].Plan_type);
                    }
                    else
                    {
                        Console.WriteLine("{0}. {1}", i + 1, "\t  " + Plans[i].Plan_Age + "\t \t"
                           + Plans[i].Plan_Rating_Area + "\t\t"
                           + Plans[i].Metal_Tier + " \t" + Math.Round(Plans[i].Co_Insurance, 2) +
                           "\t \t" + Plans[i].Deductible + " \t"
                           + Math.Round(Plans[i].Premium, 2) + "\t " + Plans[i].Plan_type);
                    }
                }
            }

        }
        private static void DisplayChosenPlan(IList<InsurancePlansInfo> Plans, int Plan_Num)
        {
            cleanHeader();
            Console.WriteLine("Plan Chosen: ");
            Console.WriteLine("\t Age Range  Plan Rating Area  Metal Tier       Co Insurance  Deductible  Premium  Plan Type");
            //int i = Plan_Num;
            Console.WriteLine("{0}. {1}", Plan_Num, "\t  " + Plans[Plan_Num - 1].Plan_Age + "\t \t"
                       + Plans[Plan_Num - 1].Plan_Rating_Area + "\t\t"
                       + Plans[Plan_Num - 1].Metal_Tier + " \t" + Math.Round(Plans[Plan_Num - 1].Co_Insurance, 2) +
                       "\t \t" + Plans[Plan_Num - 1].Deductible + " \t"
                       + Math.Round(Plans[Plan_Num - 1].Premium, 2) + "\t " + Plans[Plan_Num - 1].Plan_type);

        }

        //Business functionality
        private static int ChoosePlanOptions()
        {
            IList<InsurancePlansInfo> Plans;
            IList<InsurancePlansInfo> Age_Selected_Plans;
            _InsuranceViewer = new InsurancesPlansDAO();
            _customerViewer = new CustomerDAO();


            
            int going = 0;
            string str_Age_Chosen = "";
            while (going == 0)
            {
                cleanHeader();
                CommandPrompt("What age are you looking to purchase from? (Type all to see all plans)");
                str_Age_Chosen = Console.ReadLine();
                int age;
                if ( !str_Age_Chosen.Equals("all") && (!Int32.TryParse(str_Age_Chosen, out age) || age > 64 || age < 1) )
                {

                    CommandPrompt("Invalid age, hit any key to try again...");
                    Console.ReadLine();

                    going = 0;
                }
                else
                {
                    going = 1;
                }
            }
           

            //cleanHeader();
            going = 0;
            string str_Rating_Area = "";
            while (going == 0)
            {
                cleanHeader();
                CommandPrompt("What rating area are you looking to purchase from? (1-5)");
                str_Rating_Area = Console.ReadLine();
                int rating;
                if(!Int32.TryParse(str_Rating_Area, out rating) || rating > 5 || rating < 1)
                {
                    Console.WriteLine("Invalid rating area.  Please choose again...");
                    Console.ReadKey();
                    going = 0;
                }
                else
                {
                    going = 1;
                }

            }

            going = 0;
            string str_Dependents = "";
            int multiplier = 1;
            while(going == 0)
            {
                cleanHeader();
                for(int i = 0; i< Dependents_List.Count; i++)
                {
                    Console.WriteLine( Dependents_List[i].FirstName + " " + Dependents_List[i].LastName + " " + Dependents_List[i].Dependent_Type);
                }
                CommandPrompt("Will your plan cover all of your dependents? (Y/N)");
                str_Dependents = Console.ReadLine();
                str_Dependents = str_Dependents.ToUpper();
                if(str_Dependents.Equals("Y")){
                    multiplier = Dependents_List.Count;
                    going = 1;
                }else if (str_Dependents.Equals("N"))
                {
                    going = 1;
                }
                else
                {
                    cleanHeader();
                    CommandPrompt("Please type \"Y\" or \"N\"");
                    Console.ReadLine();
                }
            }

            if (str_Age_Chosen.Equals("all"))
            {
                int age_input;
                int rating_area;
                Int32.TryParse(str_Age_Chosen, out age_input);
                Int32.TryParse(str_Rating_Area, out rating_area);
                Plans = _InsuranceViewer.getAllPlanInfo(rating_area);

                DisplayInsurancePlans(Plans,0);


                
                int Plan_Num = 0;
                going = 0;
                while(going == 0)
                {
                    CommandPrompt("Select plan: 1-" + Plans.Count);
                    string str_plan_id = Console.ReadLine();
                    if (!Int32.TryParse(str_plan_id, out Plan_Num) || Plan_Num > Plans.Count || Plan_Num < 1)
                    {
                        Console.WriteLine("Invalid product entry.  Press any key...");
                        Console.ReadKey();
                        going = 0;

                    }
                    else
                    {
                        going = 1;
                        Int32.TryParse(str_plan_id, out Plan_Num);
                    }

                }
                

                //Int32.TryParse(str_plan_id, out Plan_Num);
                InsurancePlansInfo Chosen_Plan = new InsurancePlansInfo();

                Chosen_Plan = Plans[Plan_Num - 1];
                DisplayChosenPlan(Plans, Plan_Num);

                CommandPrompt("Are you okay with making this purchase?(Y/N)");
                string choice = Console.ReadLine();
                choice = choice.ToUpper();
                if (choice.Equals("Y"))
                {
                    int complete = MakePurchase(Chosen_Plan, Current_Customer, multiplier);
                    return complete;

                }
                else
                {
                    Console.WriteLine("Ending Process... hit any key");
                    Console.ReadKey();
                    return 0;
                }


            }
            else
            {
                
                int age;
                int rating_area;
                Int32.TryParse(str_Rating_Area, out rating_area);
                Int32.TryParse(str_Age_Chosen, out age);
                Age_Selected_Plans = _InsuranceViewer.getSpecificPlans(age, rating_area);


                //Displaying the available choices
                DisplayInsurancePlans(Age_Selected_Plans,1);


                 going = 0;
                int Plan_Num = 0;
                InsurancePlansInfo Chosen_Plan = new InsurancePlansInfo();
                while (going == 0)
                {

                    CommandPrompt("Select plan: 1-" + Age_Selected_Plans.Count);
                    string str_plan_id = Console.ReadLine();                  

                    if (!Int32.TryParse(str_plan_id, out Plan_Num) || Plan_Num > Age_Selected_Plans.Count || Plan_Num < 1)
                    {
                        Console.WriteLine("Invalid product entry.  Please choose again...");
                        Console.ReadKey();
                        going = 0;
                    }
                    else
                    {
                        Int32.TryParse(str_plan_id, out Plan_Num);                       
                        Chosen_Plan = Age_Selected_Plans[Plan_Num - 1];
                        going = 1;
                    }
                    
                }
                    
                cleanHeader();

                Console.WriteLine("Plan Chosen:");
                Console.WriteLine("\t Age Range  Plan Rating Area  Metal Tier       Co Insurance  Deductible  Premium  Plan Type");
                //int i = Plan_Num;
                Console.WriteLine("{0}. {1}", Plan_Num, "\t  " + Age_Selected_Plans[Plan_Num - 1].Plan_Age + "\t \t"
                           + Age_Selected_Plans[Plan_Num-1].Plan_Rating_Area + "\t\t"
                           + Age_Selected_Plans[Plan_Num - 1].Metal_Tier + " \t" + Math.Round(Age_Selected_Plans[Plan_Num - 1].Co_Insurance, 2) +
                           "\t \t" + Age_Selected_Plans[Plan_Num - 1].Deductible + " \t"
                           + Math.Round(Age_Selected_Plans[Plan_Num - 1].Premium, 2) + "\t " + Age_Selected_Plans[Plan_Num - 1].Plan_type);

                CommandPrompt("Confirm this plan choice?(Y/N)");
                string choice = Console.ReadLine();
                choice = choice.ToUpper();
                if (choice.Equals("Y"))
                {
                    int complete = MakePurchase(Chosen_Plan,Current_Customer, multiplier);
                    return complete;
                }
                else
                {
                    Console.WriteLine("Ending Process... hit any key");
                    Console.ReadKey();
                    return 0;
                }

               // string str_Age_Chosen = Console.ReadLine();


            }
           
        }

        private static IList<CustomerInvoiceInfo> getCustInvoiceHist(string Customer_ID)
        {
            _CustInvoiceViewer = new CustomerInvoiceDAO();
            IList<CustomerInvoiceInfo> Invoice_Hist_List;
            Invoice_Hist_List =_CustInvoiceViewer.GetInvoiceHistory(Customer_ID);
            return Invoice_Hist_List;
        }

        //private static void getMonthlyReports()
        //{
        //    return;
        //}
      
        private static int MakePurchase(InsurancePlansInfo Chosen_Plan, CustomerInfo Cust, int multiplier)
        {
            _CustInvoiceViewer = new CustomerInvoiceDAO();
            int completed = 0;
            double Premium = Chosen_Plan.Premium * 12* multiplier;
            Console.Clear();
            WriteHeader();
            CommandPrompt("The Total Cost is: $" + Math.Round(Premium,2) + "  Please Press Y to confirm this purchase, N to cancel");
            string str_CanPurchase = Console.ReadLine();
            str_CanPurchase = str_CanPurchase.ToUpper();
            if (str_CanPurchase.Equals("Y"))
            {
                completed = _CustInvoiceViewer.NewPurchase(Cust, Chosen_Plan, Premium);
                if (!completed.Equals("failed"))
                {
                    cleanHeader();
                    CommandPrompt("Success!  A receipt has been downloaded onto your computer. Press Any Key to return to the menu..."); //Will download a receipt
                    Console.ReadLine();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("Ending Process... hit any key");
                Console.ReadKey();
                return 0;
            }
        }


        private static CustomerInfo CreateNewCustomer()
        {
            int going =0;
            CustomerInfo new_Cust = new CustomerInfo();
            _customerViewer = new CustomerDAO();

            while (going == 0)
            {
                Console.Clear();
                WriteHeader();
                CommandPrompt("What is your Social Security Number? (Must be 9 numbers)");
                string str_SSN = Console.ReadLine();
                float SSN;
                if (str_SSN.Length != 9 || !float.TryParse(str_SSN, out SSN))
                {
                    cleanHeader();
                    CommandPrompt("Invalid SSN entry");
                    Console.ReadKey();
                    going = 0;
                }
                else
                {
                    new_Cust.SSN = str_SSN;
                    going = 1;
                }
            }
            Console.Clear();
            WriteHeader();
            CommandPrompt("What is your first name?");
            string str_FirstName = Console.ReadLine();
            new_Cust.FirstName = str_FirstName.Trim();

            Console.Clear();
            WriteHeader();
            CommandPrompt("What is your last name?");
            string str_LastName = Console.ReadLine();
            new_Cust.LastName = str_LastName.Trim();

            Console.Clear();
            WriteHeader();
            CommandPrompt("What is your Address?");
            string str_Address = Console.ReadLine();
            new_Cust.Address = str_Address;

            Console.Clear();
            WriteHeader();
            CommandPrompt("City?");
            string str_City = Console.ReadLine();
            new_Cust.City = str_City;

            Console.Clear();
            WriteHeader();
            CommandPrompt("State?");
            string str_State = Console.ReadLine();
            new_Cust.State = str_State;

            going = 0;
            while(going == 0)
            {
                Console.Clear();
                WriteHeader();
                CommandPrompt("Zip Code?");
                string str_ZipCode = Console.ReadLine();
                int zipcode;
                if (!int.TryParse(str_ZipCode, out zipcode))
                {

                    CommandPrompt("Invalid Zip Code.");
                    Console.ReadKey();
                    going = 0;
                }
                else
                {
                    int.TryParse(str_ZipCode, out zipcode);
                    new_Cust.Zip = Convert.ToString(zipcode);
                    going = 1;
                }
            }
           

            Console.Clear();
            WriteHeader();
            CommandPrompt("What is your DOB? (MM-DD-YYYY)");
            string str_DOB = Console.ReadLine();
            
            DateTime DOB = DateTime.Parse(str_DOB);
            //DateTime DOB=  Convert.ToDateTime(str_DOB);
            new_Cust.DOB = DOB;

           float complete = _customerViewer.AddNewCustomer(new_Cust);
            new_Cust.Customer_ID = complete.ToString();
            
               return new_Cust;
            

        }
        private static IList<DependentsInfo> CreateNewDependents(int num_dependents, string Customer_ID)
        {
            IList<DependentsInfo> Dependents = new List<DependentsInfo>();
            for(int i =0; i < num_dependents; i++)
            {
                DependentsInfo depend = QuestionsForDependentInfo(Customer_ID);
                Dependents.Add(depend);
            }
            return Dependents;
        }
        private static DependentsInfo QuestionsForDependentInfo(string Customer_ID)
        {
            cleanHeader();
            int going = 0;
            DependentsInfo new_Dependent = new DependentsInfo();
            _DependentsViewer = new DependentsDAO();

            while (going == 0)
            {
                cleanHeader();
                CommandPrompt("What is their Social Security Number? (Must be 9 numbers)");
                string str_SSN = Console.ReadLine();
                if (str_SSN.Length != 9)
                {
                    Console.WriteLine("Invalid SSN entry.");
                    Console.ReadKey();
                    going = 0;
                }
                else
                {
                    new_Dependent.SSN = str_SSN;
                    going = 1;
                }
            }
            cleanHeader();
            CommandPrompt("What is their first name?");
            string str_FirstName = Console.ReadLine();
            new_Dependent.FirstName = str_FirstName;

            cleanHeader();
            CommandPrompt("What is their last name?");
            string str_LastName = Console.ReadLine();
            new_Dependent.LastName = str_LastName;

            going = 0;
            while (going == 0)
            {
                cleanHeader();
                CommandPrompt("What Zip Code do they live in?");
                string str_ZipCode = Console.ReadLine();
                int zipcode;
                if (!int.TryParse(str_ZipCode, out zipcode))
                {

                    CommandPrompt("Invalid Zip Code.");
                     Console.ReadKey();
                    going = 0;
                }
                else
                {
                    int.TryParse(str_ZipCode, out zipcode);
                    new_Dependent.Zip = Convert.ToString(zipcode);
                    going = 1;
                }
            }


            cleanHeader();
            CommandPrompt("What is their DOB? (MM-DD-YYYY)");
            string str_DOB = Console.ReadLine();

            DateTime DOB = DateTime.Parse(str_DOB);
            new_Dependent.DOB = DOB;

            going = 0;
            int depend_num = 0;
            while (going == 0)
            {
                cleanHeader();
                Console.WriteLine("1. Spouse");
                Console.WriteLine("2. Child");

                CommandPrompt("Lastly, how are they related to you?");
                string dependent_type = Console.ReadLine();
                 depend_num = 0;
                if(!Int32.TryParse(dependent_type, out depend_num) || depend_num > 2 || depend_num < 1)
                {
                    CommandPrompt("Invalid Choice");
                    Console.ReadLine();
                    going = 0;
                }
                else
                {
                    Int32.TryParse(dependent_type, out depend_num);
                    going = 1;
                }

            }
            if(depend_num == 1)
            {
                new_Dependent.Dependent_Type = "Spouse";
            }
            else
            {
                new_Dependent.Dependent_Type = "Child";
            }

            new_Dependent.Customer_ID = Customer_ID;

            int complete = _DependentsViewer.AddNewDependent(new_Dependent);
            new_Dependent.Dependent_ID = complete.ToString();
            if (new_Dependent.Dependent_ID.Length == 5)
            {
                return new_Dependent;
            }

            return null;
           // return null;
        }



        private static int getAge()
        {
            Console.Clear();
            WriteHeader();
            CommandPrompt("Enter your Age:");
            string str_plan_id = Console.ReadLine();
            int age;
            Int32.TryParse(str_plan_id, out age);

            return age;

        }
        private static void DisplayCustomers()
        {
            IList<CustomerInfo> customers;
            _customerViewer = new CustomerDAO();
            customers = _customerViewer.getCustomersInfo();
            //WriteHeader();

            // Console.WriteLine("Hello World");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine(customers[i].FirstName + " " + customers[i].LastName + " " + customers[i].Address + " " + customers[i].City
                    + " " + customers[i].State);
                Console.WriteLine();
            }

            CommandPrompt("Press any button to return to menu");
            Console.ReadLine();
            return;
        }
        private static void DisplayPlans(IList<CustomerInvoiceInfo> Invoices)
        {
            cleanHeader();
            if(Invoices.Count > 0)
            {
                Console.WriteLine("Your ID: " + Invoices[0].Customer_ID);
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();

                Console.WriteLine("Start Date \t End Date \t Plan ID \t Plan Age \t Rating Area    When You Paid    Amount Paid");
                for (int i = 0; i < Invoices.Count; i++)
                {
                    Console.WriteLine("" + String.Format("{0:M/d/yyyy}", Invoices[i].Coverage_Start_Date) + "\t " + String.Format("{0:M/d/yyyy}", Invoices[i].Coverage_End_Date) + "\t " +
                        Invoices[i].Plan_ID + "\t \t  " + Invoices[i].Plan_Age + "\t \t " + Invoices[i].Plan_Rating_Area + "\t \t"
                        + String.Format("{0:M/d/yyyy}", Invoices[i].Paid_Date_Time) + "\t " + Invoices[i].Paid_Amount);

                    Console.WriteLine("-------------------------------------");
                }
                return;
            }
            else
            {
                Console.WriteLine("There are currently no purchase records for your plans");
                return;
            }
            
        }

        //Visual Methods
        private static void WriteHeader()
        {
            Console.Clear();
            Console.WriteLine("#### Quartz Console Application ####");
            Console.WriteLine();
            Console.WriteLine();
        }
        private static void CommandPrompt(string prompt = "")
        {
            //put the cursor at the bottom of the screen
            Console.CursorTop = Console.WindowHeight - 5;
            Console.WriteLine(prompt == string.Empty ? "Enter command" : prompt + ":");
            Console.Write("# ");

        }
        private static void cleanHeader()
        {
            Console.Clear();
            WriteHeader();           
            return;
        }

    }
}

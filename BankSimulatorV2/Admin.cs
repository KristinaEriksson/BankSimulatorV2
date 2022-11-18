﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulatorV2
{
    internal class Admin : User
    {
        Bank bank;

        public Admin(string name, int age, int idNumber, string password)
        {
            bank = new Bank();
            this.Name = name;
            this.Age = age;
            id = idNumber;
            IsAdmin = true;
            this.password = password;
            AdminMainMenu();
        }



        public void GetMeny(int userId)
        {
            if (id == userId)
            {
                AdminMainMenu();
            }
            else
            {
                bank.Menu(userId);
            }
        }
        private void AdminMainMenu()
        {
            string userChoice = "";
            Console.Clear();
            while (userChoice != "0")
            {
                Console.Clear();
                Console.WriteLine("Menu for Admin");
                Console.WriteLine("----------------------");
                Console.WriteLine("#1: Unlock Customer");
                Console.WriteLine("#2: Add Customer");
                Console.WriteLine("#3: Display all customers");
                Console.WriteLine("#0: Sign Out");
                Console.WriteLine("----------------------");
                Console.Write("Enter option: ");
                userChoice = Console.ReadLine();
                userChoice = userChoice.Trim();
                switch (userChoice)
                {
                    case "1":
                        unLockCustomer();
                        break;
                    case "2":
                        addCustomer();
                        break;
                    case "3":
                        PrintInfo();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
            Console.WriteLine("signing out, please wait");
            System.Threading.Thread.Sleep(1500);
        }
        private void unLockCustomer()
        {
            string getId = "";
            var updateCustomerList = bank.cloneCustomerList();
            Console.WriteLine("Locked Customer:");
            foreach (var customer in updateCustomerList)
            {
                if(customer.LockedOut == true)
                {
                    Console.WriteLine(customer);
                }
            }
            Console.Write("Unlock Customer: Enter customerId: ");
            getId = Console.ReadLine();
            getId = getId.Trim();
            foreach (var customer in updateCustomerList)
            {
                if (customer.IdNumber.ToString() == getId)
                {
                    customer.LockedOut = false;
                    break;
                }
            }
            bank.GetUpdatedCustList(updateCustomerList);
            Console.Clear();
            Console.WriteLine("Customer is now unlocked..");
            System.Threading.Thread.Sleep(1000);
        }

        private void addCustomer()
        {
            string name = "";
            string adress = "";
            int age = 0;
            int id = 0;
            string password = "";
            double wallet = 0;
            Console.Clear();
            Console.Write("Customer Name: ");
            name = Console.ReadLine();
            Console.Clear();
            Console.Write("Customer Adress: ");
            adress = Console.ReadLine();
            Console.Clear();
            Console.Write("Customer Age: ");
            age = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.Write("Customer ID: ");
            id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Password: ");
            password = Console.ReadLine();
            Console.Clear();
            Console.Write("Customer Wallet: ");
            wallet = Convert.ToDouble(Console.ReadLine());
            bank.addNewCustomer(name, adress, age, id, password, wallet);
            Console.Clear();
            Console.WriteLine("New customer has been added");
            System.Threading.Thread.Sleep(1000);
        }

        public override void PrintInfo()
        {
            var customerList = bank.cloneCustomerList();
            if (customerList.Count == 0)
            {
                Console.WriteLine("No Customer added");
            }
            else
            {
                Console.WriteLine("---------------------------");
                foreach (var cust in customerList)
                {
                    Console.WriteLine("\n" + cust);
                }
                Console.WriteLine("---------------------------");
            }
            Console.WriteLine("\nkey for menu..");
            Console.ReadKey();
        }
        public bool VerifyUser(int getId)
        {
            bool verified = false;
            var clonedList = bank.cloneCustomerList();
            if (id == getId)
            {
                verified = true;
            }
            else
            {
                foreach (var customer in clonedList)
                {
                    if (customer.IdNumber == getId)
                    {
                        verified = true;
                    }
                }
            }
            return verified;
        }
        public bool VerifyUser(string getPassword)
        {
            bool verified = false;
            var clonedList = bank.cloneCustomerList();
            if (password == getPassword)
            {
                verified = true;
            }
            else
            {
                foreach (var customer in clonedList)
                {
                    if (customer.Password == getPassword)
                    {
                        verified = true;
                    }
                }
            }
            return verified;
        }
        public int WrongPasswordtransfer(int getId)
        {
            int userPasswordTries = 0;
            var updateCustomerList = bank.cloneCustomerList();            
            foreach (var customer in updateCustomerList)
            {
                if(customer.IdNumber == getId)
                {
                    userPasswordTries = customer.PasswordTries();
                    break;
                }                
            }
            bank.GetUpdatedCustList(updateCustomerList);
            return userPasswordTries;
        }
    }
}

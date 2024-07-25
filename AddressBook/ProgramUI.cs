using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class ProgramUI : AddressBook
    {
        public ProgramUI(string filePath) : base(filePath)
        {
        }

        public void ShowMeniu()
        {
            Console.WriteLine("ADRESŲ KNYGA");
            Console.WriteLine("Pasirinkite meniu punktą");
            Console.WriteLine("1.Pridėti kontaktą");
            Console.WriteLine("2.Peržiūrėti kontaktus");
            Console.WriteLine("3.Ištrinti kontaktą");
            Console.WriteLine("4.Ieškoti kontakto");
            Console.WriteLine("5.Išeiti");
            HandleUserChoice();
        }
        public void HandleUserChoice()
        {
            int.TryParse(Console.ReadLine(), out int choose);
            Console.Clear();

            switch (choose)
            {
                case 1:
                    Console.WriteLine("ADRESATO PRIDĖJIMAS");
                    AddContactUI();
                    break;
                case 2:
                    Console.WriteLine("ADRESŲ SĄRAŠAS");
                    ViewContactsUI();
                    break;
                case 3:
                    Console.WriteLine("KONTAKTO TRYNIMAS");
                    DeleteContactUI();
                    break;
                case 4:
                    Console.WriteLine("ADRESATO PAIEŠKA");
                    SearchContactUI();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;

            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Norėdami grįžti į meniu spauskite bet kokį mygtuką");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            ShowMeniu();
        }
        public string AddContactUI()
        {
            Console.WriteLine();
            Console.WriteLine("Iveskite vardą, pavardę, telefono numerį(+370XXXXXXXX) ir e-paštą(visi laukai privalomi)");
            bool isOkay = true;
            while (isOkay)
            {
                Console.Write("Vardas: ");
                FirstName = Console.ReadLine();
                isOkay = false;
                if (string.IsNullOrEmpty(FirstName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Įveskite vardą");
                    Console.ResetColor();
                    isOkay = true;
                }
            }
            isOkay = true;
            while (isOkay)
            {
                Console.Write("Pavardė: ");
                LastName = Console.ReadLine();
                isOkay = false;
                if (string.IsNullOrEmpty(LastName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Įveskite pavardę");
                    Console.ResetColor();
                    isOkay = true;
                }
            }
            bool phoneValidator = true;
            while (phoneValidator)
            {
                Console.Write("Telefono numeris: ");
                PhoneNumber = Console.ReadLine();
                phoneValidator = PhoneNumberValidation(PhoneNumber);
            };
            bool emailValidator = true;
            while (emailValidator)
            {
                Console.Write("E-paštas: ");
                Email = Console.ReadLine();
                emailValidator = EmailValidation(Email);
            }
            string addContact = AddContact(FirstName, LastName, PhoneNumber, Email);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Adresatas pridetas");
            Console.ResetColor();
            Console.WriteLine();
            return addContact;
        }
        public List<string> ViewContactsUI()
        {            
            Console.WriteLine();
            
            List<string> allContacts = ViewContact();
            for (int i = 0; i < allContacts.Count; i++)
            {
                Console.WriteLine($"{i+1}. {allContacts[i]}");
            };
            return allContacts;
            
        }
        public List<string> SearchContactUI()
        {            
            Console.WriteLine();
            Console.WriteLine("Paieška pagal vardą ir pavardę, įveskite:");
            Console.Write("Vardas: ");
            string firstName = Console.ReadLine();
            Console.Write("Pavardė: ");
            string lastName = Console.ReadLine();
            string fullName = firstName +" "+ lastName;
            List<string> foundContacts = SearchContact(fullName);
            if(foundContacts.Count == 0)
            {
                Console.WriteLine("Tokio adresato nėra");
            }
            else 
            {
                foreach (string cont in foundContacts)
                {
                    Console.WriteLine($"{foundContacts.IndexOf(cont) + 1}. {cont}");
                }
            }
           
            return foundContacts;         
        }
        public void DeleteContactUI()
        {
            Console.WriteLine();
            List<string> foundContacts = SearchContactUI();
            Console.WriteLine();
            Console.WriteLine("Kurį kontaktą norite ištrinti, pažymėkite numerį");
            int.TryParse(Console.ReadLine(), out int contactIndex);
            DeleteContact(foundContacts, contactIndex);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Adresatas ištrints");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}

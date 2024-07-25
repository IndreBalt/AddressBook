using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressBook
{
    public class AddressBook : Contact    
    {
        public string FilePath { get; set; }
        public AddressBook(string filePath) 
        { 
            FilePath = filePath;            
        }
        public string AddContact(string firstName, string lastName, string phoneNumber, string email) 
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;         
            string allContactInfo = $"{FirstName}, {LastName}, {PhoneNumber}, {Email.ToLower()}";
            File.AppendAllText(FilePath, allContactInfo + Environment.NewLine);
            return allContactInfo;
        }
        public List<string> ViewContact()
        {
            List<string> allContacts = File.ReadAllLines(FilePath).ToList();
            return allContacts;
        }
        public List<string> DeleteContact(List<string> foundContacts, int contactIndex)
        {
            List<string> allLines = ViewContact();
            List<string> allLinesNew = new List<string>();
            contactIndex -= 1;
            string contactForDelete = "";
            foreach (string line in foundContacts)//eina per sarasa isfiltruota pagal varda ir pavarde
            {
                if(foundContacts.IndexOf(line) == contactIndex)
                {
                    contactForDelete = line;
                }                               
            } 
            foreach(string line in allLines) 
            { 
                if(!line.Contains(contactForDelete))
                {
                    allLinesNew.Add(line);
                }
            }
            File.WriteAllLines(FilePath, allLinesNew);//perraso faila
            return allLinesNew;      
        }
        public List<string> SearchContact(string contactNameAndLastName)
        {            
            List<string> allLines = ViewContact();
            List<string> contactValues = contactNameAndLastName.Split(" ").ToList();
            string contactFirstName = contactValues[0].ToLower();
            string contactLastName = "";
            if (contactValues.Count > 1)
            {
                contactLastName = contactValues[1].ToLower();
            }            
            List<string> allLinesNew = new List<string>();
            foreach (var line in allLines)
            {
                string firstName = line.Split(',')[0].ToLower().TrimStart(' ');
                string lastName = line.Split(",")[1].ToLower().TrimStart(' ');
                if (contactFirstName.Contains(firstName) && contactLastName.Contains(lastName))
                {
                    allLinesNew.Add(line);
                }else if (contactFirstName.Contains(firstName))
                {
                    allLinesNew.Add(line);
                }
                else if (contactFirstName.Contains(lastName))
                {
                    allLinesNew.Add(line);
                }
            }
            return allLinesNew;
        }   
        public bool PhoneNumberValidation(string phoneNumber)//validacija pagal NullOrEmpty, simboliu kieki ir '+'
        {           
            if (phoneNumber.Length != 12)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Netinkamas telefono numerio ilgis");
                Console.ResetColor();
                return true;
                    
            }
            else if (string.IsNullOrEmpty(phoneNumber))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Telefono numerį privaloma įvesti, bandykit dar kartą");
                Console.ResetColor();
                return true;                    
            }         
            else if (phoneNumber.ToCharArray()[0] != '+')
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Netinkamas telefono numerio formatas, bandykit dar kartą");
                Console.ResetColor();
                return true;                    
            }                       
            return false;            
        }
        public bool EmailValidation(string email)
        {

            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if(!isEmail)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Netinkamas e-pasto formatas");
                Console.ResetColor();
            }
            return !isEmail;
        }

    }
}

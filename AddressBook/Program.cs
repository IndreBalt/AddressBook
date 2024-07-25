namespace AddressBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../contacts.csv";
            ProgramUI program = new ProgramUI(path);
            program.ShowMeniu();

            //AddressBook adress = new AddressBook(path);
            //List<string> cont = adress.SearchContact("jonas"); 
            //foreach(var co in cont)
            //{
            //    Console.WriteLine(co);
            //}


        }
    }
}

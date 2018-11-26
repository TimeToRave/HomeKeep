using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HomeKeep
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Tag[] allTags = Controller.GetAllTags();

            Console.WriteLine("ТЕГИ");

            for (int i = 0; i < allTags.Length - 1; i++)
            {
                allTags[i].Print();

            }

            //Note testNote = new Note("Купить", "Лимон", 1);
            //Controller.CreateNote(testNote);

            Controller.DeleteNoteInterface();


            Note[] allNotes = Controller.GetAllNotes();

            for (int i = 0; i < allNotes.Length; i++)
            {
                allNotes[i].Print();

            }

            Console.ReadKey();
        }
    }
}

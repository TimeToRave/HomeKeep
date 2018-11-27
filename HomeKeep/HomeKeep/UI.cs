using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeKeep
{
    public static class UI
    {

        private static void PrintTags(Tag[] tags)
        {
            Console.Clear();

            for (int i = 0; i <tags.Length; i++)
            {
                tags[i].Print();

            }
        }

        private static void PrintNotes(Note[] notes)
        {
            Console.Clear();

            for (int i = 0; i < notes.Length; i++)
            {
                notes[i].Print();

            }
        }

        static public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в HomeKeep!");
                Console.WriteLine("1. Вывести все заметки");
                Console.WriteLine("2. Добавить заметку");
                Console.WriteLine("3. Удалить заметку");
                Console.WriteLine("4. Поиск заметки по тегу");
                Console.WriteLine("10. Вывести все теги");
                Console.WriteLine("0. Выход");

                int caseSwitch = 0;

                if (!int.TryParse(Console.ReadLine(), out caseSwitch))
                {
                    Console.WriteLine("Введите корректное значение");
                    continue;
                }

                switch (caseSwitch)
                {
                    case 1:
                        PrintNotes(Controller.GetAllNotes());
                        Console.ReadKey();
                        break;
                    case 2:
                        CreateNoteMenu();
                        break;
                    case 3:
                        DeleteNoteMenu();
                        break;
                    case 4:
                        SearchNoteByTag();
                        break;
                    case 10:
                        PrintTags(Controller.GetAllTags());
                        Console.ReadKey();
                        break;
                    default:
                        return;
                }
            }


        }
        static public void DeleteNoteMenu()
        {
            Console.Clear();

            PrintNotes(Controller.GetAllNotes());
            
            int id = 0;

            while (true)
            {
                Console.Write("Укажите идентификатор заметки для удаления: ");

                if (!Int32.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Ошибка в вводе идентификатора");
                    continue;
                }
                bool flag = false;
                //Проверяем существует ли в БД заметка  с таким номером
                Note[] notes = Controller.GetAllNotes();

                for (int i = 0; i < notes.Length; i++)
                {
                    if (notes[i].Id == id)
                    {
                        flag = true;
                    }
                }

                if (flag == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Указанного идентификатора не найдено");
                }

            }


            Controller.DeleteNote(id);

        }


        /// <summary>
        /// Подменю создания новой заметки
        /// </summary>
        static public void CreateNoteMenu()
        {
            Console.Clear();

            Console.Write("Введите заголовок заметки: ");
            string title = Console.ReadLine();

            Console.Write("Введите текст заметки: ");
            string text = Console.ReadLine();

            Tag[] tags = Controller.GetAllTags();
            PrintTags(tags);

            int tag = 0;

            while (true)
            {
                Console.Write("Введите номер тега: ");
                
                if (!int.TryParse(Console.ReadLine(), out tag))
                {
                    Console.WriteLine("Введите корректное значение");
                    continue;
                }

                bool flag = false;
                //Проверяем существует ли в БД тег с таким номером
                for(int i = 0; i < tags.Length; i++)
                {
                    if(tags[i].Id == tag)
                    {
                        flag = true;
                    }
                }

                if(flag == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Указанного идентификатора не найдено");
                }
            }

            Note note = new Note(title, text, tag);
            Controller.CreateNote(note);
        }

        public static void SearchNoteByTag ()
        {

            Tag[] tags = Controller.GetAllTags();
            PrintTags(tags);

            int searchTag = 0;

            while (true)
            {
                Console.Write("Введите номер тега: ");

                if (!int.TryParse(Console.ReadLine(), out searchTag))
                {
                    Console.WriteLine("Введите корректное значение");
                    continue;
                }

                bool flag = false;
                //Проверяем существует ли в БД тег с таким номером
                for (int i = 0; i < tags.Length; i++)
                {
                    if (tags[i].Id == searchTag)
                    {
                        flag = true;
                    }
                }

                if (flag == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Указанного идентификатора не найдено");
                }
            }

            Note [] foundNotes = Controller.SearchByTag(searchTag);
            Console.Clear();
            foreach (var note in foundNotes)
            {
                note.Print();
            }
            Console.ReadKey();
        }
    }
}

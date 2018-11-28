using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeKeep
{
    public static class CLI
    {

        private static void PrintTags(Tag[] tags)
        {
            Console.Clear();

            for (int i = 0; i < tags.Length; i++)
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

        public static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в HomeKeep!");
                Console.WriteLine("1. Вывести все заметки");
                Console.WriteLine("2. Добавить заметку");
                Console.WriteLine("3. Удалить заметку");
                Console.WriteLine("4. Поиск заметки по тегу");
                Console.WriteLine("5. Редактирование заметки");
                Console.WriteLine("6. Добавление тега");
                Console.WriteLine("7. Удаление тега");
                Console.WriteLine("8. Редактирование тега");
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
                    case 5:
                        EditNoteMenu();
                        break;
                    case 6:
                        CreateTagMenu();
                        break;
                    case 7:
                        DeleteTagMenu();
                        break;
                    case 8:
                        EditTagMenu();
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

        private static void DeleteNoteMenu()
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


        private static void DeleteTagMenu()
        {
            Console.Clear();

            PrintTags(Controller.GetAllTags());

            int id = 0;

            while (true)
            {
                Console.Write("Укажите идентификатор заметки для удаления: ");

                if (!Int32.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Ошибка в вводе идентификатора");
                    continue;
                }

                if (Controller.GetTag(id).Id != 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Указанного идентификатора не найдено");
                }

            }

            Controller.DeleteTag(id);

        }


        /// <summary>
        /// Подменю создания новой заметки
        /// </summary>
        private static void CreateNoteMenu()
        {
            Console.Clear();

            Console.Write("Введите заголовок заметки (Введите Cancel для отмены): ");
            string title = Console.ReadLine();
            if (title == "Cancel")
            {
                return;
            }

            Console.Write("Введите текст заметки (Введите Cancel для отмены): ");
            string text = Console.ReadLine();

            if (text == "Cancel")
            {
                return;
            }
            Tag[] tags = Controller.GetAllTags();
            PrintTags(tags);

            int tag = 0;

            while (true)
            {
                Console.Write("Введите номер тега (Введите Cancel для отмены): ");
                string tempTag = Console.ReadLine();
                if (tempTag == "Cancel")
                {
                    return;
                }
                if (!int.TryParse(tempTag, out tag))
                {
                    Console.WriteLine("Введите корректное значение");
                    continue;
                }

                bool flag = false;
                //Проверяем существует ли в БД тег с таким номером
                for (int i = 0; i < tags.Length; i++)
                {
                    if (tags[i].Id == tag)
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

            Note note = new Note(title, text, tag);
            Controller.CreateNote(note);
        }

        /// <summary>
        /// Подменю создания нового тега
        /// </summary>
        private static void CreateTagMenu()
        {
            Console.Clear();

            Console.Write("Введите название тега (Введите Cancel для отмены): ");
            string title = Console.ReadLine();
            if (title == "Cancel")
            {
                return;
            }

            Tag tag = new Tag(0, title);
            Controller.CreateTag(tag);
        }



        private static void EditNoteMenu()
        {
            PrintNotes(Controller.GetAllNotes());


            int id = 0;
            while (true)
            {
                Console.Write("Введите номер заметки для редактирования (Введите Cancel для отмены): ");
                string tempId = Console.ReadLine();
                if (tempId == "Cancel")
                {
                    return;
                }

                if (!int.TryParse(tempId, out id))
                {
                    Console.WriteLine("Введите корректное значение");
                    continue;
                }


                if(Controller.GetNote(id).Id != 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Заметки с таким номером не существует");
                }
                
            }

            Note editedNote = Controller.GetNote(id);

            Console.Clear();

            Console.WriteLine("Исходный заголовок: " + editedNote.Name);
            Console.Write("Введите новый заголовок, либо введите Cancel для отмены, \nнажмите Enter, чтобы не изменять заголовок: ");
            string newTitle = Console.ReadLine();
            if (newTitle == "Cancel")
            {
                return;
            }
            if (newTitle.Length == 0)
            {
                newTitle = editedNote.Name;
            }

            Console.Clear();
            Console.WriteLine("Исходный текст заметки: " + editedNote.Text);
            Console.Write("Введите новый текст, либо введите Cancel для отмены, \nнажмите Enter, чтобы не изменять заголовок: ");
            string newText = Console.ReadLine();
            if (newText == "Cancel")
            {
                return;
            }
            if (newText.Length == 0)
            {
                newText = editedNote.Text;
            }

            PrintTags(Controller.GetAllTags());
            Console.WriteLine("Исходный тэг заметки:  " + Controller.GetTagName(editedNote.Tag).ToString());
            int newTag = 0;

            while (true)
            {
                Console.Write("Введите номер нового тега, либо введите Cancel для отмены, \nнажмите Enter, чтобы не изменять заголовок: ");
                string temp = Console.ReadLine();
                if (temp.Length == 0)
                {
                    newTag = editedNote.Tag;
                    break;
                }
                if (temp == "Cancel")
                {
                    return;
                }
                else
                {
                    if (!int.TryParse(temp, out newTag))
                    {
                        Console.WriteLine("Введите корректное значение");
                        continue;

                    }

                    bool flag = false;
                    Tag[] tags = Controller.GetAllTags();
                    for (int i = 0; i < tags.Length; i++)
                    {
                        if (tags[i].Id == newTag)
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
            }

            Note newNote = new Note(editedNote.Id, newTitle, newText, newTag);

            Controller.EditNote(newNote);

        }

        private static void EditTagMenu()
        {
            PrintTags(Controller.GetAllTags());


            int id = 0;
            while (true)
            {
                Console.Write("Введите номер тега для редактирования (Введите Cancel для отмены): ");
                string tempId = Console.ReadLine();
                if (tempId == "Cancel")
                {
                    return;
                }

                if (!int.TryParse(tempId, out id))
                {
                    Console.WriteLine("Введите корректное значение");
                    continue;
                }

                if(Controller.GetTag(id).Id != 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Тега с таким идентификатором не существует");
                }

            }

            Tag editedTag = Controller.GetTag(id);

            Console.Clear();

            Console.WriteLine("Исходное название: " + editedTag.Name);
            Console.Write("Введите новое название, либо введите Cancel для отмены, \nнажмите Enter, чтобы не изменять заголовок: ");
            string newTitle = Console.ReadLine();
            if (newTitle == "Cancel")
            {
                return;
            }
            if (newTitle.Length == 0)
            {
                newTitle = editedTag.Name;
            }

            Tag newTag = new Tag(id, newTitle);
            Controller.EditTag(newTag);
        }




        private static void SearchNoteByTag ()
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

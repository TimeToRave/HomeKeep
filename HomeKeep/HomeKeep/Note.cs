using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeKeep
{
    /// <summary>
    /// Класс, реализующий заметку
    /// </summary>
    public class Note
    {

        private int id;
        private string name;
        private string text;
        private int tag;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public int Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public Note ()
        {
            Id = 0;
            Name = "Empty";
            Text = "";
            Tag = 0;
        }

        /// <summary>
        /// Конструктор предназначенный для создания заметок из приложения, 
        /// идентификатор записи ставится по умолчанию равным 0, для того, 
        /// чтобы заметка получила идентификатор в базе данных
        /// </summary>
        /// <param name="name">Имя заметки</param>
        /// <param name="text">Текст</param>
        /// <param name="tag">Тег</param>
        public Note (String name, String text, int tag)
        {
            Id = 0;
            Name = name;
            Text = text;
            Tag = tag;
        }

        //TODO Сделать аккуратные конструкторы, особенно этот!
        public Note(int id, String name, String text, int tag)
        {
            Id = id;
            Name = name;
            Text = text;
            Tag = tag;
        }

        /// <summary>
        /// Конструктор для считывания заметок из базы данных, идентификатор заметки также считывается
        /// </summary>
        /// <param name="note">Строка формата "id<#>name<#>text<#>tag"</param>
        public Note (String note)
        {
            string[] words = note.Split(new string[] { "<#>" }, StringSplitOptions.RemoveEmptyEntries);

            int temp = 0;

            if (int.TryParse(words[0], out temp))
            {
                Id = temp;
                temp = 0;
            }
            else
            {
                Id = 0;
            }

            Name = words[1];
            Text = words[2];
            
            if(int.TryParse(words[3], out temp))
            {
                Tag = temp;
            }
            else
            {
                Tag = 0;
            }

        }

        public void Print ()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Id.ToString() + " ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Controller.GetTagName(Tag) + " ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(Text + "\n");
            Console.ResetColor();
        }

    }
}

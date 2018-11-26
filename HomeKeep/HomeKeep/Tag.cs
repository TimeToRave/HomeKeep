using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeKeep
{
    public class Tag
    {
        private int Id { get; set; }
        private string Name { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Tag ()
        {
            Id = 0;
            Name = "";
        }

        /// <summary>
        /// Конструктор, для создания экземпляров класса данными из БД
        /// </summary>
        /// <param name="id">Номер тега</param>
        /// <param name="name">Имя тега</param>
        public Tag (int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Конструктор, который парсит строку на данные
        /// </summary>
        /// <param name="tag">Строка вида "id<#>name</param>
        public Tag(String tag)
        {
            string[] words = tag.Split(new string[] { "<#>" }, StringSplitOptions.RemoveEmptyEntries);

            int temp = 0;
            if (Int32.TryParse(words[0], out temp))
            {
                Id = temp;
            }
            else
            {
                Id = 0;
            }

            Name = words[1];
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(Id + " ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Name);
            Console.ResetColor();
        }

    }
}

using System.Collections;
using System.IO;

namespace Balda.Data
{
    public class GameKeys
    {
        /// <summary>
        /// Список всех клавиш
        /// </summary>
        private ArrayList _keys = new ArrayList();
        /// <summary>
        /// Путь к значениям клавиш
        /// </summary>
        private string _path = @"..\..\..\Resources\Keyboard.txt";
        /// <summary>
        /// Конструктор
        /// </summary>
        public GameKeys()
        {
            ReadKeyBoard();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Список клавиш
        /// </returns>
        public ArrayList GetKeys()
        {
            return _keys;
        }
        /// <summary>
        /// Считывание значений клавиш с файла
        /// </summary>
        private void ReadKeyBoard()
        {
            string line;
            StreamReader file = new StreamReader(_path);
            while ((line = file.ReadLine()) != null)
            {
                _keys.Add(line);
            }
            file.Close();
        }
    }
}

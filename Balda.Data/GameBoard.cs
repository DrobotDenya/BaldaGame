namespace Balda.Data
{
    public class GameBoard
    {
        /// <summary>
        /// Список значений для ячеек
        /// </summary>
        public string[][] CellPool;
        /// <summary>
        /// Ширина
        /// </summary>
        private int _width;
        /// <summary>
        /// Высота
        /// </summary>
        private int _heigth;
        /// <summary>
        /// Перегруденый конструктор
        /// </summary>
        public GameBoard(int width, int heigth)
        {
            SetSize(width, heigth);
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public GameBoard()
        {
        }
        /// <summary>
        /// Установка размеров поля
        /// </summary>
        public void SetSize(int width, int heigth)
        {
            this._width = width;
            this._heigth = heigth;

            CellPool = new string[width][];
            for (int row = 0; row < heigth; row++)
            {
                CellPool[row] = new string[heigth];
                for (int column = 0; column < width; column++)
                {
                    CellPool[row][column] = string.Empty;
                }
            }
        }
        /// <returns>
        /// Ширина
        /// </returns>
        public int Width()
        {
            return this._width;
        }
        /// <returns>
        /// Высота
        /// </returns>
        public int Heigth()
        {
            return this._heigth;
        }
        /// <summary>
        /// Очистка поля
        /// </summary>
        public void Clear()
        {
            CellPool = new string[_width][];
            CellPool[0] = new string[5];
            CellPool[1] = new string[5];
            CellPool[2] = new string[5];
            CellPool[3] = new string[5];
            CellPool[4] = new string[5];
        }

        /// <param name="row">
        /// Рядок
        /// </param>
        /// <param name="column">
        /// Колонка
        /// </param>
        /// <returns>
        /// Сначение ячеейки
        /// </returns>
        ////Return value cell
        public string GetCellValue(int row, int column)
        {
            return CellPool[row][column];
        }

        /// <summary>
        /// Установка значений для ячеек
        /// </summary>
        /// /// <param name="row">
        /// Рядок
        /// </param>
        /// <param name="column">
        /// Колонка
        /// </param>
        /// /// <param name="value">
        /// Значение ячейки
        /// </param>
        public void SetCellValue(string value, int row, int column)
        {
            CellPool[row][column] = value;
        }
    }
}

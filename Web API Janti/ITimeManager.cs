namespace Web_API_Janti
{
    public interface ITimeManager
    {
        /// <summary>
        /// Возвращает время под текущий часовой пояс
        /// </summary>
        /// <returns>Текущее время в строчном представлении "dd.MM.yyyy HH:mm:ss zzz"</returns>
        public string GetTime();

        /// <summary>
        /// Задаёт часовой пояс
        /// </summary>
        /// <param name="timeZone">Место в формате "Olson timezone"</param>
        /// <returns>Возвращает результат изменения, если true - зона изменена, если false - зона не изменена</returns>
        public bool SetTimeZone(string timeZone);

        /// <summary>
        /// Конвертирует время под текущий часовой пояс
        /// </summary>
        /// <param name="timeStamp">Временная метка</param>
        /// <returns>Сконвертированное время в строчном представлении "dd.MM.yyyy HH:mm:ss zzz"</returns>
        public string ConvertDate(string timeStamp);
    }
}

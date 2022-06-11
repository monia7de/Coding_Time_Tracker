namespace coding_time_tracker
{
    internal class DurationCalculator
    {
        /// <summary>
        /// Method <c>DurationTime</c>
        /// </summary>
        /// <param name="startTimeInputValue"></param>
        /// <param name="endTimeInputValue"></param>
        /// <returns></returns>
        internal string DurationTime(string startTimeInputValue, string endTimeInputValue)
        {
            /*
            string startTime = startTimeInputValue;
            string endTime = endTimeInputValue;

            
            TimeSpan durationInput = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

            TimeOnly durationInputValue = TimeOnly.FromDateTime(durationInput);


            return durationInputValue.ToString(); //.ToString("t)
            */

            //if for <

            var startTime = TimeOnly.Parse(startTimeInputValue);  // or Parse(String,IFormatProvider, DateTimeStyles)
            var endTime = TimeOnly.Parse(endTimeInputValue); 

            TimeSpan durationInput = endTime - startTime;

            // TimeSpan duration = public static TimeSpan operator - (TimeOnly startTime, TimeOnly endTime);
            return durationInput.ToString();    



        }

        /// <summary>
        /// Method <c>PomodorosDuration</c> converts the number of pomodoros into equivalent time duration
        /// </summary>
        /// <param name="pomodorosInputValue"></param>
        /// <returns></returns>
        internal string PomodorosDuration(int pomodorosInputValue)
        {
            var totalMinutes = pomodorosInputValue * 25;
            return String.Format("{0:00}:{ 1:00}", totalMinutes / 60, totalMinutes % 60);
            
            // Console.WriteLine("{0:00}:{1:00}", totalMinutes / 60, totalMinutes % 60);   CW TO TEST
            //var time = TimeSpan.FromMinutes(totalMinutes);
            //Console.WriteLine("{0:00}:{1:00}", (int)time.TotalHours, time.Minutes);



        }
    }
}
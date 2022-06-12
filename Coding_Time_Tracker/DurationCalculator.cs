namespace coding_time_tracker
{
    internal class DurationCalculator
    {
        /// <summary>
        /// Method <c>DurationTime</c> calculates duration from start and end time and converts the duration as string
        /// </summary>
        /// <param name="startTimeInputValue"></param>
        /// <param name="endTimeInputValue"></param>
        /// <returns>string</returns>
        internal string DurationTime(TimeOnly startTimeInputValidated, TimeOnly endTimeInputValidated)
        {
           

            var diff = endTimeInputValidated - startTimeInputValidated;   //in the future do this for 12h time

            return diff.ToString(); 
            
        }

        /// <summary>
        /// Method <c>PomodorosDuration</c> converts the number of pomodoros into equivalent time duration
        /// </summary>
        /// <param name="pomodorosInputValue"></param>
        /// <returns>string</returns>
        internal string PomodorosDuration(int pomodorosInputValidated)
        {
            var totalMinutes = pomodorosInputValidated * 25;
            var pomodoroDuration = TimeSpan.FromMinutes(totalMinutes);
            return pomodoroDuration.ToString();
            
            //???how to format the above into hours and minutes?  return String.Format("{0:00}:{ 1:00}", totalMinutes / 60, totalMinutes % 60);
                      
        }
    }
}
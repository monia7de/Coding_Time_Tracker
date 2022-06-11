namespace coding_time_tracker
{
    /// <summary>
    /// Class <c>Habit</c> models a habit that will be tracked.
    /// The properties are string <value name="Name"/> and  <value name="Date"/>,
    /// Timespan <value name="Duration"/> will be provided by the user or automatically calculated.
    /// </summary>
    internal class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Duration { get; set; }


        
    }
}
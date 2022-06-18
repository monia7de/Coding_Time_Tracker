# Coding_Time_Tracker 
A CRUD console application that tracks the time devoted to coding as well as distractions. Developed with C# and SQLite. 

# Features  
* User can Insert, View, Update and Delete Records. 
* The app allows user to view their records for today and from last week. It also has the options of displaying reports for any chosen day, month and year. Reporting uses ConsoleTableExt library to display records in a more user-friendly way.  
* User can update individual properties of records selected by id.  
* The app allows entering different types of input: duration; start and stop times of a completed session, number of pomodoros and real-time tracking of a session with a stopwatch that is in the application. The input is automatically converted into duration.  

# Challenges  
* DateTime  
* 'static' keyword applied to instantiated classes without which the app started with an infinite loop with one class creating an instance of another, which in turn, tried to create an instance of the first one  
* SQL and valid formats for SQlite date and time values

# Lessons Learned  
* the importance of git commit messages  
* strftime('%Y-%m-%d', ...)
to be continued  

# Areas to Improve  
* commit messages
* spaghetti code
to be continued  



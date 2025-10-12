using System;

namespace ExerciseTracking
{
    public abstract class Activity
    {
        protected DateTime _date;
        protected int _lengthInMinutes;

        public Activity(DateTime date, int lengthInMinutes)
        {
            _date = date;
            _lengthInMinutes = lengthInMinutes;
        }

        public DateTime Date => _date;
        public int LengthInMinutes => _lengthInMinutes;

        // Abstract methods to be overridden in derived classes
        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        // Virtual method that can be overridden if needed
        public virtual string GetSummary()
        {
            return $"{_date:dd MMM yyyy} {GetType().Name} ({_lengthInMinutes} min)- Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
        }
    }
}

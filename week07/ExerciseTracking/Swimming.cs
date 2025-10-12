using System;

namespace ExerciseTracking
{
    public class Swimming : Activity
    {
        private int _laps;
        private const double LAP_LENGTH_METERS = 50.0;
        private const double METERS_TO_MILES = 0.000621371;

        public Swimming(DateTime date, int lengthInMinutes, int laps) 
            : base(date, lengthInMinutes)
        {
            _laps = laps;
        }

        public override double GetDistance()
        {
            // Distance = laps * 50 meters * conversion to miles
            return _laps * LAP_LENGTH_METERS * METERS_TO_MILES;
        }

        public override double GetSpeed()
        {
            // Speed = distance / (minutes / 60)
            return GetDistance() * 60 / _lengthInMinutes;
        }

        public override double GetPace()
        {
            // Pace = minutes / distance
            return (double)_lengthInMinutes / GetDistance();
        }
    }
}

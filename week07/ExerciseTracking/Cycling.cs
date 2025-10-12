using System;

namespace ExerciseTracking
{
    public class Cycling : Activity
    {
        private double _speed; // in mph

        public Cycling(DateTime date, int lengthInMinutes, double speed) 
            : base(date, lengthInMinutes)
        {
            _speed = speed;
        }

        public override double GetDistance()
        {
            // Distance = speed * (minutes / 60)
            return _speed * ((double)_lengthInMinutes / 60);
        }

        public override double GetSpeed()
        {
            return _speed;
        }

        public override double GetPace()
        {
            // Pace = minutes / distance
            return (double)_lengthInMinutes / GetDistance();
        }
    }
}

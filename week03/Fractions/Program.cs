using System;

class Program
{
    static void Main(string[] args)
    {
        // Create fractions using all three constructors
        Fraction f1 = new Fraction(); // 1/1
        Fraction f2 = new Fraction(5); // 5/1
        Fraction f3 = new Fraction(3, 4); // 3/4
        Fraction f4 = new Fraction(1, 3); // 1/3

        // Demonstrate getters and setters
        f1.SetTop(1);
        f1.SetBottom(1);
        int top = f2.GetTop();
        int bottom = f2.GetBottom();
        // Update f2 values to show setters then getters
        f2.SetTop(top);
        f2.SetBottom(bottom);

        // Print required outputs
        Console.WriteLine(f1.GetFractionString());
        Console.WriteLine(f1.GetDecimalValue());

        Console.WriteLine(f2.GetFractionString());
        Console.WriteLine(f2.GetDecimalValue());

        Console.WriteLine(f3.GetFractionString());
        Console.WriteLine(f3.GetDecimalValue());

        Console.WriteLine(f4.GetFractionString());
        Console.WriteLine(f4.GetDecimalValue());
    }
}
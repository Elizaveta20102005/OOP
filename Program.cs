using System;
class Sportsmen
{
    public string name = "Ben";
    public int age = 18;
    public int height = 176;
    public string sport = "box";
    public Sportsmen(string name)
    {
        this.name = name;
    }
    public Sportsmen(string name, int age) : this(name)
    {
        this.age = age;
    }
    public Sportsmen(string name, int age, int height) : this(name, age)
    {
        this.height = height;
    }

    public Sportsmen(string name, int age, int height, string sport) : this(name, age, height)
    {
        this.sport = sport;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Athlete: {name}");
        Console.WriteLine($"Age: {age}");
        Console.WriteLine($"Sport: {sport}");
        Console.WriteLine($"Height: {height}");
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Sportsmen athlete1 = new Sportsmen("John");
        athlete1.DisplayInfo();

        Sportsmen athlete2 = new Sportsmen("Arina", 22);
        athlete2.DisplayInfo();

        Sportsmen athlete3 = new Sportsmen("Nikita", 30, 176);
        athlete3.DisplayInfo();

        Sportsmen athlete4 = new Sportsmen("Liza", 28, 180, "Tennis");
        athlete4.DisplayInfo();
    }
}
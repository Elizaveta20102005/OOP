using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using DataAccess;

public class Student
{
    private string first_Name;
    private string last_Name;
    private int age;
    private double average_Grade;

    public string FirstName
    {
        get { return first_Name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Имя не может быть пустым.");
            first_Name = value;
        }
    }

    public string LastName
    {
        get { return last_Name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Фамилия не может быть пустой.");
            last_Name = value;
        }
    }

    public int Age
    {
        get { return age; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Возраст должен быть положительным.");
            age = value;
        }
    }

    public double AverageGrade
    {
        get { return average_Grade; }
        set
        {
            if (value < 0 || value > 5)
                throw new ArgumentException("Средний балл должен быть в диапазоне от 0 до 5.");
            average_Grade = value;
        }
    }

    public Student(string firstName, string lastName, int age, double averageGrade)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        AverageGrade = averageGrade;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, возраст: {Age}, средний балл: {AverageGrade}";
    }
}

public class University
{
    private  List<Student> _students = new List<Student>();

    public void AddStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student), "Студент не может быть null.");
        if (_students.Any(s => s.FirstName == student.FirstName && s.LastName == student.LastName))
            throw new InvalidOperationException("Такой студент уже существует.");

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        if (!_students.Remove(student))
        {
            throw new ArgumentException("Студент не найден в университете.");
        }
    }

    public Student FindStudent(string firstName, string lastName)
    {
        return _students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
    }

    public IEnumerable<Student> GetAllStudents()
    {
        return _students;
    }
}

namespace DataAccess
{
    public class StudentsRepository
    {
        private readonly string _filePath;

        public StudentsRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveStudents(IEnumerable<Student> students)
        {
            var jsonData = JsonSerializer.Serialize(students);
            File.WriteAllText(_filePath, jsonData);
        }

        public List<Student> LoadStudents()
        {
            if (!File.Exists(_filePath))
                return new List<Student>();

            var jsonData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Student>>(jsonData);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var university = new University();

        try
        {
            var student1 = new Student("Арина", "Куликова", 18, 4.9);
            var student2 = new Student("Елизавета", "Романова", 21, 4.1);
            var student3 = new Student("Кира", "Кузнецова", 20, 3.8);
           

            university.AddStudent(student1);
            university.AddStudent(student2);
            university.AddStudent(student3);
     

            Console.WriteLine("Список студентов:");
            foreach (var student in university.GetAllStudents())
            {
                Console.WriteLine(student);
            }

            var repo = new StudentsRepository("students.json");

            repo.SaveStudents(university.GetAllStudents());

            var loadedStudents = repo.LoadStudents();
            Console.WriteLine("\nЗагруженные студенты:");
            foreach (var student in loadedStudents)
            {
                Console.WriteLine(student);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
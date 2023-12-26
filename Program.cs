using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Создание списка студентов
        StudentList students = new StudentList();
        students.Add(new Student("Иванов Иван Иванович", "1 курс", 1000, new List<byte> { 5, 5, 5, 3, 4 }));
        students.Add(new Student("Петров Петр Петрович", "2 курс", 1000, new List<byte> { 4, 4, 4, 4, 4 }));
        students.Add(new Student("Сидоров Сидор Сидорович", "3 курс", 1000, new List<byte> { 3, 3, 3, 3, 3 }));
        students.Add(new Student("Козлова Ирина Михайловна", "1 курс", 1000, new List<byte> { 2, 2, 2, 2, 2 }));
        students.Add(new Student("Федоров Федр Семенович", "2 курс", 1000, new List<byte> { 5, 5, 5, 5, 5 }));

        // Расчет стипендий для всех студентов
        foreach (var student in students)
        {
            student.CalculateScholarship();
        }

        // Вывод списка студентов, отсортированного по ФИО
        students.SortByFullName();
        Console.WriteLine("Список студентов:");
        foreach (var student in students)
        {
            Console.WriteLine($"ФИО: {student.FullName}, Курс: {student.Course}, Стипендия: {student.Scholarship}");
        }
        Console.WriteLine();

        // Поиск двоечников на 1 курсе
        string course = "1 курс";
        List<Student> twoGraders = students.GetTwoGradersByCourse(course);
        Console.WriteLine($"Двоечники на {course}:");
        foreach (var student in twoGraders)
        {
            Console.WriteLine($"ФИО: {student.FullName}, Курс: {student.Course}, Стипендия: {student.Scholarship}");
        }
    }
}
class Student
{
    public string FullName { get; set; }
    public string Course { get; set; }
    public double Scholarship { get; set; }
    public List<byte> Grades { get; set; }

    public Student(string fullName, string course, double scholarship, List<byte> grades)
    {
        FullName = fullName;
        Course = course;
        Grades = grades;
    }

    public void CalculateScholarship() //метод для расчета стипендии
    {
        Scholarship = double.MinValue; //инициализация значением duoble.MinValue
        if (Grades.Contains(2))
        {
            Scholarship = 0;
        }
        else if (Grades.Contains(3) && !Grades.Contains(2))
        {
            Scholarship = 1000;
        }
        else if (Grades.Min() == 4)
        {
            Scholarship = 1500;
        }
        else if (Grades.All(grade => grade == 5))
        {
            Scholarship = 2500;
        }
    }
}

class StudentList : List<Student> //Класс список студентов унаследованный от списка студентов
{
    public void SortByFullName()
    {
        this.Sort((s1, s2) => string.Compare(s1.FullName, s2.FullName));
    }

    public List<Student> GetTwoGradersByCourse(string course) //Поиск двоешников 
    {
        return this.Where(s => s.Course == course && s.Grades.Contains(2)).ToList();
    }
}

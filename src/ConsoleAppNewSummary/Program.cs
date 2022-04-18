// See https://aka.ms/new-console-template for more information
//global using IOHelper;
using ConsoleAppNewSummary;

Console.WriteLine("Hello, World!");
//stringNullableTest();
RecordTest();
static void UsingTest()
{
    using (MyFile myFile = new MyFile())
    {

    }

    using MyFile myFile1 = new MyFile();

    using var fs = File.OpenWrite("./1.txt");
    using var write = new StreamWriter(fs);
    write.WriteLine("hello");
    write.Close();
    fs.Close();
    string v = File.ReadAllText("./1.txt");//exception
    Console.WriteLine(v);
}

static void stringNullableTest()
{
    Student student = new Student("michaelshen") { Age = 18 };
    Console.WriteLine(student.ToString());
    //student.Age = 19;//error
    //var lowerPhoneNumber0 = student.PhoneNumber.ToLower();//error
    //var lowerPhoneNumber1= student.PhoneNumber!.ToLower();//error
    var lowerPhoneNumber2 = student.PhoneNumber?.ToLower();
}

static void RecordTest()
{
    Person person = new Person(Id:1,"michaelshen", 18);
    Person person4 = person with { };//类似person.clone();
    Person person5 = person with { Age=19};//类似person.clone();person5.Age=19
    Person person2 = new Person(Id:1,"michaelshen", 18);
    Person person3 = new Person(Id:2,"michaelshen", 18);
    Console.WriteLine(person.ToString());
    Console.WriteLine(person == person2);//true
    Console.WriteLine(person3 == person2);//false
    Console.WriteLine(Object.ReferenceEquals(person,person2));//false
    //person.Age = 19;//error
}
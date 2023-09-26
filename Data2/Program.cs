// See https://aka.ms/new-console-template for more information


using System.Diagnostics;

class Student
{
    public int num { get; set; }
    public string name { get; set; }
    public DateTime date { get; set; }
    public bool gender { get; set; }
    public string address { get; set; }
    public Student? Next { get; set; }

    public Student(int num, string name, DateTime date, bool gender, string address, Student? next)
    {
        this.num = num;
        this.name = name;
        this.date = date;
        this.gender = gender;
        this.address = address;
        this.Next = next;
    }
}

class Address
{
    public string name { get; set; }

    public Student? Next { get; set; }
    public Address? nextAddress { get; set; }

    public Address(string name, Student? next)
    {
        this.name = name;
        this.Next = next;
    }
    public void AddStudent(Student student)
    {
        if (Next == null)
        {
            Next = student;
            Console.WriteLine("学生添加成功！");
            return;
        }
        Student? temp = Next;
        while (temp != null)
        {
            if (temp.num == student.num)
            {
                Console.WriteLine("学生已存在！");
                return;
            }
            if (temp.Next == null)
            {
                temp.Next = student;
                Console.WriteLine("学生添加成功！");
                return;
            }
            temp = temp.Next;
        }
    }
    public void deleteStudent(int num)
    {
        Student? temp = Next;
        if (temp == null)
        {
            Console.WriteLine("通讯录为空！");
            return;
        }
        if (temp.num == num)
        {
            Next = temp.Next;
            Console.WriteLine("学生删除成功！");
            return;
        }
        while (temp.Next != null)
        {
            if (temp.Next.num == num)
            {
                temp.Next = temp.Next.Next;
                Console.WriteLine("学生删除成功！");
                return;
            }
            temp = temp.Next;
        }
        Console.WriteLine("学生不存在！");
    }
    public Student? getStudent(int num)
    {
        Student? temp = Next;
        while (temp != null)
        {
            if (temp.num == num)
            {
                return temp;
            }
            temp = temp.Next;
        }
        return null;
    }
    public void show()
    {
        Console.WriteLine("通讯录名称：" + name);
        Student? temp = Next;
        if (temp == null)
        {
            Console.WriteLine("通讯录为空！");
            return;
        }
        Console.WriteLine("学号\t姓名\t出生日期\t性别\t地址");
        while (temp != null)
        {
            Console.WriteLine(temp.num+"\t"+temp.name+"\t"+Tools.outputDate(temp.date)+"\t"+Tools.getGender(temp.gender)+"\t"+temp.address);
            temp = temp.Next;
        }
    }
}

class AddressBook
{
    Address Address { get; set; }
    public AddressBook(Address address)
    {
        Address = address;
    }
    public Address? getAddress(string name)
    {
        Address? temp = Address;
        while (temp != null)
        {
            if (temp.name == name)
            {
                return temp;
            }
            temp = temp.nextAddress;
        }
        return null;
    }
    public void AddAddress(Address address)
    {
        Address? temp = Address;
        while (temp != null)
        {
            if (temp.nextAddress != null && temp.nextAddress.name == address.name)
            {
                Console.WriteLine("通讯录已存在！");
                return;
            }
            if (temp.nextAddress == null)
            {
                temp.nextAddress = address;
                Console.WriteLine("通讯录创建成功！");
                main.operateAddress(address);
                return;
            }
            temp = temp.nextAddress;
        }
    }
}
class main
{
    static AddressBook root = new AddressBook(new Address("root", null));
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("欢迎使用一个脑残用c#写的学生通讯录管理系统。");
            Console.WriteLine("请输入你要进行的操作：");
            Console.WriteLine("1.新建通讯录");
            Console.WriteLine("2.打开通讯录");
            Console.WriteLine("3.退出");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("请输入通讯录名称：");
                    string? name = Console.ReadLine();
                    while (name == null)
                    {
                        Console.WriteLine("通讯录名称不能为空，请重新输入：");
                        name = Console.ReadLine();
                    }
                    Address address = new Address(name, null);
                    root.AddAddress(address);
                    break;
                case 2:
                    Console.WriteLine("请输入要打开的通讯录名称：");
                    string? name2 = Console.ReadLine();
                    while (name2 == null)
                    {
                        Console.WriteLine("通讯录名称不能为空，请重新输入：");
                        name2 = Console.ReadLine();
                    }
                    Address? address2 = root.getAddress(name2);
                    if (address2 == null)
                    {
                        Console.WriteLine("通讯录不存在！");
                        break;
                    }
                    operateAddress(address2);
                    break;
                case 3:
                    Console.WriteLine("感谢使用！");
                    return;
                default:
                    Console.WriteLine("输入错误，请重新输入");
                    break;
            }
        }
    }
    public static void operateAddress(Address address)
    {
        Console.WriteLine("通讯录打开成功！");
        while (true)
        {
            Console.WriteLine("请输入你要进行的操作：");
            Console.WriteLine("1.新建学生信息");
            Console.WriteLine("2.删除学生信息");
            Console.WriteLine("3.修改学生信息");
            Console.WriteLine("4.查找学生信息");
            Console.WriteLine("5.显示所有学生信息");
            Console.WriteLine("6.返回上一级");
            int choice2 = Convert.ToInt32(Console.ReadLine());
            switch (choice2)
            {
                case 1:
                    Console.WriteLine("请输入学生学号：");
                    int? num = Convert.ToInt32(Console.ReadLine());
                    while (num == null)
                    {
                        Console.WriteLine("学生学号不能为空，请重新输入：");
                        num = Convert.ToInt32(Console.ReadLine());
                    }
                    Console.WriteLine("请输入学生姓名：");
                    string? name = Console.ReadLine();
                    while (name == null)
                    {
                        Console.WriteLine("学生姓名不能为空，请重新输入：");
                        name = Console.ReadLine();
                    }
                    Console.WriteLine("请输入学生出生日期：");
                    DateTime? date = Tools.inputDate();
                    while (date == null)
                    {
                        Console.WriteLine("学生出生日期不能为空，请重新输入：");
                        date = Tools.inputDate();
                    }
                    Console.WriteLine("请输入学生性别:");
                    bool gender = Tools.inputGender();
                    Console.WriteLine("请输入学生地址：");
                    string? address2 = Console.ReadLine();
                    while (address2 == null)
                    {
                        Console.WriteLine("学生地址不能为空，请重新输入：");
                        address2 = Console.ReadLine();
                    }
                    address.AddStudent(new Student((int)num, name, (DateTime)date, gender, address2, null));
                    break;
                case 2:
                    Console.WriteLine("请输入要删除的学生学号：");
                    int? num2 = Convert.ToInt32(Console.ReadLine());
                    while (num2 == null)
                    {
                        Console.WriteLine("学生学号不能为空，请重新输入：");
                        num2 = Convert.ToInt32(Console.ReadLine());
                    }
                    address.deleteStudent((int)num2);
                    break;
                case 3:
                    Console.WriteLine("请输入要修改的学生学号：");
                    int? num3 = Convert.ToInt32(Console.ReadLine());
                    while (num3 == null)
                    {
                        Console.WriteLine("学生学号不能为空，请重新输入：");
                        num3 = Convert.ToInt32(Console.ReadLine());
                    }
                    Student? student = address.getStudent((int)num3);
                    if (student == null)
                    {
                        Console.WriteLine("学生不存在！");
                        break;
                    }
                    operateStudent(student);
                    break;
                case 4:
                    Console.WriteLine("请输入要查找的学生学号：");
                    int? num4 = Convert.ToInt32(Console.ReadLine());
                    while (num4 == null)
                    {
                        Console.WriteLine("学生学号不能为空，请重新输入：");
                        num4 = Convert.ToInt32(Console.ReadLine());
                    }
                    Student? student2 = address.getStudent((int)num4);
                    if (student2 == null)
                    {
                        Console.WriteLine("学生不存在！");
                        break;
                    }
                    Console.WriteLine("学号：" + student2.num);
                    Console.WriteLine("姓名：" + student2.name);
                    Console.WriteLine("出生日期：" + Tools.outputDate(student2.date));
                    Console.WriteLine("性别:"+Tools.getGender(student2.gender));
                    Console.WriteLine("地址：" + student2.address);
                    break;
                case 5:
                    address.show();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("输入错误，请重新输入");
                    break;
            }
        }
    }
    public static void operateStudent(Student student)
    {
        while(true)
        {
            Console.WriteLine("请输入你要进行的操作：");
            Console.WriteLine("1.修改学生姓名");
            Console.WriteLine("2.修改学生出生日期");
            Console.WriteLine("3.修改学生性别");
            Console.WriteLine("4.修改学生地址");
            Console.WriteLine("5.返回上一级");
            int choice3 = Convert.ToInt32(Console.ReadLine());
            switch (choice3)
            {
                case 1:
                    Console.WriteLine("请输入学生姓名：");
                    string? name = Console.ReadLine();
                    while (name == null)
                    {
                        Console.WriteLine("学生姓名不能为空，请重新输入：");
                        name = Console.ReadLine();
                    }
                    student.name = name;
                    Console.WriteLine("学生信息修改成功！");
                    break;
                case 2:
                    Console.WriteLine("请输入学生出生日期：");
                    DateTime? date = Tools.inputDate();
                    while (date == null)
                    {
                        Console.WriteLine("学生出生日期不能为空，请重新输入：");
                        date = Tools.inputDate();
                    }
                    student.date = (DateTime)date;
                    Console.WriteLine("学生信息修改成功！");
                    break;
                case 3:
                    Console.WriteLine("请输入学生性别：");
                    bool gender = Tools.inputGender();
                    student.gender = (bool)gender;
                    Console.WriteLine("学生信息修改成功！");
                    break;
                case 4:
                    Console.WriteLine("请输入学生地址：");
                    string? address = Console.ReadLine();
                    while (address == null)
                    {
                        Console.WriteLine("学生地址不能为空，请重新输入：");
                        address = Console.ReadLine();
                    }
                    student.address = address;
                    Console.WriteLine("学生信息修改成功！");
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("输入错误，请重新输入");
                    break;
            }
        }
    }
}

static class Tools
{
    public static DateTime inputDate()
    {
        int year, month, day;
        Console.WriteLine("请输入年份：");
        year = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("请输入月份：");
        month = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("请输入日期：");
        day = Convert.ToInt32(Console.ReadLine());
        DateTime date = new DateTime(year, month, day);
        return date;
    }
    public static string outputDate(DateTime dateTime)
    {
        return dateTime.Year+"年"+dateTime.Month+"月"+dateTime.Day+"日";
    }
    public static string getGender(bool gender) {
        if (gender)
        {
            return "男";
        }
        else
        {
            return "女";
        }
    }
    public static bool inputGender()
    {
        string? gender = Console.ReadLine();
        while(gender != "man"&&gender!="woman")
        {
            Console.WriteLine("请正确输入性别：");
            gender = Console.ReadLine();
        }
        if (gender == "man")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
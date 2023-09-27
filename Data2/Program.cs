// github: https://github.com/EmptyDust/Simple_Address/
// author: EmptyDust
// description: just a homework

using Newtonsoft.Json;

class Student
{
    public int Num { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public bool Gender { get; set; }
    public string Address { get; set; }
    public Student? Next { get; set; }

    public Student(int num, string name, DateTime date, bool gender, string address, Student? next)
    {
        Num = num;
        Name = name;
        Date = date;
        Gender = gender;
        Address = address;
        Next = next;
    }
    public static DateTime setDate()
    {
        int year, month, day;
        DateTime date;
        while (true)
        {
            try
            {
                Console.WriteLine("请输入年份：");
                year = Tools.inputInt();
                while (year >= DateTime.Now.Year)
                {
                    Console.WriteLine("年份不能大于当前年份，请重新输入：");
                    year = Tools.inputInt();
                }
                Console.WriteLine("请输入月份：");
                month = Tools.inputInt();
                while (month > 12 || month < 1)
                {
                    Console.WriteLine("月份输入错误，请重新输入：");
                    month = Tools.inputInt();
                }
                Console.WriteLine("请输入日期：");
                day = Tools.inputInt();
                while (day < 1 || day > 31)
                {
                    Console.WriteLine("日期输入错误，请重新输入：");
                    day = Tools.inputInt();
                }
                date = new(year, month, day);
                break;
            }
            catch
            {
                Console.WriteLine("输入错误，请重新输入：");
            }
        }
        return date;
    }
    public static string getDate(DateTime dateTime)
    {
        return dateTime.Year + "年" + dateTime.Month + "月" + dateTime.Day + "日";
    }
    public static string getGender(bool gender)
    {
        if (gender)
        {
            return "男";
        }
        else
        {
            return "女";
        }
    }
    public static bool setGender()
    {
        string? gender = Console.ReadLine();
        while (gender != "man" && gender != "woman")
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

class Address
{
    public string Name { get; set; }

    public Student? Next { get; set; }
    public Address? NextAddress { get; set; }

    public Address(string name, Student? next)
    {
        Name = name;
        Next = next;
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
            if (temp.Num == student.Num)
            {
                Console.WriteLine("学生已存在！");
                return;
            }
            //sort
            if (temp.Next != null && temp.Next.Num > student.Num)
            {
                student.Next = temp.Next;
                temp.Next = student;
                Console.WriteLine("学生添加成功！");
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
        if (temp.Num == num)
        {
            Next = temp.Next;
            Console.WriteLine("学生删除成功！");
            return;
        }
        while (temp.Next != null)
        {
            if (temp.Next.Num == num)
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
            if (temp.Num == num)
            {
                return temp;
            }
            temp = temp.Next;
        }
        return null;
    }
    public void show()
    {
        Console.WriteLine("通讯录名称：" + Name);
        Student? temp = Next;
        if (temp == null)
        {
            Console.WriteLine("通讯录为空！");
            return;
        }
        Console.WriteLine("学号\t姓名\t出生日期\t性别\t地址");
        while (temp != null)
        {
            Console.WriteLine(temp.Num + "\t" + temp.Name + "\t" + Student.getDate(temp.Date) + "\t" + Student.getGender(temp.Gender) + "\t" + temp.Address);
            temp = temp.Next;
        }
    }
}

class AddressBook
{
    public Address Address { get; set; }
    public AddressBook(Address address)
    {
        Address = address;
    }
    public Address? getAddress(string name)
    {
        Address? temp = Address;
        while (temp != null)
        {
            if (temp.Name == name)
            {
                return temp;
            }
            temp = temp.NextAddress;
        }
        return null;
    }
    public void AddAddress(Address address)
    {
        Address? temp = Address;
        while (temp != null)
        {
            if (temp.NextAddress != null && temp.NextAddress.Name == address.Name)
            {
                Console.WriteLine("通讯录已存在！");
                main.operateAddress(temp.NextAddress);
                return;
            }
            if (temp.NextAddress == null)
            {
                temp.NextAddress = address;
                Console.WriteLine("通讯录创建成功！");
                main.operateAddress(address);
                return;
            }
            temp = temp.NextAddress;
        }
    }
    public void deleteAddress(Address address)
    {
        Address? temp = Address;
        while (temp != null)
        {
            if (temp.NextAddress != null && temp.NextAddress.Name == address.Name)
            {
                temp.NextAddress = temp.NextAddress.NextAddress;
                Console.WriteLine("通讯录删除成功！");
                return;
            }
            temp = temp.NextAddress;
        }
    }
}
class main
{
    static AddressBook? root;
    public static void Main(string[] args)
    {
        root = JsonFile.ReadFile();
        if (root == null)
            root = new AddressBook(new Address("root", null));
        while (true)
        {
            Console.WriteLine("欢迎使用一个用c#写的学生通讯录管理系统。");
            Console.WriteLine("请输入你要进行的操作：");
            Console.WriteLine("1.新建通讯录");
            Console.WriteLine("2.打开通讯录");
            Console.WriteLine("3.删除通讯录");
            Console.WriteLine("4.保存通讯录");
            Console.WriteLine("5.退出");
            int choice = Tools.inputInt();
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
                    Console.WriteLine("请输入要删除的通讯录名称：");
                    string? name3 = Console.ReadLine();
                    while (name3 == null)
                    {
                        Console.WriteLine("通讯录名称不能为空，请重新输入：");
                        name3 = Console.ReadLine();
                    }
                    Address? address3 = root.getAddress(name3);
                    if (address3 == null)
                    {
                        Console.WriteLine("通讯录不存在！");
                        break;
                    }
                    root.deleteAddress(address3);
                    break;
                case 4:
                    JsonFile.WriteFIle(root);
                    break;
                case 5:
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
            int choice2 = Tools.inputInt();
            switch (choice2)
            {
                case 1:
                    Console.WriteLine("请输入学生学号：");
                    int num = Tools.inputInt();
                    Console.WriteLine("请输入学生姓名：");
                    string? name = Console.ReadLine();
                    while (name == null)
                    {
                        Console.WriteLine("学生姓名不能为空，请重新输入：");
                        name = Console.ReadLine();
                    }
                    Console.WriteLine("请输入学生出生日期：");
                    DateTime date = Student.setDate();
                    Console.WriteLine("请输入学生性别:");
                    bool gender = Student.setGender();
                    Console.WriteLine("请输入学生地址：");
                    string? address2 = Console.ReadLine();
                    while (address2 == null)
                    {
                        Console.WriteLine("学生地址不能为空，请重新输入：");
                        address2 = Console.ReadLine();
                    }
                    address.AddStudent(new Student(num, name, date, gender, address2, null));
                    break;
                case 2:
                    Console.WriteLine("请输入要删除的学生学号：");
                    int num2 = Tools.inputInt();
                    address.deleteStudent(num2);
                    break;
                case 3:
                    Console.WriteLine("请输入要修改的学生学号：");
                    int num3 = Tools.inputInt();
                    Student? student = address.getStudent(num3);
                    if (student == null)
                    {
                        Console.WriteLine("学生不存在！");
                        break;
                    }
                    operateStudent(student);
                    break;
                case 4:
                    Console.WriteLine("请输入要查找的学生学号：");
                    int num4 = Tools.inputInt();
                    Student? student2 = address.getStudent(num4);
                    if (student2 == null)
                    {
                        Console.WriteLine("学生不存在！");
                        break;
                    }
                    Console.WriteLine("学号：" + student2.Num);
                    Console.WriteLine("姓名：" + student2.Name);
                    Console.WriteLine("出生日期：" + Student.getDate(student2.Date));
                    Console.WriteLine("性别:" + Student.getGender(student2.Gender));
                    Console.WriteLine("地址：" + student2.Address);
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
        while (true)
        {
            Console.WriteLine("请输入你要进行的操作：");
            Console.WriteLine("1.修改学生姓名");
            Console.WriteLine("2.修改学生出生日期");
            Console.WriteLine("3.修改学生性别");
            Console.WriteLine("4.修改学生地址");
            Console.WriteLine("5.返回上一级");
            int choice3 = Tools.inputInt();
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
                    student.Name = name;
                    Console.WriteLine("学生信息修改成功！");
                    break;
                case 2:
                    Console.WriteLine("请输入学生出生日期：");
                    DateTime date = Student.setDate();
                    student.Date = date;
                    Console.WriteLine("学生信息修改成功！");
                    break;
                case 3:
                    Console.WriteLine("请输入学生性别：");
                    bool gender = Student.setGender();
                    student.Gender = gender;
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
                    student.Address = address;
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
class JsonFile
{
    static string filepath = @"D:/tempJson.json";//JSON文件路径
    public static void WriteFIle(AddressBook addressBook)
    {
        string json = JsonConvert.SerializeObject(addressBook, Newtonsoft.Json.Formatting.Indented);
        //string json = JsonConvert.SerializeObject(address, Formatting.Indented);
        Console.WriteLine(json);
        File.WriteAllText(filepath, json);
    }
    public static AddressBook? ReadFile()
    {
        string json = File.ReadAllText(filepath);
        AddressBook? addressBook = JsonConvert.DeserializeObject<AddressBook>(json);
        Console.WriteLine(addressBook);
        return addressBook;
    }
}

class Tools
{
    public static int inputInt()
    {
        int? num = null;
        while (num == null)
        {
            try
            {
                num = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("输入错误，请重新输入：");
            }
        }
        return (int)num;
    }
}
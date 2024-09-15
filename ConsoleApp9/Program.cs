// See https://aka.ms/new-console-template for more information
using ConsoleApp9.Data;
using ConsoleApp9.Models;


    Console.WriteLine("Hello, World!");
using (var context = new AppDbContext())
{
    bool continueRunning = true;

    while (continueRunning)
    {
        Console.WriteLine("1. Add Employee");
        Console.WriteLine("2. Update Employee");
        Console.WriteLine("3. Remove Employee");
        Console.WriteLine("4. Exit");
        Console.Write("Choose an option: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await AddEmployeeAsync(context);
                break;
            case "2":
                await UpdateEmployeeAsync(context);
                break;
            case "3":
                await RemoveEmployeeAsync(context);
                break;
            case "4":
                continueRunning = false;
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}
        

        static async Task AddEmployeeAsync(AppDbContext context)
{
    Console.Write("Enter employee name: ");
    var name = Console.ReadLine();
    Console.Write("Enter employee position: ");
    var position = Console.ReadLine();

    var employee = new Employee
    {
        Name = name,
        Position = position
    };

    context.Employees.Add(employee);
    await context.SaveChangesAsync();
    Console.WriteLine("Employee added.");
}

static async Task UpdateEmployeeAsync(AppDbContext context)
{
    Console.Write("Enter the ID of the employee to update: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var employee = await context.Employees.FindAsync(id);
        if (employee != null)
        {
            Console.Write("Enter new name: ");
            employee.Name = Console.ReadLine();
            Console.Write("Enter new position: ");
            employee.Position = Console.ReadLine();

            await context.SaveChangesAsync();
            Console.WriteLine("Employee updated.");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }
    else
    {
        Console.WriteLine("Invalid ID format.");
    }
}

static async Task RemoveEmployeeAsync(AppDbContext context)
{
    Console.Write("Enter the ID of the employee to remove: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var employee = await context.Employees.FindAsync(id);
        if (employee != null)
        {
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            Console.WriteLine("Employee removed.");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }
    else
    {
        Console.WriteLine("Invalid ID format.");
    }
}
    
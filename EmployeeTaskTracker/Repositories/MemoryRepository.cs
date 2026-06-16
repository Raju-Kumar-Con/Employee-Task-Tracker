using EmployeeTaskTracker.Models;

namespace EmployeeTaskTracker.Repositories
{
    public static class MemoryRepository
    {
        public static List<Employee> Employees = new()
        {
            new Employee
{
    Id = 1,
    FirstName = "Raju",
    LastName = "Kumar",
    Email = "raju@gmail.com",
    Password = "123456",
    PhoneNumber = "9876543210",
    Age = 24,
    Gender = "Male",
    Hobbies = "Reading, Sports",
    DOB = new DateTime(2001, 5, 10),
    FavoriteColor = "#0d6efd",
    SkillRange = 85,
    FileName = ""
},

            new Employee
            {
                Id = 2,
                FirstName = "Priya",
                LastName = "Sharma",
                Email = "priya@gmail.com",
                Password = "123456",
                PhoneNumber = "9876543211",
                Age = 22,
                Gender = "Female",
                Hobbies = "Reading, Sports",
                DOB = new DateTime(2003, 3, 15),
                FavoriteColor = "#dc3545",
                SkillRange = 70,
                FileName = ""
            },

            new Employee
            {
                Id = 3,
                FirstName = "Amit",
                LastName = "Singh",
                Email = "amit@gmail.com",
                Password = "123456",
                PhoneNumber = "9876543212",
                Age = 26,
                Gender = "Male",
                Hobbies = "Reading, Sports",
                DOB = new DateTime(1999, 8, 20),
                FavoriteColor = "#198754",
                SkillRange = 95,
                FileName = ""
            }
        };

        public static List<EmployeeTask> Tasks = new()
{
    new EmployeeTask
    {
        Id = 1,
        Title = "Create Login Page",
        Description = "Design login UI",
        Status = TaskState.Pending,
        Deadline = DateTime.Today.AddDays(5)
    },

    new EmployeeTask
    {
        Id = 2,
        Title = "Implement Dashboard",
        Description = "Create dashboard statistics",
        Status = TaskState.InProgress,
        Deadline = DateTime.Today.AddDays(3)
    },

    new EmployeeTask
    {
        Id = 3,
        Title = "Generate Report",
        Description = "Export employee data to Excel",
        Status = TaskState.Completed,
        Deadline = DateTime.Today
    }
};
        // Assignment Requirement
        public static Dictionary<int, List<EmployeeTask>>
        EmployeeTaskMap = new();
    }
}
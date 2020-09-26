using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02PrintMember
{
    class Program
    {
        static void Main(string[] args)
        {
            User actress = new User() { FirstName = "Jennifer", LastName = "Lawrence", Age = 25};
            Project project = new Project() { ProjectId = 1, ProjectName = "Bank System", 
                                              StartDate = DateTime.Now, DueDate = DateTime.Now.AddMonths(2) };

            //ToDo : Add interface IMemberExpression and class MemberExpression
            IMemberExpression<User> userExpression = new MemberExpression<User>(actress);
            IMemberExpression<Project> projectExpression = new MemberExpression<Project>(project);

            System.Console.WriteLine("******User******");

            userExpression.PrintMember(e => e.FirstName)
                .PrintMember(e => e.LastName)
                .PrintMember(e => e.Age);

            System.Console.WriteLine("******Project******");
            projectExpression.PrintMember(e => e.ProjectId)
                .PrintMember(e => e.ProjectName)
                .PrintMember(e => e.StartDate)
                .PrintMember(e => e.DueDate);

            System.Console.ReadLine();
        }
    }
}

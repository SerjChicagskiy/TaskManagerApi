using System;
using Microsoft.EntityFrameworkCore;

namespace WebApiTaskManager.DAL.Context
{
    public class TaskManagerDbContext:DbContext
    {
        public DbSet<TaskReminder> TaskManager{get;set;}
        public DbSet<Priority> Priority{get;set;}
        public DbSet<Priority> Users{get;set;}
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options):base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<User>().Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Lastname).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Email).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Birthday).IsRequired();
            builder.Entity<User>().Property(x => x.Phone).IsRequired();
            builder.Entity<User>().Property(x => x.PhotoPath);
            builder.Entity<User>().Property(x => x.Login).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Password).IsRequired();
            builder.Entity<User>().HasAlternateKey(x => x.Login);
            builder.Entity<User>().HasMany(x => x.UserRoles).WithOne(x => x.User);

            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<Role>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Role>().Property(x => x.Name).IsRequired();
            builder.Entity<Role>().HasAlternateKey(x => x.Name);
            builder.Entity<Role>().HasMany(x => x.UserRoles).WithOne(x => x.Role);

            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId});


            Role admin = new Role {Id = 1, Name = "Admin"};
            Role user = new Role {Id = 2, Name = "User"};

            User u1 = new User
            {
                Id = 1,
                Name = "Admin",
                Email="admin@adminskiy.com",
                Birthday=new DateTime(1986,12,22),
                Phone="0777777777",
                Lastname = "Admin",
                Login = "admin",
                Password = "admin"
            };
            User u2 = new User
            {
                Id = 2,
                Name = "User",
                Lastname = "User",
                Email="user@userskiy.com",
                Birthday=new DateTime(1990,05,25),
                Phone="0999999999",
                Login = "user",
                Password = "user"
            };

            UserRole ur1 = new UserRole {UserId = 1, RoleId = 1};
            UserRole ur2 = new UserRole {UserId = 1, RoleId = 2};
            UserRole ur3 = new UserRole {UserId = 2, RoleId = 2};

            builder.Entity<Role>().HasData(admin, user);
            builder.Entity<User>().HasData(u1,u2);
            builder.Entity<UserRole>().HasData(ur1,ur2,ur3);

            builder.Entity<Priority>().ToTable("Priority");
            builder.Entity<Priority>().HasKey(x=>x.PriorityId);
            builder.Entity<Priority>()
                    .Property(x=>x.PriorityId)
                        .IsRequired()
                            .ValueGeneratedOnAdd();
            builder.Entity<Priority>()
                        .HasMany(x=>x.Tasks)
                            .WithOne(x=>x.Priority)
                                .HasForeignKey(x=>x.PriorityId);

            Priority p1=new Priority
            {
                PriorityId=1,
                PriorityName="Не важное"
            };
            Priority p2=new Priority
            {
                PriorityId=2,
                PriorityName="Важное"
            };
            Priority p3=new Priority
            {
                PriorityId=3,
                PriorityName="Очень важное"
            };
            builder.Entity<Priority>().HasData(p1,p2,p3);

            builder.Entity<TaskReminder>().ToTable("TaskReminder");
            builder.Entity<TaskReminder>().HasKey(x=>x.TaskId);
            builder.Entity<TaskReminder>()
                    .Property(x=>x.TaskId)
                        .IsRequired()
                            .ValueGeneratedOnAdd();
            builder.Entity<TaskReminder>()
                    .Property(x=>x.Title)
                        .IsRequired()
                            .HasMaxLength(30);
            builder.Entity<TaskReminder>()
                    .Property(x=>x.Description)
                        .IsRequired()
                            .HasMaxLength(200);
            builder.Entity<TaskReminder>()
                    .Property(x=>x.DateTime)
                        .IsRequired();

            builder.Entity<TaskReminder>().HasData
            (
                new TaskReminder
                {
                    TaskId=1,
                    Title="Годовщина",
                    Description="Годовщина запоя",
                    DateTime=new DateTime(2020,11,21),
                    PriorityId=3,
                    IsDone=false,
                    IsArhive=false
                },
                new TaskReminder
                {
                    TaskId=2,
                    Title="Встреча",
                    Description="Встреча одноклассников",
                    DateTime=new DateTime(2020,10,26),
                    PriorityId=1,
                    IsDone=false,
                    IsArhive=false
                },
                new TaskReminder
                {
                    TaskId=3,
                    Title="Встреча",
                    Description="Встреча анонимных алкоголиков",
                    DateTime=new DateTime(2020,11,20),
                    PriorityId=1,
                    IsDone=false,
                    IsArhive=false
                },
                new TaskReminder
                {
                    TaskId=4,
                    Title="Встреча",
                    Description="ТО авто",
                    DateTime=new DateTime(2020,10,30),
                    PriorityId=2,
                    IsDone=false,
                    IsArhive=false
                },
                new TaskReminder
                {
                    TaskId=5,
                    Title="Годовщина",
                    Description="Годовщина свадьбы",
                    DateTime=new DateTime(2021,09,11),
                    PriorityId=3,
                    IsDone=false,
                    IsArhive=false
                },
                new TaskReminder
                {
                    TaskId=6,
                    Title="Днюха",
                    Description="день рождение тёщи",
                    DateTime=new DateTime(2020,12,08),
                    PriorityId=2,
                    IsDone=false,
                    IsArhive=false
                },
                new TaskReminder
                {
                    TaskId=7,
                    Title="Днюха",
                    Description="день рождение сына",
                    DateTime=new DateTime(2021,07,23),
                    PriorityId=3,
                    IsDone=false,
                    IsArhive=false
                },
                new TaskReminder
                {
                    TaskId=8,
                    Title="Напоминалка",
                    Description="Купить продукты, иначе не пустят домой",
                    DateTime=new DateTime(2020,12,11),
                    PriorityId=3,
                    IsDone=false,
                    IsArhive=false
                },
                new TaskReminder
                {
                    TaskId=9,
                    Title="Напоминалка",
                    Description="Купить страховку",
                    DateTime=new DateTime(2021,04,05),
                    PriorityId=2,
                    IsDone=false,
                    IsArhive=false
                },
                new TaskReminder
                {
                    TaskId=10,
                    Title="Напоминалка",
                    Description="Оплатить комммуналку",
                    DateTime=new DateTime(2021,02,15),
                    PriorityId=1,
                    IsDone=false,
                    IsArhive=false
                }
            );
        }
    }
}
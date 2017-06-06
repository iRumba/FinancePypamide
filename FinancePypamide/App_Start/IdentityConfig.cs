using FinancePypamide.Diagram;
using FinancePypamide.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FinancePypamide.App_Start
{
    public class IdentityConfig
    {
    }

    public static class UserManager
    {
        public static async Task<CreateUserResult> CreateUser(string userName, string eMail, string password, string refName)
        {
            var res = new CreateUserResult()
            {
                Success = true
            };
            
            var userNameValidator = new UsernameValidator();
            var emailValidator = new EmailValidator();
            if (!await userNameValidator.Validate(userName))
            {
                res.Errors.AddRange(userNameValidator.Errors);
                res.Success = false;
            }
                
            if (!await emailValidator.Validate(eMail))
            {
                res.Errors.AddRange(emailValidator.Errors);
                res.Success = false;
            }

            if (!res.Success)
                return res;

            

            using (var context = new DatabaseContainer())
            {
                var refId = await context.Users.Where(u => u.Login.ToLower() == refName.ToLower()).FirstOrDefaultAsync();
                var newUser = context.Users.Add(new User
                {
                    Email = eMail,
                    Login = userName,
                    Password = Md5.Encrypt(password),
                    RegDate = DateTime.Now,
                });
                if (refId != null)
                    newUser.RefererId = refId;

                await context.SaveChangesAsync();
            }

            return res;
        }

        public static async Task<User> GetUser(string identity, string password)
        {
            using (var context = new DatabaseContainer())
            {
                var id = identity.ToLower();
                return await context.Users.Where(u => (u.Email.ToLower() == id || u.Login.ToLower() == id) && u.Password == Md5.Encrypt(password)).FirstOrDefaultAsync();
            }
        }

        public static async Task<bool> CheckUserName(string name)
        {

            using (var context = new DatabaseContainer())
            {
                return (await context.Users.Where(u => u.Login.ToLower() == name.ToLower()).CountAsync()) == 1;
            } 
        }
    }

    public class CreateUserResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; } = new List<string>();
    }

    public interface IValidator : IValidator<string>
    {
        
    }

    public interface IValidator<T>
    {
        List<string> Errors { get; }
        Task<bool> Validate(T value);
    }

    public class UsernameValidator : IValidator
    {
        public List<string> Errors { get; } = new List<string>();

        public async Task<bool> Validate(string value)
        {
            if (await UserManager.CheckUserName(value))
            {
                Errors.Add("Пользователь с таким именем уже существует");
                return false;
            }
            return true;
        }
    }

    public class EmailValidator : IValidator
    {
        public List<string> Errors { get; } = new List<string>();
        public async Task<bool> Validate(string value)
        {
            using (var context = new DatabaseContainer())
            {
                if (await context.Users.Where(u => u.Email.ToLower() == value.ToLower()).CountAsync() != 0)
                {
                    Errors.Add("Пользователь с таким Email уже существует");
                    return false;
                }
                return true;
            }
        }
    }

    //public class ApplicationUserManager : UserManager<User>
    //{
    //    public ApplicationUserManager(IUserStore<User> store)
    //        : base(store)
    //    {
    //    }

    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //    {
    //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
    //        // Настройка логики проверки имен пользователей
    //        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
    //        {
    //            AllowOnlyAlphanumericUserNames = false,
    //            RequireUniqueEmail = true
    //        };

    //        // Настройка логики проверки паролей
    //        manager.PasswordValidator = new PasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = true,
    //            RequireDigit = true,
    //            RequireLowercase = true,
    //            RequireUppercase = true,
    //        };

    //        // Настройка параметров блокировки по умолчанию
    //        manager.UserLockoutEnabledByDefault = true;
    //        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //        manager.MaxFailedAccessAttemptsBeforeLockout = 5;

    //        // Регистрация поставщиков двухфакторной проверки подлинности. Для получения кода проверки пользователя в данном приложении используется телефон и сообщения электронной почты
    //        // Здесь можно указать собственный поставщик и подключить его.
    //        manager.RegisterTwoFactorProvider("Код, полученный по телефону", new PhoneNumberTokenProvider<ApplicationUser>
    //        {
    //            MessageFormat = "Ваш код безопасности: {0}"
    //        });
    //        manager.RegisterTwoFactorProvider("Код из сообщения", new EmailTokenProvider<ApplicationUser>
    //        {
    //            Subject = "Код безопасности",
    //            BodyFormat = "Ваш код безопасности: {0}"
    //        });
    //        manager.EmailService = new EmailService();
    //        manager.SmsService = new SmsService();
    //        var dataProtectionProvider = options.DataProtectionProvider;
    //        if (dataProtectionProvider != null)
    //        {
    //            manager.UserTokenProvider =
    //                new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
    //        }
    //        return manager;
    //    }
    //}

}
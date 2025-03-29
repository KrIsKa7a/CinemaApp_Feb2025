namespace CinemaApp.Data.Seeding.Interfaces
{
    using Microsoft.AspNetCore.Identity;

    public interface IIdentitySeeder<TUser, TRole>
        where TUser : class, new()
        where TRole : class, new()
    {
        UserManager<TUser> UserManager { get; }

        RoleManager<TRole> RoleManager { get; }
    }
}

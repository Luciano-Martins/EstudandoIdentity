using Microsoft.AspNetCore.Identity;

namespace MvcWebIdentity.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        //aqui vamos fazer a injeção de dependencia das class UserManager e RoleManager
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManage  ;
        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManage)
        {
            _userManager = userManager;
            _roleManage = roleManage;   
        }

        // nesse método vamos criar as roles
        public async Task SeedRolesAsync()
        {
            //vamos verificar se o perfil que estou criando  não existir , se não eu instancio um objeto
            //IdentityRole, em seguida eu crio meu perfil de User
            if ( ! await _roleManage.RoleExistsAsync("User"))
            {
                //estamos criando um perfil de User
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "USER";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
                
                IdentityResult result = await _roleManage.CreateAsync(role);
            }

            if (!await _roleManage.RoleExistsAsync("Admin"))
            {
                //estamos criando um perfil de Admin
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _roleManage.CreateAsync(role);
            }

            if (!await _roleManage.RoleExistsAsync("Gerente"))
            {
                //estamos criando um perfil de Gerente
                IdentityRole role = new IdentityRole();
                role.Name = "Gerente";
                role.NormalizedName = "GERENTE";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _roleManage.CreateAsync(role);
            }
        }


        // nesse método vamos criar os usuarios e atribui-lo a role
        public async Task SeedUsersAsync()
        {
            // nesse método , vamos verificar se o e-mail do usuario que quero criar já existe,
            // se não existir , ai vou criar esse usuario e incluo a senha do usuario
            //testo se ele é igual a null, se sim eu instancio IdentityUser e atribuo os valores
            if (await _userManager.FindByEmailAsync("usuario@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();
                //já podemos criar o usuário com esses dados e criar a senha que já estamos atribuindo já no padrões
                IdentityResult result = await _userManager.CreateAsync(user , "Caracas200@");
                //se for bem sucedido , 
                //aqui o usuário é atribuido em uma role criada no método SeedRolesAsync
                if (result.Succeeded)
                {
                    //aqui eu atribuo o usuário a role  "User"
                    await _userManager.AddToRoleAsync(user, "User");
                    
                }
            }
            if (await _userManager.FindByEmailAsync("admin@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();
                //já podemos criar o usuário com esses dados e criar a senha que já estamos atribuindo já no padrões
                IdentityResult result = await _userManager.CreateAsync(user, "Caracas200@");

                //aqui o usuário é atribuido em uma role criada no método SeedRolesAsync
                if (result.Succeeded)
                {
                    //aqui eu atribuo o usuário a role  "Admin"
                    await _userManager.AddToRoleAsync(user, "Admin");

                }
            }
            if (await _userManager.FindByEmailAsync("gerente@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "gerente@localhost";
                user.Email = "gerente@localhost";
                user.NormalizedUserName = "GERENTE@LOCALHOST";
                user.NormalizedEmail = "GERENTE@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();
                //já podemos criar o usuário com esses dados e criar a senha que já estamos atribuindo já no padrões
                IdentityResult result = await _userManager.CreateAsync(user, "Caracas200@");

                //aqui o usuário é atribuido em uma role criada no método SeedRolesAsync
                if (result.Succeeded)
                {
                    //aqui eu atribuo o usuário a role  "Gerente"
                    await _userManager.AddToRoleAsync(user, "Gerente");

                }
            }

        }
    }
}

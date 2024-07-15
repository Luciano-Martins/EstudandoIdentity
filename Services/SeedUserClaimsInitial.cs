
using Microsoft.AspNetCore.Identity;
using System.Drawing.Text;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MvcWebIdentity.Services
{
    public class SeedUserClaimsInitial : ISeedUserClaimsInitial
    {
        private readonly UserManager<IdentityUser> _userManager;

        public SeedUserClaimsInitial(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedUserClaims()
        {
            try
            {
                //Usuario 1 
                //vamos atribuir a claim CadastradoEm para outros usuarios 
                //verifico se este usuario existe , 
                IdentityUser user1 = await _userManager.FindByEmailAsync("gerente@localhost");
                if (user1 is not null)
                {
                    //se ele existe eu aqui obtenho as claims desse usuario
                    var claimsList = (await _userManager.GetClaimsAsync(user1)).Select(c => c.Type);
                    //agora verifico se ele tem a claim no cadastro dele
                    if (!claimsList.Contains("CadastradoEm"))
                    {
                        //se ele não contiver a claim eu incluo a claim e atribuo um valor
                        var claimResult = await _userManager.AddClaimAsync(user1, new Claim("CadastradoEm", "03/03/2021"));
                    }


                }
                IdentityUser user2 = await _userManager.FindByEmailAsync("usuario@localhost");
                if (user2 is not null)
                {
                    //se ele existe eu aqui obtenho as claims desse usuario
                    var claimsList = (await _userManager.GetClaimsAsync(user2)).Select(c => c.Type);
                    //agora verifico se ele tem a claim no cadastro dele
                    if (!claimsList.Contains("CadastradoEm"))
                    {
                        //se ele não contiver a claim eu incluo a claim e atribuo um valor
                        var claimResult = await _userManager.AddClaimAsync(user2, new Claim("CadastradoEm", "01/01/2020"));
                    }


                }
                IdentityUser user3 = await _userManager.FindByEmailAsync("lucianomarcosmartins925@gmail.com");
                if (user3 is not null)
                {
                    //se ele existe eu aqui obtenho as claims desse usuario
                    var claimsList = (await _userManager.GetClaimsAsync(user3)).Select(c => c.Type);
                    //agora verifico se ele tem a claim no cadastro dele
                    if (!claimsList.Contains("CadastradoEm"))
                    {
                        //se ele não contiver a claim eu incluo a claim e atribuo um valor
                        var claimResult = await _userManager.AddClaimAsync(user3, new Claim("CadastradoEm", "02/02/2017"));
                    }


                }


            }
            catch
            {
                throw;
            }
          
        }
    }
}

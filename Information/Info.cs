namespace MvcWebIdentity.Information
{
    public class Info
    {
        /* 
         * 
            Estamos criando Autorização e autenticação utilizando o Identity

        1°  criei um projeto mvc , depois adicionei todos os pacotes necessarios
        2°  depois crei uma Model aluno com sua propriedades
        3°  depois criei meu DbContext e meu DbSet para cria a tabela no banco
        4°  em seguida criei o serviço AddDbContext na classe program ,para especifcar qual banco vou usar
        5°  criei a string de conexão no arquivo appSettings.json e adicionei a referencia na program
        6°  agora foi só criar a migration nuget package manager
        7º  depois de tudo isso, criamos a controller alunos  usando a criação com mvc controller com views usando 
        EntityFrameWorkCore. dessa forma ele já cria um crud completo.
        8°  agora vamos adicionar os pacotes para EntityFrameworkCore.Identity, que são dois.
        9°  feito isso adicionamos as referencias e Serviços , no contexto adicionamos IdentityDbContext r nos Serviços 
        adicionamos o serviço para Identity
        10°  na controler Alunos adicionamos do dataAnotation a Atorização da Classe alunos [Authorize]
        11°  esse atributo Authorize pode ser utilizados  tanto nas classes como tambem em métodos actions, posso tambem
        definir o atributo Authorize para definir as roles , vamos ao exemplo [Authorize(Roles="Admin"])
        12°  depois de tudo isso , vamos agora começar a fazer a autenticação e autorização ,
        vamos criar uma controller chamada AccountController
        com 3 ações 
        -Register (Get/Post) , para Registrar o usuário
        -Login (Get/Post), para logar o usuário
        -Logout,           para sair do sistema
        
           - 1° 
              para criar o usuário eu precisso dessa classe
              - UserManager<IdentityUser> - é responsavel por gerenciar as operações de Criação, leitura,atualização
            e exclusão de Usuarios.
           - 2° 
              para fazer login precisso dessa classe.
              - SignInManage<IdentityUser> é resposanvel por gerenciar o processo de autenticação de usuarios em um
            aplicativo.

        13° Agora vamos criar uma model chamada Registro , com 3 proriedades 
          - Email
          - password
          - confirmedPassword
        14° Após isso , na conttroller Account  , vamos injetar dois tipos , a UserManage , SignInManage , e criar os Métodos.
        15° após isso, falou criar as views , ao invés de criar as views na unha , vamos criar um recurso do
        Identity com o scafouding.
        16°  clicando com o botão direito no prrojeto , eu tenho a opção , new scaffolded Item , vai abrir a janela 
        de adicionar o item , no painel a esquerda , selecione a opção Identity, em seguida ele vai apresentar todas as 
        opções de criação do Identity, vamos adicionar a opção Account\Login e a Account\Register , lembrando que temos
        muitas paginas disponiveis. só lembrando que voce pode fazer essas views na Unha.
        17° agora que tudo foi criado , precisamos acrecentar no menu uma forma do usuário fazer login , novamente utilizando
        um recursor do razor, na view Shared, com o botão direito vamos adicionar uma view razor , colocar o nome , e 
        marcar como partial , ele vai criar uma view na pasta Shared,onde vamos criar um 
        componente separado com um link para register e outro Login , dai vamos invocar o componente na shared, usando 
         a Partial . so lembrando que a partial view é uma forma de Reutilizar codigos no AsNetCore, é um especie de 
        subconjunto de uma view, que pode ser renderizada dentro de outra view.
        para injetar esse novo controle na shared uso a sintax , <partial name="nome da pagina"/>
        18°  Nessa partial que foi criada , vamos injetar as classe do Identity para ser utilizadas
        {
          @using Microsoft.AspNetCore.Identity
          @inject SignInManage<IdentityUser> SignInManage
        }


        *********************************************************************************************************
        Agora vamos usar autenticação , baseada em Roles , criaremos as Roles e Usuarios
        
     1º Foi criado Uma Pasta services para a criação de interface ISeedUserRoleInitial para o contrato e  a classe
       SeedUserRoleInitial, onde criamos dois métodos para criação de usuario e Roles utilizando as classes do Identity
       são elas UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManage.
     2° os dois método para criação foram 
        Task SeedRolesAsync();
        Task SeedUsersAsync();
     3° depois registramos  os serviços e criamos um método na program para a criação das roles e usuarios na execução
        da aplicação pela primeira vez.
     4° agora fomos nos método de endpoints e colocamos autorização (permissão). utilizando o atributo [Autorize]
                  
     ******************************************************************************************************************
     
        Vamos fazer a ultima version 
     1° vamos começar a utilizando o sconffoding para criar uma area dentro do projeto, click com o botão direito do mouse
        na opção Add => new scaffolder item => Cammon => MVC Area  o scaffolding vai cria a area dentro do projeto.
     2° vamos a classe program e  vamos definir o mapeamento da rota da area

          app.UseEndpoints(endpoints =>
        {
          endpoints.MapControllerRoute(
            name : "areas",
            pattern : "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
          );
        });

        se eu não definir este mapeamento não consigo ter acesso ao conteído da minha area , detalhe : tenho que adicionar
        logo acima do mapeamento do projeto.

     3° Vamos criar o controlador na controller da area , e´importante lembrar que todo controlador criado na area,você 
        precisa especificar que  o controllador pertence a area  [Area("Admin")] se você esquecer de fazer isso não vai 
        conseguir acessar nada . agora é só continuar a criar as view normalmente
     4° agora criamos uma nova partial  chamada _LoginAdmin, nesta partial vamos testar se usuário esta logado
        e se ele tem a role Admin, se sim mostramos o link para a area administrador
     5° feito isso agora vamos começar a criar as  controllers para gerenciar a roles e usuarios


        ************************************************************************************************
        
          Vamos implementar autorização baseada em Claims


     1°  primeiro vamos criar uma entidade chamada Produto  e criar nossa controler com o scaffoding
     2°  vamos até a service e criar uma interface e uma class para o método que vai gerar as claims
     3°  agora com o contrato e o método criado , vamos na program criar o método que vai criar as claims na primeira
        inicialização do sistema.
     4°  já na program vamos primeiramente registrar o serviço IseedUserClaimsInitial e a class SeedUserClaimsInitial
     5°  agora vamos comentar a chamada do metodo de criação de roles e vamos obetr a instancia do serviço
         IseedUserClaimsInitial
     6º  após roda o sistema , já temos nossas claims , agora é só criar as politica de autorização baseada em claims
     7°  agora vamos parar o sistema e voltaremos a class program , para configurar as pliticas 
        
        builder .Services.AddAuthorization(option => 
        {
           option.AddPolicy("RequireUserAdminGerenteRole", policy => policy.RequireRole("User","Admin","Gerente"));
        });
     8° após implementarmos esse serviço , é só implementar as authorization nos endpoints.

        ***********************************************************************************************
          Nesta version , criaremos politicas personalizadas baseada em claims, 
        
     1°  primera coisa a se fazer é
        Criar uma classe que implementa a interface IAuthorizationRequirement, essa classe deve representar
        a regra de autorização que vc deseja aplicar ao recurso.
     2° Criar uma classe que herde da classe AuthorizationHandler<T>, ONDE t é o tipo da sua classe 
        que implementa IAthorizationRequirement, está classe será responsável por avaliar se a regra 
        de autorização definida pela sua interface IAuthorizationRequirement é atendida
     3° após isso , vamos registrar sua politica de autorização personalizadadno serviço de 
        autorização da ASP.NetCore usando o mètodo AddPolicy da classe AuthorizationOptins
     4° agora é só aplicar a sua politica de autorização personalizada, usando as anotações [Authorize]
        em seus métodos de controlador ou rotas de aplicativos.
        Agora vamos crir um politica personalizada com base na claim CadastrdoEm essa claim foi atribuida ao 
        admin@localhost na ultima versão
        Definir uma politica onde vamos verificar o tempo minimo de cadastro com base no valor definido
        na claim CadastroEm
        ou seja , só vai ter acesso ao endpoint que tiver no minimo 5 anos de cadastro

     Resumo: para acessar os enpoints , fizemos politicas baseadas em Claims Personalizadas, ou seja criamos
        a politica TempoCadastroMinimo e adicionamos a ela a claim CadastradoEm , e colocamos a regra onde somente
        cadastros com 5 anos de cadastro terá acesso ao endpoint...  e ainda podemos combinar a politica com as
        roles ou seja , definimos o endpoint para acesso somente da politica estabelecida e aind a role Admin.


        *******************************************************************************************************
        
           Gerenciando Claims - Razor Views
         aqui vamos começar a criar claims e gerenciar , poderemos criar , editar e excluir as claims.

    





         
         
         
         
         
         
         
         */
    }
}

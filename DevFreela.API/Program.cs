using DevFreela.Infrastructure.Persistence; // Importa o contexto do banco de dados
using Microsoft.EntityFrameworkCore; // Importa para configuração do Entity Framework
using Microsoft.OpenApi.Models; // Importa para configuração do Swagger
using Microsoft.AspNetCore.Builder; // Importa para configuração do pipeline de middleware
using Microsoft.Extensions.DependencyInjection; // Importa para configuração de serviços
using Microsoft.Extensions.Hosting; // Importa para configuração do ambiente de hospedagem
using MediatR; // Importa para configuração do Mediator
using DevFreela.Application.Commands.CreateProject; // Importa comando de criação de projeto
using DevFreela.Core.Repositories; // Importa repositórios principais
using DevFreela.Infrastructure.Persistence.Repositories; // Importa implementações de repositórios
using FluentValidation; // Importa para validação de modelos
using FluentValidation.AspNetCore; // Importa para integração com o FluentValidation
using DevFreela.Application.Validators; // Importa validadores de comandos
using DevFreela.API.Filters; // Importa filtro de validação global
using DevFreela.Core.Services; // Importa serviços principais
using Microsoft.AspNetCore.Authentication.JwtBearer; // Importa para autenticação JWT
using Microsoft.IdentityModel.Tokens; // Importa para configuração de tokens JWT
using System.Text; // Importa para manipulação de texto
using System; // Importa para uso geral
using System.IO;
using Microsoft.Extensions.Configuration;
using DevFreela.Infrastructure.Auth; // Importa para manipulação de arquivos

var builder = WebApplication.CreateBuilder(args);

// Carregando as configurações do arquivo appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Configuração do banco de dados
builder.Services.AddDbContext<DevFreelaDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DevFreelaCs")));

// Registro de repositórios
builder.Services.AddScoped<IProjectRepository, ProjectRepository>(); // Registra o repositório de projetos
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Registra o repositório de usuários
builder.Services.AddScoped<ISkillRepository, SkillRepository>(); // Registra o repositório de habilidades
builder.Services.AddScoped<IAuthService, AuthService>(); // Registra o serviço de autenticação 

// Adiciona um filtro de validação global a todos os controladores da aplicação.
builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));

// Habilita a validação automática do FluentValidation no ASP.NET Core.
builder.Services.AddFluentValidationAutoValidation();

// Adiciona adaptadores para a validação do lado do cliente usando FluentValidation.
builder.Services.AddFluentValidationClientsideAdapters();

// Adiciona validadores da mesma assembly que contém os validadores para os comandos CreateUser e CreateProject.
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectCommandValidator>();

// Adiciona serviços para controladores
builder.Services.AddControllers();

// Adiciona serviços para explorador de API
builder.Services.AddEndpointsApiExplorer();


// Mediator
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(CreateProjectCommand).Assembly); });
// Configura o Mediator



// Configuração do Swagger
builder.Services.AddSwaggerGen(option =>
{
    // Configurações básicas do Swagger
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevFreela.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            // Contatos
            Name = "LSdev",
            Email = "larasantanadev@gmail.com"
        }
    });




    // Configuração de segurança JWT para o Swagger
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization", // Nome do parâmetro de autorização
        Type = SecuritySchemeType.ApiKey, // Tipo do esquema de segurança (API key)
        Scheme = "Bearer", // Esquema de autenticação (Bearer)
        BearerFormat = "JWT", // Formato do token (JWT)
        In = ParameterLocation.Header, // Localização do token (no cabeçalho)
        Description = "JWT Authorization header usando o esquema Bearer." // Descrição do esquema de autenticação
    });




    // Adiciona requisito de segurança para todas as operações do Swagger
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        // Define o esquema de segurança a ser utilizado
        new OpenApiSecurityScheme
        {
            // Define uma referência ao esquema de segurança anteriormente definido
            Reference = new OpenApiReference
            {
                // Especifica o tipo de referência como SecurityScheme
                Type = ReferenceType.SecurityScheme,
                // Identifica o esquema de segurança a ser utilizado, que neste caso é "Bearer"
                Id = "Bearer"
            }
        },
        // Define as permissões necessárias para acessar as operações protegidas pelo esquema de segurança
        new string[] {}  // Recebe uma string vazia
    }
});



    // Incluindo os comentários XML para documentação do Swagger
    var xmlFile = "DevFreela.API.xml"; // Nome do arquivo XML de documentação
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // Caminho completo do arquivo XML
    option.IncludeXmlComments(xmlPath); // Inclui os comentários XML na documentação do Swagger
});




// Configuração de AUTENTICAÇÃO JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Define o esquema de autenticação padrão como JWT Bearer
    .AddJwtBearer(options => // Configura o JWT Bearer
    {
        options.TokenValidationParameters = new TokenValidationParameters // Define os parâmetros de validação do token JWT
        {
            ValidateIssuer = true, // Indica se deve validar o emissor (issuer) do token
            ValidateAudience = true, // Indica se deve validar o destinatário (audience) do token
            ValidateLifetime = true, // Indica se deve validar a validade do token
            ValidateIssuerSigningKey = true, // Indica se deve validar a chave de assinatura do emissor (issuer) do token


            ValidIssuer = configuration["Jwt:Issuer"], // Define o emissor (issuer) válido do token, lendo do arquivo de configuração
            ValidAudience = configuration["Jwt:Audience"], // Define o destinatário (audience) válido do token, lendo do arquivo de configuração
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])) // Define a chave de assinatura válida para verificar a autenticidade do token, lendo do arquivo de configuração
        };
    });






var app = builder.Build();

// Configure o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    // Habilitando o Swagger para ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para redirecionamento HTTPS e autorização
app.UseHttpsRedirection();


// Autenticação para processar e validar os tokens de autenticação recebidos nas requisições
app.UseAuthentication();
// Autorização para validar as políticas de autorização definidas nos endpoints da aplicação
app.UseAuthorization();



// Mapeamento de controladores
app.MapControllers();

// Inicia a aplicação
app.Run();

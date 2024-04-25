using DevFreela.Core.Services; // Importa o namespace do serviço de autenticação da aplicação
using Microsoft.Extensions.Configuration; // Importa o namespace para acessar configurações da aplicação
using Microsoft.IdentityModel.Tokens; // Importa o namespace para lidar com tokens JWT
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevFreela.Infrastructure.Auth
{
    // Classe responsável por fornecer funcionalidades de autenticação JWT
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration; // Configuração da aplicação

        // Construtor da classe AuthService, recebe uma instância de IConfiguration
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration; // Inicializa a configuração da aplicação
        }



        // Método para gerar um token JWT com base no email e no papel (role) do usuário
        public string GenerateJwtToken(string email, string role)
        {
            // Recupera as configurações relacionadas ao JWT do arquivo de configuração - JSON.SETTINGS
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];

            // Define uma chave de segurança a partir da chave secreta definida nas configurações
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // Define as credenciais de assinatura usando a chave de segurança e o algoritmo HmacSha256
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Define as reivindicações (claims) do token, incluindo o email e o papel (role) do usuário
            var claims = new List<Claim>
            {
                 new Claim("userName", email),
                 new Claim(ClaimTypes.Role, role)
            };

            // Cria um novo token JWT com as informações fornecidas
            var token = new JwtSecurityToken(
                issuer: issuer, // Emissor do token
                audience: audience, // Audiência do token
                expires: DateTime.Now.AddHours(8), // Data de expiração do token (8 horas a partir de agora)
                signingCredentials: credentials, // Credenciais de assinatura
                claims: claims); // Reivindicações do token

            // Cria um manipulador de token JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            // Escreve o token JWT como uma string
            var stringToken = tokenHandler.WriteToken(token);

            // Retorna a string que representa o token JWT gerado
            return stringToken;
        }




        // Este método calcula o hash SHA256 de uma senha fornecida e retorna o hash resultante como uma string hexadecimal.
        public string ComputeSha256Hash(string password)
        {
            // Cria uma instância do algoritmo de hash SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Calcula o hash SHA256 dos bytes da senha (convertidos para UTF-8)
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Inicializa um StringBuilder para armazenar o hash em formato de string hexadecimal
                StringBuilder builder = new StringBuilder();

                // Converte cada byte do hash para uma representação hexadecimal e anexa ao StringBuilder
                for (int i = 0; i < bytes.Length; i++)
                {

                    // Converte o byte atual do hash para uma representação hexadecimal de dois caracteres
                    // e adiciona ao StringBuilder
                    builder.Append(bytes[i].ToString("x2"));
                }

                // Retorna o hash SHA256 como uma string hexadecimal
                return builder.ToString();
            }
        }




    }
}



// JSON Web Token (JWT) é um padrão aberto (RFC 7519) que define um formato compacto e autocontido 
// para transmitir informações com segurança entre partes como um objeto JSON. 
// Este token é composto por três partes: o cabeçalho (header), o payload (claims) e a assinatura (signature).
// Ele é frequentemente usado para autenticação e autorização em aplicações web e APIs.
// Os tokens JWT são assinados digitalmente usando um algoritmo criptográfico e podem ser facilmente 
// verificados para garantir sua integridade. Eles também podem ser criptografados para proteger 
// informações sensíveis.
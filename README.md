# DevFreelancer 🖥️
Uma API onde os usuários podem se cadastrar como Freelancers e/ou Clientes. Clientes podem publicar projetos, fornecendo informações iniciais como título, descrição e valor. Freelancers podem se candidatar para executar esses projetos.

<h2>Tecnologias ⌨️</h2>
<ul>
  <li>ASP.NET Core 8</li>
  <li>Entity Framework Core</li>
  <li>SQL Server</li>
  <li>XUnit</li>
  <li>Autenticação e Autorização com JWT Bearer</li>
</ul>

<h2>Funcionalidades ⚙️</h2>
<ul>
  <li>Cadastro de usuários (Cliente e Freelancer)</li>
  <li>Login de usuários utilizando autenticação e autorização</li>
  <li>CRUD (Create, Read, Update, Delete) de Projetos, onde apenas o Cliente tem as permissões de criação, edição e exclusão do projeto</li>
  <li>Adicionar comentários ao projeto (Clientes e Freelancers podem deixar comentários para comunicação sobre o projeto)</li>
  <li>Status do projeto - [Start e Finish]</li>
</ul>

<h2>Padrões, Conceitos e Arquitetura 📂</h2>
<ul>
  <li>Padrão Repository</li>
  <li>Arquitetura Limpa</li>
  <li>CQRS</li>
  <li>Fluent Validation para validação de API</li>
  <li>Testes Unitários utilizando XUnit, e a Biblioteca NSubstitute</li>
</ul>

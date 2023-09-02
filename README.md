# Aplicação de Exemplo ASP.NET Core

Este repositório contém o código-fonte de uma aplicação ASP.NET Core que demonstra como configurar um aplicativo web básico usando o framework ASP.NET Core. O aplicativo apresenta algumas funcionalidades, como sessões na memória, acesso a banco de dados SQL Server e injeção de dependência.

## Configuração

Antes de executar a aplicação, é necessário configurar o banco de dados SQL Server e a string de conexão correspondente. Siga as etapas abaixo para configurar o ambiente:

1. **String de Conexão**: Abra o arquivo `appsettings.json` no projeto e substitua a string de conexão no campo `"Default"` pelo seu próprio caminho de conexão com o banco de dados SQL Server.

```json
"ConnectionStrings": {
    "Default": "sua_string_de_conexao_aqui"
}
```

2. **Banco de Dados**: Certifique-se de que o banco de dados SQL Server esteja configurado corretamente e acessível a partir da aplicação. Certifique-se de que o Entity Framework Core seja configurado para funcionar com o seu banco de dados.

## Funcionalidades

A aplicação de exemplo possui as seguintes funcionalidades:

- **Sessões na Memória**: A aplicação utiliza sessões na memória para manter o estado do usuário durante a navegação. Isso é configurado em `Startup.cs`.

- **Injeção de Dependência**: A injeção de dependência é usada para registrar serviços e suas implementações. Isso é configurado em `Startup.cs`.

- **Acesso ao Banco de Dados**: A aplicação acessa um banco de dados SQL Server para armazenar dados. O contexto do banco de dados é configurado em `Startup.cs`, e a inicialização do banco de dados é feita em `Configure` usando o serviço `IDataService`.

- **Roteamento de URL**: A aplicação define rotas de URL usando o roteamento MVC em `Configure` em `Startup.cs`.

- **Ambiente de Desenvolvimento**: A aplicação detecta o ambiente de desenvolvimento e configura as páginas de erro e outras configurações apropriadas em `Configure` em `Startup.cs`.

## Executando a Aplicação

Após configurar o ambiente, você pode executar a aplicação seguindo estas etapas:

1. Abra o projeto em sua IDE (por exemplo, Visual Studio ou Visual Studio Code).

2. Compile e execute o aplicativo.

3. Acesse a aplicação em seu navegador usando a URL padrão (geralmente `http://localhost:porta`).

## Contribuições

Sinta-se à vontade para contribuir com melhorias, correções de bugs ou novos recursos para esta aplicação de exemplo. Basta criar um fork deste repositório, fazer suas alterações e enviar uma solicitação pull.

# Koda API

> **Tema:** O Futuro do Trabalho - Upskilling & Reskilling para 2030+

## Descrição do Projeto

A **Koda** é uma API RESTful desenvolvida para gerenciar uma plataforma de educação continuada. No contexto de transformação digital impulsionada por IA e automação, nossa solução visa facilitar o **Reskilling** (requalificação) e **Upskilling** (aperfeiçoamento) de profissionais.

A API permite o gerenciamento de **Trilhas de Aprendizagem** focadas em competências do futuro (como IA Generativa, ESG e Soft Skills) e o cadastro de **Usuários** que buscam se preparar para o mercado de trabalho de 2030.

**Destaques da Solução:**

  * Alinhamento com ODS 4 (Educação de Qualidade) e 8 (Trabalho Decente).
  * Arquitetura desacoplada e escalável.
  * Controle de versionamento de API.

-----

## Tecnologias Utilizadas

  * **Linguagem:** C\# (Platforma .NET 9)
  * **Framework:** ASP.NET Core Web API
  * **Banco de Dados:** Oracle Database
  * **ORM:** Entity Framework Core 9.0
  * **Documentação:** Swagger (Swashbuckle)
  * **Versionamento:** Asp.Versioning.Mvc
  * **Arquitetura:** Camadas (DDD Simplificado: Controller, Service, Repository)

-----

## Configuração e Execução

### 1\. Pré-requisitos

  * [.NET SDK 9.0](https://dotnet.microsoft.com/download) instalado.
  * Acesso a um banco de dados Oracle (Local ou Cloud/FIAP).
  * Visual Studio 2022 ou VS Code.

### 2\. Clonar e Restaurar Dependências

Abra o terminal na pasta raiz do projeto e execute:

```bash
# Restaura os pacotes NuGet definidos no projeto
dotnet restore
```

### 3\. Configurar o Banco de Dados

Abra o arquivo `SkillBridge.Api/appsettings.json` e configure sua Connection String do Oracle:

```json
"ConnectionStrings": {
  "OracleConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SID=ORCL)));"
}
```

### 4\. Rodar Migrações (Migrations)

O projeto utiliza Entity Framework Core. Para criar a estrutura do banco (caso não tenha criado via script SQL), execute:

```bash
# Gera o histórico de migração (InitialCreate)
dotnet ef migrations add InitialCreate --project SkillBridge.Infrastructure --startup-project SkillBridge.Api

# Aplica as tabelas no banco de dados
dotnet ef database update --project SkillBridge.Infrastructure --startup-project SkillBridge.Api
```

*(Obs: Se você já rodou o script SQL manual fornecido, o comando `database update` pode ser pulado).*

### 5\. Executar a Aplicação

Para subir a API:

```bash
dotnet run --project SkillBridge.Api
```

A aplicação iniciará (geralmente em `http://localhost:5006` ou porta similar). O Swagger abrirá automaticamente se estiver em ambiente de desenvolvimento.

-----

## Documentação da API (Endpoints)

A API utiliza versionamento. A base da URL é: `/api/v1/`.

### 🛤️ Recurso: Trilhas (`/api/v1/trilhas`)

  * **GET** `/api/v1/trilhas`
      * Retorna todas as trilhas cadastradas.
  * **GET** `/api/v1/trilhas/{id}`
      * Retorna uma trilha específica.
      * *Erro:* Retorna 404 com mensagem customizada se não encontrar.
  * **POST** `/api/v1/trilhas`
      * Cria uma nova trilha.
      * **Payload JSON (Exemplo):**
        ```json
        {
          "nome": "IA Generativa para Negócios",
          "descricao": "Curso focado em LLMs e produtividade.",
          "nivel": "INTERMEDIARIO",
          "cargaHoraria": 40,
          "focoPrincipal": "Tecnologia"
        }
        ```
  * **PUT** `/api/v1/trilhas/{id}`
      * Atualiza uma trilha existente.
  * **DELETE** `/api/v1/trilhas/{id}`
      * Remove uma trilha.

### Recurso: Usuários (`/api/v1/usuarios`)

  * **GET** `/api/v1/usuarios`
      * Lista todos os usuários.
  * **POST** `/api/v1/usuarios`
      * Cadastra um usuário.
      * *Validação:* Não permite e-mails duplicados.
      * **Payload JSON (Exemplo):**
        ```json
        {
          "nome": "Maria Silva",
          "email": "maria.silva@email.com",
          "areaAtuacao": "Marketing",
          "nivelCarreira": "Pleno"
        }
        ```

-----

## Como Testar

### Opção 1: Swagger (Recomendado)

Acesse a URL exibida no terminal após rodar o projeto (ex: `http://localhost:5006/swagger`).

  * Interface visual onde você pode clicar em "Try it out" e testar todos os métodos.

### Opção 2: Interface Web (Inclusa no projeto)

Abra o arquivo `index.html` localizado na raiz (ou pasta específica) no seu navegador.

  * Configure a variável `API_URL` dentro do arquivo HTML para apontar para a porta da sua API.

### Opção 3: Ferramentas Externas (Postman/Insomnia)

1.  Crie uma requisição do tipo `POST`.
2.  URL: `http://localhost:5006/api/v1/trilhas`.
3.  Body: Selecione `raw` e `JSON`.
4.  Cole o payload de exemplo acima e envie.

-----

## Estrutura do Projeto (Arquitetura)

O projeto segue uma arquitetura em camadas para garantir a separação de responsabilidades e fácil manutenção:

  * **SkillBridge.Api:** [Controller] Camada de entrada. Contém os Controllers, configuração de Swagger, Injeção de Dependência e tratamento de versionamento.
  * **SkillBridge.Application:** [Service] Contém as Regras de Negócio (ex: validação de duplicidade de e-mail) e Orquestração.
  * **SkillBridge.Domain:** [Model] Camada mais interna. Contém as Entidades (`Trilha`, `Usuario`), Interfaces (`Repository`, `Service`) e Exceções Customizadas (`TrilhaNaoEncontradaException`).
  * **SkillBridge.Infrastructure:** [Repository] Implementação do acesso a dados. Contém o `AppDbContext` (EF Core), Mapeamento das tabelas Oracle e Implementação dos Repositórios.

-----

## Integrantes do Grupo

  * Márcio Gastaldi - RM98811
  * Arthur Bessa Pian - RM99215
  * Davi Desenzi - RM550849

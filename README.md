# Koda API

> **Tema:** O Futuro do Trabalho - Upskilling & Reskilling para 2030+

## Descrição do Problema e Solução

### O Problema
O mercado de trabalho enfrenta uma transformação acelerada impulsionada por IA e automação. Profissionais correm o risco de obsolescência de suas funções atuais, enquanto empresas sofrem com a escassez de talentos qualificados nas competências do futuro (Tech, Dados, ESG).

### A Solução Proposta (Koda)
A **Koda** é uma API RESTful projetada para ser o motor de plataformas de educação continuada. Ela gerencia o ciclo de vida de **Upskilling** e **Reskilling**, permitindo:
1.  **Gestão de Trilhas:** Criação de roteiros de aprendizado focados em skills de 2030+.
2.  **Gestão de Talentos:** Cadastro e acompanhamento de usuários em transição de carreira.
3.  **Integração:** Arquitetura agnóstica pronta para conectar com front-ends web, mobile ou sistemas de RH.

---

## Deploy e Acesso (Produção)

A API está publicada e operante no Microsoft Azure.

* **Swagger UI (Documentação Interativa):** [Acessar Swagger na Nuvem](https://api-koda-fiap-hehyffhvcdgvbxdf.brazilsouth-01.azurewebsites.net/swagger)
* **Base URL:** `https://api-koda-fiap-hehyffhvcdgvbxdf.brazilsouth-01.azurewebsites.net`

> **Nota sobre Acesso:** O ambiente de produção conecta-se automaticamente a uma instância Oracle Database na nuvem. As credenciais (Usuário/Senha) estão configuradas seguramente via **Azure Environment Variables** e não são necessárias para consumir a API pública.

---

## Tecnologias e Versões

* **Linguagem:** C# (.NET 9.0)
* **Framework:** ASP.NET Core Web API
* **Banco de Dados:** Oracle Database (compatível com 11g/12c/19c/21c)
* **ORM:** Entity Framework Core 9.0
* **Documentação:** Swashbuckle (Swagger/OpenAPI)
* **Versionamento:** Asp.Versioning.Mvc

---

## Guia de Instalação e Execução Local

Siga estes passos para rodar o projeto na sua máquina.

### 1. Pré-requisitos
* [.NET SDK 9.0](https://dotnet.microsoft.com/download) instalado.
* Acesso a um banco Oracle (Local ou Remoto).
* Ferramenta de CLI do EF Core instalada globalmente:
    ```bash
    dotnet tool install --global dotnet-ef
    ```

### 2. Instalar Dependências
Abra o terminal na raiz da solução (`SkillBridge.sln`) e restaure os pacotes:

```bash
dotnet restore
````

### 3\. Configurar o Banco de Dados

Abra o arquivo `SkillBridge.Api/appsettings.json`. Localize a seção `ConnectionStrings` e insira suas credenciais do Oracle:

```json
"ConnectionStrings": {
  "OracleConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=SEU_HOST_ORACLE)(PORT=1521))(CONNECT_DATA=(SID=ORCL)));"
}
```

### 4\. Rodar Migrações (Migrations)

Para criar as tabelas no banco de dados utilizando o Entity Framework Core:

```bash
# 1. Criar o arquivo de migração (Snapshot do código atual)
dotnet ef migrations add InitialCreate --project SkillBridge.Infrastructure --startup-project SkillBridge.Api

# 2. Aplicar a migração no banco (Cria as tabelas TB_USUARIO, TB_TRILHA, etc.)
dotnet ef database update --project SkillBridge.Infrastructure --startup-project SkillBridge.Api
```

### 5\. Executar a Aplicação

Inicie o servidor da API:

```bash
dotnet run --project SkillBridge.Api
```

A aplicação estará disponível em `http://localhost:5006` (ou porta indicada no terminal).

-----

## 🔌 Exemplos de Requisições (Endpoints)

A API utiliza versionamento na URL: `/api/v1/`.

### 1\. Criar Trilha (POST)

**URL:** `/api/v1/trilhas`
**Payload JSON:**

```json
{
  "nome": "Liderança Ágil 4.0",
  "descricao": "Desenvolvimento de soft skills para gestores.",
  "nivel": "AVANCADO",
  "cargaHoraria": 60,
  "focoPrincipal": "Soft Skills"
}
```

### 2\. Listar Trilhas (GET)

**URL:** `/api/v1/trilhas`
**Resposta Esperada (200 OK):**

```json
[
  {
    "id": 1,
    "nome": "Liderança Ágil 4.0",
    "nivel": "AVANCADO",
    "cargaHoraria": 60
    ...
  }
]
```

### 3\. Cadastrar Usuário (POST)

**URL:** `/api/v1/usuarios`
**Payload JSON:**

```json
{
  "nome": "João da Silva",
  "email": "joao.silva@email.com",
  "areaAtuacao": "Contabilidade",
  "nivelCarreira": "Em transição"
}
```

-----

## Como Testar Rapidamente

### Opção A: Via Swagger (Interface Visual)

1.  Acesse `http://localhost:5006/swagger` (local) ou o link do Deploy.
2.  Clique no endpoint desejado (ex: POST /trilhas).
3.  Clique em **Try it out**.
4.  Cole o JSON de exemplo e clique em **Execute**.

### Opção B: Via Interface Web (Index.html)

Para facilitar a validação visual do Backend, mantivemos um arquivo `index.html` na raiz do projeto. Ele funciona como uma SPA (Single Page Application) simples para listar e cadastrar dados.

1.  Abra o arquivo `index.html` no seu navegador.
2.  Ele se conectará automaticamente à API para listar Trilhas e Usuários em uma interface amigável.
      * *Nota:* Caso queira alternar entre a API Local e a de Produção, basta editar a variável `API_URL` dentro do script do arquivo HTML.

### Opção C: Via cURL (Terminal)

Para testar a listagem de trilhas rapidamente via linha de comando:

```bash
curl -X 'GET' \
  '[https://api-koda-fiap-hehyffhvcdgvbxdf.brazilsouth-01.azurewebsites.net/api/v1/Trilhas](https://api-koda-fiap-hehyffhvcdgvbxdf.brazilsouth-01.azurewebsites.net/api/v1/Trilhas)' \
  -H 'accept: text/plain'
```

-----

## Arquitetura e Organização do Código

O projeto segue os princípios de **Clean Architecture** e **DDD (Domain-Driven Design)** simplificado, visando legibilidade e facilidade de manutenção:

  * **SkillBridge.Api:** Camada de **Interface**. Contém os `Controllers`, configuração de Injeção de Dependência (`Program.cs`) e documentação Swagger.
  * **SkillBridge.Application:** Camada de **Serviço**. Contém a lógica de negócios (`Services`), validações (ex: impedir e-mail duplicado) e orquestração.
  * **SkillBridge.Domain:** Camada de **Domínio**. Contém as Entidades (`Models`), Interfaces (`IRepository`) e Exceções Customizadas. É o núcleo do projeto, sem dependências externas.
  * **SkillBridge.Infrastructure:** Camada de **Infraestrutura**. Implementa o acesso a dados (`Repositories`), configuração do Entity Framework (`AppDbContext`) e mapeamento do Oracle.





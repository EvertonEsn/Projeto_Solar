# 🌞 Projeto Solar - API

Uma API robusta e escalável para gerenciamento de projetos solares, construída com arquitetura em camadas utilizando .NET 8.

## 📋 Sobre o Projeto

**Projeto Solar** é um sistema backend completo para gerenciamento de projetos de energia solar. A aplicação fornece uma API RESTful que permite o gerenciamento de:

- **Projetos Solares**: Cadastro e controle de projetos de implantação de sistemas solares
- **Clientes**: Gestão de dados e informações de clientes
- **Técnicos**: Gerenciamento de profissionais responsáveis pela execução dos projetos
- **Procedimentos**: Registro e acompanhamento de procedimentos técnicos realizados

O projeto foi desenvolvido seguindo princípios de design clean architecture, SOLID e boas práticas de desenvolvimento com .NET.

---

## 🏗️ Arquitetura

O projeto segue o padrão de **Clean Architecture** com separação clara de responsabilidades, organizando o código em camadas independentes que facilitam manutenção, testes e escalabilidade:

```
Projeto_Solar/
├── Solar.API                    # Apresentação e Controladores
├── Solar.Application            # Lógica de Negócio e Serviços
├── Solar.Domain                 # Entidades e Interfaces
├── Solar.Infrastructure         # Persistência e Banco de Dados
└── Solar.CrossCutting          # Configuração de Dependências
```

### 🎯 Princípios da Clean Architecture

A Clean Architecture nos permite:
- **Independência de Frameworks**: Não está vinculada a nenhum framework específico
- **Testabilidade**: Fácil de testar sem dependências externas
- **Independência de Interface**: A lógica de negócio não conhece detalhes de apresentação
- **Independência de Banco de Dados**: Podemos trocar de banco sem afetar a lógica
- **Independência de Regras de Negócio**: As regras estão no Domain, isoladas

### 📦 Camadas

#### **Solar.Domain** 🎯
- Contém as entidades do domínio
- Interfaces dos repositórios
- Tipos customizados
- Validações de domínio
- Completamente independente de frameworks externos

#### **Solar.Application** 🚀
- Serviços de aplicação
- DTOs (Data Transfer Objects)
- Mapeamento de dados
- Validadores (FluentValidation)
- Interfaces de serviço

#### **Solar.Infrastructure** 💾
- Implementação do Entity Framework Core
- Contexto de banco de dados
- Repositórios
- Configurações de entidades
- Migrações do banco de dados

#### **Solar.API** 🌐
- Controladores REST
- Manipulação de exceções global
- Configuração de Swagger/OpenAPI
- Endpoints HTTP

#### **Solar.CrossCutting** 🔧
- Injeção de Dependências (IoC)
- Configuração de serviços
- Registros globais

---

## 🚀 Recursos Principais

### Endpoints Disponíveis

#### **Projetos** (`/api/projetos`)
- `GET /api/projetos` - Listar todos os projetos
- `GET /api/projetos/{id}` - Obter projeto por ID
- `POST /api/projetos` - Criar novo projeto
- `PUT /api/projetos/{id}` - Atualizar projeto
- `DELETE /api/projetos/{id}` - Deletar projeto

#### **Clientes** (`/api/clientes`)
- `GET /api/clientes` - Listar todos os clientes
- `GET /api/clientes/{id}` - Obter cliente por ID
- `POST /api/clientes` - Criar novo cliente
- `PUT /api/clientes/{id}` - Atualizar cliente
- `DELETE /api/clientes/{id}` - Deletar cliente

#### **Técnicos** (`/api/tecnicos`)
- `GET /api/tecnicos` - Listar todos os técnicos
- `GET /api/tecnicos/{id}` - Obter técnico por ID
- `POST /api/tecnicos` - Criar novo técnico
- `PUT /api/tecnicos/{id}` - Atualizar técnico
- `DELETE /api/tecnicos/{id}` - Deletar técnico

#### **Procedimentos** (`/api/procedimentos`)
- `GET /api/procedimentos` - Listar todos os procedimentos
- `GET /api/procedimentos/{id}` - Obter procedimento por ID
- `POST /api/procedimentos` - Criar novo procedimento
- `PUT /api/procedimentos/{id}` - Atualizar procedimento
- `DELETE /api/procedimentos/{id}` - Deletar procedimento

### Funcionalidades Importantes

- ✅ **Validação Fluente**: Uso de FluentValidation para validações robustas
- ✅ **Tratamento de Exceções Global**: Middleware centralizado de exceções
- ✅ **Documentação Automática**: Swagger/OpenAPI integrado
- ✅ **Entity Framework Core**: ORM para acesso a dados com migrations
- ✅ **Banco de Dados MySQL**: Suporte a MySQL via Pomelo
- ✅ **Injeção de Dependências**: Configuração centralizada de IoC

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Versão | Propósito |
|-----------|--------|----------|
| .NET | 8.0 | Framework base |
| ASP.NET Core | 8.0 | Framework web |
| Entity Framework Core | 9.0.13 | ORM e persistência |
| Pomelo | - | Suporte MySQL |
| FluentValidation | 11.3.1 | Validação de dados |
| Swagger/Swashbuckle | 6.6.2 | Documentação API |
| MySQL | - | Banco de dados |
| Docker | - | Containerização |

---

## 📦 Pré-requisitos

- **.NET SDK 8.0+** ou superior
- **MySQL 8.0+** instalado e rodando
- **Docker** e **Docker Compose** (opcional, para containerização)
- **Git** para versionamento

---

## 🤝 Contribuindo

1. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
2. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
3. Push para a branch (`git push origin feature/AmazingFeature`)
4. Abra um Pull Request

---

## 📝 Padrões de Código

### Convenções Utilizadas

- **Linguagem**: C# com .NET 8.0
- **Nomenclatura**: PascalCase para classes e métodos públicos
- **Padrão de Projeto**: Clean Architecture
- **Injeção de Dependência**: Microsoft.Extensions.DependencyInjection
- **Validação**: FluentValidation
- **ORM**: Entity Framework Core


**Desenvolvido utilizando .NET 8**


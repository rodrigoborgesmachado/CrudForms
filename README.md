# CrudForms

CrudForms é uma aplicação desktop Windows, desenvolvida em C# com .NET Framework 4.8 e Windows Forms, para acelerar a exploração, manutenção, documentação e geração de sistemas CRUD a partir de bancos de dados existentes.

O sistema conecta em uma base de dados, importa sua estrutura, lista tabelas e colunas, permite consultar e manipular registros, exporta informações, gera documentação técnica e ainda cria código base para APIs C# e front-ends React.

## Proposta do Produto

CrudForms transforma uma base de dados em uma central operacional para desenvolvedores, analistas e equipes técnicas. A ferramenta reduz o trabalho repetitivo de criar telas de consulta, revisar estrutura de tabelas, montar relatórios, testar consultas, monitorar dados e iniciar projetos CRUD.

Com ele, uma equipe consegue:

- Conectar em uma base existente.
- Importar tabelas, colunas, tipos e relacionamentos.
- Navegar pelas tabelas em uma interface visual.
- Realizar consultas e manutenção de dados.
- Gerar relatórios em CSV, JSON e XML.
- Criar DER/documentação HTML da estrutura.
- Salvar consultas recorrentes.
- Criar alarmes automáticos baseados em SQL.
- Gerar classes de API em C#.
- Gerar um projeto front-end React administrativo.

## Principais Funcionalidades

### Conexão e Leitura de Bancos de Dados

- Cadastro de connection string da base de destino.
- Nomeação da conexão/base ativa.
- Seleção do tipo de banco na interface.
- Teste de conexão antes de salvar a configuração.
- Persistência da conexão selecionada em uma base local SQLite.
- Sincronização da estrutura do banco para metadados internos do CrudForms.
- Atualização manual das tabelas importadas.
- Atualização automática da estrutura após quantidade configurável de dias.
- Exibição da base ativa no rodapé da aplicação.

### Bancos de Dados

Implementado no código:

- SQL Server.
- PostgreSQL.
- MySQL.
- SQLite como banco local da aplicação e suporte de importação estrutural.

A tela também possui opção para Oracle, mas não há implementação de conexão/importação específica no código atual. Firebird não aparece implementado no repositório.

### Importação de Estrutura

Ao atualizar ou selecionar uma base, o CrudForms importa:

- Tabelas.
- Colunas.
- Tipos de dados.
- Tamanho e precisão.
- Chaves primárias.
- Chaves estrangeiras/relacionamentos.
- Campos obrigatórios.
- Campos únicos.
- Valores default.
- Comentários e metadados quando disponíveis.

Essas informações são salvas localmente e usadas para montar a navegação, formulários genéricos, DER, exportações e geradores de código.

### Navegação por Tabelas

- TreeView lateral com as tabelas importadas.
- Filtro textual para localizar tabelas rapidamente.
- Botão para limpar filtro.
- Abertura de cada tabela em abas internas.
- Reuso da aba já aberta quando a mesma tabela é selecionada novamente.
- Fechamento de tela atual e fechamento das demais telas pelo menu contextual das abas.
- Tela padrão quando nenhuma tabela está aberta.

### CRUD Genérico

Para cada tabela importada, o CrudForms monta uma tela genérica de dados com:

- Listagem dos registros em grid.
- Paginação.
- Contador de registros exibidos e total encontrado.
- Recarregamento manual da listagem.
- Visualização de detalhes por botão ou duplo clique.
- Inclusão de registros.
- Edição de registros.
- Exclusão de registros com confirmação.
- Validação básica por tipo de campo antes de inserir, editar ou filtrar.
- Controle de permissões: usuários administradores podem incluir, editar e excluir; usuários sem perfil admin ficam restritos à consulta/visualização.
- Uso de chave primária para atualizar e excluir registros.
- Aviso quando a tabela não possui PK necessária para algumas operações.

### Filtros, Ordenação e Colunas

- Filtro por coluna.
- Suporte a múltiplos valores no filtro usando `;`.
- Filtros numéricos, decimais e datas com validação.
- Busca textual com `LIKE` para campos de texto.
- Ordenação ascendente ou descendente por campo.
- Limpeza rápida dos filtros aplicados.
- Configuração de quantidade de linhas por página.
- Opção para filtrar automaticamente ao abrir a tela.
- Opção para enumerar linhas no grid.
- Configuração de colunas visíveis na listagem.
- Reordenação das colunas exibidas no grid.

### Consulta Genérica SQL

- Editor para executar consultas SQL de leitura.
- Exibição do resultado da consulta em uma aba genérica.
- Exportação do resultado.
- Salvamento da consulta para reutilização posterior.
- Reabertura da tela de consulta quando a seleção não retorna dados.

### Consultas Salvas

- Cadastro de consultas com nome.
- Edição de consultas salvas.
- Exclusão de consultas salvas.
- Execução direta de uma consulta salva.
- Criação de alarme/observer a partir de uma consulta salva.

### Exportação de Dados

O sistema exporta dados em:

- CSV.
- JSON.
- XML.

As exportações podem ser feitas a partir de:

- Uma tabela específica pelo menu contextual da árvore.
- Todas as tabelas importadas.
- Resultado exibido no grid da tabela.
- Resultado de consulta genérica.

Recursos adicionais de exportação:

- Escolha do diretório de destino.
- Geração do arquivo com nome baseado na tabela.
- Opção de exportar somente os registros exibidos ou todos os registros encontrados pelo filtro.
- Opção de copiar o conteúdo da exportação para a área de transferência em vez de gerar arquivo.
- Abertura automática do diretório após exportação em lote.

### Importação de Planilha CSV

- Importação de arquivo CSV separado por `;`.
- Criação de tabela no banco conectado a partir do cabeçalho do CSV.
- Conversão de nomes de colunas substituindo espaços e `/` por `_`.
- Criação das colunas como `VARCHAR`.
- Inserção dos dados do arquivo em lotes.
- Inclusão da tabela importada nos metadados locais do CrudForms.
- Validação de arquivo vazio e retorno de mensagens de erro.

### Geração de DER e Documentação HTML

- Geração de DER completo.
- Geração de DER filtrando tabelas selecionadas.
- Criação de relatório HTML navegável.
- Frame lateral com índice das tabelas.
- Página principal com tabelas, colunas, tipos, PK/FK, obrigatoriedade, unicidade, default, comentários e notas.
- Geração de CSS e cópia de imagens usadas no relatório.
- Abertura automática do HTML gerado.
- Regeneração do DER após atualização/importação da estrutura da base.

### Alarmes e Observers Automáticos

O módulo de alarmes permite monitorar consultas SQL de leitura periodicamente.

Funcionalidades:

- Cadastro de observer com descrição.
- Consulta SQL validada antes de salvar.
- Lista de e-mails destinatários.
- Intervalo de execução configurável, com mínimo de 5 minutos.
- Ativação e desativação de alarmes.
- Edição e exclusão de alarmes.
- Execução automática dos observers ativos ao iniciar o sistema.
- Reexecução por timer no intervalo configurado.
- Transformação dos resultados em JSON por linha.
- Controle de registros já enviados por hash para evitar notificações duplicadas.
- Envio de e-mail em HTML com tabela dos novos resultados.

### Devtools de Texto

Ferramentas auxiliares disponíveis no menu Devtools:

- Identar JSON.
- Identar XML.
- Converter XML para JSON.
- Converter JSON para XML.
- Copiar o resultado para a área de transferência.
- Mensagens de erro quando o input é inválido.

### Geração de API C#

O CrudForms gera classes C# a partir das tabelas selecionadas.

Arquivos gerados:

- Entity.
- DTO.
- ViewModel.
- Interface de Repository.
- Implementação de Repository.
- Interface de Service/AppService.
- Implementação de Service/AppService.
- Controller.
- Profiles AutoMapper para DTO.
- Profiles AutoMapper para ViewModel.

Recursos incluídos nos templates:

- Endpoints REST para listar, buscar por código, inserir, atualizar e remover.
- Endpoint paginado.
- Endpoint de exportação/relatório.
- Endpoint para alteração de status.
- Uso de `Authorize` nos controllers.
- Uso de `Guid Code` nos endpoints de leitura/atualização/status.
- Mapeamento de tipos de banco para tipos C#.
- Ignora campos de sistema comuns como `Code`, `IsActive`, `IsDeleted`, `Created` e `Updated` nas propriedades geradas.
- Organização dos arquivos em diretórios de domínio, aplicação, infraestrutura e apresentação.

### Geração de Front-end React

O CrudForms também cria um projeto administrativo em React a partir das tabelas selecionadas.

Recursos do gerador:

- Criação de projeto Vite + React.
- Validação de Node.js compatível com Vite.
- Instalação de dependências npm.
- Recriação da estrutura `src`.
- Geração de assets, styles, components, config, layouts, pages, routes, services, redux e utils.
- Configuração de `config.json` com URL da API e quantidade padrão de itens por tabela.
- Uso de Bootstrap, Bootstrap Icons, React Router, Redux Toolkit, React Toastify, Axios, File Saver e date-fns.

Funcionalidades geradas no front-end:

- Tela de login.
- Recuperação de senha.
- Confirmação de usuário por token.
- Layout administrativo com sidebar, header e breadcrumb.
- Dashboard inicial.
- Menu lateral baseado nas tabelas selecionadas.
- Listagem paginada por entidade.
- Filtros por termo, data inicial, data final e status.
- Exportação por entidade chamando endpoint da API.
- Página de detalhes.
- Formulário de inclusão e edição.
- Ação de ativar/desativar item.
- Modais de confirmação, mensagem e loading.
- Serviço Axios com token Bearer e tratamento de erro 401.
- Estado de autenticação e loading com Redux.
- Tema claro/escuro no front-end gerado.

Configuração visual do front-end:

- Edição de apelido das tabelas.
- Escolha de ícones Bootstrap Icons para o menu.
- Preview do ícone selecionado.
- Reordenação das tabelas no menu.
- Edição de apelido das colunas.
- Seleção de colunas visíveis na listagem.
- Seleção de colunas visíveis no formulário de inclusão/edição.
- Seleção de colunas visíveis na tela de detalhes.
- Reordenação de colunas.
- Configuração de variáveis CSS de tema claro, tema escuro, status e layout.
- Seletor visual de cores para variáveis CSS.
- Restauração das variáveis CSS padrão.

### Login, Permissões e Atualização

- Tela de login integrada à API da SunSale.
- Login também por argumentos de linha de comando: usuário e senha.
- Hash local da senha antes do envio.
- Armazenamento do usuário logado em memória com e-mail, token, perfil admin e versão disponível.
- Controle de acesso por perfil admin/leitor.
- Consulta de versão atual do executável.
- Verificação de nova versão disponível.
- Download do instalador da nova versão.
- Execução automática do instalador baixado.

### Logs e Operação

- Criação automática dos diretórios principais usados pelo sistema.
- Registro de logs simples e detalhados.
- Manutenção automática de arquivos de log antigos.
- Configuração da quantidade de dias para manter logs.
- Menu para abrir arquivos de log existentes.
- Barra de carregamento em operações demoradas como importação, geração de DER, exportação e geração de código.
- Mensagens padronizadas de sucesso, alerta, erro, informação e confirmação.
- Modo escuro no aplicativo desktop.

## Tecnologias

- C#.
- .NET Framework 4.8.
- Windows Forms.
- SQLite para armazenamento local/metadados.
- SQL Server via `System.Data.SqlClient`.
- PostgreSQL via `Npgsql`.
- MySQL via `MySql.Data`.
- Newtonsoft.Json.
- System.Text.Json.
- RestSharp.
- Inno Setup para instalador.

O front-end gerado usa:

- React.
- Vite.
- Bootstrap.
- Bootstrap Icons.
- React Router.
- Redux Toolkit.
- Axios.
- React Toastify.
- date-fns.
- file-saver.

## Arquitetura do Repositório

- `CrudForms/`: aplicação WinForms principal.
- `CrudForms/Visao/`: telas, formulários e user controls.
- `CrudForms/Regras/`: regras de negócio, importadores, exportadores, DER, observers e geradores de código.
- `CrudForms/Regras/ApiClasses/`: geração de classes C# para API.
- `CrudForms/Regras/FrontEndClasses/`: geração de projeto React.
- `CrudForms/Regras/AcessoBancoCliente/`: montagem de comandos e operações CRUD nos bancos de cliente.
- `CrudForms/Importador/`: importação estrutural por tipo de banco.
- `CrudForms/Model/`: modelos usados pela aplicação.
- `Utils/`: biblioteca compartilhada com DAO, conexão, utilitários, enums e integrações.
- `sqliteLib/`: dependências SQLite.
- `Instalador/`: script Inno Setup.
- `DEMO/`: executável de demonstração.

## Observações Técnicas

- A aplicação inicia abrindo uma base SQLite local usada para parâmetros e metadados.
- Após conectar em um banco de cliente, o sistema alterna temporariamente a conexão para executar operações e volta para a base local.
- Muitas operações dependem dos metadados importados; após trocar a connection string, a estrutura local é limpa e reimportada.
- Operações de edição/exclusão dependem de chave primária na tabela.
- O gerador de front-end remove e recria o projeto no diretório informado caso uma pasta com o mesmo nome já exista.
- O gerador de front-end exige Node.js em versão compatível com Vite: mínimo 20.19 ou 22.12.

## Compilação

Abra `CrudForms.sln` no Visual Studio e compile com .NET Framework 4.8.

Há referências ao SQLite. Se ocorrer erro ao debugar relacionado às DLLs do SQLite, copie os arquivos de `sqliteLib/` para o diretório de saída (`bin/Debug` ou equivalente) e execute novamente.

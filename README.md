# Teste Desenvolvedor C#

O projeto foi criado usando o .net core 3.1.
O acesso a dados esta configurado para um banco local SQL Server

# Autenticação
* Autenticação JWT.
    * Token de 15 minutos de duração
# Swagger UI
* Possui o swagger, acesso em /swagger/
    # Autenticador/Login
    * Este endpoint deverá ter as seguintes implementações: 
      * Método DoLogin recebendo parametros: email e senha: 
        * (usuario: prova@doubleit.com.br, senha: Prova@DoubleIt21, nome: Candidato)
      * valida se as credenciais do usuário estão corretas através de uma consulta ao banco de dados passando email e senha
      * caso não existe uma mensagem de Usuario não encontrado" é ser informada
      * caso exista cria um token e adiciona uma claim chamada "user" passando o objeto usuario vindo do banco de dados
    # Categorias 
      * Método GetAll sem nenhum parametro e retorna uma lista de Categorias.
      * Método Add recebendo um objeto do tipo Categorias e com as regras abaixo:
      * valida se o campo Nome foi preenchido e retorna mensagem de erro caso estiver vazio.
      * valida se a categoria existe com o mesmo nome informado.
    # Produtos
      * Método  GetAll recebendo como parametro o term, page, e pageSize. O método retorna uma lista de Produtos.
         * possui paginação
         * filtro usando o parâmetro term usando o Contains nas propriedades Nome e Categoria.Nome
      * Método Add recebendo um objeto do tipo Produtos e com as regras abaixo:
         * valida se o campo Nome foi preenchido e retorna mensagem de erro caso estiver vazio.
         * valida se a produto existe com o mesmo nome informado.
         * valida se o preço unitario é > 0 e retorna mensagem de erro caso resultado for false.
         * valida se o quantidade é > 0 e retorna mensagem de erro caso resultado for false.
      * Método Update recebendo um objeto do tipo Produtos e com as regras abaixo:
         * valida se o produto id informado existe no banco de dados e retorna mensagem de erro caso não tenha sido encontrado.
         * valida se o campo Nome foi preenchido e retorna mensagem de erro caso estiver vazio.
         * valida se o preço unitario é > 0 e retornr mensagem de erro caso resultado for false.
         * valida se o quantidade é > 0 e retorna mensagem de erro caso resultado for false.
      * Método Delete recebendo o id do produto e com as regras abaixo:
         * busca o produto pelo id e retorna mensagem de erro caso não tenha sido encontrado.
    # Testes
    * Testes unitários para os métodos do serviço ProductsService
          
          
    
    


  




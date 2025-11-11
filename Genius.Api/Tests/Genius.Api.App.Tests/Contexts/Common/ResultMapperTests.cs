using Genius.Api.App.Contexts.Common;
using Shouldly;

namespace Genius.Api.App.Tests.Contexts.Common
{
    public class ResultMapperTests
    {
        private class Usuario
        {
            public int ID { get; set; }
            public string? Name { get; set; }
        }

        #region Casos de Sucesso - OK (200)

        [Fact]
        public void ToIResult_QuandoResultForValido_DeveRetornarOk()
        {
            // Arrange
            const string tipoEsperado = "OperadorTeste";
            const string msgPersonalizada = "Uma mensagem personalizada";
            const string Code = "IDENTICADOR";
            var operadorEsperado = new Usuario() { ID = 1, Name = "Teste" };
            var sucesso = Result.Success(operadorEsperado);

            // Act
            var result = ResultMapper.ToIResult(
                sucesso,
                messageType: tipoEsperado,
                message: msgPersonalizada,
                code: Code
            );

            // Assert
            result.ShouldBeOfType<Ok<ApiResponse<Usuario>>>();
            var respostaOk = result as Ok<ApiResponse<Usuario>>;
            respostaOk.ShouldNotBeNull();
            respostaOk.Value.ShouldNotBeNull();
            respostaOk.Value.Data.ShouldNotBeNull();
            respostaOk.Value.Data.ID.ShouldBe(operadorEsperado.ID);
            respostaOk.Value.Data.Name.ShouldBe(operadorEsperado.Name);
            respostaOk.Value.Header.ShouldNotBeNull();
            respostaOk.Value.Header.MessageType.ShouldBe(tipoEsperado);
            respostaOk.Value.Result.ShouldNotBeNull();
            respostaOk.Value.Result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            respostaOk.Value.Result.Code.ShouldBe(Code);
            respostaOk.Value.Result.Message.ShouldBe(msgPersonalizada);
        }

        [Fact]
        public void ToIResult_QuandoResultForValidoSemParametrosOpcionais_DeveRetornarOkComValoresPadrao()
        {
            // Arrange
            var operadorEsperado = new Usuario() { ID = 2, Name = "Teste Padrão" };
            var sucesso = Result.Success(operadorEsperado);

            // Act
            var result = ResultMapper.ToIResult(sucesso);

            // Assert
            result.ShouldBeOfType<Ok<ApiResponse<Usuario>>>();
            var respostaOk = result as Ok<ApiResponse<Usuario>>;
            respostaOk.ShouldNotBeNull();
            respostaOk.Value.ShouldNotBeNull();
            respostaOk.Value.Data.ShouldBe(operadorEsperado);
            respostaOk.Value.Header.MessageType.ShouldBe(string.Empty);
            respostaOk.Value.Result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            respostaOk.Value.Result.Message.ShouldBe("Operação concluída com sucesso.");
            respostaOk.Value.Result.Code.ShouldBeNull();
        }

        [Fact]
        public void ToIResult_QuandoResultForValidoComValorNulo_DeveRetornarOk()
        {
            // Arrange
            var sucesso = Result.Success<Usuario?>(null);

            // Act
            var result = ResultMapper.ToIResult(sucesso);

            // Assert
            result.ShouldBeOfType<Ok<ApiResponse<Usuario?>>>();
            var respostaOk = result as Ok<ApiResponse<Usuario?>>;
            respostaOk.ShouldNotBeNull();
            respostaOk.Value!.Data.ShouldBeNull();
            respostaOk.Value.Result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }

        #endregion

        #region Casos de Sucesso - Created (201)

        [Fact]
        public void ToIResult_QuandoResultForCreated_DeveRetornarCreatedSemUri()
        {
            // Arrange
            const string tipoEsperado = "OperadorTeste";
            var operadorEsperado = new Usuario() { ID = 1, Name = "Teste" };
            var sucesso = Result.Success(operadorEsperado);

            // Act
            var result = ResultMapper.ToIResult(
                sucesso,
                StatusCodes.Status201Created,
                messageType: tipoEsperado
            );

            // Assert
            result.ShouldBeOfType<Created<ApiResponse<Usuario>>>();
            var respostaCreated = result as Created<ApiResponse<Usuario>>;
            respostaCreated.ShouldNotBeNull();
            respostaCreated.Value.ShouldNotBeNull();
            respostaCreated.Value.Data.ShouldNotBeNull();
            respostaCreated.Value.Data.ID.ShouldBe(operadorEsperado.ID);
            respostaCreated.Value.Data.Name.ShouldBe(operadorEsperado.Name);
            respostaCreated.Value.Header.ShouldNotBeNull();
            respostaCreated.Value.Header.MessageType.ShouldBe(tipoEsperado);
            respostaCreated.Value.Result.ShouldNotBeNull();
            respostaCreated.Value.Result.StatusCode.ShouldBe(StatusCodes.Status201Created);
            respostaCreated.Location.ShouldBe(string.Empty);
        }

        [Fact]
        public void ToIResult_QuandoResultForCreated_DeveRetornarCreatedComUri()
        {
            // Arrange
            const string tipoEsperado = "OperadorTeste";
            var operadorEsperado = new Usuario() { ID = 1, Name = "Teste" };
            var sucesso = Result.Success(operadorEsperado);

            // Act
            var result = ResultMapper.ToIResult(
                sucesso,
                StatusCodes.Status201Created,
                messageType: tipoEsperado,
                createdLocation: u => $"/api/exemplo/{u.ID}"
            );

            // Assert
            result.ShouldBeOfType<Created<ApiResponse<Usuario>>>();
            var respostaCreated = result as Created<ApiResponse<Usuario>>;
            respostaCreated.ShouldNotBeNull();
            respostaCreated.Value.ShouldNotBeNull();
            respostaCreated.Value.Data.ShouldNotBeNull();
            respostaCreated.Value.Data.ID.ShouldBe(operadorEsperado.ID);
            respostaCreated.Value.Data.Name.ShouldBe(operadorEsperado.Name);
            respostaCreated.Value.Header.ShouldNotBeNull();
            respostaCreated.Value.Header.MessageType.ShouldBe(tipoEsperado);
            respostaCreated.Value.Result.ShouldNotBeNull();
            respostaCreated.Value.Result.StatusCode.ShouldBe(StatusCodes.Status201Created);
            respostaCreated.Location.ShouldNotBeNull();
            respostaCreated.Location.ShouldBe("/api/exemplo/1");
        }

        [Fact]
        public void ToIResult_QuandoResultForCreatedComMensagemCustomizada_DeveRetornarCreatedComMensagem()
        {
            // Arrange
            const string mensagemCustomizada = "Recurso criado com sucesso";
            const string codigoCustomizado = "RESOURCE_CREATED";
            var operadorEsperado = new Usuario() { ID = 5, Name = "Novo Operador" };
            var sucesso = Result.Success(operadorEsperado);

            // Act
            var result = ResultMapper.ToIResult(
                sucesso,
                StatusCodes.Status201Created,
                createdLocation: u => $"/api/operadores/{u.ID}",
                message: mensagemCustomizada,
                code: codigoCustomizado
            );

            // Assert
            result.ShouldBeOfType<Created<ApiResponse<Usuario>>>();
            var respostaCreated = result as Created<ApiResponse<Usuario>>;
            respostaCreated.ShouldNotBeNull();
            respostaCreated.Value!.Result.Message.ShouldBe(mensagemCustomizada);
            respostaCreated.Value.Result.Code.ShouldBe(codigoCustomizado);
            respostaCreated.Location.ShouldBe("/api/operadores/5");
        }

        #endregion

        #region Casos de Sucesso - NoContent (204)

        [Fact]
        public void ToIResult_QuandoResultForNoContent_DeveRetornarNoContent()
        {
            // Arrange
            var sucesso = Result.Success<Usuario?>(null);

            // Act
            var result = ResultMapper.ToIResult(sucesso, StatusCodes.Status204NoContent);

            // Assert
            result.ShouldBeOfType<NoContent>();
            var respostaNoContent = result as NoContent;
            respostaNoContent.ShouldNotBeNull();
            respostaNoContent.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }

        [Fact]
        public void ToIResult_QuandoResultForNoContentComDados_DeveRetornarNoContentIgnorandoDados()
        {
            // Arrange
            var operador = new Usuario() { ID = 1, Name = "Teste" };
            var sucesso = Result.Success(operador);

            // Act
            var result = ResultMapper.ToIResult(sucesso, StatusCodes.Status204NoContent);

            // Assert
            result.ShouldBeOfType<NoContent>();
            // NoContent não deve retornar dados mesmo que estejam presentes
        }

        #endregion

        #region Casos de Erro - Validation (400)

        [Fact]
        public void ToIResult_QuandoResultForErrorValidation_DeveRetornarBadRequest()
        {
            // Arrange
            Result<object?> error = Error.Validation("ERRO_VALIDACAO", "Valores fora do esperado.");

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            ValidarErro<BadRequest<ApiResponse<object?>>>(
                result,
                error,
                StatusCodes.Status400BadRequest
            );
        }

        [Fact]
        public void ToIResult_QuandoResultForErrorValidationComMessageType_DeveRetornarBadRequestComMessageType()
        {
            // Arrange
            const string messageType = "VALIDATION_ERROR";
            Result<object?> error = Error.Validation("CAMPO_INVALIDO", "Campo nome é obrigatório");

            // Act
            var result = ResultMapper.ToIResult(error, messageType: messageType);

            // Assert
            result.ShouldBeOfType<BadRequest<ApiResponse<object?>>>();
            var badRequest = result as BadRequest<ApiResponse<object?>>;
            badRequest.ShouldNotBeNull();
            badRequest.Value.ShouldNotBeNull();
            badRequest.Value.Header.MessageType.ShouldBe(messageType);
            badRequest.Value.Result.Code.ShouldBe("CAMPO_INVALIDO");
            badRequest.Value.Result.Message.ShouldBe("Campo nome é obrigatório");
        }

        #endregion

        #region Casos de Erro - Unauthorized (401)

        [Fact]
        public void ToIResult_QuandoResultForErroUnauthorized_DeveRetornarUnauthorized()
        {
            // Arrange
            Result<object?> error = Error.Unauthorized(
                "SEM_AUTORIZACAO",
                "Sem autorização para acesso."
            );

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            ValidarErro<JsonHttpResult<ApiResponse<object?>>>(
                result,
                error,
                StatusCodes.Status401Unauthorized
            );
        }

        [Fact]
        public void ToIResult_QuandoResultForErroUnauthorizedComTokenInvalido_DeveRetornarUnauthorized()
        {
            // Arrange
            Result<object?> error = Error.Unauthorized(
                "TOKEN_INVALIDO",
                "Token de autenticação inválido ou expirado"
            );

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            result.ShouldBeOfType<JsonHttpResult<ApiResponse<object?>>>();
            var jsonResult = result as JsonHttpResult<ApiResponse<object?>>;
            jsonResult.ShouldNotBeNull();
            jsonResult.StatusCode.ShouldBe(StatusCodes.Status401Unauthorized);
            jsonResult.Value.ShouldNotBeNull();
            jsonResult.Value.Result.Code.ShouldBe("TOKEN_INVALIDO");
        }

        #endregion

        #region Casos de Erro - Forbidden (403)

        [Fact]
        public void ToIResult_QuandoResultForErroForbidden_DeveRetornarForbidden()
        {
            // Arrange
            Result<object?> error = Error.Forbidden(
                "ACESSO_PROIBIDO",
                "Acesso permanentemente proibido"
            );

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            ValidarErro<JsonHttpResult<ApiResponse<object?>>>(
                result,
                error,
                StatusCodes.Status403Forbidden
            );
        }

        [Fact]
        public void ToIResult_QuandoResultForErroForbiddenSemPermissao_DeveRetornarForbidden()
        {
            // Arrange
            Result<object?> error = Error.Forbidden(
                "SEM_PERMISSAO",
                "Usuário não possui permissão para acessar este recurso"
            );

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            result.ShouldBeOfType<JsonHttpResult<ApiResponse<object?>>>();
            var jsonResult = result as JsonHttpResult<ApiResponse<object?>>;
            jsonResult.ShouldNotBeNull();
            jsonResult.StatusCode.ShouldBe(StatusCodes.Status403Forbidden);
            jsonResult.Value!.Result.Code.ShouldBe("SEM_PERMISSAO");
        }

        #endregion

        #region Casos de Erro - NotFound (404)

        [Fact]
        public void ToIResult_QuandoResultForErroNaoEncontrado_DeveRetornarNotFound()
        {
            // Arrange
            Result<object?> error = Error.NotFound("NAO_ENCONTRADO", "Item não localizado");

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            ValidarErro<NotFound<ApiResponse<object?>>>(
                result,
                error,
                StatusCodes.Status404NotFound
            );
        }

        [Fact]
        public void ToIResult_QuandoResultForErroRecursoNaoEncontrado_DeveRetornarNotFoundComDetalhes()
        {
            // Arrange
            Result<object?> error = Error.NotFound(
                "OPERADOR_NAO_ENCONTRADO",
                "Operador com ID 999 não foi encontrado"
            );

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            result.ShouldBeOfType<NotFound<ApiResponse<object?>>>();
            var notFound = result as NotFound<ApiResponse<object?>>;
            notFound.ShouldNotBeNull();
            notFound.Value!.Result.Code.ShouldBe("OPERADOR_NAO_ENCONTRADO");
            notFound.Value.Result.Message.ShouldBe("Operador com ID 999 não foi encontrado");
        }

        #endregion

        #region Casos de Erro - Conflict (409)

        [Fact]
        public void ToIResult_QuandoResultForErroConflict_DeveRetornarConflict()
        {
            // Arrange
            Result<object?> error = Error.Conflict("VALORES_DUPLICADOS", "Itens já registrado.");

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            ValidarErro<Conflict<ApiResponse<object?>>>(
                result,
                error,
                StatusCodes.Status409Conflict
            );
        }

        [Fact]
        public void ToIResult_QuandoResultForErroConflictEmailDuplicado_DeveRetornarConflictComDetalhes()
        {
            // Arrange
            Result<object?> error = Error.Conflict(
                "EMAIL_DUPLICADO",
                "Já existe um operador cadastrado com este email"
            );

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            result.ShouldBeOfType<Conflict<ApiResponse<object?>>>();
            var conflict = result as Conflict<ApiResponse<object?>>;
            conflict.ShouldNotBeNull();
            conflict.Value!.Result.Code.ShouldBe("EMAIL_DUPLICADO");
            conflict.Value.Result.Message.ShouldBe(
                "Já existe um operador cadastrado com este email"
            );
        }

        #endregion

        #region Casos de Erro - Internal Server Error (500)

        [Fact]
        public void ToIResult_QuandoResultForErrorFailure_DeveRetornarErroInterno()
        {
            // Arrange
            Result<object?> error = Error.Failure(
                "ERRO_INTERNO",
                "Não foi possível processar a requisição"
            );

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            ValidarErro<InternalServerError<ApiResponse<object?>>>(
                result,
                error,
                StatusCodes.Status500InternalServerError
            );
        }

        [Fact]
        public void ToIResult_QuandoResultForErrorFailureComExcecao_DeveRetornarErroInternoComDetalhes()
        {
            // Arrange
            Result<object?> error = Error.Failure(
                "DATABASE_ERROR",
                "Erro ao conectar com o banco de dados"
            );

            // Act
            var result = ResultMapper.ToIResult(error);

            // Assert
            result.ShouldBeOfType<InternalServerError<ApiResponse<object?>>>();
            var internalError = result as InternalServerError<ApiResponse<object?>>;
            internalError.ShouldNotBeNull();
            internalError.Value!.Result.Code.ShouldBe("DATABASE_ERROR");
            internalError.Value.Result.Message.ShouldBe("Erro ao conectar com o banco de dados");
        }

        #endregion

        #region Cenários Complexos

        [Fact]
        public void ToIResult_QuandoCriarRecursoCompleto_DeveRetornarCreatedComTodosCampos()
        {
            // Arrange
            var novoOperador = new Usuario() { ID = 10, Name = "Operador Completo" };
            var sucesso = Result.Success(novoOperador);

            // Act
            var result = ResultMapper.ToIResult(
                sucesso,
                successStatus: StatusCodes.Status201Created,
                createdLocation: op => $"/api/operadores/{op.ID}",
                messageType: "OPERADOR_CRIADO",
                message: "Operador criado com sucesso no sistema",
                code: "OP_CREATE_SUCCESS"
            );

            // Assert
            result.ShouldBeOfType<Created<ApiResponse<Usuario>>>();
            var created = result as Created<ApiResponse<Usuario>>;
            created.ShouldNotBeNull();
            created.Location.ShouldBe("/api/operadores/10");
            created.Value?.Data?.ID.ShouldBe(10);
            created.Value?.Data?.Name.ShouldBe("Operador Completo");
            created.Value?.Header.MessageType.ShouldBe("OPERADOR_CRIADO");
            created.Value?.Result.Message.ShouldBe("Operador criado com sucesso no sistema");
            created.Value?.Result.Code.ShouldBe("OP_CREATE_SUCCESS");
            created.Value?.Result.StatusCode.ShouldBe(StatusCodes.Status201Created);
        }

        [Fact]
        public void ToIResult_QuandoAtualizarRecurso_DeveRetornarOkComDadosAtualizados()
        {
            // Arrange
            var operadorAtualizado = new Usuario() { ID = 5, Name = "Nome Atualizado" };
            var sucesso = Result.Success(operadorAtualizado);

            // Act
            var result = ResultMapper.ToIResult(
                sucesso,
                messageType: "OPERADOR_ATUALIZADO",
                message: "Operador atualizado com sucesso",
                code: "OP_UPDATE_SUCCESS"
            );

            // Assert
            result.ShouldBeOfType<Ok<ApiResponse<Usuario>>>();
            var ok = result as Ok<ApiResponse<Usuario>>;
            ok.ShouldNotBeNull();
            ok.Value?.Data?.Name.ShouldBe("Nome Atualizado");
            ok.Value?.Header.MessageType.ShouldBe("OPERADOR_ATUALIZADO");
            ok.Value?.Result.Message.ShouldBe("Operador atualizado com sucesso");
        }

        [Fact]
        public void ToIResult_QuandoDeletarRecurso_DeveRetornarNoContent()
        {
            // Arrange
            var sucesso = Result.Success<Usuario?>(null);

            // Act
            var result = ResultMapper.ToIResult(
                sucesso,
                successStatus: StatusCodes.Status204NoContent
            );

            // Assert
            result.ShouldBeOfType<NoContent>();
        }

        [Fact]
        public void ToIResult_QuandoErroComMessageTypeCustomizado_DeveIncluirMessageType()
        {
            // Arrange
            const string messageType = "ERRO_NEGOCIO";
            Result<object?> error = Error.Validation(
                "REGRA_NEGOCIO_VIOLADA",
                "A operação viola uma regra de negócio"
            );

            // Act
            var result = ResultMapper.ToIResult(error, messageType: messageType);

            // Assert
            result.ShouldBeOfType<BadRequest<ApiResponse<object?>>>();
            var badRequest = result as BadRequest<ApiResponse<object?>>;
            badRequest.ShouldNotBeNull();
            badRequest.Value?.Header.MessageType.ShouldBe(messageType);
        }

        #endregion

        #region Método Auxiliar

        private static void ValidarErro<T>(
            IResult result,
            Result<object?> error,
            int expectedStatusCode
        )
            where T : class, IResult
        {
            result.ShouldBeOfType<T>();

            var typedResult = result as T;
            typedResult.ShouldNotBeNull();

            // Acessa a propriedade Value dinamicamente
            var valueProperty = typeof(T).GetProperty("Value")?.GetValue(typedResult);
            valueProperty.ShouldNotBeNull();

            var response = valueProperty as ApiResponse<object?>;
            response.ShouldNotBeNull();
            response!.Header.ShouldNotBeNull();
            response.Data.ShouldBeNull();
            response.Result.ShouldNotBeNull();

            response.Result!.Code.ShouldBe(error.Errors[0].Code);
            response.Result.Message.ShouldBe(error.Errors[0].Message);
            response.Result.StatusCode.ShouldBe(expectedStatusCode);
        }

        #endregion
    }
}
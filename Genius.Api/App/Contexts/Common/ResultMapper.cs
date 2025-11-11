using Genius.Common.Api.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Api.App.Contexts.Common
{
    /// <summary>
    /// Converte objetos Result<T> da camada Application em respostas HTTP (IResult) padronizadas.
    /// </summary>
    public static class ResultMapper
    {
        private const string DefaultSuccessMessage = "Operação concluída com sucesso.";

        /// <summary>
        /// Converte um <see cref="Result{T}"/> em um <see cref="IResult"/> compatível com Minimal APIs.
        /// </summary>
        /// <typeparam name="T">O tipo do valor contido no resultado.</typeparam>
        /// <param name="result">
        /// Instância de <see cref="Result{T}"/> que representa o resultado de uma operação.
        /// </param>
        /// <param name="successStatus">
        /// Código HTTP a ser retornado em caso de sucesso. O padrão é <see cref="StatusCodes.Status200OK"/>.
        /// </param>
        /// <param name="createdLocation">
        /// Função opcional que define o local do recurso criado (utilizado em respostas <see cref="StatusCodes.Status201Created"/>).
        /// Retorna uma string representando a URL do recurso.
        /// </param>
        /// <param name="messageType">
        /// Tipo de mensagem opcional a ser incluída na resposta
        /// </param>
        /// <param name="message">
        /// Mensagem opcional a ser incluída na resposta.
        /// </param>
        /// <param name="code">
        /// Código de erro ou identificação opcional da mensagem.
        /// </param>
        /// <returns>
        /// Um <see cref="IResult"/> representando o resultado HTTP adequado,
        /// com base no estado de sucesso ou falha de <paramref name="result"/>.
        /// </returns>
        public static IResult ToIResult<T>(
            Result<T> result,
            int successStatus = StatusCodes.Status200OK,
            Func<T, string>? createdLocation = null,
            string? messageType = null,
            string? message = null,
            string? code = null
        )
        {
            return result.IsSuccess
                ? HandleSuccess(result, successStatus, createdLocation, messageType, message, code)
                : HandleError(result, messageType);
        }


        /// <summary>
        /// Cria um <see cref="IResult"/> de sucesso com base no <see cref="Result{T}"/> informado.
        /// </summary>
        /// <typeparam name="T">O tipo do valor retornado pela operação.</typeparam>
        /// <param name="result">
        /// Instância de <see cref="Result{T}"/> representando o resultado da operação bem-sucedida.
        /// </param>
        /// <param name="successStatus">
        /// Código HTTP a ser retornado em caso de sucesso (ex.: 200, 201, 204).
        /// </param>
        /// <param name="createdLocation">
        /// Função opcional que retorna a URL do recurso criado, utilizada quando o status é 
        /// <see cref="StatusCodes.Status201Created"/>.
        /// </param>
        /// <param name="messageType">
        /// Tipo de mensagem opcional a ser incluída no cabeçalho da resposta (ex.: "INFO", "SUCCESS").
        /// </param>
        /// <param name="message">
        /// Mensagem opcional de sucesso a ser retornada. Caso não seja informada, é usada uma mensagem padrão.
        /// </param>
        /// <param name="code">
        /// Código de referência opcional que identifica o tipo de sucesso ou operação.
        /// </param>
        /// <returns>
        /// Um <see cref="IResult"/> representando a resposta HTTP adequada ao tipo de sucesso:
        /// </returns>
        /// <remarks>
        /// Este método constrói um objeto <see cref="ApiResponse{T}"/> contendo os dados, cabeçalho e metadados
        /// da resposta antes de retorná-lo como um <see cref="IResult"/> apropriado.
        /// </remarks>
        private static IResult HandleSuccess<T>(
            Result<T> result,
            int successStatus,
            Func<T, string>? createdLocation = null,
            string? messageType = null,
            string? message = null,
            string? code = null
        )
        {
            var responseHeader = new ResponseHeader() { MessageType = messageType ?? string.Empty };
            var responseResult = new ResponseResult()
            {
                Message = message ?? DefaultSuccessMessage,
                StatusCode = successStatus,
                Code = code,
            };

            var response = new ApiResponse<T>(result.Value, responseHeader, responseResult);

            var location = createdLocation?.Invoke(result.Value!) ?? string.Empty;

            return successStatus switch
            {
                StatusCodes.Status201Created => Results.Created(location, response),

                StatusCodes.Status204NoContent => Results.NoContent(),
                _ => Results.Ok(response),
            };
        }


        /// <summary>
        /// Cria um <see cref="IResult"/> representando uma resposta de erro com base no <see cref="Result{T}"/> informado.
        /// </summary>
        /// <typeparam name="T">O tipo do valor associado ao resultado da operação.</typeparam>
        /// <param name="result">
        /// Instância de <see cref="Result{T}"/> que contém informações sobre o erro ocorrido.
        /// Espera-se que <paramref name="result"/> contenha pelo menos um item em <c>Errors</c>.
        /// </param>
        /// <param name="messageType">
        /// Tipo de mensagem opcional a ser incluída no cabeçalho da resposta (por exemplo, "ERROR" ou "VALIDATION").
        /// </param>
        /// <returns>
        /// Um <see cref="IResult"/> representando a resposta HTTP correspondente ao tipo de erro.
        /// </returns>
        /// <remarks>
        /// Este método utiliza <see cref="MapErrorTypeToStatusCode(string)"/> para converter o tipo de erro
        /// em um código de status HTTP apropriado e retorna um <see cref="ApiResponse{T}"/> contendo
        /// detalhes estruturados sobre o erro ocorrido.
        /// </remarks>
        private static IResult HandleError<T>(Result<T> result, string? messageType = null)
        {
            var error = result.Errors[0];

            var statusCode = MapErrorTypeToStatusCode(error.Type);

            var response = new ApiResponse<T>(
                result.Value,
                messageType,
                statusCode,
                error.Code,
                error.Message
            );

            return statusCode switch
            {
                StatusCodes.Status400BadRequest => Results.BadRequest(response),
                StatusCodes.Status401Unauthorized
                    => Results.Json(response, statusCode: StatusCodes.Status401Unauthorized),
                StatusCodes.Status403Forbidden
                    => Results.Json(response, statusCode: StatusCodes.Status403Forbidden),
                StatusCodes.Status404NotFound => Results.NotFound(response),
                StatusCodes.Status409Conflict => Results.Conflict(response),
                _ => Results.InternalServerError(response)
            };
        }


        /// <summary>
        /// Mapeia um valor de <see cref="ErrorType"/> para o código de status HTTP correspondente.
        /// </summary>
        /// <param name="errorType">
        /// Tipo de erro a ser convertido em um código de status HTTP.
        /// </param>
        /// <returns>
        /// Um valor inteiro representando o código de status HTTP adequado ao tipo de erro informado.
        /// </returns>
        /// <remarks>
        /// Este método centraliza a lógica de conversão entre os tipos de erro da camada de domínio e os códigos HTTP
        /// utilizados pela API, garantindo consistência nas respostas de erro.
        /// </remarks>
        private static int MapErrorTypeToStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
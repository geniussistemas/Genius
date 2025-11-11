using Genius.Api.InterfaceAdapters.DTO;
using Genius.Common.Lib.Results;
using Genius.Domain.Entities;
using Genius.Domain.Enums;

namespace Genius.Api.InterfaceAdapters.Mappers
{
    public static class CaixaRequestMapper
    {
        public static Result<TerminalCaixa> ToEntity(this CadastrarNovoCaixaRequest request)
        {
            if (
                !Enum.TryParse<TerminalTipo>(request.Tipo, ignoreCase: true, out var tipo)
                || tipo == (TerminalTipo.NaoDefinido)
            )
            {
                var valoresPermitidos = Enum.GetNames<TerminalTipo>()
                    .Where(v => v != nameof(TerminalTipo.NaoDefinido));

                return Error.Validation(
                    "CAIXA_TIPO_INEXISTENTE",
                    $"O valor '{request.Tipo}' não é válido para o campo 'Tipo'. "
                        + $"Valores aceitos: {string.Join(", ", valoresPermitidos)}."
                );
            }

            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                return Error.Validation(
                    "CAIXA_NOME_INVALIDO",
                    "o nome do terminal não informado ou inválido"
                );
            }

            if (request.NumeroTerminal <= 0)
            {
                return Error.Validation(
                    "CAIXA_TERMINAL_INVALIDO",
                    "O número do terminal informado é inválido ou está ausente."
                );
            }

            return new TerminalCaixa
            {
                Terminal = request.NumeroTerminal,
                Nome = request.Nome,
                Tipo = tipo
            };
        }
    }
}

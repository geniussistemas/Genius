using Genius.Api.InterfaceAdapters.DTO;
using Genius.Application.DTO;
using static Genius.Api.InterfaceAdapters.DTO.ConfiguracaoCaixaResponse;

namespace Genius.Api.InterfaceAdapters.Mappers
{
    public static class ConfiguracaoCaixaResponseMapper
    {
        public static ConfiguracaoCaixaResponse MapToResponse(this CaixaConfiguracao config)
        {
            var response = new ConfiguracaoCaixaResponse();

            MapearCabecalhoRodape(config, response);
            MapearConvenios(config, response);
            MapearTabelas(config, response);

            return response;
        }


        private static void MapearCabecalhoRodape(CaixaConfiguracao? config, ConfiguracaoCaixaResponse response)
        {
            response.Cabecalho = config?.DadosImpressaoTicket?.Cabecalho ?? [];
            response.Rodape = config?.DadosImpressaoTicket?.Rodape;
        }

        private static void MapearConvenios(CaixaConfiguracao? config, ConfiguracaoCaixaResponse response)
        {
            if (config?.Convenios is not { Count: > 0 }) return;

            response.Convenios = [];

            foreach (var convenio in config.Convenios)
            {
                response.Convenios.Add(new ConveniosSimplificadoResponse(convenio.Id, convenio.Nome!));
            }
        }

        private static void MapearTabelas(CaixaConfiguracao? config, ConfiguracaoCaixaResponse response)
        {
            if (config?.TabelasPrecos is not { Count: > 0 }) return;

            response.TabelasPreco = [];

            foreach (var tabela in config.TabelasPrecos)
            {
                response.TabelasPreco.Add(new TabelaPrecoSimplificadaResponse(tabela.NumTabela, tabela.NomeTabela!));
            }
        }
    }
}

namespace introducao_csharp.Output
{
    public class FactoryExportar
    {
        public static IExportar RetornarExportador(EnumTipoExportacao tipo, string nomeArquivo)
        {
            IExportar exporta;

            switch (tipo)
            {
                case EnumTipoExportacao.Csv:
                    exporta = new ExportarCsv(nomeArquivo);
                    break;
                case EnumTipoExportacao.Email:
                    exporta = new ExportarEmail(nomeArquivo);
                    break;
                default:
                    exporta = new ExportarXml(nomeArquivo);
                    break;
            }

            return exporta;
        }
    }
}
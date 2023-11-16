using System.ComponentModel;

namespace DriveAnheu.Domain.Enums
{
    public enum ItemTipoEnum
    {
        [Description("Pasta")]
        Pasta = 1,

        [Description("Arquivo do tipo imagem, documento, planilha, etc")]
        Arquivo = 2
    }
}
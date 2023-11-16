using System.ComponentModel;

namespace DriveAnheu.Domain.Enums
{
    public enum ItemTipoEnum
    {
        [Description("Pasta")]
        Pasta = 1,

        [Description("Documento de texto")]
        Doc = 2,

        [Description("Planilha eletrônica, planilha de cálculo ou folha de cálculo")]
        Planilha = 3,

        [Description("Imagem")]
        Imagem = 4
    }
}
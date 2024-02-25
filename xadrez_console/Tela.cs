namespace xadrez_console;

public static class Tela
{
    public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
    {
        for (int v = 0; v < tabuleiro.Linhas; v++)
        {
            for (int j = 0; j < tabuleiro.Colunas; j++)
            {
                if (tabuleiro.Peca(v, j) == null)
                    Console.Write("- ");
                else
                {
                    Console.Write(tabuleiro.Peca(v, j) + " ");
                }


            }
            Console.WriteLine();
        }
    }
}

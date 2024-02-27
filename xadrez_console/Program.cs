using System.Security.Cryptography;

namespace xadrez_console;

class Program
{
    static void Main(string[] args)
    {
        // bool x = true;
        // Console.WriteLine(!x);
        try
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();
            while (!partida.Terminada)
            {
                Console.Clear();
                Tela.ImprimirTabuleiro(partida._tabuleiro);
                Console.Write("\nOrigem: ");
                Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                bool[,] posicoesPossiveis = partida._tabuleiro.Peca(origem).MovimentosPossiveis();
                Console.Clear();
                Tela.ImprimirTabuleiro(partida._tabuleiro, posicoesPossiveis);


                Console.Write("\nDestino: ");
                Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                partida.ExecMovimento(origem, destino);

            }

        }
        catch (TabuleiroException x)
        {
            Console.WriteLine(x.Message);
        }

        int[,] matriz = new int[3, 4];
    }
}

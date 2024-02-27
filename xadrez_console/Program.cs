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
                Console.Write("Origem: ");
                Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                Console.Write("Destino: ");
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

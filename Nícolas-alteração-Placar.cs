using System;

class Program
{
    static void Main(string[] args)

    {
        int opcaoMenu;
        int modoJogo;

        //Placar

        int vitoriasJogador1 = 0;
        int vitoriasJoador2 = 0;
        int vitoriasJogadorMaquina = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("=== JOGO DA VELHA ===");
            Console.WriteLine("1 - Jogar");
            Console.WriteLine("2 - Instruções");
            Console.WriteLine("3 - Sair");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcaoMenu))
                opcaoMenu = 0;

            switch (opcaoMenu)
            {
                case 1:
                    // Escolher modo de jogo
                    Console.Clear();
                    Console.WriteLine("Escolha o modo de jogo:");
                    Console.WriteLine("1 - Jogador vs Jogador");
                    Console.WriteLine("2 - Jogador vs Máquina");
                    Console.Write("Digite sua escolha: ");

                    if (!int.TryParse(Console.ReadLine(), out modoJogo))
                        modoJogo = 0;

                    if (modoJogo == 1)
                    {
                        Console.WriteLine("Iniciando modo: Jogador vs Jogador...");
                        //Placar Jogador VS Jogador

                        Console.WriteLine($"Placar:  J1:{vitoriasJogador1} | J2: {vitoriasJoador2}");
                        Console.WriteLine();

                        //Lógica Placar
                        Console.WriteLine("SimulnadoVitoria");
                        vitoria

                        // Aqui depois colocamos a lógica do JxJ
                    }
                    else if (modoJogo == 2)
                    {
                        Console.WriteLine("Iniciando modo: Jogador vs Máquina...");

                        //Placar Jogador VS Maquina
                        Console.WriteLine($"Placar: J1: {vitoriasJogador1} | JM: {vitoriasJogadorMaquina}");
                        Console.WriteLine();

                        // Aqui depois colocamos a lógica do JxM
                    }
                    else
                    {
                        Console.WriteLine("Modo inválido!");
                    }

                    Console.ReadKey();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("=== INSTRUÇÕES ===");
                    Console.WriteLine("- Dois jogadores alternam jogadas.");
                    Console.WriteLine("- Vence quem fizer 3 em linha (horizontal, vertical ou diagonal).");
                    Console.WriteLine("- No modo Máquina, o computador fará jogadas automáticas.");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Saindo do jogo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    Console.ReadKey();
                    break;
            }

        } while (opcaoMenu != 3);
    }
}

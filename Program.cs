﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    // Adicionado: Dicionário estático para o ranking
    static Dictionary<string, int> ranking = new Dictionary<string, int>();
    static string[,] tabuleiro = new string[3, 3]; // tabuleiro agora é global

    static void Main()
    {
        // Adicionado: Carrega o ranking ao iniciar o programa (comentado, mas pronto para uso)
        //CarregarRanking();
        int opcaoMenu;
        int modoJogo;

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            CentralizarTexto("=== JOGO DA VELHA ===\n");
            CentralizarTexto("1 - Jogar");
            CentralizarTexto("2 - Instruções");
            CentralizarTexto("3 - Ranking");
            CentralizarTexto("4 - Sair");
            CentralizarTexto("Escolha uma opção:");

            if (!int.TryParse(Console.ReadLine(), out opcaoMenu))
                opcaoMenu = 0;

            switch (opcaoMenu)
            {
                case 1:
                    Console.Clear();
                    int modoJogoValido = 0;
                    // Loop até o usuário digitar um modo válido (1 ou 2)
                    do
                    {
                        CentralizarTexto("Escolha o modo de jogo:\n");
                        CentralizarTexto("1 - Jogador vs Jogador");
                        CentralizarTexto("2 - Jogador vs Máquina");
                        CentralizarTexto("Digite sua escolha: ");

                        string entradaModo = Console.ReadLine();
                        // Verifica se a entrada é válida (1 ou 2)
                        if (!int.TryParse(entradaModo, out modoJogoValido) || (modoJogoValido != 1 && modoJogoValido != 2))
                        {
                            CentralizarTexto("Modo inválido, tente novamente.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    } while (modoJogoValido != 1 && modoJogoValido != 2);

                    modoJogo = modoJogoValido;

                    if (modoJogo == 1)
                    {
                        Placar(modoJogo);
                        InicializarTabuleiro(modoJogo);
                        Jojarjxj(modoJogo);
                    }
                    else if (modoJogo == 2)
                    {
                        int dificuldadeValida = 0;
                        // Loop até o usuário digitar uma dificuldade válida (1, 2 ou 3)
                        do
                        {
                            Console.Clear();
                            CentralizarTexto("Escolha a dificuldade:\n");
                            CentralizarTexto("1 - Fácil");
                            CentralizarTexto("2 - Médio");
                            CentralizarTexto("3 - Difícil");
                            CentralizarTexto("Digite sua escolha:");
                            string entradaDif = Console.ReadLine();
                            if (!int.TryParse(entradaDif, out dificuldadeValida) || dificuldadeValida < 1 || dificuldadeValida > 3)
                            {
                                CentralizarTexto("Dificuldade inválida, tente novamente.");
                                Console.ReadKey();
                            }
                        } while (dificuldadeValida < 1 || dificuldadeValida > 3);

                        Placar(modoJogo);
                        InicializarTabuleiro(modoJogo);
                        Jojarjxm(modoJogo, dificuldadeValida);
                    }

                    Console.ReadKey();
                    break;

                case 2:
                    Console.Clear();
                    CentralizarTexto("=== INSTRUÇÕES ===\n");
                    CentralizarTexto("- Dois jogadores alternam jogadas.");
                    CentralizarTexto("- Vence quem fazer 3 em linha (horizontal, vertical ou diagonal).");
                    CentralizarTexto("- No modo Máquina, o computador fará jogadas automáticas.");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.Clear();
                    MostrarRanking();
                    Console.ReadKey();
                    break;

                case 4:
                    CentralizarTexto("Saindo do jogo...");
                    break;

                default:
                    CentralizarTexto("Opção inválida. Tente novamente.");
                    Console.ReadKey();
                    break;
            }

        } while (opcaoMenu != 4);
    }

    static void Jojarjxj(int modoJogo)
    {
        string jogadorAtual = "X";
        int tentativas = 0;

        InicializarTabuleiro(modoJogo);

        while (tentativas < 9)
        {
            Console.Clear();
            Placar(modoJogo);
            MostrarTabuleiro();

            CentralizarTexto($"Vez do jogador {jogadorAtual}");
            CentralizarTexto("Digite a linha (1-3): ");
            int linha = int.Parse(Console.ReadLine()) - 1;

            CentralizarTexto("Digite a coluna (1-3): ");
            int coluna = int.Parse(Console.ReadLine()) - 1;

            if (tabuleiro[linha, coluna] == "   ")
            {
                tabuleiro[linha, coluna] = $" {jogadorAtual} ";
                tentativas++;

                if (VerificarVencedor(jogadorAtual))
                {
                    Console.Clear();
                    MostrarTabuleiro();
                    CentralizarTexto($"Jogador {jogadorAtual} venceu!");
                    // Adicionado: Atualiza a pontuação no ranking
                    AtualizarPontuacao($"Jogador {jogadorAtual}");
                    // Alterado: Pergunta se deseja continuar ou voltar ao menu, sem limpar a tela
                    if (PerguntarContinuar())
                    {
                        InicializarTabuleiro(modoJogo);
                        Jojarjxj(modoJogo);
                    }
                    return;
                }

                jogadorAtual = (jogadorAtual == "X") ? "O" : "X";
            }
            else
            {
                CentralizarTexto("Posição já ocupada! Tente novamente.");
                Console.ReadKey();
            }
        }

        // Se saiu do laço sem vencedor -> empate
        Console.Clear();
        MostrarTabuleiro();
        CentralizarTexto("Deu velha! Empate.");
        // Alterado: Pergunta se deseja continuar ou voltar ao menu, sem limpar a tela
        if (PerguntarContinuar())
        {
            InicializarTabuleiro(modoJogo);
            Jojarjxj(modoJogo);
        }
    }

    static void Jojarjxm(int modoJogo, int dificuldade)
    {
        string jogadorAtual = "X";
        int tentativas = 0;
        Random rnd = new Random();

        Placar(modoJogo);
        InicializarTabuleiro(modoJogo);

        while (tentativas < 9)
        {
            Console.Clear();
            Placar(modoJogo);
            MostrarTabuleiro();

            if (jogadorAtual == "X")
            {
                CentralizarTexto("Sua vez (Jogador X)");
                CentralizarTexto("Digite a linha (1-3): ");
                int linha = int.Parse(Console.ReadLine()) - 1;

                CentralizarTexto("Digite a coluna (1-3): ");
                int coluna = int.Parse(Console.ReadLine()) - 1;

                if (tabuleiro[linha, coluna] == "   ")
                {
                    tabuleiro[linha, coluna] = $" {jogadorAtual} ";
                    tentativas++;
                }
                else
                {
                    CentralizarTexto("Posição já ocupada! Tente novamente.");
                    Console.ReadKey();
                    continue;
                }
            }
            else
            {
                CentralizarTexto("Vez da Máquina (O)");
                int linha, coluna;

                if (dificuldade == 1)
                {
                    do
                    {
                        linha = rnd.Next(0, 3);
                        coluna = rnd.Next(0, 3);
                    } while (tabuleiro[linha, coluna] != "   ");
                }
                else if (dificuldade == 2)
                {
                    if (!TentarJogar("O", out linha, out coluna))
                    {
                        do
                        {
                            linha = rnd.Next(0, 3);
                            coluna = rnd.Next(0, 3);
                        } while (tabuleiro[linha, coluna] != "   ");
                    }
                }
                else
                {
                    if (!TentarJogar("O", out linha, out coluna))
                    {
                        if (!TentarJogar("X", out linha, out coluna))
                        {
                            if (tabuleiro[1, 1] == "   ")
                            {
                                linha = 1; coluna = 1;
                            }
                            else
                            {
                                int[,] cantos = new int[,] { { 0, 0 }, { 0, 2 }, { 2, 0 }, { 2, 2 } };
                                bool jogou = false;

                                for (int i = 0; i < cantos.GetLength(0); i++)
                                {
                                    int lc = cantos[i, 0];
                                    int cc = cantos[i, 1];
                                    if (tabuleiro[lc, cc] == "   ")
                                    {
                                        linha = lc;
                                        coluna = cc;
                                        jogou = true;
                                        break;
                                    }
                                }

                                if (!jogou)
                                {
                                    do
                                    {
                                        linha = rnd.Next(0, 3);
                                        coluna = rnd.Next(0, 3);
                                    } while (tabuleiro[linha, coluna] != "   ");
                                }
                            }
                        }
                    }
                }

                tabuleiro[linha, coluna] = $" {jogadorAtual} ";
                tentativas++;
            }

            if (VerificarVencedor(jogadorAtual))
            {
                Console.Clear();
                MostrarTabuleiro();
                if (jogadorAtual == "X")
                {
                    CentralizarTexto("Você venceu!");
                    // Adicionado: Atualiza a pontuação no ranking
                    AtualizarPontuacao("Jogador X");
                }
                else
                {
                    CentralizarTexto("A máquina venceu!");
                    // Adicionado: Atualiza a pontuação no ranking
                    AtualizarPontuacao("Máquina");
                }
                // Alterado: Pergunta se deseja continuar ou voltar ao menu, sem limpar a tela
                if (PerguntarContinuar())
                {
                    InicializarTabuleiro(modoJogo);
                    Jojarjxm(modoJogo, dificuldade);
                }
                return;
            }

            jogadorAtual = (jogadorAtual == "X") ? "O" : "X";
        }

        Console.Clear();
        MostrarTabuleiro();
        CentralizarTexto("Deu velha! Empate.");
        // Alterado: Pergunta se deseja continuar ou voltar ao menu, sem limpar a tela
        if (PerguntarContinuar())
        {
            InicializarTabuleiro(modoJogo);
            Jojarjxm(modoJogo, dificuldade);
        }
    }

    static bool TentarJogar(string jogador, out int linhaEscolhida, out int colunaEscolhida)
    {
        for (int linha = 0; linha < 3; linha++)
        {
            for (int coluna = 0; coluna < 3; coluna++)
            {
                if (tabuleiro[linha, coluna] == "   ")
                {
                    tabuleiro[linha, coluna] = $" {jogador} ";
                    bool venceu = VerificarVencedor(jogador);
                    tabuleiro[linha, coluna] = "   ";

                    if (venceu)
                    {
                        linhaEscolhida = linha;
                        colunaEscolhida = coluna;
                        return true;
                    }
                }
            }
        }
        linhaEscolhida = -1;
        colunaEscolhida = -1;
        return false;
    }

    static void Placar(int modoJogo)
    {
        // Acessa o dicionário e usa 0 como valor padrão se a chave não existir
        int vitoriasJ1 = ranking.ContainsKey("Jogador X") ? ranking["Jogador X"] : 0;
        int vitoriasJ2 = ranking.ContainsKey("Jogador O") ? ranking["Jogador O"] : 0;
        int vitoriasMaquina = ranking.ContainsKey("Máquina") ? ranking["Máquina"] : 0;

        if (modoJogo == 1)
        {
            CentralizarTexto($"Placar: Jogador X: {vitoriasJ1} | Jogador O: {vitoriasJ2}");
        }
        else if (modoJogo == 2)
        {
            CentralizarTexto($"Placar: Jogador X: {vitoriasJ1} | Máquina: {vitoriasMaquina}");
        }
        Console.WriteLine();
    }

    static void InicializarTabuleiro(int modoJogo)
    {
        Console.Clear();
        Placar(modoJogo);

        for (int linha = 0; linha < tabuleiro.GetLength(0); linha++)
        {
            for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
            {
                tabuleiro[linha, coluna] = "   ";
            }
        }
    }

    static void MostrarTabuleiro()
    {
        for (int linha = 0; linha < tabuleiro.GetLength(0); linha++)
        {
            string linhaTabuleiro = "";
            for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
            {
                linhaTabuleiro += tabuleiro[linha, coluna];
                if (coluna < 2) linhaTabuleiro += "|";
            }
            CentralizarTexto(linhaTabuleiro);

            if (linha < 2)
                CentralizarTexto("---+---+---");
        }
    }

    static void CentralizarTexto(string texto)
    {
        int larguraConsole = Console.WindowWidth;
        int posicaoInicial = Math.Max(0, (larguraConsole - texto.Length) / 2);

        Console.SetCursorPosition(posicaoInicial, Console.CursorTop);
        Console.WriteLine(texto);
    }

    static bool VerificarVencedor(string jogador)
    {
        for (int linha = 0; linha < 3; linha++)
        {
            if (tabuleiro[linha, 0] == $" {jogador} " &&
                tabuleiro[linha, 1] == $" {jogador} " &&
                tabuleiro[linha, 2] == $" {jogador} ")
            {
                return true;
            }
        }

        for (int coluna = 0; coluna < 3; coluna++)
        {
            if (tabuleiro[0, coluna] == $" {jogador} " &&
                tabuleiro[1, coluna] == $" {jogador} " &&
                tabuleiro[2, coluna] == $" {jogador} ")
            {
                return true;
            }
        }

        if (tabuleiro[0, 0] == $" {jogador} " &&
            tabuleiro[1, 1] == $" {jogador} " &&
            tabuleiro[2, 2] == $" {jogador} ")
        {
            return true;
        }

        if (tabuleiro[0, 2] == $" {jogador} " &&
            tabuleiro[1, 1] == $" {jogador} " &&
            tabuleiro[2, 0] == $" {jogador} ")
        {
            return true;
        }

        return false;
    }

    // Adicionado: Implementa a exibição do ranking
    static void MostrarRanking()
    {
        Console.Clear();
        CentralizarTexto("=== RANKING ===");
        CentralizarTexto("-------------------");
        if (ranking.Count == 0) // verifica se o dicionário de ranking está vazio 
        {
            CentralizarTexto("Nenhum jogador no ranking ainda.");
        }
        else
        {
            // Ordena o dicionário pelo número de vitórias (do maior para o menor)
            var rankingOrdenado = ranking.OrderByDescending(p => p.Value);
            int i = 1;
            foreach (var player in rankingOrdenado)
            {
                CentralizarTexto($"{i}. {player.Key} - Vitórias: {player.Value}");
                i++;
            }
        }
        CentralizarTexto("-------------------");
    }

    static bool PerguntarContinuar()
    {
        while (true)
        {
            // Alterado: Não limpar a tela para manter a mensagem de vitória/empate visível
            CentralizarTexto("");
            CentralizarTexto("Você deseja continuar a partida ou voltar para o menu?");
            CentralizarTexto("Opções: 1 - Continuar   2 - Voltar para o menu");
            CentralizarTexto("Escolha uma opção: ");
            string resposta = Console.ReadLine();

            if (resposta == "1")
                return true;
            else if (resposta == "2")
                return false;
            else if (string.IsNullOrEmpty(resposta))
            {
                CentralizarTexto("Você não digitou nada. Pressione qualquer tecla para tentar novamente.");
                Console.ReadKey();
            }
            else
            {
                CentralizarTexto("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                Console.ReadKey();
            }
        }
    }

    // Adicionado: Novo método para carregar o ranking do arquivo
    static void CarregarRanking()
    {
        const string ARQUIVO = "ranking.txt";
        if (File.Exists(ARQUIVO))
        {
            var linhas = File.ReadAllLines(ARQUIVO);
            foreach (var linha in linhas)
            {
                var partes = linha.Split(',');
                if (partes.Length == 2 && int.TryParse(partes[1], out int vitorias))
                {
                    ranking[partes[0]] = vitorias;
                }
            }
        }
    }

    // Adicionado: Novo método para salvar o ranking no arquivo
    static void SalvarRanking()
    {
        const string ARQUIVO = "ranking.txt";
        var linhas = ranking.Select(p => $"{p.Key},{p.Value}").ToArray();
        File.WriteAllLines(ARQUIVO, linhas);
    }

    // Adicionado: Novo método para atualizar a pontuação
    static void AtualizarPontuacao(string nomeJogador)
    {
        if (ranking.ContainsKey(nomeJogador))
        {
            ranking[nomeJogador]++;
        }
        else
        {
            ranking[nomeJogador] = 1;
        }
        SalvarRanking();
    }
}
using System;

class GemHuntersGame
{
    static char[,] board;
    static int boardSize = 6;
    static int maxTurns = 15;
    static int turnsLeft;

    static Player p1;
    static Player p2;

    static void Main()
    {
        InitializeGame();
        PlayGame();
    }

    static void InitializeGame()
    {
        board = new char[boardSize, boardSize];
        turnsLeft = maxTurns;

        p1 = new Player("P1", 0, 0);
        p2 = new Player("P2", boardSize - 1, boardSize - 1);

        InitializeBoard();
    }

    static void InitializeBoard()
    {
        Random random = new Random();

        // Initialize board with empty spaces
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                board[i, j] = '-';
            }
        }

        // Place players
        board[p1.PositionX, p1.PositionY] = 'p1';
        board[p2.PositionX, p2.PositionY] = 'p2';

        // Place gems
        for (int i = 0; i < 5; i++)
        {
            int gemX = random.Next(boardSize);
            int gemY = random.Next(boardSize);

            // Ensure not placing gems on players or other gems
            while (board[gemX, gemY] != '-')
            {
                gemX = random.Next(boardSize);
                gemY = random.Next(boardSize);
            }

            board[gemX, gemY] = 'G';
        }

        // Place obstacles
        for (int i = 0; i < 5; i++)
        {
            int obstacleX = random.Next(boardSize);
            int obstacleY = random.Next(boardSize);

            // Ensure not placing obstacles on players, gems, or other obstacles
            while (board[obstacleX, obstacleY] != '-')
            {
                obstacleX = random.Next(boardSize);
                obstacleY = random.Next(boardSize);
            }

            board[obstacleX, obstacleY] = 'O';
        }
    }

    static void DisplayBoard()
    {
        Console.Clear();

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }

    static void PlayGame()
    {
        while (turnsLeft > 0)
        {
            DisplayBoard();

            Console.WriteLine($"Turns left: {turnsLeft}");
            Console.WriteLine($"Player 1 Gems: {p1.GemCount}");
            Console.WriteLine($"Player 2 Gems: {p2.GemCount}");
            Console.WriteLine("Player 1's turn:");

            PlayerMove(p1);

            DisplayBoard();

            Console.WriteLine($"Turns left: {turnsLeft}");
            Console.WriteLine($"Player 1 Gems: {p1.GemCount}");
            Console.WriteLine($"Player 2 Gems: {p2.GemCount}");
            Console.WriteLine("Player 2's turn:");

            PlayerMove(p2);

            turnsLeft--;
        }

        
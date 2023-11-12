// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public sealed class HangmanGameInUnityEditorWindow : EditorWindow
{
    private const string WindowName = "Hangman";
    private string secretWord = "UNITY";
    private char[] guessedWord;
    private const int maxAttempts = 6;
    private int attemptsLeft;
    private readonly List<char> guessedLetters = new();
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    [MenuItem("RimuruDev Games/Hangman Game")]
    public static void ShowWindow() =>
        GetWindow<HangmanGameInUnityEditorWindow>(false, WindowName, true);

    private void OnEnable() =>
        StartNewGame();

    private void OnGUI()
    {
        DrawHeader();
        DrawLetters();
        CheckedGameState();
    }

    private void DrawHeader()
    {
        GUILayout.Label("Welcome to Hangman!", EditorStyles.boldLabel);
        GUILayout.Label("Guess the word:");
        GUILayout.Label(new string(guessedWord));
        GUILayout.Label("Attempts left: " + attemptsLeft);
    }

    private void GameState(string label)
    {
        GUILayout.Label(label);

        if (GUILayout.Button("Play Again"))
            StartNewGame();
    }

    private void CheckedGameState()
    {
        if (attemptsLeft <= 0)
            GameState("Game Over!");
        else if (new string(guessedWord).Equals(secretWord))
            GameState("You Win!");
    }

    private void DrawLetters()
    {
        GUILayout.BeginHorizontal();

        foreach (var letter in Alphabet)
        {
            if (guessedLetters.Contains(letter))
                continue;

            if (GUILayout.Button(letter.ToString()))
            {
                guessedLetters.Add(letter);
                GuessLetter(letter);
            }
        }

        GUILayout.EndHorizontal();
    }

    private void GuessLetter(char letter)
    {
        var correctGuess = false;

        for (var i = 0; i < secretWord.Length; i++)
        {
            if (secretWord[i] == letter)
            {
                guessedWord[i] = letter;
                correctGuess = true;
            }
        }

        if (!correctGuess)
            attemptsLeft--;
    }

    private void StartNewGame()
    {
        secretWord = secretWord.ToUpper();
        guessedWord = new char[secretWord.Length];

        for (var i = 0; i < secretWord.Length; i++)
            guessedWord[i] = '_';

        attemptsLeft = maxAttempts;
        guessedLetters.Clear();
    }
}
#endif
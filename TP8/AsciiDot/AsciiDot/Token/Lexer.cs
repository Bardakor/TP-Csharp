using System;
using System.Collections.Generic;
using System.Reflection;

namespace AsciiDot.Token
{
    /**
     * <summary>Present an error during grid lexing.</summary>
     */
    public class LexerError : Exception
    {
        /**
         * The char that caused the error. 
         */
        private readonly char _c;

        /**
         * <summary>LexerError constructor.</summary>
         *
         * <param name="c">The char that cause the error.</param>
         */
        public LexerError(char c) =>
            _c = c;

        /**
         * <summary>String representation of the error.</summary>
         * 
         * <returns>A readable error message</returns>
         */
        public override string ToString() =>
            $"Invalid character: '{_c}'";
    }

    /**
     * Class To lex the board
     *
     * /!\ BIG WARNING /!\
     * 
     * BLACK MAGIC INCOMING
     * 
     * /!\ BIG WARNING /!\
     */
    public static class Lexer
    {
        /**
         * <summary>Get the given value or the default one</summary>
         *
         * <param name="array">There array to get the value</param>
         * <param name="i">The index of the element to get</param>
         * <param name="defaultValue">Default value if the index is out of range</param>
         *
         * <returns>
         * The value of the <paramref name="i"/> index if it's inside the array.
         * Otherwise the <param name="defaultValue"> value.</param>
         * </returns>
         */
        private static T GetOrDefault<T>(IReadOnlyList<T> array, int i, T defaultValue) =>
            // Check if it's inside
            i < array.Count && i >= 0
                // it's inside juste return the value
                ? array[i]
                // it's outside return de the default value
                : defaultValue;

        /**
         * <summary>Get the given char at the given index or a null character.</summary>
         *
         * <param name="array">There array to get the value</param>
         * <param name="i">The index of the char to get</param>
         *
         * <returns>
         * The character of the <paramref name="i"/> index of the <paramref name="array"/> array.
         * Otherwise a null character 
         * </returns>
         */
        private static char GetOrDefault(IReadOnlyList<char> array, int i) =>
            GetOrDefault(array, i, '\0');

        /**
         * <summary>Get the corresponding token of the table character at the coordinate x, y.</summary>
         *
         * <param name="table">The table that represent the board.</param>
         * <param name="x">the abscess of the character</param>
         * <param name="y">the ordinate of the character</param>
         *
         * <returns>The corresponding token</returns>
         *
         * <exception cref="LexerError">The character don't match any token</exception>
         * <exception cref="NotSupportedException">The developer make a mistake and don't implement well is class</exception>
         */
        public static Token Lex(char[][] table, int x, int y) =>
            (GetOrDefault(table[y], x - 1), GetOrDefault(table[y], x + 1)) switch
            {
                // if it's a vertical redirection (`[Operator]`)
                ('[', ']') => Lex(table[y][x], Direction.Up),
                // if it's a horizontal redirection (`{Operator}`)
                ('{', '}') => Lex(table[y][x], Direction.Right),
                // no redirection
                _ => Lex(GetOrDefault(table[y], x, ' '))
            };

        /**
         * <summary>List of possible simple token type.</summary>
         */
        private static readonly Type[] TokenTypes =
        {
            typeof(TokenStart),
            typeof(TokenEmpty),
            typeof(TokenEnd),
            typeof(TokenPath),
            typeof(TokenMirror),
            typeof(TokenInsertor),
            typeof(TokenReflector),
            typeof(TokenQuote),
            typeof(TokenOutput),
            typeof(TokenValue),
            typeof(TokenInput),
            typeof(TokenDuplicate),
            typeof(TokenConditional),
            typeof(TokenChar),
            typeof(TokenNumber),
        };

        /**
         * <summary>Try to parse the given char.</summary>
         * 
         * Try to parse the char <paramref name="c"/> with every possible token parser (constructor).
         *
         * <param name="c">The character that represent the given token.</param>
         *
         * <returns>The token that correspond to the given token</returns>
         *
         * <exception cref="LexerError">The character don't match any token</exception>
         * <exception cref="NotSupportedException">The developer make a mistake and don't implement well is class</exception>
         */
        private static Token Lex(char c) =>
            Lex(TokenTypes, new[] {typeof(char)}, new object[] {c});

        /**
         * <summary>List of possible directed token type.</summary>
         */
        private static readonly Type[] DirectedTokenTypes =
        {
            // TODO: Uncomment when implement
            typeof(TokenOperator)
        };

        /**
         * <summary>Try to parse the given char and direction.</summary>
         * 
         * Try to parse the char <paramref name="c"/> with the <paramref name="outputDirection"/> output direction with
         * every possible token parser (constructor).
         *
         * <param name="c">The character that represent the given token.</param>
         * <param name="outputDirection">The direction were the token output new dots</param>
         *
         * <returns>The token that correspond to the given char and direction.</returns>
         *
         * <exception cref="LexerError">The character don't match any token</exception>
         * <exception cref="NotSupportedException">The developer make a mistake and don't implement well is class</exception>
         */
        private static Token Lex(char c, Direction outputDirection)
        {
            try
            {
                return Lex(
                    DirectedTokenTypes,
                    new[] {typeof(char), typeof(Direction)},
                    new object[] {c, outputDirection}
                );
            }
            catch (LexerError)
            {
                return Lex(c);
            }
        }

        /**
         * <summary>Variadic constructor lexer.</summary>
         * 
         * Try to parse the given <paramref name="parametersValue "/> with the constructor with
         * <paramref name="parametersType"/> types of the <paramref name="tokensType"/> lists.
         *
         * NB: it doesn't matter if you don't understand this function.
         *
         * <param name="tokensType">list of possible token types.</param>
         * <param name="parametersType">list constructor's parameters types.</param>
         * <param name="parametersValue">list constructor's parameters values.</param>
         *
         * <returns>The token that correspond to the given <paramref name="parametersValue"/></returns>
         *
         * <exception cref="LexerError">The character don't match any token</exception>
         * <exception cref="NotSupportedException">The developer make a mistake and don't implement well is class</exception>
         */
        private static Token Lex(IEnumerable<Type> tokensType, Type[] parametersType, object[] parametersValue)
        {
            // Try each possible token type    
            foreach (var type in tokensType)
            {
                // try to get the constructor that as the given prototype
                var constructor = type.GetConstructor(parametersType);
                try
                {
                    // Try to create new token of the `type` type.
                    return constructor == null
                        // in case we don't have a constructor.
                        // (1. it's very sus) (2. the error is often between the keyboard and the chair)
                        // we throw an error.
                        ? throw new NotSupportedException("no corresponding constructor")
                        // if we find it juste invoke it with the character.
                        : (Token) constructor.Invoke(parametersValue);
                }
                // if there is an error during constructor 
                catch (TargetInvocationException invocation)
                {
                    // If the error is not an lexer error throw it
                    if (invocation.InnerException is not LexerError)
                        throw;
                    // If it's an Lexer error try the next token type.
                }
            }

            // if we can't find any corresponding token it's an invalid char. So throw an error.
            throw new LexerError((char) parametersValue[0]);
        }
    }
}
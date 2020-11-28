using System;
using System.Collections.Generic;
using System.Text;

namespace DGL.Utility
{
   /// <summary>
   /// Creates random callsigns similar to those used for aviation navigation charts. Every callsign will be created
   /// with a [consonant][vowel][consonant]+ or [vowel][consonant][vowel]+ pattern, and letters are chosen randomly, with this logic:
   /// 1. Frequent consonants are twice more likely to appear the normal consonants, and three times more than rare consonants.
   /// 2. Words never start with "X".
   /// 3. Words never end with "J" or "Q".
   /// 4. "Q" is always followed by "U".
   /// In this way, the words are more readable while still being completely random. If any word has an English meaning, that's
   /// just happenstance.
   /// </summary>
   public class CallsignGenerator
   {
      private static readonly List<char> Vowels = new List<char>() { 'A', 'E', 'I', 'O', 'U' };
      private static readonly List<char> FrequentConsonants = new List<char>() { 'B', 'C', 'L', 'M', 'N', 'P', 'R', 'S', 'T' };
      private static readonly List<char> NormalConsonants = new List<char>() { 'D', 'F', 'G', 'H', 'J' };
      private static readonly List<char> RareConsonants = new List<char>() { 'K', 'Q', 'V', 'X', 'Y', 'Z' };
      private int MinLetters = 3;
      private int MaxLetters = 6;
      private Random _random = new Random();
      private int _frequency;

      #region Properties

      /// <summary>
      /// Get the list of all random callsigns generated so far, in the order generated.
      /// </summary>
      public List<string> RandomNames { get; private set; } = new List<string>();

      /// <summary>
      /// Get the list of all random callsigns generated so far, in sorted order.
      /// </summary>
      public List<string> NamesSorted {
         get {
            List<string> alphabetic = new List<string>(RandomNames);
            alphabetic.Sort();
            return alphabetic;
         }
      }
     
      /// <summary>
      /// Get the list of unique names generated so far, in the order generated.
      /// </summary>
      public HashSet<string> UniqueNames { get; private set; } = new HashSet<string>();

      /// <summary>
      /// Returns the next consonant, with a 3/6 chance of being a frequent consonant,
      /// 2/6 chance of being a normal consonant, and a 1/6 chance of being a rare consonant.
      /// </summary>
      private char RandomConsonant {
         get {
            _frequency = _random.Next(1, 7);
            if(1 <= _frequency && _frequency <= 3) // 1, 2, 3
               return FrequentConsonants[_random.Next(0, FrequentConsonants.Count)];
            else if(4 <= _frequency && _frequency <= 5) // 4, 5
               return NormalConsonants[_random.Next(0, NormalConsonants.Count)];
            else // 6
               return RareConsonants[_random.Next(0, RareConsonants.Count)];
         }
      }

      /// <summary>
      /// Returns a random vowel.
      /// </summary>
      private char RandomVowel => Vowels[_random.Next(0, Vowels.Count)];

      #endregion

      #region Constructors

      /// <summary>
      /// Constructor.
      /// </summary>
      public CallsignGenerator() { }

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="minLetters">Minimum letters a callsign should have</param>
      /// <param name="maxLetters">Maximum letters a callsign should have</param>
      public CallsignGenerator(int minLetters, int maxLetters)
      {
         if (minLetters > maxLetters)
            throw new ArgumentOutOfRangeException("Minimum letters should be less than maximum letters");
         else if (minLetters <= 0 || maxLetters <= 0)
            throw new ArgumentOutOfRangeException("Values should be greater than zero");

         MinLetters = minLetters;
         MaxLetters = maxLetters;
      }

      #endregion

      /// <summary>
      /// Create a new callsign unique to this instance. Does not store the callsign.
      /// </summary>
      /// <param name="firstLetter">Optional letter the callsign should start with.</param>
      /// <returns>The generated callsign.</returns>
      public string NextUnique(char? firstLetter = null) {
         string callsign = NextRandom(firstLetter);
         while(UniqueNames.Contains(callsign))
         { // TODO add a counter that expires with enough iterations and throws an exception instead of looping
            Console.Write(callsign + " is not unique. Generating... ");
            callsign = NextRandom();
            Console.WriteLine(callsign);
         }
         UniqueNames.Add(callsign);
         return callsign;
      }

      /// <summary>
      /// Create and store a new callsign unique to this instance.
      /// </summary>
      /// <param name="firstLetter">Optional letter the callsign should start with</param>
      /// <returns>The generated callsign.</returns>
      public string AddNextUnique(char? firstLetter = null)
      {
         string callsign = NextUnique(firstLetter);
         RandomNames.Add(callsign);
         UniqueNames.Add(callsign);
         return callsign;
      }

      /// <summary>
      /// Create a new callsign (not guaranteed to be unique). Does not store the callsign.
      /// </summary>
      /// <param name="firstLetter">Optional letter the callsign should start with</param>
      /// <returns>The generated callsign.</returns>
      public string NextRandom(char? firstLetter = null) {
         int characters = _random.Next(MinLetters, MaxLetters + 1);
         StringBuilder builder = new StringBuilder();
         bool nextIsVowel = false;
         char previousConsonant;

         if(firstLetter != null)
         {
            builder.Append(firstLetter);
            characters--;
            if(Vowels.Contains((char)firstLetter))
               nextIsVowel = false;
            else
               nextIsVowel = true;
         }

         for (int i = 1; i <= characters; i++)
         {
            if (nextIsVowel == true)
            {
               builder.Append(RandomVowel);
               nextIsVowel = false;
            }
            else
            {
               previousConsonant = RandomConsonant;

               // Should not start with X
               if (i == 1 && previousConsonant == 'X')
               {
                  while (previousConsonant == 'X')
                     previousConsonant = RandomConsonant;
                  builder.Append(previousConsonant);
                  nextIsVowel = true;
               }
               // Use QU or replace Q
               else if (previousConsonant == 'Q')
               {
                  // No Q at the end of a word
                  if (i == characters)
                  {
                     while (previousConsonant == 'Q')
                        previousConsonant = RandomConsonant;
                     builder.Append(previousConsonant);
                     nextIsVowel = true;
                  }
                  // Otherwise always use QU
                  else
                  {
                     builder.Append("QU");
                     i++;
                     nextIsVowel = false;
                  }
               }
               // Never end with J
               else if (i == characters && previousConsonant == 'J')
               {
                  while (previousConsonant == 'J')
                     previousConsonant = RandomConsonant;
                  builder.Append(previousConsonant);
               }
               else
               {
                  builder.Append(previousConsonant);
                  nextIsVowel = true;
               }
            }
         }

         return builder.ToString();
      }

      /// <summary>
      /// Create and store a new callsign (not guaranteed to be unique).
      /// </summary>
      /// <param name="firstLetter">Optional letter the callsign should start with</param>
      /// <returns>The generated callsign</returns>
      public string AddNextRandom(char? firstLetter = null)
      {
         string callsign = NextRandom(firstLetter);
         RandomNames.Add(callsign);
         UniqueNames.Add(callsign);
         return callsign;
      }

      /// <summary>
      /// Generates and stores a set of non-unique callsigns.
      /// </summary>
      /// <param name="number">The number of words to generate.</param>
      /// <returns>The new set of generated callsigns</returns>
      public string[] AddRandom(int number)
      {
         if (number <= 0)
            throw new ArgumentOutOfRangeException("Number must be greater than zero");

         string[] names = new string[number];
         for (int i = 0; i < names.Length; i++)
            names[i] = AddNextRandom();
         return names;
      }

      /// <summary>
      /// Generates and stores a set of unique random callsigns.
      /// </summary>
      /// <param name="number">The number of words to generate.</param>
      /// <returns>The new set of generated callsigns</returns>
      public string[] AddUnique(int number)
      {
         if (number <= 0)
            throw new ArgumentOutOfRangeException("Number must be greater than zero");

         string[] names = new string[number];
         for (int i = 0; i < number; i++)
            names[i] = AddNextUnique();
         return names;
      }

      /// <summary>
      /// Lists all the names stored by this generator.
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         StringBuilder builder = new StringBuilder();
         builder.AppendJoin(", ", NamesSorted);
         return builder.ToString();
      }
   }
}

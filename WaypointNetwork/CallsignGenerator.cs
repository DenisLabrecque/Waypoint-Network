using System;
using System.Collections.Generic;
using System.Text;

namespace WaypointNetwork
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
      private const int MinLetters = 3;
      private const int MaxLetters = 6;
      private static Random _random;
      private static int _frequency;
      private static HashSet<string> _uniqueNames;

      /// <summary>
      /// Returns the next consonant, with a 3/6 chance of being a frequent consonant,
      /// 2/6 chance of being a normal consonant, and a 1/6 chance of being a rare consonant.
      /// </summary>
      private static char RandomConsonant {
         get {
            _frequency = _random.Next(1, 7);
            if(1 <= _frequency && _frequency <= 3) // 1, 2, 3
            {
               return FrequentConsonants[_random.Next(0, FrequentConsonants.Count)];
            }
            else if(4 <= _frequency && _frequency <= 5) // 4, 5
            {
               return NormalConsonants[_random.Next(0, NormalConsonants.Count)];
            }
            else // 6
            {
               return RareConsonants[_random.Next(0, RareConsonants.Count)];
            }
         }
      }

      /// <summary>
      /// Returns a random vowel.
      /// </summary>
      private static char RandomVowel {
         get {
            return Vowels[_random.Next(0, Vowels.Count)];
         }
      }

      /// <summary>
      /// Create a new callsign that is unique to this class instance.
      /// </summary>
      public string Unique {
         get {
            string callsign = Random;
            while(_uniqueNames.Contains(callsign))
            {
               Console.Write(callsign + " is not unique. Generating... ");
               callsign = Random;
               Console.WriteLine(callsign);
            }
            _uniqueNames.Add(callsign);
            return callsign;
         }
      }

      /// <summary>
      /// Create a new callsign that is not guaranteed to be unique.
      /// </summary>
      public string Random {
         get {
            int characters = _random.Next(MinLetters, MaxLetters + 1);
            StringBuilder builder = new StringBuilder();
            bool isVowel = false;
            char lastConsonant;

            for (int i = 1; i <= characters; i++)
            {
               if (isVowel == true)
               {
                  builder.Append(RandomVowel);
                  isVowel = false;
               }
               else
               {
                  lastConsonant = RandomConsonant;

                  // Should not start with X
                  if (i == 1 && lastConsonant == 'X')
                  {
                     while (lastConsonant == 'X')
                     {
                        lastConsonant = RandomConsonant;
                     }
                     builder.Append(lastConsonant);
                     isVowel = true;
                  }
                  // Use QU or replace Q
                  else if (lastConsonant == 'Q')
                  {
                     // No Q at the end of a word
                     if (i == characters)
                     {
                        while (lastConsonant == 'Q')
                        {
                           lastConsonant = RandomConsonant;
                        }
                        builder.Append(lastConsonant);
                        isVowel = true;
                     }
                     // Otherwise always use QU
                     else
                     {
                        builder.Append("QU");
                        i++;
                        isVowel = false;
                     }
                  }
                  // Never end with J
                  else if (i == characters && lastConsonant == 'J')
                  {
                     while (lastConsonant == 'J')
                     {
                        lastConsonant = RandomConsonant;
                     }
                     builder.Append(lastConsonant);
                  }
                  else
                  {
                     builder.Append(lastConsonant);
                     isVowel = true;
                  }
               }
            }

            return builder.ToString();
         }
      }

      /// <summary>
      /// Constructor.
      /// </summary>
      public CallsignGenerator()
      {
         _random = new Random();
         _uniqueNames = new HashSet<string>();
      }

      /// <summary>
      /// Gets a set of non-unique callsigns.
      /// </summary>
      /// <param name="number">The number of words to generate.</param>
      /// <returns>A set of random strings in the form [consonant][vowel][consonant]+ or [vowel][consonant][vowel]+.</returns>
      public List<string> RandomCallsigns(int number)
      {
         List<string> names = new List<string>();
         for(int i = 1; i <= number; i++)
         {
            names.Add(Random);
         }
         return names;
      }

      /// <summary>
      /// Gets a set of unique random callsigns.
      /// </summary>
      /// <param name="number">The number of words to generate.</param>
      /// <returns>A set of random strings in the form [consonant][vowel][consonant]+ or [vowel][consonant][vowel]+.</returns>
      public List<string> UniqueCallsigns(int number)
      {
         List<string> names = new List<string>();
         for (int i = 1; i <= number; i++)
         {
            names.Add(Unique);
         }
         return names;
      }
   }
}

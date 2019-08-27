using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveStadium.Core
{
    public class GBStringEncoding
    {

        private static char[] GBEncoding = {
            ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
            ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
            ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
            ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
            ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
            ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
            ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
            ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
            'A', 'B','C','D','E','F','G','H','I','J','K','L','M','N','O','P',
            'Q', 'R','S','T','U','V','W','X','Y','Z','(',')',':',';','[',']',
            'a', 'b','c','d','e','f','g','h','i','j','k','l','m','n','o','p',
            'q', 'r','s','t','u','v','w','x','y','z','é','d','l','s','t','v',
            ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
            ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
            ' ', '\'',' ','-','r','m','?','!','.',' ',' ',' ','▷','▶','▼','♂',
            ' ', ' ',' ','.','/',',','♀','1','2','3','4','5','6','7','8','9',
    };

        public static int EncodeChar(char c)
        {
            int i = 0;

            for (i = 0; i < GBEncoding.Length; i++)
                if (GBEncoding[i] == c)
                    break;

            return i;
        }

        public static string DecodeString(int block)
        {
            string str = "";

            for (int i = 0; i < 4; i++)
            {
                var c = ((block >> (8 * (3-i))) & 0xFF);
                if (c > 0x60)
                {
                    str += GBEncoding[c];
                }
            }

            return str;
        }

        public static int[] EncodeString(string s)
        {
            int[] o = new int[3];

            for(int i = 0; i < s.Length; i++)
            {
                o[i / 4] |= EncodeChar(s[i]) << (8 * (3 - (i % 4)));
            }

            return o;
        }
    }
}

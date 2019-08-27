using System;
using System.Collections.Generic;
using System.Drawing;

namespace SaveStadium.WinForm
{
    public class PokemonIcons
    {
        private static List<Bitmap> Icons = new List<Bitmap>();
        private static List<Bitmap> UnownIcons = new List<Bitmap>();

        public static Bitmap GetIcon(int pokemonID, int UnownID = 0)
        {
            if(Icons.Count == 0)
            {
                var res = Properties.Resources.pokemonicons;

                for(int i = 0; i < res.Width / 45; i++)
                {
                    Rectangle cloneRect = new Rectangle(i * 45, 0, 45, 45);
                    Bitmap bmp = res.Clone(cloneRect, res.PixelFormat);
                    Icons.Add(bmp);
                }

                for (int i = 0; i < 25; i++)
                {
                    Rectangle cloneRect = new Rectangle((i + 254) * 45, 0, 45, 45);
                    Bitmap bmp = res.Clone(cloneRect, res.PixelFormat);
                    UnownIcons.Add(bmp);
                }
            }

            if (UnownID != 0)
                return UnownIcons[UnownID - 1];

            return Icons[pokemonID];
        }
    }
}

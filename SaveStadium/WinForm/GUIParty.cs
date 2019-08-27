using SaveStadium.Core;
using System;
using System.Windows.Forms;

namespace SaveStadium.WinForm
{
    public class GUIParty : GroupBox
    {
        private Label SetLabel = new Label();
        public GUIPokemon[] Pokemon = new GUIPokemon[6];

        public GUIParty()
        {
            BackgroundImage = Properties.Resources.registerback;
            BackgroundImageLayout = ImageLayout.Center;

            Text = "Set";

            Width = 120 * 3;
            Height = 64 * 2 + 20;

            for(int i = 0; i < Pokemon.Length; i++)
            {
                Pokemon[i] = new GUIPokemon();
                var x = i * 120;
                var y = 0;

                if(i > 2)
                {
                    x = (i - 3) * 120;
                    y = 64;
                }

                Pokemon[i].SetBounds(x + 4, y + 16, 120, 64);
                Controls.Add(Pokemon[i]);
            }
        }

        public void SetParty(Party party)
        {
            for(int i =0; i < 6; i++)
            {
                Pokemon[i].SetPokemon(party.Pokemon[i]);
            }
        }
    }
}

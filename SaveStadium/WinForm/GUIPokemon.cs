using SaveStadium.Core;
using System.Windows.Forms;

namespace SaveStadium.WinForm
{
    public class GUIPokemon : Panel
    {
        private PictureBox PictureBox = new PictureBox();
        private Label PokemonName = new Label();
        private Label PokemonInfo = new Label();

        private Pokemon Pokemon;

        public GUIPokemon()
        {
            BackColor = System.Drawing.Color.Transparent;

            Width = 128;
            Height = 64;

            ForeColor = System.Drawing.Color.White;

            PictureBox.Width = 40;
            PictureBox.Dock = DockStyle.Left;
            PokemonName.Dock = DockStyle.Top;
            PokemonInfo.Dock = DockStyle.Top;
            Controls.Add(PokemonInfo);
            Controls.Add(PokemonName);
            Controls.Add(PictureBox);

            ContextMenu = new ContextMenu();
            {
                var item = new MenuItem("Open");
                item.Click += (sender, args) =>
                {
                    if(Pokemon != null)
                        Program.Instance.LoadPokemon(Pokemon);
                };
                ContextMenu.MenuItems.Add(item);
            }
            {
                var item = new MenuItem("Set");
                item.Click += (sender, args) =>
                {
                    if (Pokemon == null)
                        Pokemon = new Pokemon();
                    Program.Instance.SetPokemon(Pokemon);
                    SetPokemon(Pokemon);
                };
                ContextMenu.MenuItems.Add(item);
            }
        }

        public Pokemon GetPokemon()
        {
            return Pokemon;
        }

        public void SetPokemon(Pokemon pokemon)
        {
            Pokemon = pokemon;
            PictureBox.Image = null;
            PokemonName.Text = "";

            if (pokemon == null)
                return;

            var pinfo = PokemonDatabase.GetPokemonInfoFromID(pokemon.PokemonID);

            PictureBox.Image = PokemonIcons.GetIcon(pokemon.PokemonID);

            PokemonName.Text = pinfo.Name;
            if (pokemon.NickName != "")
                PokemonName.Text = pokemon.NickName;
            
            PokemonInfo.Text = "Lv: " + pokemon.Level + " " + (pinfo.HasGender() ? (pinfo.IsFemale(pokemon.ATKIV) ? "♀" : "♂") : "-"); ;
        }
    }
}

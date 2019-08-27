using SaveStadium.Core;
using SaveStadium.WinForm;
using System;
using System.Windows.Forms;

namespace SaveStadium
{
    //TODO
    /*
     * Export pokemon to pkm formats
     * various non-registered pokemon stuff
     * Catch location information (crystal only not important)
     */

    public partial class Main : Form
    {
        private static string Filter = "Pokemon Stadium 2 Save (*.sav*.fla)|*.sav;*.fla";

        private string FilePath;
        private PS2_Save Save;
        private GUIParty[] PartySets = new GUIParty[10];

        public Main()
        {
            InitializeComponent();

            buttonLittleCup.Enabled = false;
            buttonPokeCup.Enabled = false;
            buttonPrimeCup.Enabled = false;
            buttonFreeBattle.Enabled = false;
            buttonGymCastle.Enabled = false;

            for (int i = 9; i >= 0; i--)
            {
                PartySets[i] = new GUIParty();
                PartySets[i].Text = "Set " + (i + 1);
                PartySets[i].Dock = DockStyle.Top;
                RegisteredSetPanels.Controls.Add(PartySets[i]);
            }
            
            for(int i = 0; i < 252; i++)
            {
                cbSpecies.Items.Add(PokemonDatabase.GetPokemonInfoFromID(i).Name);
            }

            foreach(var attack in PokemonDatabase.AttackInfo)
            {
                cbAttack1.Items.Add(attack.Name);
                cbAttack2.Items.Add(attack.Name);
                cbAttack3.Items.Add(attack.Name);
                cbAttack4.Items.Add(attack.Name);
            }

            cbSpecies.SelectedIndex = 0;
            cbHeldItem.SelectedIndex = 0;
            cbCaughtTime.SelectedIndex = 0;
            cbCaughtGender.SelectedIndex = 0;
            cbLocation.SelectedIndex = 0;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = Filter;

                if(d.ShowDialog() == DialogResult.OK)
                {
                    FilePath = d.FileName;
                    Save = new PS2_Save();
                    Save.Open(d.FileName);
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    saveToolStripMenuItem.Enabled = true;
                    saveAsToolStripMenuItem.Enabled = true;
                    buttonLittleCup.Enabled = true;
                    buttonPokeCup.Enabled = true;
                    buttonPrimeCup.Enabled = true;
                    buttonFreeBattle.Enabled = true;
                    buttonGymCastle.Enabled = true;
                    buttonLittleCup_Click(null, null);
                }
            }
        }

        public void LoadPokemon(Pokemon p)
        {
            tbNickname.Text = p.NickName;
            cbSpecies.SelectedIndex = p.PokemonID;
            nudLevel.Value = p.Level;
            tbEXP.Text = p.EXP.ToString();

            cbHeldItem.SelectedIndex = p.HeldItem;
            nudFriendship.Value = p.Friendship;
            tbTrainerName.Text = p.TrainerName;
            nudTrainerID.Value = p.TrainerID;

            nudATKIV.Value = p.ATKIV;
            nudDEFIV.Value = p.DEFIV;
            nudSPCIV.Value = p.SPCIV;
            nudSPDIV.Value = p.SPDIV;

            nudHPEV.Value = (int)Math.Min(255, Math.Sqrt(p.HPEV) / 4);
            nudATKEV.Value = (int)Math.Min(255, Math.Sqrt(p.ATKEV) / 4);
            nudDEFEV.Value = (int)Math.Min(255, Math.Sqrt(p.DEFEV) / 4);
            nudSPCEV.Value = (int)Math.Min(255, Math.Sqrt(p.SPCEV) / 4);
            nudSPDEV.Value = (int)Math.Min(255, Math.Sqrt(p.SPDEV) / 4);

            cbAttack1.SelectedIndex = p.Attack1;
            cbAttack2.SelectedIndex = p.Attack2;
            cbAttack3.SelectedIndex = p.Attack3;
            cbAttack4.SelectedIndex = p.Attack4;
            
            nudPP1.Value = p.PP1;
            nudPP2.Value = p.PP2;
            nudPP3.Value = p.PP3;
            nudPP5.Value = p.PP4;

            nudPPUp1.Value = p.PPUp1;
            nudPPUp2.Value = p.PPUp2;
            nudPPUp3.Value = p.PPUp3;
            nudPPUp4.Value = p.PPUp4;

            cbCaughtGender.SelectedIndex = p.CaughtGender;
            cbCaughtTime.SelectedIndex = p.CaughtDay;
            cbLocation.SelectedIndex = p.CaughtLocation;
            nudCaughtLevel.Value = p.CaughtLevel;

            nudPokerusStrain.Value = p.PokerusStrain;
            nudPokerusDays.Value = p.PokerusDays;
        }

        public void SetPokemon(Pokemon p)
        {
            p.NickName = tbNickname.Text;
            p.PokemonID = (byte)cbSpecies.SelectedIndex;
            p.Level = (byte)nudLevel.Value;
            p.EXP = int.Parse(tbEXP.Text);

            p.HeldItem = (byte)cbHeldItem.SelectedIndex;
            p.Friendship = (byte)nudFriendship.Value;
            p.TrainerID = (ushort)nudTrainerID.Value;
            p.TrainerName = p.TrainerName;

            p.ATKIV = (byte)nudATKIV.Value;
            p.DEFIV = (byte)nudDEFIV.Value;
            p.SPCIV = (byte)nudSPCIV.Value;
            p.SPDIV = (byte)nudSPDIV.Value;

            p.HPEV = (ushort)(Math.Pow((int)nudHPEV.Value * 4, 2));
            p.ATKEV = (ushort)(Math.Pow((int)nudATKEV.Value * 4, 2));
            p.DEFEV = (ushort)(Math.Pow((int)nudDEFEV.Value * 4, 2));
            p.SPCEV = (ushort)(Math.Pow((int)nudSPCEV.Value * 4, 2));
            p.SPDEV = (ushort)(Math.Pow((int)nudSPDEV.Value * 4, 2));

            p.Attack1 = (byte)cbAttack1.SelectedIndex;
            p.Attack2 = (byte)cbAttack2.SelectedIndex;
            p.Attack3 = (byte)cbAttack3.SelectedIndex;
            p.Attack4 = (byte)cbAttack4.SelectedIndex;

            p.PP1 = (byte)nudPP1.Value;
            p.PP2 = (byte)nudPP2.Value;
            p.PP3 = (byte)nudPP3.Value;
            p.PP4 = (byte)nudPP5.Value;

            p.PPUp1 = (byte)nudPPUp1.Value;
            p.PPUp2 = (byte)nudPPUp2.Value;
            p.PPUp3 = (byte)nudPPUp3.Value;
            p.PPUp4 = (byte)nudPPUp4.Value;

            p.CaughtDay = (byte)cbCaughtTime.SelectedIndex;
            p.CaughtGender = (byte)cbCaughtGender.SelectedIndex;
            p.CaughtLocation = (byte)cbLocation.SelectedIndex;
            p.CaughtLevel = (byte)nudCaughtLevel.Value;

            p.PokerusStrain = (int)nudPokerusStrain.Value;
            p.PokerusDays = (int)nudPokerusDays.Value;
        }

        private void tbNickname_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = PokemonIcons.GetIcon(cbSpecies.SelectedIndex);
            UpdateFinalStats(null, null);
        }

        private void nudLevel_ValueChanged(object sender, EventArgs e)
        {
            UpdateFinalStats(null, null);
        }

        private void UpdateFinalStats(object sender, EventArgs e)
        {
            var pokeInfo = PokemonDatabase.GetPokemonInfoFromID(cbSpecies.SelectedIndex);

            if (pokeInfo.HasGender())
            {
                if (pokeInfo.IsFemale((int)nudATKIV.Value))
                    lbGender.Text = "♀";
                else
                    lbGender.Text = "♂";
            }
            else
                lbGender.Text = "-";

            if (IsShiny())
                lbGender.Text += "*";
            
            tbEXP.Text = pokeInfo.GetEXPForLevel((int)nudLevel.Value).ToString();

            var hpiv = (((int)nudATKIV.Value & 0x1) << 3) | (((int)nudDEFIV.Value & 0x1) << 2) | (((int)nudSPDIV.Value & 0x1) << 1) | ((int)nudSPCIV.Value & 0x1);

            nudHPIV.Value = hpiv;

            lbCalHP.Text        = pokeInfo.CalcuateHP((int)nudLevel.Value, hpiv, (int)nudHPEV.Value).ToString();
            lbCalATK.Text       = pokeInfo.CalcuateATK((int)nudLevel.Value, (int)nudATKIV.Value, (int)nudDEFEV.Value).ToString();
            lbCalDEF.Text       = pokeInfo.CalcuateDEF((int)nudLevel.Value, (int)nudDEFIV.Value, (int)nudDEFEV.Value).ToString();
            lbCalSPCATK.Text    = pokeInfo.CalcuateSPCATK((int)nudLevel.Value, (int)nudSPCIV.Value, (int)nudSPCEV.Value).ToString();
            lbCalSPCDEF.Text    = pokeInfo.CalcuateSPCDEF((int)nudLevel.Value, (int)nudSPCIV.Value, (int)nudSPCEV.Value).ToString();
            lbCalSPD.Text       = pokeInfo.CalcuateSPD((int)nudLevel.Value, (int)nudSPDIV.Value, (int)nudSPDEV.Value).ToString();

            if (cbSpecies.SelectedIndex == 201)
                pictureBox1.Image = PokemonIcons.GetIcon(cbSpecies.SelectedIndex, CalculateUnownType());
        }

        private void UpdatePP(object sender, EventArgs args)
        {
            if (cbAttack1.SelectedIndex != -1)
                nudPP1.Value = CalculatePP((int)nudPP1.Value, PokemonDatabase.AttackInfo[cbAttack1.SelectedIndex].PP, (int)nudPPUp1.Value);
            if (cbAttack2.SelectedIndex != -1)
                nudPP2.Value = CalculatePP((int)nudPP2.Value, PokemonDatabase.AttackInfo[cbAttack2.SelectedIndex].PP, (int)nudPPUp2.Value);
            if (cbAttack3.SelectedIndex != -1)
                nudPP3.Value = CalculatePP((int)nudPP3.Value, PokemonDatabase.AttackInfo[cbAttack3.SelectedIndex].PP, (int)nudPPUp3.Value);
            if (cbAttack4.SelectedIndex != -1)
                nudPP5.Value = CalculatePP((int)nudPP5.Value, PokemonDatabase.AttackInfo[cbAttack4.SelectedIndex].PP, (int)nudPPUp4.Value);
        }

        private int CalculatePP(int value, int basepp, int ppup)
        {
            value = basepp;
            if (ppup > 0)
            {
                var inc = basepp / 5;
                value += inc * ppup;
            }
            if (value > 63)
                value = 63;
            return value;
        }

        private bool IsShiny()
        {
            return nudDEFIV.Value == 10 && nudSPDIV.Value == 10 && nudSPCIV.Value == 10
                && (nudATKIV.Value == 2 || nudATKIV.Value == 3
                    || nudATKIV.Value == 6 || nudATKIV.Value == 7
                    || nudATKIV.Value == 10 || nudATKIV.Value == 11
                    || nudATKIV.Value == 14 || nudATKIV.Value == 15);
        }
        
        private int CalculateUnownType()
        {
            var atk = ((int)nudATKIV.Value >> 1) & 0x3;
            var def = ((int)nudDEFIV.Value >> 1) & 0x3;
            var spc = ((int)nudSPCIV.Value >> 1) & 0x3;
            var spd = ((int)nudSPDIV.Value >> 1) & 0x3;

            var nib = (atk << 6) | (def << 4) | (spd << 2) | spc;

            return (int)Math.Floor(nib / 10f);
        }

        private void buttonLittleCup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
                PartySets[i].SetParty(Save.LittleCup[i]);
        }

        private void buttonPokeCup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
                PartySets[i].SetParty(Save.PokeCup[i]);
        }

        private void buttonPrimeCup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
                PartySets[i].SetParty(Save.PrimeCup[i]);
        }

        private void buttonChallengeCup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
                PartySets[i].SetParty(Save.FreeBattle[i]);
        }

        private void buttonGymCastle_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
                PartySets[i].SetParty(Save.GymCastle[i]);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save.Save(FilePath);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog d = new SaveFileDialog())
            {
                d.Filter = Filter;
                d.FileName = System.IO.Path.GetFileName(FilePath);

                if(d.ShowDialog() == DialogResult.OK)
                {
                    FilePath = d.FileName;
                    Save.Save(FilePath);
                }
            }
        }

        private void nudPokerusStrain_ValueChanged(object sender, EventArgs e)
        {
            nudPokerusDays.Value = (nudPokerusStrain.Value % 4) + 1;
            if(nudPokerusStrain.Value > 0)
            {
                pokerusLabel.Text = $"Type {(char)((int)'A' + (int)nudPokerusStrain.Value % 4)}";
            }
            else
                pokerusLabel.Text = "None";
        }
    }
}

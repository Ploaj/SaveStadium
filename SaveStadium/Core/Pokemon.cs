using System;
using System.Text;

namespace SaveStadium.Core
{
    /// <summary>
    /// Represents a pokemon data structure
    /// </summary>
    public class Pokemon
    {
        private int[] _data = new int[0xF];

        public byte PokemonID   { get => (byte)((_data[0] >> 24) & 0xFF); set => _data[0] = (int)(_data[0] & 0x00FFFFFFL) | (value << 24); }
        public byte HeldItem    { get => (byte)((_data[0] >> 16) & 0xFF); set => _data[0] = (int)(_data[0] & 0xFF00FFFFL) | (value << 16); }
        public byte Attack1     { get => (byte)((_data[0] >> 8) & 0xFF);  set => _data[0] = (int)(_data[0] & 0xFFFF00FFL) | (value << 8); }
        public byte Attack2     { get => (byte)((_data[0] >> 0) & 0xFF);  set => _data[0] = (int)(_data[0] & 0xFFFFFF00L) | (value); }

        public byte Attack3     { get => (byte)((_data[1] >> 24) & 0xFF); set => _data[1] = (int)(_data[1] & 0x00FFFFFFL) | (value << 24); }
        public byte Attack4     { get => (byte)((_data[1] >> 16) & 0xFF); set => _data[1] = (int)(_data[1] & 0xFF00FFFFL) | (value << 16); }
        public ushort TrainerID { get => (ushort)(_data[1]&0xFFFF); set => _data[1] = (int)(_data[1] & 0xFFFF0000L) | value; }
        
        public int EXP { get => _data[2]; set => _data[2] = value; }

        public ushort HPEV { get => (ushort)((_data[3] >> 16) & 0xFFFF); set => _data[3] = (_data[3] & 0x0000FFFF) | (value << 16); }
        public ushort ATKEV { get => (ushort)(_data[3] & 0xFFFF); set => _data[3] = (int)(_data[3] & 0xFFFF0000L) | value; }

        public ushort DEFEV { get => (ushort)((_data[4] >> 16) & 0xFFFF); set => _data[4] = (_data[4] & 0x0000FFFF) | (value << 16); }
        public ushort SPDEV { get => (ushort)(_data[4] & 0xFFFF); set => _data[4] = (int)(_data[4] & 0xFFFF0000L) | value; }

        public ushort SPCEV { get => (ushort)((_data[5] >> 16) & 0xFFFF); set => _data[5] = (_data[5] & 0x0000FFFF) | (value << 16); }
        private ushort DVS { get => (ushort)(_data[5] & 0xFFFF); set => _data[5] = (int)(_data[5] & 0xFFFF0000L) | value; }

        public byte PP1 { get => (byte)((_data[6] >> 24) & 0x3F); set => _data[6] = (int)(_data[6] & 0xC0FFFFFFL) | ((value&0x3F) << 24); }
        public byte PP2 { get => (byte)((_data[6] >> 16) & 0x3F); set => _data[6] = (int)(_data[6] & 0xFFC0FFFFL) | ((value & 0x3F) << 16); }
        public byte PP3 { get => (byte)((_data[6] >> 8) & 0x3F);  set => _data[6] = (int)(_data[6] & 0xFFFFC0FFL) | ((value & 0x3F) << 8); }
        public byte PP4 { get => (byte)((_data[6] >> 0) & 0x3F);  set => _data[6] = (int)(_data[6] & 0xFFFFFFC0L) | ((value & 0x3F)); }
        
        public byte PPUp1 { get => (byte)((_data[6] >> 30) & 0x3); set => _data[6] = (int)(_data[6] & 0x3FFFFFFFL) | ((value & 0x3) << 30); }
        public byte PPUp2 { get => (byte)((_data[6] >> 24) & 0x3); set => _data[6] = (int)(_data[6] & 0xFF3FFFFFL) | ((value & 0x3) << 24); }
        public byte PPUp3 { get => (byte)((_data[6] >> 14) & 0x3); set => _data[6] = (int)(_data[6] & 0xFFFF3FFFL) | ((value & 0x3) << 14); }
        public byte PPUp4 { get => (byte)((_data[6] >> 6) & 0x3);  set => _data[6] = (int)(_data[6] & 0xFFFFFF3FL) | ((value & 0x3) << 6); }

        public byte Friendship { get => (byte)((_data[7] >> 24) & 0xFF); set => _data[7] = (int)(_data[7] & 0x00FFFFFFL) | (value << 24); }
        public byte Level { get => (byte)((_data[7] >> 16) & 0xFF);
            set
            {
                _data[7] = (int)(_data[7] & 0xFF00FFFFL) | (value << 16);
                EXP = PokemonDatabase.GetPokemonInfoFromID(PokemonID).GetEXPForLevel(value);
            }
        }

        private byte Pokerus { get => (byte)((_data[7] >> 8) & 0xFF); set => _data[8] = (int)(_data[7] & 0xFFFF00FFL) | (value << 8); }

        public int PokerusStrain { get => Pokerus >> 4; set => Pokerus = (byte)((Pokerus & 0x0F) | ((value & 0xF) << 4)); }
        public int PokerusDays { get => Pokerus & 0xF; set => Pokerus = (byte)((Pokerus & 0xF0) | (value & 0xF)); }

        private int CaughtData { get => _data[8] >> 8; set => _data[8] = value << 8; }

        public byte CaughtLocation  { get => (byte)(CaughtData & 0x7F); set => CaughtData = (CaughtData & 0xFF80) | (value & 0x7F); }
        public byte CaughtGender    { get => (byte)((CaughtData >> 7) & 1); set => CaughtData = (CaughtData & 0xFF7F) | ((value & 0x1) << 7); }

        public byte CaughtLevel     { get => (byte)((CaughtData >> 8) & 0x3F); set => CaughtData = (CaughtData & 0xC0FF) | ((value & 0x3F) << 8); }
        public byte CaughtDay       { get => (byte)((CaughtData >> 14) & 0x03); set => CaughtData = (CaughtData & 0x3FFF) | ((value & 0x03) << 14); }

        public string NickName
        {
            get
            {
                StringBuilder nn = new StringBuilder();
                nn.Append(GBStringEncoding.DecodeString(_data[9]));
                nn.Append(GBStringEncoding.DecodeString(_data[10]));
                nn.Append(GBStringEncoding.DecodeString(_data[11]));
                return nn.ToString();
            }
            set
            {
                var en = GBStringEncoding.EncodeString(value);
                _data[9] = en[0];
                _data[10] = en[1];
                _data[11] = en[2];
            }
        }

        public string TrainerName
        {
            get
            {
                StringBuilder nn = new StringBuilder();
                nn.Append(GBStringEncoding.DecodeString(_data[12]));
                nn.Append(GBStringEncoding.DecodeString(_data[13]));
                nn.Append(GBStringEncoding.DecodeString(_data[14]));
                return nn.ToString();
            }
            set
            {
                var en = GBStringEncoding.EncodeString(value);
                _data[12] = en[0];
                _data[13] = en[1];
                _data[14] = en[2];
            }
        }
        
        public byte ATKIV { get => (byte)((DVS >> 12) & 0xF); set => DVS = (ushort)((DVS & 0x0FFF) | ((value & 0xF) << 12)); }
        public byte DEFIV { get => (byte)((DVS >> 8) & 0xF); set => DVS = (ushort)((DVS & 0xF0FF) | ((value & 0xF) << 8)); }
        public byte SPDIV { get => (byte)((DVS >> 4) & 0xF); set => DVS = (ushort)((DVS & 0xFF0F) | ((value & 0xF) << 4)); }
        public byte SPCIV { get => (byte)((DVS) & 0xF); set => DVS = (ushort)((DVS & 0xFFF0) | ((value & 0xF))); }
        

        public Pokemon()
        {
            PokemonID = 1;
            Level = 5;
            Pokerus = 0x04;
        }

        public Pokemon(DataReader reader)
        {
            for (int i = 0; i < _data.Length; i++)
                _data[i] = reader.ReadInt32();
        }

        public byte[] GetBytes(bool bigEndian)
        {
            byte[] o = new byte[60];
            using (DataWriter writer = new DataWriter())
            {
                writer.BigEndian = bigEndian;
                foreach (var v in _data)
                    writer.WriteInt(v);
                o = writer.GetData();
            }
            return o;
        }
    }
}

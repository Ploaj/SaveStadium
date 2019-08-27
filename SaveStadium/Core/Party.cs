namespace SaveStadium.Core
{
    public class Party
    {
        public Pokemon[] Pokemon = new Pokemon[6];

        public ushort TrainerID { get; set; }
        public string TrainerName { get; set; }

        public bool IsUsed
        {
            get
            {
                bool used = false;
                foreach (var p in Pokemon)
                    if (p.PokemonID != 0)
                        used = true;
                return used;
            }
        }

        public Party()
        {

        }

        public Party(DataReader reader)
        {
            var inUse = reader.ReadInt32();

            TrainerID = (ushort)(inUse & 0xFFFF);
            TrainerName += GBStringEncoding.DecodeString(reader.ReadInt32())
                + GBStringEncoding.DecodeString(reader.ReadInt32())
                + GBStringEncoding.DecodeString(reader.ReadInt32());

            for (int i = 0; i < 6; i++)
                Pokemon[i] = new Pokemon(reader);

            var p3magic = reader.ReadInt32();
            var checksum = reader.ReadInt32();
        }

        public byte[] GetBytes(bool bigEndian)
        {
            byte[] o = new byte[0x180];
            using (DataWriter writer = new DataWriter())
            {
                writer.BigEndian = bigEndian;

                writer.WriteInt((TrainerID) | ((IsUsed ? 0x0100 : 0) << 16));

                if(TrainerName == "")
                {
                    writer.WriteInt(0x50000000);
                    writer.WriteInt(0);
                    writer.WriteInt(0);
                }
                else
                {
                    var en = GBStringEncoding.EncodeString(TrainerName);
                    writer.WriteInt(en[0]);
                    writer.WriteInt(en[1]);
                    writer.WriteInt(en[2]);
                }

                for (int i = 0; i < Pokemon.Length; i++)
                    writer.Write(Pokemon[i].GetBytes(bigEndian));

                writer.WriteInt(0x00005033);
                
                var sum = writer.CheckSum(0, 0x180 - 4) + 0x76 + 0x30;

                writer.WriteInt((0x7630 << 16) | (ushort)sum);

                o = writer.GetData();
            }
            
            return o;
        }
    }
}

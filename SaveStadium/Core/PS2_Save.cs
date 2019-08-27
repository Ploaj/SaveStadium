using System;

namespace SaveStadium.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class PS2_Save
    {
        private byte[] SaveData;
        private bool BigEndian = false;

        // Structure 1 - 0x3C00 has the registered sets for the cups
        public Party[] FreeBattle = new Party[10];
        public Party[] LittleCup = new Party[10];
        public Party[] PokeCup = new Party[10];
        public Party[] PrimeCup = new Party[10];

        // Structure 2 - ?? related to the gym castle 0x4000 - 0x3198
        
        public Party[] GymCastle = new Party[10];
        public Party[] GymCastle2 = new Party[10];

        // Structure 3 - ?? has stuff for the academy 0x3EDC

        // 0x1A0 - some other data? maybe related to stadium

        // 0x618 - academy

        // 0x10000+ mostly a copy of everything, but used in round 2?

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void Open(string filePath)
        {
            SaveData = System.IO.File.ReadAllBytes(filePath);
            using (DataReader reader = new DataReader(filePath))
            {
                if (filePath.EndsWith(".fla"))
                    reader.BigEndian = false;
                else
                    reader.BigEndian = true;

                BigEndian = reader.BigEndian;

                for (int i = 0; i < 10; i++)
                    FreeBattle[i] = new Party(reader);

                for (int i = 0; i < 10; i++)
                    LittleCup[i] = new Party(reader);

                for (int i = 0; i < 10; i++)
                    PokeCup[i] = new Party(reader);

                for (int i = 0; i < 10; i++)
                    PrimeCup[i] = new Party(reader);


                // 14 of these?
                reader.Seek(0x4000);

                for (int i = 0; i < 10; i++)
                    GymCastle[i] = new Party(reader);

                for (int i = 0; i < 10; i++)
                    GymCastle2[i] = new Party(reader);

                // anything goes starts again at 0x10000?
            }
        }

        public void Save(string filePath)
        {
            // update structures
            var bigendian = false;
            if (filePath.EndsWith(".sav"))
                bigendian = true;
            
            // endian switch
            if(bigendian != BigEndian)
            {
                var temp = new byte[SaveData.Length];
                for(int i =0; i < SaveData.Length; i += 4)
                {
                    temp[i] = SaveData[i + 3];
                    temp[i + 1] = SaveData[i + 2];
                    temp[i + 2] = SaveData[i + 1];
                    temp[i + 3] = SaveData[i];
                }
                SaveData = temp;
                BigEndian = bigendian;
            }

            for (int i = 0; i < FreeBattle.Length; i++)
                Array.Copy(FreeBattle[i].GetBytes(bigendian), 0, SaveData, i * 0x180, 0x180);

            int offset = 0x180 * 10;
            for (int i = 0; i < LittleCup.Length; i++)
                Array.Copy(LittleCup[i].GetBytes(bigendian), 0, SaveData, offset + i * 0x180, 0x180);

            offset += 0x180 * 10;
            for (int i = 0; i < PokeCup.Length; i++)
                Array.Copy(PokeCup[i].GetBytes(bigendian), 0, SaveData, offset + i * 0x180, 0x180);

            offset += 0x180 * 10;
            for (int i = 0; i < PrimeCup.Length; i++)
                Array.Copy(PrimeCup[i].GetBytes(bigendian), 0, SaveData, offset + i * 0x180, 0x180);


            offset = 0x4000;
            for (int i = 0; i < GymCastle.Length; i++)
                Array.Copy(GymCastle[i].GetBytes(bigendian), 0, SaveData, offset + i * 0x180, 0x180);

            offset += 0x180 * 10;
            for (int i = 0; i < GymCastle2.Length; i++)
                Array.Copy(GymCastle2[i].GetBytes(bigendian), 0, SaveData, offset + i * 0x180, 0x180);


            // this is like a backup copy?
            Array.Copy(SaveData, 0, SaveData, 0x10000, SaveData.Length - 0x10000);

            System.IO.File.WriteAllBytes(filePath, SaveData);
        }
    }
}

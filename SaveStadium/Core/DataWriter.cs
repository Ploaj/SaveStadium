using System;
using System.IO;

namespace SaveStadium.Core
{
    public class DataWriter : BinaryWriter
    {
        public bool BigEndian = false;

        public DataWriter() : base(new MemoryStream())
        {

        }

        public byte[] Reverse(byte[] b)
        {
            if (BitConverter.IsLittleEndian == BigEndian)
                Array.Reverse(b);
            return b;
        }

        public void WriteInt(int i)
        {
            Write(Reverse(BitConverter.GetBytes(i)));
        }

        public int CheckSum(int start, int length)
        {
            int sum = 0;
            var temp = BaseStream.Position;

            BaseStream.Position = start;
            for (int i = 0; i < length; i++)
                sum += BaseStream.ReadByte();
            BaseStream.Position = temp;

            return sum;
        }

        public byte[] GetData()
        {
            return (BaseStream as MemoryStream).ToArray();
        }
    }
}

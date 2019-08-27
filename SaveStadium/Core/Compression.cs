namespace SaveStadium.Core
{
    public class Compression
    {
        public static byte[] DecompressYAY0(byte[] b, int decomSize, int countOffset, int dataOffset)
        {
            int srcPos = 0;
            int desPos = 0;

            int codePos = 0;
            int countPos = 0;

            int validBitCount = 0;
            int code = 0;

            byte[] dst = new byte[decomSize];

            while (desPos < decomSize)
            {
                if (desPos >= 25174) break;
                if (validBitCount == 0)
                {
                    code = b[16 + codePos] & 0xFF;
                    codePos++;
                    validBitCount = 8;
                }

                if ((code & 0x80) == 0x80)
                {
                    // copy
                    dst[desPos] = b[dataOffset + srcPos];
                    desPos++;
                    srcPos++;
                }
                else
                {
                    //RLE
                    int b1 = b[countOffset + countPos] & 0xFF;
                    int b2 = b[countOffset + countPos + 1] & 0xFF;
                    countPos += 2;

                    int dist = ((b1 & 0xF) << 8) | b2;
                    int copySource = desPos - (dist + 1);

                    int numByte = b1 >> 4;
                    if (numByte == 0)
                    {
                        numByte = b[dataOffset + srcPos] + 0x12;
                        srcPos++;
                    }
                    else
                        numByte += 2;

                    for (int i = 0; i < numByte; ++i)
                    {
                        dst[desPos] = dst[copySource];
                        copySource++;
                        desPos++;
                    }
                }

                code <<= 1;
                validBitCount -= 1;
            }

            return dst;
        }
    }
}

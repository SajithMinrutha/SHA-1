using System;
public class SHA1
{
    public static void Main(string[] args)
    {
        string input = "Hello,World";
        byte[] bytedInput = SHAMath.ConvertToAsciiBytes(input); // Converts the input string into an array of ASCII bytes
        byte[] paddedBytes = SHAMath.AddPaddingToBytes(bytedInput);  // Adds padding to meet SHA-1's 512-bit block requirements
        string hash = SHAMath.ComputeHash(paddedBytes); // Processes the padded bytes and returns the SHA-1 hash
        Console.WriteLine(string.Join(",", bytedInput)); //testing
        Console.WriteLine(string.Join(",", paddedBytes)); //testing
        Console.WriteLine(hash);

    }

}
public static class SHAMath
{
    public static byte[] ConvertToAsciiBytes(string input)
    {
        try
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            else

            {
                int lengthOfInput = input.Length; //get the number of characters
                byte[] bytedArray = new byte[lengthOfInput]; //creating an array of bytes with the legth of the input

                for (int i = 0; i < lengthOfInput; i++)
                {
                    char ch = input[i];
                    bytedArray[i] = ch <= 127 ? (byte)ch : (byte)'?'; //converting each charcater to bytes if its ASCII value is less than 127;else converts ? to bytes
                }
                return bytedArray; //return the array of bytes
            }
        }

        catch (Exception e)
        {
            Console.WriteLine($"Unexpected Error : {e}");
            throw;
        }



    }
    public static byte[] AddPaddingToBytes(byte[] input)
    {
        ulong bitlength = (ulong)input.Length * 8; //converts bytes into bits
        List<byte> paddedList = new List<byte>(input); //create a new list to add paddings
        paddedList.Add(0x80); // add 1 bit padding

        while ((paddedList.Count % 64) != 56)
        {
            paddedList.Add(0x00);
        }
        byte[] lengthBytes = ConvertUlongToBytes(bitlength);
        paddedList.AddRange(lengthBytes);

        return paddedList.ToArray();
    }
    public static byte[] ConvertUlongToBytes(ulong input) //generates big endian bytes
    {

        byte[] bigEndianBytes = new byte[8];
        for (int i = 7; i >= 0; i--)
        {
            bigEndianBytes[i] = (byte)(input & 0xFF);
            input >>= 8;
        }


        return bigEndianBytes;
    }
    public static string ComputeHash(byte[] paddedBytes)
    {
        // Initial hash values
        uint h0 = 0x67452301;
        uint h1 = 0xEFCDAB89;
        uint h2 = 0x98BADCFE;
        uint h3 = 0x10325476;
        uint h4 = 0xC3D2E1F0;

        // Process each 512 bit chunk
        for (int chunkStart = 0; chunkStart < paddedBytes.Length; chunkStart += 64)
        {


        // Break chunk into sixteen 32 bit big endian words
            uint[] words = new uint[80];

            for (int i = 0; i < 16; i++)
            {
                words[i] = ((uint)paddedBytes[chunkStart + (i * 4)] << 24) |
                        ((uint)paddedBytes[chunkStart + (i * 4) + 1] << 16) |
                        ((uint)paddedBytes[chunkStart + (i * 4) + 2] << 8) |
                        ((uint)paddedBytes[chunkStart + (i * 4) + 3]);
            }

            // 2. Extend the 16 words to 80 words
            for (int i = 16; i < 80; i++)
            {
                words[i] = LeftRotate(words[i - 3] ^ words[i - 8] ^ words[i - 14] ^ words[i - 16], 1);
            }

            // 3. Initialize working variables
            uint a = h0;
            uint b = h1;
            uint c = h2;
            uint d = h3;
            uint e = h4;

            // 4. Main loop 80 rounds
            for (int i = 0; i < 80; i++)
            {
                uint f, k;
                if (i < 20)
                {
                    f = (b & c) | (~b & d);
                    k = 0x5A827999;
                }
                else if (i < 40)
                {
                    f = b ^ c ^ d;
                    k = 0x6ED9EBA1;
                }
                else if (i < 60)
                {
                    f = (b & c) | (b & d) | (c & d);
                    k = 0x8F1BBCDC;
                }
                else
                {
                    f = b ^ c ^ d;
                    k = 0xCA62C1D6;
                }

                uint temp = LeftRotate(a, 5) + f + e + k + words[i];
                e = d;
                d = c;
                c = LeftRotate(b, 30);
                b = a;
                a = temp;
            }

            // Add the compressed chunk to the current hash value
            h0 += a;
            h1 += b;
            h2 += c;
            h3 += d;
            h4 += e;
        }

        //  Produce the final hash value as a hexadecimal string
        return $"{h0:x8}{h1:x8}{h2:x8}{h3:x8}{h4:x8}";
    }
    public static uint LeftRotate(uint value, int bits)
    {
        return (value << bits) | (value >> (32 - bits));
    }


}
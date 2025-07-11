using System;
public class SHA1

{

    public static void Main(string[] args)
    {
        string input = "Hello,World!";
        byte[] bytedInput = SHAMath.ConvertToAsciiBytes(input);
        byte[] paddedBytes = SHAMath.AddPaddingToBytes(bytedInput);
        string hash = SHAMath.ComputeHash(paddedBytes);
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

        // Process each 512-bit chunk
        for (int chunkStart = 0; chunkStart < paddedBytes.Length; chunkStart += 64)
        {
        }
        return "";
    }

    }




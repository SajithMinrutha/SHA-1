using System;
using System.Text;
public class SHA1

{

    public static void Main(string[] args)
    {
        string input = "Hello,World!";
        byte[] bytedInput = ShaMath.ConvertToAsciiBytes(input);
        byte[] paddedBytes = ShaMath.AddPaddingToBytes(bytedInput);
        Console.WriteLine(string.Join(",", bytedInput)); //testing
        Console.WriteLine(string.Join(",", paddedBytes)); //testing
        string hashedOutput = ShaMath.ComputeHash(input);
    }



public static class ShaMath
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
            ulong bitlength = (ulong)input.Length * 8;

            return [65, 54];
    }
    public static string[] ConvertToBytes(string input)
    {

        return ["", ""];
    }
    public static string ComputeHash(string input)
    {
        return "hello";
    }

}

}
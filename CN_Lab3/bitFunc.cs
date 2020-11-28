using System;
using System.Collections;
using System.Linq;

namespace CN_Lab3
{
    public static class bitFunc
    {
        public static int BitArrayToInt(BitArray bitArray)
        {
            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];
        }

        public static BitArray IntToBitArray(int value)
        {
            return new BitArray(System.BitConverter.GetBytes(value));
        }
        
        public static BitArray Increment(BitArray bitArray)
        {
            for (int i = 0; i < CPU.REGISTER_BIT_SIZE; i++)
            {
                bool previous = bitArray[i];
                bitArray[i] = !previous;
                if (!previous)
                {
                    // Found a clear bit - now that we've set it, we're done
                    return bitArray;
                }
            }
            return bitArray;
        }

        public static string StringBitArray(BitArray bitArray)
        {
            string result = "";
            for (int i = CPU.REGISTER_BIT_SIZE - 1; i >= 0; i--)
            {
                byte dig = Convert.ToByte(bitArray[i]);
                result +=  dig + " ";
            }
            return result;
        }

        public static byte typeOfCommandBits(BitArray bitArray)
        {
            string strCommandBits = "";
            for (int i = CPU.REGISTER_BIT_SIZE - 1; i >= CPU.REGISTER_BIT_SIZE-CPU.AMOUNT_OF_COMMAND_BITS ; i--)
            {
                strCommandBits += Convert.ToByte(bitArray[i]);
            }
            return Convert.ToByte(strCommandBits, 2);
        }

        public static BitArray commandBitArrayToNumBitArray(BitArray bitArray, int sign)
        {
            BitArray resultingBitArray = new BitArray(CPU.REGISTER_BIT_SIZE, false);
            
            int regBitSize = CPU.REGISTER_BIT_SIZE;
            int amCommBits = CPU.AMOUNT_OF_COMMAND_BITS;
            
            for (int j = regBitSize - amCommBits - 1; j >= 0; j--)
            {
                resultingBitArray[j] = bitArray[j];
            }
            
            if (sign == 1)
            {
                for (int j = regBitSize - 1; j >= regBitSize - amCommBits; j--)
                {
                    resultingBitArray[j] = true;
                }
            }


            return resultingBitArray;
        }

        public static BitArray checkSignOfOperation(BitArray bitArray)
        {

            int regBitSize = CPU.REGISTER_BIT_SIZE;
            int amCommBits = CPU.AMOUNT_OF_COMMAND_BITS;
            if (bitArray[regBitSize - amCommBits - 1] == false)
            {
                return new BitArray(CPU.REGISTER_BIT_SIZE, false);
            }
            
            BitArray resultArray = new BitArray(CPU.REGISTER_BIT_SIZE, false);
            resultArray[0] = true;
            return resultArray;
        }

        public static BitArray bitArrayLeftShift(BitArray bitArray)
        {
            int regBitSize = CPU.REGISTER_BIT_SIZE;
            
            BitArray tempBitArray = new BitArray(regBitSize, false);
            for (int i = regBitSize - 1; i > 0; i--)
            {
                tempBitArray[i] = bitArray[i - 1];
            }
            return tempBitArray;
        }

        public static BitArray bitArrayRightShift(BitArray bitArray)
        {
            int regBitSize = CPU.REGISTER_BIT_SIZE;

            BitArray tempBitArray = new BitArray(regBitSize, false);
            for (int i = regBitSize - 1; i > 0; i--)
            {
                tempBitArray[i - 1] = bitArray[i];
            }

            if (bitArray[regBitSize - 1] == true)
            {
                tempBitArray[regBitSize - 1] = true;
            }

            return tempBitArray;
        }

        public static BitArray checkSignOfOperationForShift(BitArray bitArray)
        {
            int regBitSize = CPU.REGISTER_BIT_SIZE;
            if (bitArray[regBitSize - 1] == false)
            {
                return new BitArray(CPU.REGISTER_BIT_SIZE, false);
            }
            
            BitArray resultArray = new BitArray(CPU.REGISTER_BIT_SIZE, false);
            resultArray[0] = true;
            return resultArray;
        }
    }
}
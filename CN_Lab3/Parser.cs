using System;
using System.Collections;

namespace CN_Lab3
{
    public class Parser
    {
        public static BitArray CommandToBitArray(string command)
        {
            BitArray resultingBitArray = new BitArray(CPU.REGISTER_BIT_SIZE,false);
            BitArray commandBits = new BitArray(CPU.REGISTER_BIT_SIZE, false);
            BitArray numBits = new BitArray(CPU.REGISTER_BIT_SIZE, false);

            string strCommand = "";
            string strNum = "";
            
            int i = 0;
            while (command[i] != ' ')
            {
                strCommand += command[i];
                i++;
            }
            for (int j = i; j < command.Length; j++)
            {
                strNum += command[j];
            }

            switch (strCommand)
            {
                case "MOV":
                {
                    commandBits[CPU.REGISTER_BIT_SIZE - 1] = false;     // Исправить циклом для того, чтобы можно было автоматически менять размер регистров
                    commandBits[CPU.REGISTER_BIT_SIZE - 2] = true;
                    break;
                }
                case "LSFT":
                {
                    commandBits[CPU.REGISTER_BIT_SIZE - 1] = true;
                    commandBits[CPU.REGISTER_BIT_SIZE - 2] = false;
                    break;
                }
                case "RSFT":
                {
                    commandBits[CPU.REGISTER_BIT_SIZE - 1] = true;
                    commandBits[CPU.REGISTER_BIT_SIZE - 2] = true;
                    break;
                }
            }
            numBits = bitFunc.IntToBitArray(Convert.ToInt32(strNum));

            int regBitSize = CPU.REGISTER_BIT_SIZE;
            int amCommBits = CPU.AMOUNT_OF_COMMAND_BITS;
            for (int j = regBitSize - 1; j >= regBitSize - amCommBits; j--)
            {
                resultingBitArray[j] = commandBits[j];
            }
            for (int j = regBitSize - amCommBits - 1; j >= 0; j--)
            {
                resultingBitArray[j] = numBits[j];
            }

            return resultingBitArray;
        }
    }
}
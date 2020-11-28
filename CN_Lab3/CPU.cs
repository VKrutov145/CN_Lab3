using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;

namespace CN_Lab3
{
    enum Register
    {
        IR,
        AL,
        PS,
        PC,
        TC
    }

    enum Commands
    {
        UNKNOWN_COMMAND, // "00" - UNKNOWN COMMAND
        MOV, // "01" - MOV
        LSFT, // "10" - LSFT
        RSFT // "11" - RSFT
    }
    public class CPU
    {
        public void NextCycle()
        {
            bitFunc.Increment(Registers[(int)Register.TC]);
        }
        private void WriteCpuLog(string strCommand)
        {    
            Console.WriteLine("Command = " + strCommand); //Writing Command
            Console.WriteLine("IR: " + bitFunc.StringBitArray(Registers[(int)Register.IR])); //Writing IR in bits
            Console.WriteLine("AL: " + bitFunc.StringBitArray(Registers[(int)Register.AL]));    //Writing AL in bits
            Console.WriteLine("PS: " + bitFunc.BitArrayToInt(Registers[(int)Register.PS]));   //Writing PS in int
            Console.WriteLine("PC: " + bitFunc.BitArrayToInt(Registers[(int)Register.PC]));    //Writing PC in int
            Console.WriteLine("TC: " + bitFunc.BitArrayToInt(Registers[(int)Register.TC]));    //Writing TC in int
        }
        private string readCommandFromFile()
        {
            int neededCommand = bitFunc.BitArrayToInt(Registers[(int) Register.PC]);
            
            string path = @"C:\Users\PRO\Desktop\CommandsToCPU.txt";
            if (!File.Exists(path))
            {
                throw new SystemException("THERE_IS_NO_COMMAND_FILE");
            }

            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                int i = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    if (i == neededCommand)
                    {
                        return s;
                    } 
                    i++;
                }
                return null;
            }
        }

        public const int REGISTER_BIT_SIZE = 12;
        public const int AMOUNT_OF_COMMAND_BITS = 2;
        private const int AMOUNT_OF_REGISTERS = 5;
        List<BitArray> Registers = new List<BitArray>() { };
        private string commandToCPU;

        public CPU()
        {
            for (int i = 0; i < AMOUNT_OF_REGISTERS; i++)
            {
                BitArray registerData = new BitArray(REGISTER_BIT_SIZE, false);
                Registers.Add(registerData);
            }
        }
        
        public void readCommand()
        {
            commandToCPU = readCommandFromFile();
            Registers[(int)Register.PC] = bitFunc.Increment(Registers[(int)Register.PC]); //Incrementing PC because of reading next command
            Registers[(int)Register.TC] = bitFunc.Increment(new BitArray(CPU.REGISTER_BIT_SIZE, false));
            Registers[(int)Register.IR] = Parser.CommandToBitArray(commandToCPU); //Writing command in bit array to IR 
            WriteCpuLog(commandToCPU);
            Console.WriteLine();
            
            // Console.WriteLine(bitFunc.typeOfCommandBits(Registers[(int)Register.PC]));
            // Console.WriteLine((int)Commands.LSFT);
        }

        public void proceedCommand()
        {
            ExecuteInstruction(Registers[(int) Register.IR]);
            WriteCpuLog(commandToCPU);
            Console.WriteLine("___________________________");
        }

        private void ExecuteInstruction(BitArray commandArray)
        {
            byte commandType = bitFunc.typeOfCommandBits(commandArray);
            switch (commandType)
            {
                case (int) Commands.MOV:
                {
                    Registers[(int) Register.PS] = bitFunc.checkSignOfOperation(commandArray);
                    Registers[(int) Register.AL] = bitFunc.commandBitArrayToNumBitArray(commandArray, bitFunc.BitArrayToInt(Registers[(int)Register.PS]));
                    break;
                }
                case (int) Commands.LSFT:
                {
                    Registers[(int) Register.AL] = bitFunc.bitArrayLeftShift(Registers[(int) Register.AL]);
                    Registers[(int) Register.PS] = bitFunc.checkSignOfOperationForShift(Registers[(int) Register.AL]);
                    break;
                }
                case (int) Commands.RSFT:
                {
                    Registers[(int) Register.AL] = bitFunc.bitArrayRightShift(Registers[(int) Register.AL]);
                    Registers[(int) Register.PS] = bitFunc.checkSignOfOperationForShift(Registers[(int) Register.AL]);
                    break;
                }
            }
            // Console.WriteLine(commandType);
            // Console.WriteLine((int) Commands.MOV);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VINLib
{
    class VIN
    { 
        public static bool CheckVIN(string vin)
        {

            if (vin.Length != 17) return false;
            else
            {
                int result = 0;
                double checkSum = 0;
                double checkNum = 0;
                int i = 0;
                int w = 0;
                vin = vin.ToLower();
                foreach( char c in vin.ToCharArray())
                {
                    i++;
                    if (char.IsNumber(c))
                    {
                        result = int.Parse(c.ToString());
                    }
                    else
                    {
                        switch (c)
                        {
                            case 'a':
                            case 'j': result = 1; break;
                            case 'b':
                            case 'k':
                            case 's':
                            result = 2; break;
                            case 'c':
                            case 'l':
                            case 't':
                            result = 3; break;
                            case 'd':
                            case 'm':
                            case 'u':
                            result = 4; break;
                            case 'e':
                            case 'n':
                            case 'v':
                            result = 5; break;
                            case 'f':
                            case 'w':
                            result = 6; break;
                            case 'g':
                            case 'p':
                            case 'x':
                            result = 7; break;
                            case 'h':
                            case 'y':
                            result = 8; break;
                            case 'r':
                            case 'z':
                            result = 9; break;
                        }
                    }
                    if (i <= 7 || i == 9) w = 9 - i;
                    else if (i == 8) w = 10;
                    else if (i >= 10 && i <= 17) w = 19 - i;
                    checkSum += w * i;

                }
                return checkSum - checkSum % 11 * 11 == checkNum;
            }

        }
    }
}

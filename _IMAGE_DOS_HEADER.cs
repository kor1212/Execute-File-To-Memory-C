using System;
using System.Runtime.InteropServices;

namespace dadys
{
	[StructLayout(LayoutKind.Explicit)]
	public struct _IMAGE_DOS_HEADER
	{
		[FieldOffset(0)]
		public ushort e_magic;

		[FieldOffset(60)]
		public uint e_lfanew;
	}
}
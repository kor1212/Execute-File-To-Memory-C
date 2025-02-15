using System;
using System.Runtime.InteropServices;

namespace dadya
{
	[StructLayout(LayoutKind.Explicit)]
	public struct _IMAGE_OPTIONAL_HEADER
	{
		[FieldOffset(0)]
		public ushort Magic;

		[FieldOffset(16)]
		public uint AddressOfEntryPoint;

		[FieldOffset(28)]
		public uint ImageBase;

		[FieldOffset(56)]
		public uint SizeOfImage;

		[FieldOffset(60)]
		public uint SizeOfHeaders;
	}
}
using dadya;
using dadyr;
using System;
using System.Runtime.InteropServices;

namespace dadyc
{
	[StructLayout(LayoutKind.Explicit)]
	public struct _IMAGE_NT_HEADERS
	{
		[FieldOffset(0)]
		public uint Signature;

		[FieldOffset(4)]
		public _IMAGE_FILE_HEADER FileHeader;

		[FieldOffset(24)]
		public _IMAGE_OPTIONAL_HEADER OptionalHeader;
	}
}
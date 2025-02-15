using System;
using System.Runtime.InteropServices;

namespace mommya
{
	[StructLayout(LayoutKind.Explicit)]
	public struct _IMAGE_SECTION_HEADER
	{
		[FieldOffset(12)]
		public uint VirtualAddress;

		[FieldOffset(16)]
		public uint SizeOfRawData;

		[FieldOffset(20)]
		public uint PointerToRawData;
	}
}
using System;
using System.Runtime.InteropServices;

namespace refresh
{
	[StructLayout(LayoutKind.Explicit)]
	public struct _CONTEXT
	{
		[FieldOffset(0)]
		public uint ContextFlags;

		[FieldOffset(164)]
		public uint Ebx;

		[FieldOffset(176)]
		public uint Eax;
	}
}
using System;
using System.Runtime.InteropServices;

namespace refreshamd
{
	[StructLayout(LayoutKind.Explicit)]
	public struct _CONTEXT_AMD64
	{
		[FieldOffset(48)]
		public uint ContextFlags;

		[FieldOffset(128)]
		public ulong Rcx;

		[FieldOffset(136)]
		public ulong Rdx;
	}
}
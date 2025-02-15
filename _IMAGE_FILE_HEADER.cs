using System;
using System.Runtime.InteropServices;

namespace dadyr
{
	[StructLayout(LayoutKind.Explicit)]
	public struct _IMAGE_FILE_HEADER
	{
		[FieldOffset(2)]
		public ushort NumberOfSections;
	}
}
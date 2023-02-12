using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace IsStrongOrSigned
{

    /// <summary>
    /// Taken from here and then modified
    /// https://gist.github.com/reigningshells/d4d9d97a59a2aea8890e75c203230e92
    /// </summary>
    internal class DLLExportLister
    {
        // Can't use sizeof for IMAGE_SECTION_HEADER because of unmanaged type
        public const int SizeOfImageSectionHeader = 40;

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct IMAGE_DOS_HEADER
        {
            public UInt16 e_magic;              // Magic number
            public UInt16 e_cblp;               // Bytes on last page of file
            public UInt16 e_cp;                 // Pages in file
            public UInt16 e_crlc;               // Relocations
            public UInt16 e_cparhdr;            // Size of header in paragraphs
            public UInt16 e_minalloc;           // Minimum extra paragraphs needed
            public UInt16 e_maxalloc;           // Maximum extra paragraphs needed
            public UInt16 e_ss;                 // Initial (relative) SS value
            public UInt16 e_sp;                 // Initial SP value
            public UInt16 e_csum;               // Checksum
            public UInt16 e_ip;                 // Initial IP value
            public UInt16 e_cs;                 // Initial (relative) CS value
            public UInt16 e_lfarlc;             // File address of relocation table
            public UInt16 e_ovno;               // Overlay number
            public UInt16 e_res_0;              // Reserved words
            public UInt16 e_res_1;              // Reserved words
            public UInt16 e_res_2;              // Reserved words
            public UInt16 e_res_3;              // Reserved words
            public UInt16 e_oemid;              // OEM identifier (for e_oeminfo)
            public UInt16 e_oeminfo;            // OEM information; e_oemid specific
            public UInt16 e_res2_0;             // Reserved words
            public UInt16 e_res2_1;             // Reserved words
            public UInt16 e_res2_2;             // Reserved words
            public UInt16 e_res2_3;             // Reserved words
            public UInt16 e_res2_4;             // Reserved words
            public UInt16 e_res2_5;             // Reserved words
            public UInt16 e_res2_6;             // Reserved words
            public UInt16 e_res2_7;             // Reserved words
            public UInt16 e_res2_8;             // Reserved words
            public UInt16 e_res2_9;             // Reserved words
            public Int32 e_lfanew;             // File address of new exe header
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct IMAGE_DATA_DIRECTORY
        {
            public UInt32 VirtualAddress;
            public UInt32 Size;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct IMAGE_FILE_HEADER
        {
            public UInt16 Machine;
            public UInt16 NumberOfSections;
            public UInt32 TimeDateStamp;
            public UInt32 PointerToSymbolTable;
            public UInt32 NumberOfSymbols;
            public UInt16 SizeOfOptionalHeader;
            public UInt16 Characteristics;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGE_OPTIONAL_HEADER64
        {
            public UInt16 Magic;
            public byte MajorLinkerVersion;
            public byte MinorLinkerVersion;
            public uint SizeOfCode;
            public uint SizeOfInitializedData;
            public uint SizeOfUninitializedData;
            public uint AddressOfEntryPoint;
            public uint BaseOfCode;
            public ulong ImageBaseLong;
            public uint SectionAlignment;
            public uint FileAlignment;
            public ushort MajorOperatingSystemVersion;
            public ushort MinorOperatingSystemVersion;
            public ushort MajorImageVersion;
            public ushort MinorImageVersion;
            public ushort MajorSubsystemVersion;
            public ushort MinorSubsystemVersion;
            public uint Win32VersionValue;
            public uint SizeOfImage;
            public uint SizeOfHeaders;
            public uint CheckSum;
            public UInt16 Subsystem;
            public UInt16 DllCharacteristics;
            public ulong SizeOfStackReserve;
            public ulong SizeOfStackCommit;
            public ulong SizeOfHeapReserve;
            public ulong SizeOfHeapCommit;
            public uint LoaderFlags;
            public uint NumberOfRvaAndSizes;
            public IMAGE_DATA_DIRECTORY ExportTable;
            public IMAGE_DATA_DIRECTORY ImportTable;
            public IMAGE_DATA_DIRECTORY ResourceTable;
            public IMAGE_DATA_DIRECTORY ExceptionTable;
            public IMAGE_DATA_DIRECTORY CertificateTable;
            public IMAGE_DATA_DIRECTORY BaseRelocationTable;
            public IMAGE_DATA_DIRECTORY Debug;
            public IMAGE_DATA_DIRECTORY Architecture;
            public IMAGE_DATA_DIRECTORY GlobalPtr;
            public IMAGE_DATA_DIRECTORY TLSTable;
            public IMAGE_DATA_DIRECTORY LoadConfigTable;
            public IMAGE_DATA_DIRECTORY BoundImport;
            public IMAGE_DATA_DIRECTORY IAT;
            public IMAGE_DATA_DIRECTORY DelayImportDescriptor;
            public IMAGE_DATA_DIRECTORY CLRRuntimeHeader;
            public IMAGE_DATA_DIRECTORY Reserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct IMAGE_OPTIONAL_HEADER32
        {
            public UInt16 Magic;
            public Byte MajorLinkerVersion;
            public Byte MinorLinkerVersion;
            public UInt32 SizeOfCode;
            public UInt32 SizeOfInitializedData;
            public UInt32 SizeOfUninitializedData;
            public UInt32 AddressOfEntryPoint;
            public UInt32 BaseOfCode;
            public UInt32 BaseOfData;
            public UInt32 ImageBase;
            public UInt32 SectionAlignment;
            public UInt32 FileAlignment;
            public UInt16 MajorOperatingSystemVersion;
            public UInt16 MinorOperatingSystemVersion;
            public UInt16 MajorImageVersion;
            public UInt16 MinorImageVersion;
            public UInt16 MajorSubsystemVersion;
            public UInt16 MinorSubsystemVersion;
            public UInt32 Win32VersionValue;
            public UInt32 SizeOfImage;
            public UInt32 SizeOfHeaders;
            public UInt32 CheckSum;
            public UInt16 Subsystem;
            public UInt16 DllCharacteristics;
            public UInt32 SizeOfStackReserve;
            public UInt32 SizeOfStackCommit;
            public UInt32 SizeOfHeapReserve;
            public UInt32 SizeOfHeapCommit;
            public UInt32 LoaderFlags;
            public UInt32 NumberOfRvaAndSizes;

            public IMAGE_DATA_DIRECTORY ExportTable;
            public IMAGE_DATA_DIRECTORY ImportTable;
            public IMAGE_DATA_DIRECTORY ResourceTable;
            public IMAGE_DATA_DIRECTORY ExceptionTable;
            public IMAGE_DATA_DIRECTORY CertificateTable;
            public IMAGE_DATA_DIRECTORY BaseRelocationTable;
            public IMAGE_DATA_DIRECTORY Debug;
            public IMAGE_DATA_DIRECTORY Architecture;
            public IMAGE_DATA_DIRECTORY GlobalPtr;
            public IMAGE_DATA_DIRECTORY TLSTable;
            public IMAGE_DATA_DIRECTORY LoadConfigTable;
            public IMAGE_DATA_DIRECTORY BoundImport;
            public IMAGE_DATA_DIRECTORY IAT;
            public IMAGE_DATA_DIRECTORY DelayImportDescriptor;
            public IMAGE_DATA_DIRECTORY CLRRuntimeHeader;
            public IMAGE_DATA_DIRECTORY Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGE_NT_HEADERS32
        {
            public UInt32 Signature;
            public IMAGE_FILE_HEADER FileHeader;
            public IMAGE_OPTIONAL_HEADER32 OptionalHeader;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGE_NT_HEADERS64
        {
            public UInt32 Signature;
            public IMAGE_FILE_HEADER FileHeader;
            public IMAGE_OPTIONAL_HEADER64 OptionalHeader;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGE_EXPORT_DIRECTORY
        {
            public UInt32 Characteristics;
            public UInt32 TimeDateStamp;
            public UInt16 MajorVersion;
            public UInt16 MinorVersion;
            public UInt32 Name;
            public UInt32 Base;
            public UInt32 NumberOfFunctions;
            public UInt32 NumberOfNames;
            public UInt32 AddressOfFunctions; // RVA from base of image
            public UInt32 AddressOfNames; // RVA from base of image
            public UInt32 AddressOfNameOrdinals; // RVA from base of image
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct IMAGE_SECTION_HEADER
        {
            [FieldOffset(0)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Name;
            [FieldOffset(8)]
            public UInt32 VirtualSize;
            [FieldOffset(12)]
            public UInt32 VirtualAddress;
            [FieldOffset(16)]
            public UInt32 SizeOfRawData;
            [FieldOffset(20)]
            public UInt32 PointerToRawData;
            [FieldOffset(24)]
            public UInt32 PointerToRelocations;
            [FieldOffset(28)]
            public UInt32 PointerToLinenumbers;
            [FieldOffset(32)]
            public UInt16 NumberOfRelocations;
            [FieldOffset(34)]
            public UInt16 NumberOfLinenumbers;
            [FieldOffset(36)]
            uint Characteristics;

            public string Section
            {
                get { return new string(Name); }
            }
        }

        public static T CastBytesToType<T>(byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return theStructure;
        }

        public static (string crtVersion, string compileType) GetCrtVersion(string fullName)
        {
            try
            {
                return ReadFromFile(fullName);
            }
            catch (Exception e)
            {
                // TODO: Log this out
                Console.WriteLine(e);
            }

            return ("CRT?", "unknown");
        }
        
        static unsafe (string crtVersion, string compileType) ReadFromFile(string fullName)
        {
            var ext = Path.GetExtension(fullName).ToLowerInvariant();
            switch (ext)
            {
                case ".exe":
                case ".dll":
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fullName), @"Unsupported file type", ext);
            }

            // Read bytes of DLL
            byte[] sourceFileByteBuffer = System.IO.File.ReadAllBytes(fullName);

            // Get IMAGE_DOS_HEADER because we need e_lfanew for offset to IMAGE_NT_HEADER
            IMAGE_DOS_HEADER imageDosHeader = CastBytesToType<IMAGE_DOS_HEADER>(sourceFileByteBuffer.Skip(0).Take(sizeof(IMAGE_DOS_HEADER)).ToArray());

            // Get IMAGE_FILE_HEADER to check if the DLL is 32 or 64 bit
            IMAGE_FILE_HEADER imageFileHeader = CastBytesToType<IMAGE_FILE_HEADER>(sourceFileByteBuffer.Skip(imageDosHeader.e_lfanew + 4).Take(sizeof(IMAGE_FILE_HEADER)).ToArray());

            int sectionOffset = 0;
            int exportOffset;
            string compileType;
            if (imageFileHeader.Machine == 0x8664) // 64bit 0x8664
            {
                compileType = @"x64";
                // Get IMAGE_NT_HEADER because we need it for address of export table
                IMAGE_NT_HEADERS64 imageNtHeader = CastBytesToType<IMAGE_NT_HEADERS64>(sourceFileByteBuffer.Skip(imageDosHeader.e_lfanew).Take(sizeof(IMAGE_NT_HEADERS64)).ToArray());

                // Loop through sections to find where export table resides for section offset (VA - PTRToRawData) use to calculate appropriate file offset

                int sectionHeadersOffset = imageDosHeader.e_lfanew + sizeof(IMAGE_NT_HEADERS64);

                for (int i = 0; i < imageNtHeader.FileHeader.NumberOfSections; i++)
                {
                    // Parse IMAGE_SECTION_HEADER
                    IMAGE_SECTION_HEADER imageSectionHeader = CastBytesToType<IMAGE_SECTION_HEADER>(sourceFileByteBuffer.Skip(sectionHeadersOffset).Take(SizeOfImageSectionHeader).ToArray());
                    if (imageNtHeader.OptionalHeader.ExportTable.VirtualAddress > imageSectionHeader.VirtualAddress && imageNtHeader.OptionalHeader.ExportTable.VirtualAddress < (imageSectionHeader.VirtualAddress + imageSectionHeader.VirtualSize))
                    {
                        sectionOffset = (int)((imageSectionHeader.VirtualAddress - imageSectionHeader.PointerToRawData));
                        break;
                    }
                    sectionHeadersOffset += SizeOfImageSectionHeader;
                }

                // Now that we have offset to turn RVA into a file offset, can get IMAGE_EXPORT_DIRECTORY and start looping through arrays to get exports

                exportOffset = (int)(imageNtHeader.OptionalHeader.ExportTable.VirtualAddress - sectionOffset);
            }
            else // 32bit
            {
                compileType = @"x86";

                IMAGE_NT_HEADERS32 imageNtHeader = CastBytesToType<IMAGE_NT_HEADERS32>(sourceFileByteBuffer.Skip(imageDosHeader.e_lfanew).Take(sizeof(IMAGE_NT_HEADERS32)).ToArray());

                int sectionHeadersOffset = imageDosHeader.e_lfanew + sizeof(IMAGE_NT_HEADERS32);

                for (int i = 0; i < imageNtHeader.FileHeader.NumberOfSections; i++)
                {
                    // Parse IMAGE_SECTION_HEADER
                    IMAGE_SECTION_HEADER imageSectionHeader = CastBytesToType<IMAGE_SECTION_HEADER>(sourceFileByteBuffer.Skip(sectionHeadersOffset).Take(SizeOfImageSectionHeader).ToArray());
                    if (imageNtHeader.OptionalHeader.ExportTable.VirtualAddress > imageSectionHeader.VirtualAddress && imageNtHeader.OptionalHeader.ExportTable.VirtualAddress < (imageSectionHeader.VirtualAddress + imageSectionHeader.VirtualSize))
                    {
                        sectionOffset = (int)((imageSectionHeader.VirtualAddress - imageSectionHeader.PointerToRawData));
                        break;
                    }
                    sectionHeadersOffset += SizeOfImageSectionHeader;
                }

                // Now that we have offset to turn RVA into a file offset, can get IMAGE_EXPORT_DIRECTORY and start looping through arrays to get exports

                exportOffset = (int)(imageNtHeader.OptionalHeader.ExportTable.VirtualAddress - sectionOffset);
            }

            IMAGE_EXPORT_DIRECTORY exportTable = CastBytesToType<IMAGE_EXPORT_DIRECTORY>(sourceFileByteBuffer.Skip(exportOffset).Take(sizeof(IMAGE_EXPORT_DIRECTORY)).ToArray());

            return ($@"crt{exportTable.MajorVersion}.{exportTable.MinorVersion}", compileType);
        }
    }
}

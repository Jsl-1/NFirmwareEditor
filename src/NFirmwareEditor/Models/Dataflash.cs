﻿using System;
using System.IO;
using NFirmwareEditor.Managers;

// ReSharper disable InconsistentNaming
namespace NFirmwareEditor.Models
{
	internal class Dataflash
	{
		public DataflashParams Params { get; set; }

		[BinaryOffset(Absolute = 0x100)]
		public DataflashInfo Info { get; set; }
	}

	internal class DataflashParams
	{
		public uint PageChecksum { get; set; }
		public uint HardwareVersion { get; set; }
		public byte Magic { get; set; }
		public BootMode BootMode { get; set; }
		public Mode Mode { get; set; }

		[BinaryOffset(Relative = 1)]
		public ushort Power { get; set; }

		public ushort Temperature { get; set; }
		public ushort TCPower { get; set; }
		public ushort VWVolts { get; set; }
		public byte APT { get; set; }
		public byte ResistanceType { get; set; }
		public byte TemperatureAlgo { get; set; }
		public bool IsCelsius { get; set; }
		public ushort Resistance { get; set; }
		public ushort ResistanceTi { get; set; }
		public ushort ResistanceNi { get; set; }
		public bool ResistanceTiLocked { get; set; }
		public bool ResistanceNiLocked { get; set; }
		public bool TiOn { get; set; }
		public bool StealthOn { get; set; }

		[BinaryArray(Length = 21)]
		public ushort[] TempCoefsNi { get; set; }

		[BinaryArray(Length = 21)]
		public ushort[] TempCoefsTi { get; set; }

		/// <summary>4 bytes structure.</summary>
		[BinaryOffset(Relative = 2)]
		public DataflashStatus Status { get; set; }

		public ushort AtomizerResistance { get; set; }

		/// <summary>0,1,2,3,4 = Open,Short,Low,Large,Ok</summary>
		public byte AtomizerStatus { get; set; }

		[BinaryOffset(Relative = 1)]
		public ushort ResistanceSS { get; set; }

		public bool ResistanceSSLocked { get; set; }
		public byte UIVersion { get; set; }
		public byte TCRIndex { get; set; }

		[BinaryOffset(Relative = 1), BinaryArray(Length = 3)]
		public ushort[] TCR { get; set; }

		public ushort ResistanceTCR { get; set; }
		public bool ResistanceTCRLocked { get; set; }
		public bool ScreenSaver { get; set; }
		//[BinaryOffset(Relative = 1)]
		public byte LastTCMode { get; set; }
		public byte ScreenProtection { get; set; }

		[ /*BinaryOffset(Relative = 1), */BinaryArray(Length = 10)]
		public ushort[] SavedCfgRez { get; set; }

		[BinaryArray(Length = 10)]
		public ushort[] SavedCfgPwr { get; set; }

		public ushort FBBest { get; set; }
		public byte FBSpeed { get; set; }
		public byte byte_2000033D { get; set; }

		/// <summary>pc = ( ( 100 * dfContrast ) / 255 );</summary>
		public byte Contrast { get; set; }

		public Modes DisabledModes { get; set; }
		public ushort ClkRatio { get; set; }
		public ushort PreheatPwr { get; set; }
		public byte PreheatTime { get; set; }

		[BinaryArray(Length = 3)]
		public byte[] MClicks { get; set; }
	}

	internal class DataflashInfo
	{
		public uint FWVersion { get; set; }
		public uint LDVersion { get; set; }
		public uint fmcCID { get; set; }
		public uint fmcDID { get; set; }
		public uint fmcPID { get; set; }

		[BinaryArray(Length = 3)]
		public uint[] fmcUID { get; set; }

		[BinaryArray(Length = 4)]
		public uint[] fmcUCID { get; set; }

		public uint PuffCount { get; set; }
		public uint TimeCount { get; set; }

		[AsciiString(Length = 4)]
		public string ProductID { get; set; }

		public uint MaxHWVersion { get; set; }
		public ushort Year { get; set; }
		public byte Month { get; set; }
		public byte Day { get; set; }
		public byte Hour { get; set; }
		public byte Minute { get; set; }
		public byte Second { get; set; }
	}

	internal class DataflashStatus : IBinaryReaderWriter
	{
		// 1st byte
		public bool Off { get; set; }
		public bool Keylock { get; set; }
		public bool Flipped { get; set; }
		public bool NoLogo { get; set; }
		public bool AnalogClock { get; set; }
		public bool VirtualCom { get; set; }
		public bool Storage { get; set; }
		public bool DebugEnable { get; set; }

		// 2nd byte
		public bool X32Off { get; set; }
		public bool TemperatureDominant { get; set; }
		public bool Step1W { get; set; }
		public bool DigitalClock { get; set; }
		public bool BatteryPercent { get; set; }
		public bool PreheatPercent { get; set; }

		#region Implementation of IBinaryReaderWriter
		public void Read(BinaryReader r)
		{
			var data = r.ReadBytes(4);
			var b1 = data[0];
			{
				Off = DataflashManager.GetBit(b1, 1);
				Keylock = DataflashManager.GetBit(b1, 2);
				Flipped = DataflashManager.GetBit(b1, 3);
				NoLogo = DataflashManager.GetBit(b1, 4);
				AnalogClock = DataflashManager.GetBit(b1, 5);
				VirtualCom = DataflashManager.GetBit(b1, 6);
				Storage = DataflashManager.GetBit(b1, 7);
				DebugEnable = DataflashManager.GetBit(b1, 8);
			}

			var b2 = data[1];
			{
				X32Off = DataflashManager.GetBit(b2, 1);
				TemperatureDominant = DataflashManager.GetBit(b2, 2);
				Step1W = DataflashManager.GetBit(b2, 3);
				DigitalClock = DataflashManager.GetBit(b2, 4);
				BatteryPercent = DataflashManager.GetBit(b2, 5);
				PreheatPercent = DataflashManager.GetBit(b2, 6);
			}
		}

		public void Write(BinaryWriter r)
		{
			byte b1 = 0;
			b1 = DataflashManager.SetBit(b1, 1, Off);
			b1 = DataflashManager.SetBit(b1, 2, Keylock);
			b1 = DataflashManager.SetBit(b1, 3, Flipped);
			b1 = DataflashManager.SetBit(b1, 4, NoLogo);
			b1 = DataflashManager.SetBit(b1, 5, AnalogClock);
			b1 = DataflashManager.SetBit(b1, 6, VirtualCom);
			b1 = DataflashManager.SetBit(b1, 7, Storage);
			b1 = DataflashManager.SetBit(b1, 8, DebugEnable);

			byte b2 = 0;
			b2 = DataflashManager.SetBit(b2, 1, X32Off);
			b2 = DataflashManager.SetBit(b2, 2, TemperatureDominant);
			b2 = DataflashManager.SetBit(b2, 3, Step1W);
			b2 = DataflashManager.SetBit(b2, 4, DigitalClock);
			b2 = DataflashManager.SetBit(b2, 5, BatteryPercent);
			b2 = DataflashManager.SetBit(b2, 6, PreheatPercent);

			r.Write(new byte[] { b1, b2, 0, 0 });
		}
		#endregion
	}

	internal enum BootMode : byte
	{
		APROM = 0,
		LDROM = 1
	}

	internal enum Mode : byte
	{
		TempNi = 0,
		TempTi = 1,
		TempSS = 2,
		TCR = 3,
		Power = 4,
		Bypass = 5,
		Start = 6
	}

	[Flags]
	internal enum Modes : byte
	{
		TempNi = 1 << 0,
		TempTi = 1 << 1,
		TempSS = 1 << 2,
		TCR = 1 << 3,
		Power = 1 << 4,
		Bypass = 1 << 5,
		Start = 1 << 6
	}
}
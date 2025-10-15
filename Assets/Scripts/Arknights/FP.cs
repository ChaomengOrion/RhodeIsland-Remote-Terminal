// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using UnityEngine;

namespace RhodeIsland.Arknights
{
	[Serializable]
	public struct FP : IEquatable<FP>, IComparable<FP>
	{
		private FP(long rawValue)
		{
			_serializedValue = rawValue;
		}

		public FP(int value)
		{
			_serializedValue = value;
		}

		public static int Sign(FP value)
		{
			if (value._serializedValue < 0)
				return -1;
			else
				return (value._serializedValue != 0) ? 1 : 0 + ((value._serializedValue > 0) ? 1 : 0);
		}

		public static FP Abs(FP value)
		{
			return default(FP);
		}

		public static FP FastAbs(FP value)
		{
			return default(FP);
		}

		public static FP Floor(FP value)
		{
			return default(FP);
		}

		public static FP Ceiling(FP value)
		{
			return default(FP);
		}

		public static FP Round(FP value)
		{
			return default(FP);
		}

		public static FP operator +(FP x, FP y)
		{
			return default(FP);
		}

		public static FP OverflowAdd(FP x, FP y)
		{
			return default(FP);
		}

		public static FP FastAdd(FP x, FP y)
		{
			return default(FP);
		}

		public static FP operator -(FP x, FP y)
		{
			return default(FP);
		}

		public static FP OverflowSub(FP x, FP y)
		{
			return default(FP);
		}

		public static FP FastSub(FP x, FP y)
		{
			return default(FP);
		}

		private static long AddOverflowHelper(long x, long y, ref bool overflow)
		{
			return default(long);
		}

		public static FP operator *(FP x, FP y)
		{
			return default(FP);
		}

		public static FP OverflowMul(FP x, FP y)
		{
			return default(FP);
		}

		public static FP FastMul(FP x, FP y)
		{
			return default(FP);
		}

		public static int CountLeadingZeroes(ulong x)
		{
			return default(int);
		}

		public static FP operator /(FP x, FP y)
		{
			return default(FP);
		}

		public static FP operator %(FP x, FP y)
		{
			return default(FP);
		}

		public static FP FastMod(FP x, FP y)
		{
			return default(FP);
		}

		public static FP operator -(FP x)
		{
			return default(FP);
		}

		public static bool operator ==(FP x, FP y)
		{
			return default(bool);
		}

		public static bool operator !=(FP x, FP y)
		{
			return default(bool);
		}

		public static bool operator >(FP x, FP y)
		{
			return default(bool);
		}

		public static bool operator <(FP x, FP y)
		{
			return default(bool);
		}

		public static bool operator >=(FP x, FP y)
		{
			return default(bool);
		}

		public static bool operator <=(FP x, FP y)
		{
			return default(bool);
		}

		public static FP Sqrt(FP x)
		{
			return default(FP);
		}

		public static FP Sin(FP x)
		{
			return default(FP);
		}

		public static FP FastSin(FP x)
		{
			return default(FP);
		}

		public static long ClampSinValue(long angle, out bool flipHorizontal, out bool flipVertical)
		{
			flipHorizontal = default;
			flipVertical = default;
			return default(long);
		}

		public static FP Cos(FP x)
		{
			return default(FP);
		}

		public static FP FastCos(FP x)
		{
			return default(FP);
		}

		public static FP Tan(FP x)
		{
			return default(FP);
		}

		public static FP Atan(FP y)
		{
			return default(FP);
		}

		public static FP Atan2(FP y, FP x)
		{
			return default(FP);
		}

		public static FP Asin(FP value)
		{
			return default(FP);
		}

		public static FP Acos(FP value)
		{
			return default(FP);
		}

		public static implicit operator FP(long value)
		{
			return default(FP);
		}

		public static explicit operator long(FP value)
		{
			return default(long);
		}

		public static implicit operator FP(float value)
		{
			return default(FP);
		}

		public static explicit operator float(FP value)
		{
			return default(float);
		}

		public static implicit operator FP(double value)
		{
			return default(FP);
		}

		public static explicit operator double(FP value)
		{
			return default(double);
		}

		public static explicit operator FP(decimal value)
		{
			return default(FP);
		}

		public static implicit operator FP(int value)
		{
			return default(FP);
		}

		public static explicit operator decimal(FP value)
		{
			return 0m;
		}

		public float AsFloat()
		{
			return default(float);
		}

		public int AsInt()
		{
			return default(int);
		}

		public long AsLong()
		{
			return default(long);
		}

		public double AsDouble()
		{
			return default(double);
		}

		public decimal AsDecimal()
		{
			return 0m;
		}

		public static float ToFloat(FP value)
		{
			return default(float);
		}

		public static int ToInt(FP value)
		{
			return default(int);
		}

		public static FP FromFloat(float value)
		{
			return default(FP);
		}

		public static bool IsInfinity(FP value)
		{
			return default(bool);
		}

		public static bool IsNaN(FP value)
		{
			return default(bool);
		}

		public override bool Equals(object obj)
		{
			return default(bool);
		}

		public override int GetHashCode()
		{
			return default(int);
		}

		public bool Equals(FP other)
		{
			return default(bool);
		}

		public int CompareTo(FP other)
		{
			return default(int);
		}

		public override string ToString()
		{
			return null;
		}

		public static FP FromRaw(long rawValue)
		{
			return default(FP);
		}

		internal static void GenerateAcosLut()
		{
		}

		internal static void GenerateSinLut()
		{
		}

		internal static void GenerateTanLut()
		{
		}

		public long RawValue
		{
			get
			{
				return default(long);
			}
		}

		[SerializeField]
		public long _serializedValue;

		[NonSerialized]
		public const long MAX_VALUE = 9223372036854775807L;

		[NonSerialized]
		public const long MIN_VALUE = -9223372036854775808L;

		[NonSerialized]
		public const int NUM_BITS = 64;

		[NonSerialized]
		public const int FRACTIONAL_PLACES = 32;

		[NonSerialized]
		public const long ONE = 4294967296L;

		[NonSerialized]
		public const long TEN = 42949672960L;

		[NonSerialized]
		public const long HALF = 2147483648L;

		[NonSerialized]
		public const long PI_TIMES_2 = 26986075409L;

		[NonSerialized]
		public const long PI = 13493037704L;

		[NonSerialized]
		public const long PI_OVER_2 = 6746518852L;

		[NonSerialized]
		public const int LUT_SIZE = 205887;

		[NonSerialized]
		public static readonly decimal Precision;

		[NonSerialized]
		public static readonly FP MaxValue;

		[NonSerialized]
		public static readonly FP MinValue;

		[NonSerialized]
		public static readonly FP One;

		[NonSerialized]
		public static readonly FP Ten;

		[NonSerialized]
		public static readonly FP Half;

		[NonSerialized]
		public static readonly FP Zero;

		[NonSerialized]
		public static readonly FP PositiveInfinity;

		[NonSerialized]
		public static readonly FP NegativeInfinity;

		[NonSerialized]
		public static readonly FP NaN;

		[NonSerialized]
		public static readonly FP EN1;

		[NonSerialized]
		public static readonly FP EN2;

		[NonSerialized]
		public static readonly FP EN3;

		[NonSerialized]
		public static readonly FP EN4;

		[NonSerialized]
		public static readonly FP EN5;

		[NonSerialized]
		public static readonly FP EN6;

		[NonSerialized]
		public static readonly FP EN7;

		[NonSerialized]
		public static readonly FP EN8;

		[NonSerialized]
		public static readonly FP Epsilon;

		[NonSerialized]
		public static readonly FP Pi;

		[NonSerialized]
		public static readonly FP PiOver2;

		[NonSerialized]
		public static readonly FP PiTimes2;

		[NonSerialized]
		public static readonly FP PiInv;

		[NonSerialized]
		public static readonly FP PiOver2Inv;

		[NonSerialized]
		public static readonly FP Deg2Rad;

		[NonSerialized]
		public static readonly FP Rad2Deg;

		[NonSerialized]
		public static readonly FP LutInterval;

		[NonSerialized]
		public static readonly long[] AcosLut;

		[NonSerialized]
		public static readonly long[] SinLut;

		[NonSerialized]
		public static readonly long[] TanLut;
	}
}

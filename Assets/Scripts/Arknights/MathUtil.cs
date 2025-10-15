// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using UnityEngine;

namespace RhodeIsland.Arknights
{
	public static class MathUtil
	{
		public static int ToIntSign(float value)
		{
			return default(int);
		}

		public static bool GT(float a, float b)
		{
			return a > b + 0.00001f;
		}

		public static bool GT(double a, double b)
		{
			return default(bool);
		}
		public static bool GE(float a, float b)
		{
			return default(bool);
		}
		public static bool GE(double a, double b)
		{
			return default(bool);
		}
		public static bool LT(float a, float b)
		{
			return default(bool);
		}
		public static bool LT(double a, double b)
		{
			return default(bool);
		}
		public static bool LE(float a, float b)
		{
			return b + 0.00001f >= a;
		}
		public static bool LE(double a, double b)
		{
			return default(bool);
		}
		public static bool Equals(float a, float b)
		{
			return Mathf.Abs(a - b) <= 0.00001f;
		}
		public static bool Equals(double a, double b)
		{
			return default(bool);
		}
		public static bool Similar(float a, float b)
		{
			return Mathf.Abs(a - b) <= 0.01f;
		}
		public static bool Similar(Vector2 a, Vector2 b)
		{
			if (!Similar(a.x, b.x))
            {
				return false;
            }
			return Similar(a.y, b.y);
		}
		public static bool Similar(Vector3 a, Vector3 b)
		{
			return default(bool);
		}
		/*public static Func<float, float, bool> GetCompareFuncFloat(CompareType type)
		{
			return null;
		}
		public static bool CompareFloat(float lhs, float rhs, CompareType type)
		{
			return default(bool);
		}*/
		public static bool Between(float a, float min, float max)
		{
			return default(bool);
		}
		public static bool Contains(Rect rect, Vector2 point)
		{
			return default(bool);
		}
		public static bool IsZero(float v)
		{
			return Math.Abs(v) <= 0.00001f;
		}
		public static bool IsInOneFrame(float v)
		{
			return default(bool);
		}
		public static bool IsInHalfFrame(float v)
		{
			return default(bool);
		}
		public static void Swap<T>(ref T a, ref T b)
		{
		}
		/*public static T Max<T>(T a, T b) where T : IComparable
		{
			return null;
		}
		public static T Min<T>(T a, T b) where T : IComparable
		{
			return null;
		}
		public static T Clamp<T>(T val, T min, T max) where T : IComparable
		{
			return null;
		}*/
		public static int Lerp(int a, int b, float t)
		{
			return default(int);
		}
		public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
		{
			return default(Vector2);
		}
		public static Vector2 Project(Vector2 a, Vector2 b)
		{
			return default(Vector2);
		}
		public static float Square(float val)
		{
			return default(float);
		}
		public static float ModOne(float val)
		{
			return default(float);
		}
		public static int ModWithinRange(int value, int range)
		{
			return default(int);
		}
		public static long FloorToLong(double value)
		{
			return default(long);
		}
		public static long RoundToLong(double value)
		{
			return default(long);
		}
		public static long CeilToLong(double value)
		{
			return default(long);
		}
		public static long ClampLong(long value, long min, long max)
		{
			return default(long);
		}
		public static float InverseLerp(float v, float a, float b)
		{
			return default(float);
		}
		public static float InverseLerp(long v, long a, long b)
		{
			return default(float);
		}
		public static bool IsZero(Vector2 pos)
		{
			return default(bool);
		}
		public static bool IsZero(Vector3 pos)
		{
			return default(bool);
		}
		public static bool Equals(Vector2 lhs, Vector2 rhs)
		{
			return default(bool);
		}
		public static bool Equals(Vector3 lhs, Vector3 rhs)
		{
			return default(bool);
		}
		public static Vector3 DivideEachComponent(Vector2 lhs, Vector2 rhs)
		{
			return default(Vector3);
		}
		public static Vector3 DivideEachComponent(Vector3 lhs, Vector3 rhs)
		{
			return default(Vector3);
		}
		public static Vector3 MultiEachComponent(Vector3 lhs, Vector3 rhs)
		{
			return default(Vector3);
		}
		public static bool CheckDistance(Vector2 a, Vector2 b, float radius)
		{
			return default(bool);
		}
		public static bool CheckDistance(Vector2 a, Vector2 b, FP radius)
		{
			return default(bool);
		}
		public static bool CheckDistance(Vector3 a, Vector3 b, float radius)
		{
			return default(bool);
		}
		public static Vector2 NormalizeDirectionSafely(Vector2 direction, float tolerance = 1E-05f)
		{
			return default(Vector2);
		}
		public static Vector3 NormalizeDirectionSafely(Vector3 direction)
		{
			return default(Vector3);
		}
		public static Vector3 Abs(Vector3 v)
		{
			return default(Vector3);
		}
		public static Rect Extend(this Rect rect, float extendX, float extendY)
		{
			return default(Rect);
		}
		public static bool GT(FP a, FP b)
		{
			return default(bool);
		}
		public static bool GE(FP a, FP b)
		{
			return default(bool);
		}
		public static bool LT(FP a, FP b)
		{
			return default(bool);
		}
		public static bool LE(FP a, FP b)
		{
			return default(bool);
		}
		public static FP Square(FP val)
		{
			return default(FP);
		}
		public static FP ModOne(FP val)
		{
			return default(FP);
		}
		public static FP Lerp(FP a, FP b, FP t)
		{
			return default(FP);
		}
		public static FP InverseLerp(FP a, FP b, FP value)
		{
			return default(FP);
		}
		public static int RoundToInt(FP value)
		{
			return default(int);
		}
		public static int FloorToInt(FP value)
		{
			return default(int);
		}
		public static int CeilToInt(FP value)
		{
			return default(int);
		}
		public static bool IsZero(FP value)
		{
			return default(bool);
		}
		/*public static bool IsZero(TSVector2 pos)
		{
			return default(bool);
		}*/
		public static bool Equals(FP a, FP b)
		{
			return default(bool);
		}
		public static bool Similar(FP a, FP b)
		{
			return default(bool);
		}
		public static bool Between(FP a, FP min, FP max)
		{
			return default(bool);
		}
		public static bool IsInOneFrame(FP v)
		{
			return default(bool);
		}
		public static bool IsInHalfFrame(FP v)
		{
			return default(bool);
		}
		public static FP Clamp01(FP value)
		{
			return default(FP);
		}
		/*public static Func<FP, FP, bool> GetCompareFuncFP(CompareType type)
		{
			return null;
		}
		public static bool CompareFP(FP lhs, FP rhs, CompareType type)
		{
			return default(bool);
		}*/
		public const float LARGE_EPS = 1E-05f;
		public const float HUGE_EPS = 0.01f;
		public const double SIN30 = 0.5;
		public const double COS30 = 0.8660254;
		public static readonly FP FP_LARGE_EPS;
		public static readonly FP FP_HUGE_EPS;
	}
}

using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// Represents a half-precision floating point number.
    /// </summary>
    /// <remarks>
    /// Note:
    ///     GlmHalf is not fast enought and precision is also very bad,
    ///     so is should not be used for matemathical computation (use Single instead).
    ///     The main advantage of GlmHalf type is lower memory cost: two bytes per number.
    ///     GlmHalf is typically used in graphical applications.
    ///
    /// Note:
    ///     All functions, where is used conversion half->float/float->half,
    ///     are approx. ten times slower than float->double/double->float, i.e. ~3ns on 2GHz CPU.
    ///
    /// References:
    ///     - Fast GlmHalf Float Conversions, Jeroen van der Zijp, link: http://www.fox-toolkit.org/ftp/fasthalffloatconversion.pdf
    ///     - IEEE 754 revision, link: http://grouper.ieee.org/groups/754/
    /// </remarks>
    [Serializable]
    public struct GlmHalf : IComparable, IFormattable, IConvertible, IComparable<GlmHalf>, IEquatable<GlmHalf>
    {
        /// <summary>
        /// Internal representation of the half-precision floating-point number.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal ushort value;

        #region Constants
        /// <summary>
        /// Represents the smallest positive System.GlmHalf value greater than zero. This field is constant.
        /// </summary>
        public static readonly GlmHalf Epsilon = GlmHalf.ToHalf(0x0001);
        /// <summary>
        /// Represents the largest possible value of System.GlmHalf. This field is constant.
        /// </summary>
        public static readonly GlmHalf MaxValue = GlmHalf.ToHalf(0x7bff);
        /// <summary>
        /// Represents the smallest possible value of System.GlmHalf. This field is constant.
        /// </summary>
        public static readonly GlmHalf MinValue = GlmHalf.ToHalf(0xfbff);
        /// <summary>
        /// Represents not a number (NaN). This field is constant.
        /// </summary>
        public static readonly GlmHalf NaN = GlmHalf.ToHalf(0xfe00);
        /// <summary>
        /// Represents negative infinity. This field is constant.
        /// </summary>
        public static readonly GlmHalf NegativeInfinity = GlmHalf.ToHalf(0xfc00);
        /// <summary>
        /// Represents positive infinity. This field is constant.
        /// </summary>
        public static readonly GlmHalf PositiveInfinity = GlmHalf.ToHalf(0x7c00);
        /// <summary>
        /// Represents the 1.0 constant. This field is constant.
        /// </summary>
        public static readonly GlmHalf One = new GlmHalf(1.0f);
        /// <summary>
        /// Represents the zero constant. This field is constant.
        /// </summary>
        public static readonly GlmHalf Zero = new GlmHalf(0.0f);
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of System.GlmHalf to the value of the specified single-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to represent as a System.GlmHalf.</param>
        public GlmHalf(float value) { this = HalfHelper.SingleToHalf(value); }
        /// <summary>
        /// Initializes a new instance of System.GlmHalf to the value of the specified 32-bit signed integer.
        /// </summary>
        /// <param name="value">The value to represent as a System.GlmHalf.</param>
        public GlmHalf(int value) : this((float)value) { }
        /// <summary>
        /// Initializes a new instance of System.GlmHalf to the value of the specified 64-bit signed integer.
        /// </summary>
        /// <param name="value">The value to represent as a System.GlmHalf.</param>
        public GlmHalf(long value) : this((float)value) { }
        /// <summary>
        /// Initializes a new instance of System.GlmHalf to the value of the specified double-precision floating-point number.
        /// </summary>
        /// <param name="value">The value to represent as a System.GlmHalf.</param>
        public GlmHalf(double value) : this((float)value) { }
        /// <summary>
        /// Initializes a new instance of System.GlmHalf to the value of the specified decimal number.
        /// </summary>
        /// <param name="value">The value to represent as a System.GlmHalf.</param>
        public GlmHalf(decimal value) : this((float)value) { }
        /// <summary>
        /// Initializes a new instance of System.GlmHalf to the value of the specified 32-bit unsigned integer.
        /// </summary>
        /// <param name="value">The value to represent as a System.GlmHalf.</param>
        public GlmHalf(uint value) : this((float)value) { }
        /// <summary>
        /// Initializes a new instance of System.GlmHalf to the value of the specified 64-bit unsigned integer.
        /// </summary>
        /// <param name="value">The value to represent as a System.GlmHalf.</param>
        public GlmHalf(ulong value) : this((float)value) { }
        #endregion

        #region Numeric operators

        /// <summary>
        /// Returns the result of multiplying the specified System.GlmHalf value by negative one.
        /// </summary>
        /// <param name="half">A System.GlmHalf.</param>
        /// <returns>A System.GlmHalf with the value of half, but the opposite sign. -or- Zero, if half is zero.</returns>
        public static GlmHalf Negate(GlmHalf half) { return -half; }
        /// <summary>
        /// Adds two specified System.GlmHalf values.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>A System.GlmHalf value that is the sum of half1 and half2.</returns>
        public static GlmHalf Add(GlmHalf half1, GlmHalf half2) { return half1 + half2; }
        /// <summary>
        /// Subtracts one specified System.GlmHalf value from another.
        /// </summary>
        /// <param name="half1">A System.GlmHalf (the minuend).</param>
        /// <param name="half2">A System.GlmHalf (the subtrahend).</param>
        /// <returns>The System.GlmHalf result of subtracting half2 from half1.</returns>
        public static GlmHalf Subtract(GlmHalf half1, GlmHalf half2) { return half1 - half2; }
        /// <summary>
        /// Multiplies two specified System.GlmHalf values.
        /// </summary>
        /// <param name="half1">A System.GlmHalf (the multiplicand).</param>
        /// <param name="half2">A System.GlmHalf (the multiplier).</param>
        /// <returns>A System.GlmHalf that is the result of multiplying half1 and half2.</returns>
        public static GlmHalf Multiply(GlmHalf half1, GlmHalf half2) { return half1 * half2; }
        /// <summary>
        /// Divides two specified System.GlmHalf values.
        /// </summary>
        /// <param name="half1">A System.GlmHalf (the dividend).</param>
        /// <param name="half2">A System.GlmHalf (the divisor).</param>
        /// <returns>The System.GlmHalf that is the result of dividing half1 by half2.</returns>
        /// <exception cref="System.DivideByZeroException">half2 is zero.</exception>
        public static GlmHalf Divide(GlmHalf half1, GlmHalf half2) { return half1 / half2; }

        /// <summary>
        /// Returns the value of the System.GlmHalf operand (the sign of the operand is unchanged).
        /// </summary>
        /// <param name="half">The System.GlmHalf operand.</param>
        /// <returns>The value of the operand, half.</returns>
        public static GlmHalf operator +(GlmHalf half) { return half; }
        /// <summary>
        /// Negates the value of the specified System.GlmHalf operand.
        /// </summary>
        /// <param name="half">The System.GlmHalf operand.</param>
        /// <returns>The result of half multiplied by negative one (-1).</returns>
        public static GlmHalf operator -(GlmHalf half) { return HalfHelper.Negate(half); }
        /// <summary>
        /// Increments the System.GlmHalf operand by 1.
        /// </summary>
        /// <param name="half">The System.GlmHalf operand.</param>
        /// <returns>The value of half incremented by 1.</returns>
        public static GlmHalf operator ++(GlmHalf half) { return (GlmHalf)(half + 1f); }
        /// <summary>
        /// Decrements the System.GlmHalf operand by one.
        /// </summary>
        /// <param name="half">The System.GlmHalf operand.</param>
        /// <returns>The value of half decremented by 1.</returns>
        public static GlmHalf operator --(GlmHalf half) { return (GlmHalf)(half - 1f); }
        /// <summary>
        /// Adds two specified System.GlmHalf values.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>The System.GlmHalf result of adding half1 and half2.</returns>
        public static GlmHalf operator +(GlmHalf half1, GlmHalf half2) { return (GlmHalf)((float)half1 + (float)half2); }
        /// <summary>
        /// Subtracts two specified System.GlmHalf values.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>The System.GlmHalf result of subtracting half1 and half2.</returns>
        public static GlmHalf operator -(GlmHalf half1, GlmHalf half2) { return (GlmHalf)((float)half1 - (float)half2); }
        /// <summary>
        /// Multiplies two specified System.GlmHalf values.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>The System.GlmHalf result of multiplying half1 by half2.</returns>
        public static GlmHalf operator *(GlmHalf half1, GlmHalf half2) { return (GlmHalf)((float)half1 * (float)half2); }
        /// <summary>
        /// Calculates the modulo of the two specified System.GlmHalf values.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>The System.GlmHalf result of module half1 by half2.</returns>
        public static GlmHalf operator %(GlmHalf half1, GlmHalf half2) { return (GlmHalf)((float)half1 % (float)half2); }
        /// <summary>
        /// Divides two specified System.GlmHalf values.
        /// </summary>
        /// <param name="half1">A System.GlmHalf (the dividend).</param>
        /// <param name="half2">A System.GlmHalf (the divisor).</param>
        /// <returns>The System.GlmHalf result of half1 by half2.</returns>
        public static GlmHalf operator /(GlmHalf half1, GlmHalf half2) { return (GlmHalf)((float)half1 / (float)half2); }
        /// <summary>
        /// Returns a value indicating whether two instances of System.GlmHalf are equal.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>true if half1 and half2 are equal; otherwise, false.</returns>
        public static bool operator ==(GlmHalf half1, GlmHalf half2) { return (!IsNaN(half1) && (half1.value == half2.value)); }
        /// <summary>
        /// Returns a value indicating whether two instances of System.GlmHalf are not equal.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>true if half1 and half2 are not equal; otherwise, false.</returns>
        public static bool operator !=(GlmHalf half1, GlmHalf half2) { return !(half1.value == half2.value); }
        /// <summary>
        /// Returns a value indicating whether a specified System.GlmHalf is less than another specified System.GlmHalf.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>true if half1 is less than half1; otherwise, false.</returns>
        public static bool operator <(GlmHalf half1, GlmHalf half2) { return (float)half1 < (float)half2; }
        /// <summary>
        /// Returns a value indicating whether a specified System.GlmHalf is greater than another specified System.GlmHalf.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>true if half1 is greater than half2; otherwise, false.</returns>
        public static bool operator >(GlmHalf half1, GlmHalf half2) { return (float)half1 > (float)half2; }
        /// <summary>
        /// Returns a value indicating whether a specified System.GlmHalf is less than or equal to another specified System.GlmHalf.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>true if half1 is less than or equal to half2; otherwise, false.</returns>
        public static bool operator <=(GlmHalf half1, GlmHalf half2) { return (half1 == half2) || (half1 < half2); }
        /// <summary>
        /// Returns a value indicating whether a specified System.GlmHalf is greater than or equal to another specified System.GlmHalf.
        /// </summary>
        /// <param name="half1">A System.GlmHalf.</param>
        /// <param name="half2">A System.GlmHalf.</param>
        /// <returns>true if half1 is greater than or equal to half2; otherwise, false.</returns>
        public static bool operator >=(GlmHalf half1, GlmHalf half2) { return (half1 == half2) || (half1 > half2); }
        #endregion

        #region Type casting operators
        /// <summary>
        /// Converts an 8-bit unsigned integer to a System.GlmHalf.
        /// </summary>
        /// <param name="value">An 8-bit unsigned integer.</param>
        /// <returns>A System.GlmHalf that represents the converted 8-bit unsigned integer.</returns>
        public static implicit operator GlmHalf(byte value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a 16-bit signed integer to a System.GlmHalf.
        /// </summary>
        /// <param name="value">A 16-bit signed integer.</param>
        /// <returns>A System.GlmHalf that represents the converted 16-bit signed integer.</returns>
        public static implicit operator GlmHalf(short value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a Unicode character to a System.GlmHalf.
        /// </summary>
        /// <param name="value">A Unicode character.</param>
        /// <returns>A System.GlmHalf that represents the converted Unicode character.</returns>
        public static implicit operator GlmHalf(char value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a 32-bit signed integer to a System.GlmHalf.
        /// </summary>
        /// <param name="value">A 32-bit signed integer.</param>
        /// <returns>A System.GlmHalf that represents the converted 32-bit signed integer.</returns>
        public static implicit operator GlmHalf(int value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a 64-bit signed integer to a System.GlmHalf.
        /// </summary>
        /// <param name="value">A 64-bit signed integer.</param>
        /// <returns>A System.GlmHalf that represents the converted 64-bit signed integer.</returns>
        public static implicit operator GlmHalf(long value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a single-precision floating-point number to a System.GlmHalf.
        /// </summary>
        /// <param name="value">A single-precision floating-point number.</param>
        /// <returns>A System.GlmHalf that represents the converted single-precision floating point number.</returns>
        public static explicit operator GlmHalf(float value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a double-precision floating-point number to a System.GlmHalf.
        /// </summary>
        /// <param name="value">A double-precision floating-point number.</param>
        /// <returns>A System.GlmHalf that represents the converted double-precision floating point number.</returns>
        public static explicit operator GlmHalf(double value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a decimal number to a System.GlmHalf.
        /// </summary>
        /// <param name="value">decimal number</param>
        /// <returns>A System.GlmHalf that represents the converted decimal number.</returns>
        public static explicit operator GlmHalf(decimal value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a System.GlmHalf to an 8-bit unsigned integer.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>An 8-bit unsigned integer that represents the converted System.GlmHalf.</returns>
        public static explicit operator byte(GlmHalf value) { return (byte)(float)value; }
        /// <summary>
        /// Converts a System.GlmHalf to a Unicode character.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A Unicode character that represents the converted System.GlmHalf.</returns>
        public static explicit operator char(GlmHalf value) { return (char)(float)value; }
        /// <summary>
        /// Converts a System.GlmHalf to a 16-bit signed integer.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A 16-bit signed integer that represents the converted System.GlmHalf.</returns>
        public static explicit operator short(GlmHalf value) { return (short)(float)value; }
        /// <summary>
        /// Converts a System.GlmHalf to a 32-bit signed integer.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A 32-bit signed integer that represents the converted System.GlmHalf.</returns>
        public static explicit operator int(GlmHalf value) { return (int)(float)value; }
        /// <summary>
        /// Converts a System.GlmHalf to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A 64-bit signed integer that represents the converted System.GlmHalf.</returns>
        public static explicit operator long(GlmHalf value) { return (long)(float)value; }
        /// <summary>
        /// Converts a System.GlmHalf to a single-precision floating-point number.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A single-precision floating-point number that represents the converted System.GlmHalf.</returns>
        public static implicit operator float(GlmHalf value) { return (float)HalfHelper.HalfToSingle(value); }
        /// <summary>
        /// Converts a System.GlmHalf to a double-precision floating-point number.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A double-precision floating-point number that represents the converted System.GlmHalf.</returns>
        public static implicit operator double(GlmHalf value) { return (double)(float)value; }
        /// <summary>
        /// Converts a System.GlmHalf to a decimal number.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A decimal number that represents the converted System.GlmHalf.</returns>
        public static explicit operator decimal (GlmHalf value) { return (decimal)(float)value; }
        /// <summary>
        /// Converts a System.GlmHalf to a System.Complex.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A System.Complex that represents the converted System.GlmHalf with zero imaginary part.</returns>
        public static explicit operator Complex (GlmHalf value) { return (Complex)(float)value; }
        /// <summary>
        /// Converts an 8-bit signed integer to a System.GlmHalf.
        /// </summary>
        /// <param name="value">An 8-bit signed integer.</param>
        /// <returns>A System.GlmHalf that represents the converted 8-bit signed integer.</returns>
        public static implicit operator GlmHalf(sbyte value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a 16-bit unsigned integer to a System.GlmHalf.
        /// </summary>
        /// <param name="value">A 16-bit unsigned integer.</param>
        /// <returns>A System.GlmHalf that represents the converted 16-bit unsigned integer.</returns>
        public static implicit operator GlmHalf(ushort value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a 32-bit unsigned integer to a System.GlmHalf.
        /// </summary>
        /// <param name="value">A 32-bit unsigned integer.</param>
        /// <returns>A System.GlmHalf that represents the converted 32-bit unsigned integer.</returns>
        public static implicit operator GlmHalf(uint value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a 64-bit unsigned integer to a System.GlmHalf.
        /// </summary>
        /// <param name="value">A 64-bit unsigned integer.</param>
        /// <returns>A System.GlmHalf that represents the converted 64-bit unsigned integer.</returns>
        public static implicit operator GlmHalf(ulong value) { return new GlmHalf((float)value); }
        /// <summary>
        /// Converts a System.GlmHalf to an 8-bit signed integer.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>An 8-bit signed integer that represents the converted System.GlmHalf.</returns>
        public static explicit operator sbyte(GlmHalf value) { return (sbyte)(float)value; }
        /// <summary>
        /// Converts a System.GlmHalf to a 16-bit unsigned integer.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A 16-bit unsigned integer that represents the converted System.GlmHalf.</returns>
        public static explicit operator ushort(GlmHalf value) { return (ushort)(float)value; }
        /// <summary>
        /// Converts a System.GlmHalf to a 32-bit unsigned integer.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A 32-bit unsigned integer that represents the converted System.GlmHalf.</returns>
        public static explicit operator uint(GlmHalf value) { return (uint)(float)value; }
        /// <summary>
        /// Converts a System.GlmHalf to a 64-bit unsigned integer.
        /// </summary>
        /// <param name="value">A System.GlmHalf to convert.</param>
        /// <returns>A 64-bit unsigned integer that represents the converted System.GlmHalf.</returns>
        public static explicit operator ulong(GlmHalf value) { return (ulong)(float)value; }
        #endregion

        /// <summary>
        /// Compares this instance to a specified System.GlmHalf object.
        /// </summary>
        /// <param name="other">A System.GlmHalf object.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and value.
        /// Return Value Meaning Less than zero This instance is less than value. Zero
        /// This instance is equal to value. Greater than zero This instance is greater than value.
        /// </returns>
        public int CompareTo(GlmHalf other)
        {
            int result = 0;
            if (this < other)
            {
                result = -1;
            }
            else if (this > other)
            {
                result = 1;
            }
            else if (this != other)
            {
                if (!IsNaN(this))
                {
                    result = 1;
                }
                else if (!IsNaN(other))
                {
                    result = -1;
                }
            }

            return result;
        }
        /// <summary>
        /// Compares this instance to a specified System.Object.
        /// </summary>
        /// <param name="obj">An System.Object or null.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and value.
        /// Return Value Meaning Less than zero This instance is less than value. Zero
        /// This instance is equal to value. Greater than zero This instance is greater
        /// than value. -or- value is null.
        /// </returns>
        /// <exception cref="System.ArgumentException">value is not a System.GlmHalf</exception>
        public int CompareTo(object obj)
        {
            int result = 0;
            if (obj == null)
            {
                result = 1;
            }
            else
            {
                if (obj is GlmHalf)
                {
                    result = CompareTo((GlmHalf)obj);
                }
                else
                {
                    throw new ArgumentException("Object must be of type GlmHalf.");
                }
            }

            return result;
        }
        /// <summary>
        /// Returns a value indicating whether this instance and a specified System.GlmHalf object represent the same value.
        /// </summary>
        /// <param name="other">A System.GlmHalf object to compare to this instance.</param>
        /// <returns>true if value is equal to this instance; otherwise, false.</returns>
        public bool Equals(GlmHalf other)
        {
            return ((other == this) || (IsNaN(other) && IsNaN(this)));
        }
        /// <summary>
        /// Returns a value indicating whether this instance and a specified System.Object
        /// represent the same type and value.
        /// </summary>
        /// <param name="obj">An System.Object.</param>
        /// <returns>true if value is a System.GlmHalf and equal to this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is GlmHalf)
            {
                GlmHalf half = (GlmHalf)obj;
                if ((half == this) || (IsNaN(half) && IsNaN(this)))
                {
                    result = true;
                }
            }

            return result;
        }
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        /// <summary>
        /// Returns the System.TypeCode for value type System.GlmHalf.
        /// </summary>
        /// <returns>The enumerated constant (TypeCode)255.</returns>
        public TypeCode GetTypeCode()
        {
            return (TypeCode)255;
        }

        #region BitConverter & Math methods for GlmHalf
        /// <summary>
        /// Returns the specified half-precision floating point value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 2.</returns>
        public static byte[] GetBytes(GlmHalf value)
        {
            return BitConverter.GetBytes(value.value);
        }
        /// <summary>
        /// Converts the value of a specified instance of System.GlmHalf to its equivalent binary representation.
        /// </summary>
        /// <param name="value">A System.GlmHalf value.</param>
        /// <returns>A 16-bit unsigned integer that contain the binary representation of value.</returns>
        public static ushort GetBits(GlmHalf value)
        {
            return value.value;
        }
        /// <summary>
        /// Returns a half-precision floating point number converted from two bytes
        /// at a specified position in a byte array.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <returns>A half-precision floating point number formed by two bytes beginning at startIndex.</returns>
        /// <exception cref="System.ArgumentException">
        /// startIndex is greater than or equal to the length of value minus 1, and is
        /// less than or equal to the length of value minus 1.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">startIndex is less than zero or greater than the length of value minus 1.</exception>
        public static GlmHalf ToHalf(byte[] value, int startIndex)
        {
            return GlmHalf.ToHalf((ushort)BitConverter.ToInt16(value, startIndex));
        }
        /// <summary>
        /// Returns a half-precision floating point number converted from its binary representation.
        /// </summary>
        /// <param name="bits">Binary representation of System.GlmHalf value</param>
        /// <returns>A half-precision floating point number formed by its binary representation.</returns>
        public static GlmHalf ToHalf(ushort bits)
        {
            const ushort NegZero = (ushort)32768u;
            const ushort Zero = (ushort)0u;

            return new GlmHalf { value = bits == NegZero ? Zero : bits };
        }

        /// <summary>
        /// Returns a value indicating the sign of a half-precision floating-point number.
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>
        /// A number indicating the sign of value. Number Description -1 value is less
        /// than zero. 0 value is equal to zero. 1 value is greater than zero.
        /// </returns>
        /// <exception cref="System.ArithmeticException">value is equal to System.GlmHalf.NaN.</exception>
        public static int Sign(GlmHalf value)
        {
            if (value < 0)
            {
                return -1;
            }
            else if (value > 0)
            {
                return 1;
            }
            else
            {
                if (value != 0)
                {
                    throw new ArithmeticException("Function does not accept floating point Not-a-Number values.");
                }
            }

            return 0;
        }
        /// <summary>
        /// Returns the absolute value of a half-precision floating-point number.
        /// </summary>
        /// <param name="value">A number in the range System.GlmHalf.MinValue ≤ value ≤ System.GlmHalf.MaxValue.</param>
        /// <returns>A half-precision floating-point number, x, such that 0 ≤ x ≤System.GlmHalf.MaxValue.</returns>
        public static GlmHalf Abs(GlmHalf value)
        {
            return HalfHelper.Abs(value);
        }
        /// <summary>
        /// Returns the larger of two half-precision floating-point numbers.
        /// </summary>
        /// <param name="value1">The first of two half-precision floating-point numbers to compare.</param>
        /// <param name="value2">The second of two half-precision floating-point numbers to compare.</param>
        /// <returns>
        /// Parameter value1 or value2, whichever is larger. If value1, or value2, or both val1
        /// and value2 are equal to System.GlmHalf.NaN, System.GlmHalf.NaN is returned.
        /// </returns>
        public static GlmHalf Max(GlmHalf value1, GlmHalf value2)
        {
            return (value1 < value2) ? value2 : value1;
        }
        /// <summary>
        /// Returns the smaller of two half-precision floating-point numbers.
        /// </summary>
        /// <param name="value1">The first of two half-precision floating-point numbers to compare.</param>
        /// <param name="value2">The second of two half-precision floating-point numbers to compare.</param>
        /// <returns>
        /// Parameter value1 or value2, whichever is smaller. If value1, or value2, or both val1
        /// and value2 are equal to System.GlmHalf.NaN, System.GlmHalf.NaN is returned.
        /// </returns>
        public static GlmHalf Min(GlmHalf value1, GlmHalf value2)
        {
            return (value1 < value2) ? value1 : value2;
        }
        #endregion

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to not a number (System.GlmHalf.NaN).
        /// </summary>
        /// <param name="half">A half-precision floating-point number.</param>
        /// <returns>true if value evaluates to not a number (System.GlmHalf.NaN); otherwise, false.</returns>
        public static bool IsNaN(GlmHalf half)
        {
            return HalfHelper.IsNaN(half);
        }
        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative or positive infinity.
        /// </summary>
        /// <param name="half">A half-precision floating-point number.</param>
        /// <returns>true if half evaluates to System.GlmHalf.PositiveInfinity or System.GlmHalf.NegativeInfinity; otherwise, false.</returns>
        public static bool IsInfinity(GlmHalf half)
        {
            return HalfHelper.IsInfinity(half);
        }
        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative infinity.
        /// </summary>
        /// <param name="half">A half-precision floating-point number.</param>
        /// <returns>true if half evaluates to System.GlmHalf.NegativeInfinity; otherwise, false.</returns>
        public static bool IsNegativeInfinity(GlmHalf half)
        {
            return HalfHelper.IsNegativeInfinity(half);
        }
        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to positive infinity.
        /// </summary>
        /// <param name="half">A half-precision floating-point number.</param>
        /// <returns>true if half evaluates to System.GlmHalf.PositiveInfinity; otherwise, false.</returns>
        public static bool IsPositiveInfinity(GlmHalf half)
        {
            return HalfHelper.IsPositiveInfinity(half);
        }

        #region String operations (Parse and ToString)
        /// <summary>
        /// Converts the string representation of a number to its System.GlmHalf equivalent.
        /// </summary>
        /// <param name="value">The string representation of the number to convert.</param>
        /// <returns>The System.GlmHalf number equivalent to the number contained in value.</returns>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        /// <exception cref="System.FormatException">value is not in the correct format.</exception>
        /// <exception cref="System.OverflowException">value represents a number less than System.GlmHalf.MinValue or greater than System.GlmHalf.MaxValue.</exception>
        public static GlmHalf Parse(string value)
        {
            return (GlmHalf)float.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Converts the string representation of a number to its System.GlmHalf equivalent
        /// using the specified culture-specific format information.
        /// </summary>
        /// <param name="value">The string representation of the number to convert.</param>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific parsing information about value.</param>
        /// <returns>The System.GlmHalf number equivalent to the number contained in s as specified by provider.</returns>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        /// <exception cref="System.FormatException">value is not in the correct format.</exception>
        /// <exception cref="System.OverflowException">value represents a number less than System.GlmHalf.MinValue or greater than System.GlmHalf.MaxValue.</exception>
        public static GlmHalf Parse(string value, IFormatProvider provider)
        {
            return (GlmHalf)float.Parse(value, provider);
        }
        /// <summary>
        /// Converts the string representation of a number in a specified style to its System.GlmHalf equivalent.
        /// </summary>
        /// <param name="value">The string representation of the number to convert.</param>
        /// <param name="style">
        /// A bitwise combination of System.Globalization.NumberStyles values that indicates
        /// the style elements that can be present in value. A typical value to specify is
        /// System.Globalization.NumberStyles.Number.
        /// </param>
        /// <returns>The System.GlmHalf number equivalent to the number contained in s as specified by style.</returns>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// style is not a System.Globalization.NumberStyles value. -or- style is the
        /// System.Globalization.NumberStyles.AllowHexSpecifier value.
        /// </exception>
        /// <exception cref="System.FormatException">value is not in the correct format.</exception>
        /// <exception cref="System.OverflowException">value represents a number less than System.GlmHalf.MinValue or greater than System.GlmHalf.MaxValue.</exception>
        public static GlmHalf Parse(string value, NumberStyles style)
        {
            return (GlmHalf)float.Parse(value, style, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Converts the string representation of a number to its System.GlmHalf equivalent
        /// using the specified style and culture-specific format.
        /// </summary>
        /// <param name="value">The string representation of the number to convert.</param>
        /// <param name="style">
        /// A bitwise combination of System.Globalization.NumberStyles values that indicates
        /// the style elements that can be present in value. A typical value to specify is
        /// System.Globalization.NumberStyles.Number.
        /// </param>
        /// <param name="provider">An System.IFormatProvider object that supplies culture-specific information about the format of value.</param>
        /// <returns>The System.GlmHalf number equivalent to the number contained in s as specified by style and provider.</returns>
        /// <exception cref="System.ArgumentNullException">value is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// style is not a System.Globalization.NumberStyles value. -or- style is the
        /// System.Globalization.NumberStyles.AllowHexSpecifier value.
        /// </exception>
        /// <exception cref="System.FormatException">value is not in the correct format.</exception>
        /// <exception cref="System.OverflowException">value represents a number less than System.GlmHalf.MinValue or greater than System.GlmHalf.MaxValue.</exception>
        public static GlmHalf Parse(string value, NumberStyles style, IFormatProvider provider)
        {
            return (GlmHalf)float.Parse(value, style, provider);
        }
        /// <summary>
        /// Converts the string representation of a number to its System.GlmHalf equivalent.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">The string representation of the number to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the System.GlmHalf number that is equivalent
        /// to the numeric value contained in value, if the conversion succeeded, or is zero
        /// if the conversion failed. The conversion fails if the s parameter is null,
        /// is not a number in a valid format, or represents a number less than System.GlmHalf.MinValue
        /// or greater than System.GlmHalf.MaxValue. This parameter is passed uninitialized.
        /// </param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string value, out GlmHalf result)
        {
            float f;
            if (float.TryParse(value, out f))
            {
                result = (GlmHalf)f;
                return true;
            }

            result = new GlmHalf();
            return false;
        }
        /// <summary>
        /// Converts the string representation of a number to its System.GlmHalf equivalent
        /// using the specified style and culture-specific format. A return value indicates
        /// whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">The string representation of the number to convert.</param>
        /// <param name="style">
        /// A bitwise combination of System.Globalization.NumberStyles values that indicates
        /// the permitted format of value. A typical value to specify is System.Globalization.NumberStyles.Number.
        /// </param>
        /// <param name="provider">An System.IFormatProvider object that supplies culture-specific parsing information about value.</param>
        /// <param name="result">
        /// When this method returns, contains the System.GlmHalf number that is equivalent
        /// to the numeric value contained in value, if the conversion succeeded, or is zero
        /// if the conversion failed. The conversion fails if the s parameter is null,
        /// is not in a format compliant with style, or represents a number less than
        /// System.GlmHalf.MinValue or greater than System.GlmHalf.MaxValue. This parameter is passed uninitialized.
        /// </param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        /// <exception cref="System.ArgumentException">
        /// style is not a System.Globalization.NumberStyles value. -or- style
        /// is the System.Globalization.NumberStyles.AllowHexSpecifier value.
        /// </exception>
        public static bool TryParse(string value, NumberStyles style, IFormatProvider provider, out GlmHalf result)
        {
            bool parseResult = false;
            float f;
            if (float.TryParse(value, style, provider, out f))
            {
                result = (GlmHalf)f;
                parseResult = true;
            }
            else
            {
                result = new GlmHalf();
            }

            return parseResult;
        }
        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>A string that represents the value of this instance.</returns>
        public override string ToString()
        {
            return ((float)this).ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation
        /// using the specified culture-specific format information.
        /// </summary>
        /// <param name="formatProvider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by provider.</returns>
        public string ToString(IFormatProvider formatProvider)
        {
            return ((float)this).ToString(formatProvider);
        }
        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation, using the specified format.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>The string representation of the value of this instance as specified by format.</returns>
        public string ToString(string format)
        {
            return ((float)this).ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation
        /// using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by format and provider.</returns>
        /// <exception cref="System.FormatException">format is invalid.</exception>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ((float)this).ToString(format, formatProvider);
        }
        #endregion

        #region IConvertible Members
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return (float)this;
        }
        TypeCode IConvertible.GetTypeCode()
        {
            return GetTypeCode();
        }
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean((float)this);
        }
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte((float)this);
        }
        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "Invalid cast from '{0}' to '{1}'.", "GlmHalf", "Char"));
        }
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "Invalid cast from '{0}' to '{1}'.", "GlmHalf", "DateTime"));
        }
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal((float)this);
        }
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble((float)this);
        }
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16((float)this);
        }
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32((float)this);
        }
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64((float)this);
        }
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte((float)this);
        }
        string IConvertible.ToString(IFormatProvider provider)
        {
            return Convert.ToString((float)this, CultureInfo.InvariantCulture);
        }
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return (((float)this) as IConvertible).ToType(conversionType, provider);
        }
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16((float)this);
        }
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32((float)this);
        }
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64((float)this);
        }
        #endregion
    }
}

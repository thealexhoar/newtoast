using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Numerics;
using System.Linq;
using GlmSharp.Swizzle;

// ReSharper disable InconsistentNaming

namespace GlmSharp
{

    /// <summary>
    /// A vector of type GlmHalf with 2 components.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "vec")]
    [StructLayout(LayoutKind.Sequential)]
    public struct hvec2 : IReadOnlyList<GlmHalf>, IEquatable<hvec2>
    {

        #region Fields

        /// <summary>
        /// x-component
        /// </summary>
        [DataMember]
        public GlmHalf x;

        /// <summary>
        /// y-component
        /// </summary>
        [DataMember]
        public GlmHalf y;

        #endregion


        #region Constructors

        /// <summary>
        /// Component-wise constructor
        /// </summary>
        public hvec2(GlmHalf x, GlmHalf y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// all-same-value constructor
        /// </summary>
        public hvec2(GlmHalf v)
        {
            this.x = v;
            this.y = v;
        }

        /// <summary>
        /// from-vector constructor
        /// </summary>
        public hvec2(hvec2 v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        /// <summary>
        /// from-vector constructor (additional fields are truncated)
        /// </summary>
        public hvec2(hvec3 v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        /// <summary>
        /// from-vector constructor (additional fields are truncated)
        /// </summary>
        public hvec2(hvec4 v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        /// <summary>
        /// From-array/list constructor (superfluous values are ignored, missing values are zero-filled).
        /// </summary>
        public hvec2(IReadOnlyList<GlmHalf> v)
        {
            var c = v.Count;
            this.x = c < 0 ? GlmHalf.Zero : v[0];
            this.y = c < 1 ? GlmHalf.Zero : v[1];
        }

        /// <summary>
        /// Generic from-array constructor (superfluous values are ignored, missing values are zero-filled).
        /// </summary>
        public hvec2(Object[] v)
        {
            var c = v.Length;
            this.x = c < 0 ? GlmHalf.Zero : (GlmHalf)v[0];
            this.y = c < 1 ? GlmHalf.Zero : (GlmHalf)v[1];
        }

        /// <summary>
        /// From-array constructor (superfluous values are ignored, missing values are zero-filled).
        /// </summary>
        public hvec2(GlmHalf[] v)
        {
            var c = v.Length;
            this.x = c < 0 ? GlmHalf.Zero : v[0];
            this.y = c < 1 ? GlmHalf.Zero : v[1];
        }

        /// <summary>
        /// From-array constructor with base index (superfluous values are ignored, missing values are zero-filled).
        /// </summary>
        public hvec2(GlmHalf[] v, int startIndex)
        {
            var c = v.Length;
            this.x = c + startIndex < 0 ? GlmHalf.Zero : v[0 + startIndex];
            this.y = c + startIndex < 1 ? GlmHalf.Zero : v[1 + startIndex];
        }

        /// <summary>
        /// From-IEnumerable constructor (superfluous values are ignored, missing values are zero-filled).
        /// </summary>
        public hvec2(IEnumerable<GlmHalf> v)
            : this(v.ToArray())
        {
        }

        #endregion


        #region Implicit Operators

        /// <summary>
        /// Implicitly converts this to a vec2.
        /// </summary>
        public static implicit operator vec2(hvec2 v) => new vec2((float)v.x, (float)v.y);

        /// <summary>
        /// Implicitly converts this to a dvec2.
        /// </summary>
        public static implicit operator dvec2(hvec2 v) => new dvec2((double)v.x, (double)v.y);

        /// <summary>
        /// Implicitly converts this to a cvec2.
        /// </summary>
        public static implicit operator cvec2(hvec2 v) => new cvec2((Complex)v.x, (Complex)v.y);

        #endregion


        #region Explicit Operators

        /// <summary>
        /// Explicitly converts this to a ivec2.
        /// </summary>
        public static explicit operator ivec2(hvec2 v) => new ivec2((int)v.x, (int)v.y);

        /// <summary>
        /// Explicitly converts this to a ivec3. (Higher components are zeroed)
        /// </summary>
        public static explicit operator ivec3(hvec2 v) => new ivec3((int)v.x, (int)v.y, 0);

        /// <summary>
        /// Explicitly converts this to a ivec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator ivec4(hvec2 v) => new ivec4((int)v.x, (int)v.y, 0, 0);

        /// <summary>
        /// Explicitly converts this to a uvec2.
        /// </summary>
        public static explicit operator uvec2(hvec2 v) => new uvec2((uint)v.x, (uint)v.y);

        /// <summary>
        /// Explicitly converts this to a uvec3. (Higher components are zeroed)
        /// </summary>
        public static explicit operator uvec3(hvec2 v) => new uvec3((uint)v.x, (uint)v.y, 0u);

        /// <summary>
        /// Explicitly converts this to a uvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator uvec4(hvec2 v) => new uvec4((uint)v.x, (uint)v.y, 0u, 0u);

        /// <summary>
        /// Explicitly converts this to a vec3. (Higher components are zeroed)
        /// </summary>
        public static explicit operator vec3(hvec2 v) => new vec3((float)v.x, (float)v.y, 0f);

        /// <summary>
        /// Explicitly converts this to a vec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator vec4(hvec2 v) => new vec4((float)v.x, (float)v.y, 0f, 0f);

        /// <summary>
        /// Explicitly converts this to a hvec3. (Higher components are zeroed)
        /// </summary>
        public static explicit operator hvec3(hvec2 v) => new hvec3((GlmHalf)v.x, (GlmHalf)v.y, GlmHalf.Zero);

        /// <summary>
        /// Explicitly converts this to a hvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator hvec4(hvec2 v) => new hvec4((GlmHalf)v.x, (GlmHalf)v.y, GlmHalf.Zero, GlmHalf.Zero);

        /// <summary>
        /// Explicitly converts this to a dvec3. (Higher components are zeroed)
        /// </summary>
        public static explicit operator dvec3(hvec2 v) => new dvec3((double)v.x, (double)v.y, 0.0);

        /// <summary>
        /// Explicitly converts this to a dvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator dvec4(hvec2 v) => new dvec4((double)v.x, (double)v.y, 0.0, 0.0);

        /// <summary>
        /// Explicitly converts this to a decvec2.
        /// </summary>
        public static explicit operator decvec2(hvec2 v) => new decvec2((decimal)v.x, (decimal)v.y);

        /// <summary>
        /// Explicitly converts this to a decvec3. (Higher components are zeroed)
        /// </summary>
        public static explicit operator decvec3(hvec2 v) => new decvec3((decimal)v.x, (decimal)v.y, 0m);

        /// <summary>
        /// Explicitly converts this to a decvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator decvec4(hvec2 v) => new decvec4((decimal)v.x, (decimal)v.y, 0m, 0m);

        /// <summary>
        /// Explicitly converts this to a cvec3. (Higher components are zeroed)
        /// </summary>
        public static explicit operator cvec3(hvec2 v) => new cvec3((Complex)v.x, (Complex)v.y, Complex.Zero);

        /// <summary>
        /// Explicitly converts this to a cvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator cvec4(hvec2 v) => new cvec4((Complex)v.x, (Complex)v.y, Complex.Zero, Complex.Zero);

        /// <summary>
        /// Explicitly converts this to a lvec2.
        /// </summary>
        public static explicit operator lvec2(hvec2 v) => new lvec2((long)v.x, (long)v.y);

        /// <summary>
        /// Explicitly converts this to a lvec3. (Higher components are zeroed)
        /// </summary>
        public static explicit operator lvec3(hvec2 v) => new lvec3((long)v.x, (long)v.y, 0);

        /// <summary>
        /// Explicitly converts this to a lvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator lvec4(hvec2 v) => new lvec4((long)v.x, (long)v.y, 0, 0);

        /// <summary>
        /// Explicitly converts this to a bvec2.
        /// </summary>
        public static explicit operator bvec2(hvec2 v) => new bvec2(v.x != GlmHalf.Zero, v.y != GlmHalf.Zero);

        /// <summary>
        /// Explicitly converts this to a bvec3. (Higher components are zeroed)
        /// </summary>
        public static explicit operator bvec3(hvec2 v) => new bvec3(v.x != GlmHalf.Zero, v.y != GlmHalf.Zero, false);

        /// <summary>
        /// Explicitly converts this to a bvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator bvec4(hvec2 v) => new bvec4(v.x != GlmHalf.Zero, v.y != GlmHalf.Zero, false, false);

        /// <summary>
        /// Explicitly converts this to a GlmHalf array.
        /// </summary>
        public static explicit operator GlmHalf[](hvec2 v) => new [] { v.x, v.y };

        /// <summary>
        /// Explicitly converts this to a generic object array.
        /// </summary>
        public static explicit operator Object[](hvec2 v) => new Object[] { v.x, v.y };

        #endregion


        #region Indexer

        /// <summary>
        /// Gets/Sets a specific indexed component (a bit slower than direct access).
        /// </summary>
        public GlmHalf this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
        }

        #endregion


        #region Properties

        /// <summary>
        /// Returns an object that can be used for arbitrary swizzling (e.g. swizzle.zy)
        /// </summary>
        public swizzle_hvec2 swizzle => new swizzle_hvec2(x, y);

        /// <summary>
        /// Gets or sets the specified subset of components. For more advanced (read-only) swizzling, use the .swizzle property.
        /// </summary>
        public hvec2 xy
        {
            get
            {
                return new hvec2(x, y);
            }
            set
            {
                x = value.x;
                y = value.y;
            }
        }

        /// <summary>
        /// Gets or sets the specified subset of components. For more advanced (read-only) swizzling, use the .swizzle property.
        /// </summary>
        public hvec2 rg
        {
            get
            {
                return new hvec2(x, y);
            }
            set
            {
                x = value.x;
                y = value.y;
            }
        }

        /// <summary>
        /// Gets or sets the specified RGBA component. For more advanced (read-only) swizzling, use the .swizzle property.
        /// </summary>
        public GlmHalf r
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        /// <summary>
        /// Gets or sets the specified RGBA component. For more advanced (read-only) swizzling, use the .swizzle property.
        /// </summary>
        public GlmHalf g
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        /// <summary>
        /// Returns an array with all values
        /// </summary>
        public GlmHalf[] Values => new[] { x, y };

        /// <summary>
        /// Returns the number of components (2).
        /// </summary>
        public int Count => 2;

        /// <summary>
        /// Returns the minimal component of this vector.
        /// </summary>
        public GlmHalf MinElement => GlmHalf.Min(x, y);

        /// <summary>
        /// Returns the maximal component of this vector.
        /// </summary>
        public GlmHalf MaxElement => GlmHalf.Max(x, y);

        /// <summary>
        /// Returns the euclidean length of this vector.
        /// </summary>
        public float Length => (float)Math.Sqrt((x*x + y*y));

        /// <summary>
        /// Returns the squared euclidean length of this vector.
        /// </summary>
        public float LengthSqr => (x*x + y*y);

        /// <summary>
        /// Returns the sum of all components.
        /// </summary>
        public GlmHalf Sum => (x + y);

        /// <summary>
        /// Returns the euclidean norm of this vector.
        /// </summary>
        public float Norm => (float)Math.Sqrt((x*x + y*y));

        /// <summary>
        /// Returns the one-norm of this vector.
        /// </summary>
        public float Norm1 => (GlmHalf.Abs(x) + GlmHalf.Abs(y));

        /// <summary>
        /// Returns the two-norm (euclidean length) of this vector.
        /// </summary>
        public float Norm2 => (float)Math.Sqrt((x*x + y*y));

        /// <summary>
        /// Returns the max-norm of this vector.
        /// </summary>
        public float NormMax => GlmHalf.Max(GlmHalf.Abs(x), GlmHalf.Abs(y));

        /// <summary>
        /// Returns a copy of this vector with length one (undefined if this has zero length).
        /// </summary>
        public hvec2 Normalized => this / (GlmHalf)Length;

        /// <summary>
        /// Returns a copy of this vector with length one (returns zero if length is zero).
        /// </summary>
        public hvec2 NormalizedSafe => this == Zero ? Zero : this / (GlmHalf)Length;

        /// <summary>
        /// Returns the vector angle (atan2(y, x)) in radians.
        /// </summary>
        public double Angle => Math.Atan2((double)y, (double)x);

        #endregion


        #region Static Properties

        /// <summary>
        /// Predefined all-zero vector
        /// </summary>
        public static hvec2 Zero { get; } = new hvec2(GlmHalf.Zero, GlmHalf.Zero);

        /// <summary>
        /// Predefined all-ones vector
        /// </summary>
        public static hvec2 Ones { get; } = new hvec2(GlmHalf.One, GlmHalf.One);

        /// <summary>
        /// Predefined unit-X vector
        /// </summary>
        public static hvec2 UnitX { get; } = new hvec2(GlmHalf.One, GlmHalf.Zero);

        /// <summary>
        /// Predefined unit-Y vector
        /// </summary>
        public static hvec2 UnitY { get; } = new hvec2(GlmHalf.Zero, GlmHalf.One);

        /// <summary>
        /// Predefined all-MaxValue vector
        /// </summary>
        public static hvec2 MaxValue { get; } = new hvec2(GlmHalf.MaxValue, GlmHalf.MaxValue);

        /// <summary>
        /// Predefined all-MinValue vector
        /// </summary>
        public static hvec2 MinValue { get; } = new hvec2(GlmHalf.MinValue, GlmHalf.MinValue);

        /// <summary>
        /// Predefined all-Epsilon vector
        /// </summary>
        public static hvec2 Epsilon { get; } = new hvec2(GlmHalf.Epsilon, GlmHalf.Epsilon);

        /// <summary>
        /// Predefined all-NaN vector
        /// </summary>
        public static hvec2 NaN { get; } = new hvec2(GlmHalf.NaN, GlmHalf.NaN);

        /// <summary>
        /// Predefined all-NegativeInfinity vector
        /// </summary>
        public static hvec2 NegativeInfinity { get; } = new hvec2(GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity);

        /// <summary>
        /// Predefined all-PositiveInfinity vector
        /// </summary>
        public static hvec2 PositiveInfinity { get; } = new hvec2(GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity);

        #endregion


        #region Operators

        /// <summary>
        /// Returns true iff this equals rhs component-wise.
        /// </summary>
        public static bool operator==(hvec2 lhs, hvec2 rhs) => lhs.Equals(rhs);

        /// <summary>
        /// Returns true iff this does not equal rhs (component-wise).
        /// </summary>
        public static bool operator!=(hvec2 lhs, hvec2 rhs) => !lhs.Equals(rhs);

        #endregion


        #region Functions

        /// <summary>
        /// Returns an enumerator that iterates through all components.
        /// </summary>
        public IEnumerator<GlmHalf> GetEnumerator()
        {
            yield return x;
            yield return y;
        }

        /// <summary>
        /// Returns an enumerator that iterates through all components.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns a string representation of this vector using ', ' as a seperator.
        /// </summary>
        public override string ToString() => ToString(", ");

        /// <summary>
        /// Returns a string representation of this vector using a provided seperator.
        /// </summary>
        public string ToString(string sep) => (x + sep + y);

        /// <summary>
        /// Returns a string representation of this vector using a provided seperator and a format provider for each component.
        /// </summary>
        public string ToString(string sep, IFormatProvider provider) => (x.ToString(provider) + sep + y.ToString(provider));

        /// <summary>
        /// Returns a string representation of this vector using a provided seperator and a format for each component.
        /// </summary>
        public string ToString(string sep, string format) => (x.ToString(format) + sep + y.ToString(format));

        /// <summary>
        /// Returns a string representation of this vector using a provided seperator and a format and format provider for each component.
        /// </summary>
        public string ToString(string sep, string format, IFormatProvider provider) => (x.ToString(format, provider) + sep + y.ToString(format, provider));

        /// <summary>
        /// Returns true iff this equals rhs component-wise.
        /// </summary>
        public bool Equals(hvec2 rhs) => (x.Equals(rhs.x) && y.Equals(rhs.y));

        /// <summary>
        /// Returns true iff this equals rhs type- and component-wise.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is hvec2 && Equals((hvec2) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((x.GetHashCode()) * 397) ^ y.GetHashCode();
            }
        }

        /// <summary>
        /// Returns the p-norm of this vector.
        /// </summary>
        public double NormP(double p) => Math.Pow((Math.Pow((double)GlmHalf.Abs(x), p) + Math.Pow((double)GlmHalf.Abs(y), p)), 1 / p);

        /// <summary>
        /// Returns a 2D vector that was rotated by a given angle in radians (CAUTION: result is casted and may be truncated).
        /// </summary>
        public hvec2 Rotated(double angleInRad) => (hvec2)(dvec2.FromAngle(Angle + angleInRad) * (double)Length);

        #endregion


        #region Static Functions

        /// <summary>
        /// Converts the string representation of the vector into a vector representation (using ', ' as a separator).
        /// </summary>
        public static hvec2 Parse(string s) => Parse(s, ", ");

        /// <summary>
        /// Converts the string representation of the vector into a vector representation (using a designated separator).
        /// </summary>
        public static hvec2 Parse(string s, string sep)
        {
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 2) throw new FormatException("input has not exactly 2 parts");
            return new hvec2(GlmHalf.Parse(kvp[0].Trim()), GlmHalf.Parse(kvp[1].Trim()));
        }

        /// <summary>
        /// Converts the string representation of the vector into a vector representation (using a designated separator and a type provider).
        /// </summary>
        public static hvec2 Parse(string s, string sep, IFormatProvider provider)
        {
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 2) throw new FormatException("input has not exactly 2 parts");
            return new hvec2(GlmHalf.Parse(kvp[0].Trim(), provider), GlmHalf.Parse(kvp[1].Trim(), provider));
        }

        /// <summary>
        /// Converts the string representation of the vector into a vector representation (using a designated separator and a number style).
        /// </summary>
        public static hvec2 Parse(string s, string sep, NumberStyles style)
        {
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 2) throw new FormatException("input has not exactly 2 parts");
            return new hvec2(GlmHalf.Parse(kvp[0].Trim(), style), GlmHalf.Parse(kvp[1].Trim(), style));
        }

        /// <summary>
        /// Converts the string representation of the vector into a vector representation (using a designated separator and a number style and a format provider).
        /// </summary>
        public static hvec2 Parse(string s, string sep, NumberStyles style, IFormatProvider provider)
        {
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 2) throw new FormatException("input has not exactly 2 parts");
            return new hvec2(GlmHalf.Parse(kvp[0].Trim(), style, provider), GlmHalf.Parse(kvp[1].Trim(), style, provider));
        }

        /// <summary>
        /// Tries to convert the string representation of the vector into a vector representation (using ', ' as a separator), returns false if string was invalid.
        /// </summary>
        public static bool TryParse(string s, out hvec2 result) => TryParse(s, ", ", out result);

        /// <summary>
        /// Tries to convert the string representation of the vector into a vector representation (using a designated separator), returns false if string was invalid.
        /// </summary>
        public static bool TryParse(string s, string sep, out hvec2 result)
        {
            result = Zero;
            if (string.IsNullOrEmpty(s)) return false;
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 2) return false;
            GlmHalf x = GlmHalf.Zero, y = GlmHalf.Zero;
            var ok = (GlmHalf.TryParse(kvp[0].Trim(), out x) && GlmHalf.TryParse(kvp[1].Trim(), out y));
            result = ok ? new hvec2(x, y) : Zero;
            return ok;
        }

        /// <summary>
        /// Tries to convert the string representation of the vector into a vector representation (using a designated separator and a number style and a format provider), returns false if string was invalid.
        /// </summary>
        public static bool TryParse(string s, string sep, NumberStyles style, IFormatProvider provider, out hvec2 result)
        {
            result = Zero;
            if (string.IsNullOrEmpty(s)) return false;
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 2) return false;
            GlmHalf x = GlmHalf.Zero, y = GlmHalf.Zero;
            var ok = (GlmHalf.TryParse(kvp[0].Trim(), style, provider, out x) && GlmHalf.TryParse(kvp[1].Trim(), style, provider, out y));
            result = ok ? new hvec2(x, y) : Zero;
            return ok;
        }

        /// <summary>
        /// Returns true iff distance between lhs and rhs is less than or equal to epsilon
        /// </summary>
        public static bool ApproxEqual(hvec2 lhs, hvec2 rhs, float eps = 0.1f) => Distance(lhs, rhs) <= eps;

        /// <summary>
        /// OuterProduct treats the first parameter c as a column vector (matrix with one column) and the second parameter r as a row vector (matrix with one row) and does a linear algebraic matrix multiply c * r, yielding a matrix whose number of rows is the number of components in c and whose number of columns is the number of components in r.
        /// </summary>
        public static hmat2 OuterProduct(hvec2 c, hvec2 r) => new hmat2(c.x * r.x, c.y * r.x, c.x * r.y, c.y * r.y);

        /// <summary>
        /// OuterProduct treats the first parameter c as a column vector (matrix with one column) and the second parameter r as a row vector (matrix with one row) and does a linear algebraic matrix multiply c * r, yielding a matrix whose number of rows is the number of components in c and whose number of columns is the number of components in r.
        /// </summary>
        public static hmat2x3 OuterProduct(hvec3 c, hvec2 r) => new hmat2x3(c.x * r.x, c.y * r.x, c.z * r.x, c.x * r.y, c.y * r.y, c.z * r.y);

        /// <summary>
        /// OuterProduct treats the first parameter c as a column vector (matrix with one column) and the second parameter r as a row vector (matrix with one row) and does a linear algebraic matrix multiply c * r, yielding a matrix whose number of rows is the number of components in c and whose number of columns is the number of components in r.
        /// </summary>
        public static hmat3x2 OuterProduct(hvec2 c, hvec3 r) => new hmat3x2(c.x * r.x, c.y * r.x, c.x * r.y, c.y * r.y, c.x * r.z, c.y * r.z);

        /// <summary>
        /// OuterProduct treats the first parameter c as a column vector (matrix with one column) and the second parameter r as a row vector (matrix with one row) and does a linear algebraic matrix multiply c * r, yielding a matrix whose number of rows is the number of components in c and whose number of columns is the number of components in r.
        /// </summary>
        public static hmat2x4 OuterProduct(hvec4 c, hvec2 r) => new hmat2x4(c.x * r.x, c.y * r.x, c.z * r.x, c.w * r.x, c.x * r.y, c.y * r.y, c.z * r.y, c.w * r.y);

        /// <summary>
        /// OuterProduct treats the first parameter c as a column vector (matrix with one column) and the second parameter r as a row vector (matrix with one row) and does a linear algebraic matrix multiply c * r, yielding a matrix whose number of rows is the number of components in c and whose number of columns is the number of components in r.
        /// </summary>
        public static hmat4x2 OuterProduct(hvec2 c, hvec4 r) => new hmat4x2(c.x * r.x, c.y * r.x, c.x * r.y, c.y * r.y, c.x * r.z, c.y * r.z, c.x * r.w, c.y * r.w);

        /// <summary>
        /// Returns a unit 2D vector with a given angle in radians (CAUTION: result may be truncated for integer types).
        /// </summary>
        public static hvec2 FromAngle(double angleInRad) => new hvec2((GlmHalf)Math.Cos(angleInRad), (GlmHalf)Math.Sin(angleInRad));

        /// <summary>
        /// Returns the inner product (dot product, scalar product) of the two vectors.
        /// </summary>
        public static GlmHalf Dot(hvec2 lhs, hvec2 rhs) => (lhs.x * rhs.x + lhs.y * rhs.y);

        /// <summary>
        /// Returns the euclidean distance between the two vectors.
        /// </summary>
        public static float Distance(hvec2 lhs, hvec2 rhs) => (lhs - rhs).Length;

        /// <summary>
        /// Returns the squared euclidean distance between the two vectors.
        /// </summary>
        public static float DistanceSqr(hvec2 lhs, hvec2 rhs) => (lhs - rhs).LengthSqr;

        /// <summary>
        /// Calculate the reflection direction for an incident vector (N should be normalized in order to achieve the desired result).
        /// </summary>
        public static hvec2 Reflect(hvec2 I, hvec2 N) => I - 2 * Dot(N, I) * N;

        /// <summary>
        /// Calculate the refraction direction for an incident vector (The input parameters I and N should be normalized in order to achieve the desired result).
        /// </summary>
        public static hvec2 Refract(hvec2 I, hvec2 N, GlmHalf eta)
        {
            var dNI = Dot(N, I);
            var k = 1 - eta * eta * (1 - dNI * dNI);
            if (k < 0) return Zero;
            return eta * I - (eta * dNI + (GlmHalf)Math.Sqrt(k)) * N;
        }

        /// <summary>
        /// Returns a vector pointing in the same direction as another (faceforward orients a vector to point away from a surface as defined by its normal. If dot(Nref, I) is negative faceforward returns N, otherwise it returns -N).
        /// </summary>
        public static hvec2 FaceForward(hvec2 N, hvec2 I, hvec2 Nref) => Dot(Nref, I) < 0 ? N : -N;

        /// <summary>
        /// Returns the length of the outer product (cross product, vector product) of the two vectors.
        /// </summary>
        public static GlmHalf Cross(hvec2 l, hvec2 r) => l.x * r.y - l.y * r.x;

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed uniform values between 0.0 and 1.0.
        /// </summary>
        public static hvec2 Random(Random random) => new hvec2((GlmHalf)random.NextDouble(), (GlmHalf)random.NextDouble());

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed uniform values between -1.0 and 1.0.
        /// </summary>
        public static hvec2 RandomSigned(Random random) => new hvec2((GlmHalf)(random.NextDouble() * 2.0 - 1.0), (GlmHalf)(random.NextDouble() * 2.0 - 1.0));

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed values according to a normal distribution (zero mean, unit variance).
        /// </summary>
        public static hvec2 RandomNormal(Random random) => new hvec2((GlmHalf)(Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))), (GlmHalf)(Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))));

        #endregion


        #region Component-Wise Static Functions

        /// <summary>
        /// Returns a bvec2 from component-wise application of Equal (lhs == rhs).
        /// </summary>
        public static bvec2 Equal(hvec2 lhs, hvec2 rhs) => new bvec2(lhs.x == rhs.x, lhs.y == rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of Equal (lhs == rhs).
        /// </summary>
        public static bvec2 Equal(hvec2 lhs, GlmHalf rhs) => new bvec2(lhs.x == rhs, lhs.y == rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of Equal (lhs == rhs).
        /// </summary>
        public static bvec2 Equal(GlmHalf lhs, hvec2 rhs) => new bvec2(lhs == rhs.x, lhs == rhs.y);

        /// <summary>
        /// Returns a bvec from the application of Equal (lhs == rhs).
        /// </summary>
        public static bvec2 Equal(GlmHalf lhs, GlmHalf rhs) => new bvec2(lhs == rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of NotEqual (lhs != rhs).
        /// </summary>
        public static bvec2 NotEqual(hvec2 lhs, hvec2 rhs) => new bvec2(lhs.x != rhs.x, lhs.y != rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of NotEqual (lhs != rhs).
        /// </summary>
        public static bvec2 NotEqual(hvec2 lhs, GlmHalf rhs) => new bvec2(lhs.x != rhs, lhs.y != rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of NotEqual (lhs != rhs).
        /// </summary>
        public static bvec2 NotEqual(GlmHalf lhs, hvec2 rhs) => new bvec2(lhs != rhs.x, lhs != rhs.y);

        /// <summary>
        /// Returns a bvec from the application of NotEqual (lhs != rhs).
        /// </summary>
        public static bvec2 NotEqual(GlmHalf lhs, GlmHalf rhs) => new bvec2(lhs != rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of GreaterThan (lhs &gt; rhs).
        /// </summary>
        public static bvec2 GreaterThan(hvec2 lhs, hvec2 rhs) => new bvec2(lhs.x > rhs.x, lhs.y > rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of GreaterThan (lhs &gt; rhs).
        /// </summary>
        public static bvec2 GreaterThan(hvec2 lhs, GlmHalf rhs) => new bvec2(lhs.x > rhs, lhs.y > rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of GreaterThan (lhs &gt; rhs).
        /// </summary>
        public static bvec2 GreaterThan(GlmHalf lhs, hvec2 rhs) => new bvec2(lhs > rhs.x, lhs > rhs.y);

        /// <summary>
        /// Returns a bvec from the application of GreaterThan (lhs &gt; rhs).
        /// </summary>
        public static bvec2 GreaterThan(GlmHalf lhs, GlmHalf rhs) => new bvec2(lhs > rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of GreaterThanEqual (lhs &gt;= rhs).
        /// </summary>
        public static bvec2 GreaterThanEqual(hvec2 lhs, hvec2 rhs) => new bvec2(lhs.x >= rhs.x, lhs.y >= rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of GreaterThanEqual (lhs &gt;= rhs).
        /// </summary>
        public static bvec2 GreaterThanEqual(hvec2 lhs, GlmHalf rhs) => new bvec2(lhs.x >= rhs, lhs.y >= rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of GreaterThanEqual (lhs &gt;= rhs).
        /// </summary>
        public static bvec2 GreaterThanEqual(GlmHalf lhs, hvec2 rhs) => new bvec2(lhs >= rhs.x, lhs >= rhs.y);

        /// <summary>
        /// Returns a bvec from the application of GreaterThanEqual (lhs &gt;= rhs).
        /// </summary>
        public static bvec2 GreaterThanEqual(GlmHalf lhs, GlmHalf rhs) => new bvec2(lhs >= rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of LesserThan (lhs &lt; rhs).
        /// </summary>
        public static bvec2 LesserThan(hvec2 lhs, hvec2 rhs) => new bvec2(lhs.x < rhs.x, lhs.y < rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of LesserThan (lhs &lt; rhs).
        /// </summary>
        public static bvec2 LesserThan(hvec2 lhs, GlmHalf rhs) => new bvec2(lhs.x < rhs, lhs.y < rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of LesserThan (lhs &lt; rhs).
        /// </summary>
        public static bvec2 LesserThan(GlmHalf lhs, hvec2 rhs) => new bvec2(lhs < rhs.x, lhs < rhs.y);

        /// <summary>
        /// Returns a bvec from the application of LesserThan (lhs &lt; rhs).
        /// </summary>
        public static bvec2 LesserThan(GlmHalf lhs, GlmHalf rhs) => new bvec2(lhs < rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of LesserThanEqual (lhs &lt;= rhs).
        /// </summary>
        public static bvec2 LesserThanEqual(hvec2 lhs, hvec2 rhs) => new bvec2(lhs.x <= rhs.x, lhs.y <= rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of LesserThanEqual (lhs &lt;= rhs).
        /// </summary>
        public static bvec2 LesserThanEqual(hvec2 lhs, GlmHalf rhs) => new bvec2(lhs.x <= rhs, lhs.y <= rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of LesserThanEqual (lhs &lt;= rhs).
        /// </summary>
        public static bvec2 LesserThanEqual(GlmHalf lhs, hvec2 rhs) => new bvec2(lhs <= rhs.x, lhs <= rhs.y);

        /// <summary>
        /// Returns a bvec from the application of LesserThanEqual (lhs &lt;= rhs).
        /// </summary>
        public static bvec2 LesserThanEqual(GlmHalf lhs, GlmHalf rhs) => new bvec2(lhs <= rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of IsInfinity (GlmHalf.IsInfinity(v)).
        /// </summary>
        public static bvec2 IsInfinity(hvec2 v) => new bvec2(GlmHalf.IsInfinity(v.x), GlmHalf.IsInfinity(v.y));

        /// <summary>
        /// Returns a bvec from the application of IsInfinity (GlmHalf.IsInfinity(v)).
        /// </summary>
        public static bvec2 IsInfinity(GlmHalf v) => new bvec2(GlmHalf.IsInfinity(v));

        /// <summary>
        /// Returns a bvec2 from component-wise application of IsFinite (!GlmHalf.IsNaN(v) &amp;&amp; !GlmHalf.IsInfinity(v)).
        /// </summary>
        public static bvec2 IsFinite(hvec2 v) => new bvec2(!GlmHalf.IsNaN(v.x) && !GlmHalf.IsInfinity(v.x), !GlmHalf.IsNaN(v.y) && !GlmHalf.IsInfinity(v.y));

        /// <summary>
        /// Returns a bvec from the application of IsFinite (!GlmHalf.IsNaN(v) &amp;&amp; !GlmHalf.IsInfinity(v)).
        /// </summary>
        public static bvec2 IsFinite(GlmHalf v) => new bvec2(!GlmHalf.IsNaN(v) && !GlmHalf.IsInfinity(v));

        /// <summary>
        /// Returns a bvec2 from component-wise application of IsNaN (GlmHalf.IsNaN(v)).
        /// </summary>
        public static bvec2 IsNaN(hvec2 v) => new bvec2(GlmHalf.IsNaN(v.x), GlmHalf.IsNaN(v.y));

        /// <summary>
        /// Returns a bvec from the application of IsNaN (GlmHalf.IsNaN(v)).
        /// </summary>
        public static bvec2 IsNaN(GlmHalf v) => new bvec2(GlmHalf.IsNaN(v));

        /// <summary>
        /// Returns a bvec2 from component-wise application of IsNegativeInfinity (GlmHalf.IsNegativeInfinity(v)).
        /// </summary>
        public static bvec2 IsNegativeInfinity(hvec2 v) => new bvec2(GlmHalf.IsNegativeInfinity(v.x), GlmHalf.IsNegativeInfinity(v.y));

        /// <summary>
        /// Returns a bvec from the application of IsNegativeInfinity (GlmHalf.IsNegativeInfinity(v)).
        /// </summary>
        public static bvec2 IsNegativeInfinity(GlmHalf v) => new bvec2(GlmHalf.IsNegativeInfinity(v));

        /// <summary>
        /// Returns a bvec2 from component-wise application of IsPositiveInfinity (GlmHalf.IsPositiveInfinity(v)).
        /// </summary>
        public static bvec2 IsPositiveInfinity(hvec2 v) => new bvec2(GlmHalf.IsPositiveInfinity(v.x), GlmHalf.IsPositiveInfinity(v.y));

        /// <summary>
        /// Returns a bvec from the application of IsPositiveInfinity (GlmHalf.IsPositiveInfinity(v)).
        /// </summary>
        public static bvec2 IsPositiveInfinity(GlmHalf v) => new bvec2(GlmHalf.IsPositiveInfinity(v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Abs (GlmHalf.Abs(v)).
        /// </summary>
        public static hvec2 Abs(hvec2 v) => new hvec2(GlmHalf.Abs(v.x), GlmHalf.Abs(v.y));

        /// <summary>
        /// Returns a hvec from the application of Abs (GlmHalf.Abs(v)).
        /// </summary>
        public static hvec2 Abs(GlmHalf v) => new hvec2(GlmHalf.Abs(v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of HermiteInterpolationOrder3 ((3 - 2 * v) * v * v).
        /// </summary>
        public static hvec2 HermiteInterpolationOrder3(hvec2 v) => new hvec2((3 - 2 * v.x) * v.x * v.x, (3 - 2 * v.y) * v.y * v.y);

        /// <summary>
        /// Returns a hvec from the application of HermiteInterpolationOrder3 ((3 - 2 * v) * v * v).
        /// </summary>
        public static hvec2 HermiteInterpolationOrder3(GlmHalf v) => new hvec2((3 - 2 * v) * v * v);

        /// <summary>
        /// Returns a hvec2 from component-wise application of HermiteInterpolationOrder5 (((6 * v - 15) * v + 10) * v * v * v).
        /// </summary>
        public static hvec2 HermiteInterpolationOrder5(hvec2 v) => new hvec2(((6 * v.x - 15) * v.x + 10) * v.x * v.x * v.x, ((6 * v.y - 15) * v.y + 10) * v.y * v.y * v.y);

        /// <summary>
        /// Returns a hvec from the application of HermiteInterpolationOrder5 (((6 * v - 15) * v + 10) * v * v * v).
        /// </summary>
        public static hvec2 HermiteInterpolationOrder5(GlmHalf v) => new hvec2(((6 * v - 15) * v + 10) * v * v * v);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Sqr (v * v).
        /// </summary>
        public static hvec2 Sqr(hvec2 v) => new hvec2(v.x * v.x, v.y * v.y);

        /// <summary>
        /// Returns a hvec from the application of Sqr (v * v).
        /// </summary>
        public static hvec2 Sqr(GlmHalf v) => new hvec2(v * v);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Pow2 (v * v).
        /// </summary>
        public static hvec2 Pow2(hvec2 v) => new hvec2(v.x * v.x, v.y * v.y);

        /// <summary>
        /// Returns a hvec from the application of Pow2 (v * v).
        /// </summary>
        public static hvec2 Pow2(GlmHalf v) => new hvec2(v * v);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Pow3 (v * v * v).
        /// </summary>
        public static hvec2 Pow3(hvec2 v) => new hvec2(v.x * v.x * v.x, v.y * v.y * v.y);

        /// <summary>
        /// Returns a hvec from the application of Pow3 (v * v * v).
        /// </summary>
        public static hvec2 Pow3(GlmHalf v) => new hvec2(v * v * v);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Step (v &gt;= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero).
        /// </summary>
        public static hvec2 Step(hvec2 v) => new hvec2(v.x >= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero, v.y >= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero);

        /// <summary>
        /// Returns a hvec from the application of Step (v &gt;= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero).
        /// </summary>
        public static hvec2 Step(GlmHalf v) => new hvec2(v >= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Sqrt ((GlmHalf)Math.Sqrt((double)v)).
        /// </summary>
        public static hvec2 Sqrt(hvec2 v) => new hvec2((GlmHalf)Math.Sqrt((double)v.x), (GlmHalf)Math.Sqrt((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Sqrt ((GlmHalf)Math.Sqrt((double)v)).
        /// </summary>
        public static hvec2 Sqrt(GlmHalf v) => new hvec2((GlmHalf)Math.Sqrt((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of InverseSqrt ((GlmHalf)(1.0 / Math.Sqrt((double)v))).
        /// </summary>
        public static hvec2 InverseSqrt(hvec2 v) => new hvec2((GlmHalf)(1.0 / Math.Sqrt((double)v.x)), (GlmHalf)(1.0 / Math.Sqrt((double)v.y)));

        /// <summary>
        /// Returns a hvec from the application of InverseSqrt ((GlmHalf)(1.0 / Math.Sqrt((double)v))).
        /// </summary>
        public static hvec2 InverseSqrt(GlmHalf v) => new hvec2((GlmHalf)(1.0 / Math.Sqrt((double)v)));

        /// <summary>
        /// Returns a ivec2 from component-wise application of Sign (Math.Sign(v)).
        /// </summary>
        public static ivec2 Sign(hvec2 v) => new ivec2(Math.Sign(v.x), Math.Sign(v.y));

        /// <summary>
        /// Returns a ivec from the application of Sign (Math.Sign(v)).
        /// </summary>
        public static ivec2 Sign(GlmHalf v) => new ivec2(Math.Sign(v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Max (GlmHalf.Max(lhs, rhs)).
        /// </summary>
        public static hvec2 Max(hvec2 lhs, hvec2 rhs) => new hvec2(GlmHalf.Max(lhs.x, rhs.x), GlmHalf.Max(lhs.y, rhs.y));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Max (GlmHalf.Max(lhs, rhs)).
        /// </summary>
        public static hvec2 Max(hvec2 lhs, GlmHalf rhs) => new hvec2(GlmHalf.Max(lhs.x, rhs), GlmHalf.Max(lhs.y, rhs));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Max (GlmHalf.Max(lhs, rhs)).
        /// </summary>
        public static hvec2 Max(GlmHalf lhs, hvec2 rhs) => new hvec2(GlmHalf.Max(lhs, rhs.x), GlmHalf.Max(lhs, rhs.y));

        /// <summary>
        /// Returns a hvec from the application of Max (GlmHalf.Max(lhs, rhs)).
        /// </summary>
        public static hvec2 Max(GlmHalf lhs, GlmHalf rhs) => new hvec2(GlmHalf.Max(lhs, rhs));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Min (GlmHalf.Min(lhs, rhs)).
        /// </summary>
        public static hvec2 Min(hvec2 lhs, hvec2 rhs) => new hvec2(GlmHalf.Min(lhs.x, rhs.x), GlmHalf.Min(lhs.y, rhs.y));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Min (GlmHalf.Min(lhs, rhs)).
        /// </summary>
        public static hvec2 Min(hvec2 lhs, GlmHalf rhs) => new hvec2(GlmHalf.Min(lhs.x, rhs), GlmHalf.Min(lhs.y, rhs));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Min (GlmHalf.Min(lhs, rhs)).
        /// </summary>
        public static hvec2 Min(GlmHalf lhs, hvec2 rhs) => new hvec2(GlmHalf.Min(lhs, rhs.x), GlmHalf.Min(lhs, rhs.y));

        /// <summary>
        /// Returns a hvec from the application of Min (GlmHalf.Min(lhs, rhs)).
        /// </summary>
        public static hvec2 Min(GlmHalf lhs, GlmHalf rhs) => new hvec2(GlmHalf.Min(lhs, rhs));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Pow ((GlmHalf)Math.Pow((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec2 Pow(hvec2 lhs, hvec2 rhs) => new hvec2((GlmHalf)Math.Pow((double)lhs.x, (double)rhs.x), (GlmHalf)Math.Pow((double)lhs.y, (double)rhs.y));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Pow ((GlmHalf)Math.Pow((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec2 Pow(hvec2 lhs, GlmHalf rhs) => new hvec2((GlmHalf)Math.Pow((double)lhs.x, (double)rhs), (GlmHalf)Math.Pow((double)lhs.y, (double)rhs));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Pow ((GlmHalf)Math.Pow((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec2 Pow(GlmHalf lhs, hvec2 rhs) => new hvec2((GlmHalf)Math.Pow((double)lhs, (double)rhs.x), (GlmHalf)Math.Pow((double)lhs, (double)rhs.y));

        /// <summary>
        /// Returns a hvec from the application of Pow ((GlmHalf)Math.Pow((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec2 Pow(GlmHalf lhs, GlmHalf rhs) => new hvec2((GlmHalf)Math.Pow((double)lhs, (double)rhs));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Log ((GlmHalf)Math.Log((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec2 Log(hvec2 lhs, hvec2 rhs) => new hvec2((GlmHalf)Math.Log((double)lhs.x, (double)rhs.x), (GlmHalf)Math.Log((double)lhs.y, (double)rhs.y));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Log ((GlmHalf)Math.Log((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec2 Log(hvec2 lhs, GlmHalf rhs) => new hvec2((GlmHalf)Math.Log((double)lhs.x, (double)rhs), (GlmHalf)Math.Log((double)lhs.y, (double)rhs));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Log ((GlmHalf)Math.Log((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec2 Log(GlmHalf lhs, hvec2 rhs) => new hvec2((GlmHalf)Math.Log((double)lhs, (double)rhs.x), (GlmHalf)Math.Log((double)lhs, (double)rhs.y));

        /// <summary>
        /// Returns a hvec from the application of Log ((GlmHalf)Math.Log((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec2 Log(GlmHalf lhs, GlmHalf rhs) => new hvec2((GlmHalf)Math.Log((double)lhs, (double)rhs));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec2 Clamp(hvec2 v, hvec2 min, hvec2 max) => new hvec2(GlmHalf.Min(GlmHalf.Max(v.x, min.x), max.x), GlmHalf.Min(GlmHalf.Max(v.y, min.y), max.y));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec2 Clamp(hvec2 v, hvec2 min, GlmHalf max) => new hvec2(GlmHalf.Min(GlmHalf.Max(v.x, min.x), max), GlmHalf.Min(GlmHalf.Max(v.y, min.y), max));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec2 Clamp(hvec2 v, GlmHalf min, hvec2 max) => new hvec2(GlmHalf.Min(GlmHalf.Max(v.x, min), max.x), GlmHalf.Min(GlmHalf.Max(v.y, min), max.y));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec2 Clamp(hvec2 v, GlmHalf min, GlmHalf max) => new hvec2(GlmHalf.Min(GlmHalf.Max(v.x, min), max), GlmHalf.Min(GlmHalf.Max(v.y, min), max));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec2 Clamp(GlmHalf v, hvec2 min, hvec2 max) => new hvec2(GlmHalf.Min(GlmHalf.Max(v, min.x), max.x), GlmHalf.Min(GlmHalf.Max(v, min.y), max.y));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec2 Clamp(GlmHalf v, hvec2 min, GlmHalf max) => new hvec2(GlmHalf.Min(GlmHalf.Max(v, min.x), max), GlmHalf.Min(GlmHalf.Max(v, min.y), max));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec2 Clamp(GlmHalf v, GlmHalf min, hvec2 max) => new hvec2(GlmHalf.Min(GlmHalf.Max(v, min), max.x), GlmHalf.Min(GlmHalf.Max(v, min), max.y));

        /// <summary>
        /// Returns a hvec from the application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec2 Clamp(GlmHalf v, GlmHalf min, GlmHalf max) => new hvec2(GlmHalf.Min(GlmHalf.Max(v, min), max));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Mix(hvec2 min, hvec2 max, hvec2 a) => new hvec2(min.x * (1-a.x) + max.x * a.x, min.y * (1-a.y) + max.y * a.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Mix(hvec2 min, hvec2 max, GlmHalf a) => new hvec2(min.x * (1-a) + max.x * a, min.y * (1-a) + max.y * a);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Mix(hvec2 min, GlmHalf max, hvec2 a) => new hvec2(min.x * (1-a.x) + max * a.x, min.y * (1-a.y) + max * a.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Mix(hvec2 min, GlmHalf max, GlmHalf a) => new hvec2(min.x * (1-a) + max * a, min.y * (1-a) + max * a);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Mix(GlmHalf min, hvec2 max, hvec2 a) => new hvec2(min * (1-a.x) + max.x * a.x, min * (1-a.y) + max.y * a.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Mix(GlmHalf min, hvec2 max, GlmHalf a) => new hvec2(min * (1-a) + max.x * a, min * (1-a) + max.y * a);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Mix(GlmHalf min, GlmHalf max, hvec2 a) => new hvec2(min * (1-a.x) + max * a.x, min * (1-a.y) + max * a.y);

        /// <summary>
        /// Returns a hvec from the application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Mix(GlmHalf min, GlmHalf max, GlmHalf a) => new hvec2(min * (1-a) + max * a);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Lerp(hvec2 min, hvec2 max, hvec2 a) => new hvec2(min.x * (1-a.x) + max.x * a.x, min.y * (1-a.y) + max.y * a.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Lerp(hvec2 min, hvec2 max, GlmHalf a) => new hvec2(min.x * (1-a) + max.x * a, min.y * (1-a) + max.y * a);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Lerp(hvec2 min, GlmHalf max, hvec2 a) => new hvec2(min.x * (1-a.x) + max * a.x, min.y * (1-a.y) + max * a.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Lerp(hvec2 min, GlmHalf max, GlmHalf a) => new hvec2(min.x * (1-a) + max * a, min.y * (1-a) + max * a);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Lerp(GlmHalf min, hvec2 max, hvec2 a) => new hvec2(min * (1-a.x) + max.x * a.x, min * (1-a.y) + max.y * a.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Lerp(GlmHalf min, hvec2 max, GlmHalf a) => new hvec2(min * (1-a) + max.x * a, min * (1-a) + max.y * a);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Lerp(GlmHalf min, GlmHalf max, hvec2 a) => new hvec2(min * (1-a.x) + max * a.x, min * (1-a.y) + max * a.y);

        /// <summary>
        /// Returns a hvec from the application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec2 Lerp(GlmHalf min, GlmHalf max, GlmHalf a) => new hvec2(min * (1-a) + max * a);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec2 Smoothstep(hvec2 edge0, hvec2 edge1, hvec2 v) => new hvec2(((v.x - edge0.x) / (edge1.x - edge0.x)).Clamp().HermiteInterpolationOrder3(), ((v.y - edge0.y) / (edge1.y - edge0.y)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec2 Smoothstep(hvec2 edge0, hvec2 edge1, GlmHalf v) => new hvec2(((v - edge0.x) / (edge1.x - edge0.x)).Clamp().HermiteInterpolationOrder3(), ((v - edge0.y) / (edge1.y - edge0.y)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec2 Smoothstep(hvec2 edge0, GlmHalf edge1, hvec2 v) => new hvec2(((v.x - edge0.x) / (edge1 - edge0.x)).Clamp().HermiteInterpolationOrder3(), ((v.y - edge0.y) / (edge1 - edge0.y)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec2 Smoothstep(hvec2 edge0, GlmHalf edge1, GlmHalf v) => new hvec2(((v - edge0.x) / (edge1 - edge0.x)).Clamp().HermiteInterpolationOrder3(), ((v - edge0.y) / (edge1 - edge0.y)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec2 Smoothstep(GlmHalf edge0, hvec2 edge1, hvec2 v) => new hvec2(((v.x - edge0) / (edge1.x - edge0)).Clamp().HermiteInterpolationOrder3(), ((v.y - edge0) / (edge1.y - edge0)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec2 Smoothstep(GlmHalf edge0, hvec2 edge1, GlmHalf v) => new hvec2(((v - edge0) / (edge1.x - edge0)).Clamp().HermiteInterpolationOrder3(), ((v - edge0) / (edge1.y - edge0)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec2 Smoothstep(GlmHalf edge0, GlmHalf edge1, hvec2 v) => new hvec2(((v.x - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3(), ((v.y - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec from the application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec2 Smoothstep(GlmHalf edge0, GlmHalf edge1, GlmHalf v) => new hvec2(((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec2 Smootherstep(hvec2 edge0, hvec2 edge1, hvec2 v) => new hvec2(((v.x - edge0.x) / (edge1.x - edge0.x)).Clamp().HermiteInterpolationOrder5(), ((v.y - edge0.y) / (edge1.y - edge0.y)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec2 Smootherstep(hvec2 edge0, hvec2 edge1, GlmHalf v) => new hvec2(((v - edge0.x) / (edge1.x - edge0.x)).Clamp().HermiteInterpolationOrder5(), ((v - edge0.y) / (edge1.y - edge0.y)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec2 Smootherstep(hvec2 edge0, GlmHalf edge1, hvec2 v) => new hvec2(((v.x - edge0.x) / (edge1 - edge0.x)).Clamp().HermiteInterpolationOrder5(), ((v.y - edge0.y) / (edge1 - edge0.y)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec2 Smootherstep(hvec2 edge0, GlmHalf edge1, GlmHalf v) => new hvec2(((v - edge0.x) / (edge1 - edge0.x)).Clamp().HermiteInterpolationOrder5(), ((v - edge0.y) / (edge1 - edge0.y)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec2 Smootherstep(GlmHalf edge0, hvec2 edge1, hvec2 v) => new hvec2(((v.x - edge0) / (edge1.x - edge0)).Clamp().HermiteInterpolationOrder5(), ((v.y - edge0) / (edge1.y - edge0)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec2 Smootherstep(GlmHalf edge0, hvec2 edge1, GlmHalf v) => new hvec2(((v - edge0) / (edge1.x - edge0)).Clamp().HermiteInterpolationOrder5(), ((v - edge0) / (edge1.y - edge0)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec2 Smootherstep(GlmHalf edge0, GlmHalf edge1, hvec2 v) => new hvec2(((v.x - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5(), ((v.y - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec from the application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec2 Smootherstep(GlmHalf edge0, GlmHalf edge1, GlmHalf v) => new hvec2(((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec2 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec2 Fma(hvec2 a, hvec2 b, hvec2 c) => new hvec2(a.x * b.x + c.x, a.y * b.y + c.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec2 Fma(hvec2 a, hvec2 b, GlmHalf c) => new hvec2(a.x * b.x + c, a.y * b.y + c);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec2 Fma(hvec2 a, GlmHalf b, hvec2 c) => new hvec2(a.x * b + c.x, a.y * b + c.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec2 Fma(hvec2 a, GlmHalf b, GlmHalf c) => new hvec2(a.x * b + c, a.y * b + c);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec2 Fma(GlmHalf a, hvec2 b, hvec2 c) => new hvec2(a * b.x + c.x, a * b.y + c.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec2 Fma(GlmHalf a, hvec2 b, GlmHalf c) => new hvec2(a * b.x + c, a * b.y + c);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec2 Fma(GlmHalf a, GlmHalf b, hvec2 c) => new hvec2(a * b + c.x, a * b + c.y);

        /// <summary>
        /// Returns a hvec from the application of Fma (a * b + c).
        /// </summary>
        public static hvec2 Fma(GlmHalf a, GlmHalf b, GlmHalf c) => new hvec2(a * b + c);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Add (lhs + rhs).
        /// </summary>
        public static hvec2 Add(hvec2 lhs, hvec2 rhs) => new hvec2(lhs.x + rhs.x, lhs.y + rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Add (lhs + rhs).
        /// </summary>
        public static hvec2 Add(hvec2 lhs, GlmHalf rhs) => new hvec2(lhs.x + rhs, lhs.y + rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Add (lhs + rhs).
        /// </summary>
        public static hvec2 Add(GlmHalf lhs, hvec2 rhs) => new hvec2(lhs + rhs.x, lhs + rhs.y);

        /// <summary>
        /// Returns a hvec from the application of Add (lhs + rhs).
        /// </summary>
        public static hvec2 Add(GlmHalf lhs, GlmHalf rhs) => new hvec2(lhs + rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Sub (lhs - rhs).
        /// </summary>
        public static hvec2 Sub(hvec2 lhs, hvec2 rhs) => new hvec2(lhs.x - rhs.x, lhs.y - rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Sub (lhs - rhs).
        /// </summary>
        public static hvec2 Sub(hvec2 lhs, GlmHalf rhs) => new hvec2(lhs.x - rhs, lhs.y - rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Sub (lhs - rhs).
        /// </summary>
        public static hvec2 Sub(GlmHalf lhs, hvec2 rhs) => new hvec2(lhs - rhs.x, lhs - rhs.y);

        /// <summary>
        /// Returns a hvec from the application of Sub (lhs - rhs).
        /// </summary>
        public static hvec2 Sub(GlmHalf lhs, GlmHalf rhs) => new hvec2(lhs - rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Mul (lhs * rhs).
        /// </summary>
        public static hvec2 Mul(hvec2 lhs, hvec2 rhs) => new hvec2(lhs.x * rhs.x, lhs.y * rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Mul (lhs * rhs).
        /// </summary>
        public static hvec2 Mul(hvec2 lhs, GlmHalf rhs) => new hvec2(lhs.x * rhs, lhs.y * rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Mul (lhs * rhs).
        /// </summary>
        public static hvec2 Mul(GlmHalf lhs, hvec2 rhs) => new hvec2(lhs * rhs.x, lhs * rhs.y);

        /// <summary>
        /// Returns a hvec from the application of Mul (lhs * rhs).
        /// </summary>
        public static hvec2 Mul(GlmHalf lhs, GlmHalf rhs) => new hvec2(lhs * rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Div (lhs / rhs).
        /// </summary>
        public static hvec2 Div(hvec2 lhs, hvec2 rhs) => new hvec2(lhs.x / rhs.x, lhs.y / rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Div (lhs / rhs).
        /// </summary>
        public static hvec2 Div(hvec2 lhs, GlmHalf rhs) => new hvec2(lhs.x / rhs, lhs.y / rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Div (lhs / rhs).
        /// </summary>
        public static hvec2 Div(GlmHalf lhs, hvec2 rhs) => new hvec2(lhs / rhs.x, lhs / rhs.y);

        /// <summary>
        /// Returns a hvec from the application of Div (lhs / rhs).
        /// </summary>
        public static hvec2 Div(GlmHalf lhs, GlmHalf rhs) => new hvec2(lhs / rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Modulo (lhs % rhs).
        /// </summary>
        public static hvec2 Modulo(hvec2 lhs, hvec2 rhs) => new hvec2(lhs.x % rhs.x, lhs.y % rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Modulo (lhs % rhs).
        /// </summary>
        public static hvec2 Modulo(hvec2 lhs, GlmHalf rhs) => new hvec2(lhs.x % rhs, lhs.y % rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Modulo (lhs % rhs).
        /// </summary>
        public static hvec2 Modulo(GlmHalf lhs, hvec2 rhs) => new hvec2(lhs % rhs.x, lhs % rhs.y);

        /// <summary>
        /// Returns a hvec from the application of Modulo (lhs % rhs).
        /// </summary>
        public static hvec2 Modulo(GlmHalf lhs, GlmHalf rhs) => new hvec2(lhs % rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of Degrees (Radians-To-Degrees Conversion).
        /// </summary>
        public static hvec2 Degrees(hvec2 v) => new hvec2((GlmHalf)(v.x * new GlmHalf(57.295779513082320876798154814105170332405472466564321)), (GlmHalf)(v.y * new GlmHalf(57.295779513082320876798154814105170332405472466564321)));

        /// <summary>
        /// Returns a hvec from the application of Degrees (Radians-To-Degrees Conversion).
        /// </summary>
        public static hvec2 Degrees(GlmHalf v) => new hvec2((GlmHalf)(v * new GlmHalf(57.295779513082320876798154814105170332405472466564321)));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Radians (Degrees-To-Radians Conversion).
        /// </summary>
        public static hvec2 Radians(hvec2 v) => new hvec2((GlmHalf)(v.x * new GlmHalf(0.0174532925199432957692369076848861271344287188854172)), (GlmHalf)(v.y * new GlmHalf(0.0174532925199432957692369076848861271344287188854172)));

        /// <summary>
        /// Returns a hvec from the application of Radians (Degrees-To-Radians Conversion).
        /// </summary>
        public static hvec2 Radians(GlmHalf v) => new hvec2((GlmHalf)(v * new GlmHalf(0.0174532925199432957692369076848861271344287188854172)));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Acos ((GlmHalf)Math.Acos((double)v)).
        /// </summary>
        public static hvec2 Acos(hvec2 v) => new hvec2((GlmHalf)Math.Acos((double)v.x), (GlmHalf)Math.Acos((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Acos ((GlmHalf)Math.Acos((double)v)).
        /// </summary>
        public static hvec2 Acos(GlmHalf v) => new hvec2((GlmHalf)Math.Acos((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Asin ((GlmHalf)Math.Asin((double)v)).
        /// </summary>
        public static hvec2 Asin(hvec2 v) => new hvec2((GlmHalf)Math.Asin((double)v.x), (GlmHalf)Math.Asin((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Asin ((GlmHalf)Math.Asin((double)v)).
        /// </summary>
        public static hvec2 Asin(GlmHalf v) => new hvec2((GlmHalf)Math.Asin((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Atan ((GlmHalf)Math.Atan((double)v)).
        /// </summary>
        public static hvec2 Atan(hvec2 v) => new hvec2((GlmHalf)Math.Atan((double)v.x), (GlmHalf)Math.Atan((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Atan ((GlmHalf)Math.Atan((double)v)).
        /// </summary>
        public static hvec2 Atan(GlmHalf v) => new hvec2((GlmHalf)Math.Atan((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Cos ((GlmHalf)Math.Cos((double)v)).
        /// </summary>
        public static hvec2 Cos(hvec2 v) => new hvec2((GlmHalf)Math.Cos((double)v.x), (GlmHalf)Math.Cos((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Cos ((GlmHalf)Math.Cos((double)v)).
        /// </summary>
        public static hvec2 Cos(GlmHalf v) => new hvec2((GlmHalf)Math.Cos((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Cosh ((GlmHalf)Math.Cosh((double)v)).
        /// </summary>
        public static hvec2 Cosh(hvec2 v) => new hvec2((GlmHalf)Math.Cosh((double)v.x), (GlmHalf)Math.Cosh((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Cosh ((GlmHalf)Math.Cosh((double)v)).
        /// </summary>
        public static hvec2 Cosh(GlmHalf v) => new hvec2((GlmHalf)Math.Cosh((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Exp ((GlmHalf)Math.Exp((double)v)).
        /// </summary>
        public static hvec2 Exp(hvec2 v) => new hvec2((GlmHalf)Math.Exp((double)v.x), (GlmHalf)Math.Exp((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Exp ((GlmHalf)Math.Exp((double)v)).
        /// </summary>
        public static hvec2 Exp(GlmHalf v) => new hvec2((GlmHalf)Math.Exp((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Log ((GlmHalf)Math.Log((double)v)).
        /// </summary>
        public static hvec2 Log(hvec2 v) => new hvec2((GlmHalf)Math.Log((double)v.x), (GlmHalf)Math.Log((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Log ((GlmHalf)Math.Log((double)v)).
        /// </summary>
        public static hvec2 Log(GlmHalf v) => new hvec2((GlmHalf)Math.Log((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Log2 ((GlmHalf)Math.Log((double)v, 2)).
        /// </summary>
        public static hvec2 Log2(hvec2 v) => new hvec2((GlmHalf)Math.Log((double)v.x, 2), (GlmHalf)Math.Log((double)v.y, 2));

        /// <summary>
        /// Returns a hvec from the application of Log2 ((GlmHalf)Math.Log((double)v, 2)).
        /// </summary>
        public static hvec2 Log2(GlmHalf v) => new hvec2((GlmHalf)Math.Log((double)v, 2));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Log10 ((GlmHalf)Math.Log10((double)v)).
        /// </summary>
        public static hvec2 Log10(hvec2 v) => new hvec2((GlmHalf)Math.Log10((double)v.x), (GlmHalf)Math.Log10((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Log10 ((GlmHalf)Math.Log10((double)v)).
        /// </summary>
        public static hvec2 Log10(GlmHalf v) => new hvec2((GlmHalf)Math.Log10((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Floor ((GlmHalf)Math.Floor(v)).
        /// </summary>
        public static hvec2 Floor(hvec2 v) => new hvec2((GlmHalf)Math.Floor(v.x), (GlmHalf)Math.Floor(v.y));

        /// <summary>
        /// Returns a hvec from the application of Floor ((GlmHalf)Math.Floor(v)).
        /// </summary>
        public static hvec2 Floor(GlmHalf v) => new hvec2((GlmHalf)Math.Floor(v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Ceiling ((GlmHalf)Math.Ceiling(v)).
        /// </summary>
        public static hvec2 Ceiling(hvec2 v) => new hvec2((GlmHalf)Math.Ceiling(v.x), (GlmHalf)Math.Ceiling(v.y));

        /// <summary>
        /// Returns a hvec from the application of Ceiling ((GlmHalf)Math.Ceiling(v)).
        /// </summary>
        public static hvec2 Ceiling(GlmHalf v) => new hvec2((GlmHalf)Math.Ceiling(v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Round ((GlmHalf)Math.Round(v)).
        /// </summary>
        public static hvec2 Round(hvec2 v) => new hvec2((GlmHalf)Math.Round(v.x), (GlmHalf)Math.Round(v.y));

        /// <summary>
        /// Returns a hvec from the application of Round ((GlmHalf)Math.Round(v)).
        /// </summary>
        public static hvec2 Round(GlmHalf v) => new hvec2((GlmHalf)Math.Round(v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Sin ((GlmHalf)Math.Sin((double)v)).
        /// </summary>
        public static hvec2 Sin(hvec2 v) => new hvec2((GlmHalf)Math.Sin((double)v.x), (GlmHalf)Math.Sin((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Sin ((GlmHalf)Math.Sin((double)v)).
        /// </summary>
        public static hvec2 Sin(GlmHalf v) => new hvec2((GlmHalf)Math.Sin((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Sinh ((GlmHalf)Math.Sinh((double)v)).
        /// </summary>
        public static hvec2 Sinh(hvec2 v) => new hvec2((GlmHalf)Math.Sinh((double)v.x), (GlmHalf)Math.Sinh((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Sinh ((GlmHalf)Math.Sinh((double)v)).
        /// </summary>
        public static hvec2 Sinh(GlmHalf v) => new hvec2((GlmHalf)Math.Sinh((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Tan ((GlmHalf)Math.Tan((double)v)).
        /// </summary>
        public static hvec2 Tan(hvec2 v) => new hvec2((GlmHalf)Math.Tan((double)v.x), (GlmHalf)Math.Tan((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Tan ((GlmHalf)Math.Tan((double)v)).
        /// </summary>
        public static hvec2 Tan(GlmHalf v) => new hvec2((GlmHalf)Math.Tan((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Tanh ((GlmHalf)Math.Tanh((double)v)).
        /// </summary>
        public static hvec2 Tanh(hvec2 v) => new hvec2((GlmHalf)Math.Tanh((double)v.x), (GlmHalf)Math.Tanh((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Tanh ((GlmHalf)Math.Tanh((double)v)).
        /// </summary>
        public static hvec2 Tanh(GlmHalf v) => new hvec2((GlmHalf)Math.Tanh((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Truncate ((GlmHalf)Math.Truncate((double)v)).
        /// </summary>
        public static hvec2 Truncate(hvec2 v) => new hvec2((GlmHalf)Math.Truncate((double)v.x), (GlmHalf)Math.Truncate((double)v.y));

        /// <summary>
        /// Returns a hvec from the application of Truncate ((GlmHalf)Math.Truncate((double)v)).
        /// </summary>
        public static hvec2 Truncate(GlmHalf v) => new hvec2((GlmHalf)Math.Truncate((double)v));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Fract ((GlmHalf)(v - Math.Floor(v))).
        /// </summary>
        public static hvec2 Fract(hvec2 v) => new hvec2((GlmHalf)(v.x - Math.Floor(v.x)), (GlmHalf)(v.y - Math.Floor(v.y)));

        /// <summary>
        /// Returns a hvec from the application of Fract ((GlmHalf)(v - Math.Floor(v))).
        /// </summary>
        public static hvec2 Fract(GlmHalf v) => new hvec2((GlmHalf)(v - Math.Floor(v)));

        /// <summary>
        /// Returns a hvec2 from component-wise application of Trunc ((long)(v)).
        /// </summary>
        public static hvec2 Trunc(hvec2 v) => new hvec2((long)(v.x), (long)(v.y));

        /// <summary>
        /// Returns a hvec from the application of Trunc ((long)(v)).
        /// </summary>
        public static hvec2 Trunc(GlmHalf v) => new hvec2((long)(v));

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec2 Random(Random random, hvec2 minValue, hvec2 maxValue) => new hvec2((GlmHalf)random.NextDouble() * (maxValue.x - minValue.x) + minValue.x, (GlmHalf)random.NextDouble() * (maxValue.y - minValue.y) + minValue.y);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec2 Random(Random random, hvec2 minValue, GlmHalf maxValue) => new hvec2((GlmHalf)random.NextDouble() * (maxValue - minValue.x) + minValue.x, (GlmHalf)random.NextDouble() * (maxValue - minValue.y) + minValue.y);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec2 Random(Random random, GlmHalf minValue, hvec2 maxValue) => new hvec2((GlmHalf)random.NextDouble() * (maxValue.x - minValue) + minValue, (GlmHalf)random.NextDouble() * (maxValue.y - minValue) + minValue);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec2 Random(Random random, GlmHalf minValue, GlmHalf maxValue) => new hvec2((GlmHalf)random.NextDouble() * (maxValue - minValue) + minValue);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec2 RandomUniform(Random random, hvec2 minValue, hvec2 maxValue) => new hvec2((GlmHalf)random.NextDouble() * (maxValue.x - minValue.x) + minValue.x, (GlmHalf)random.NextDouble() * (maxValue.y - minValue.y) + minValue.y);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec2 RandomUniform(Random random, hvec2 minValue, GlmHalf maxValue) => new hvec2((GlmHalf)random.NextDouble() * (maxValue - minValue.x) + minValue.x, (GlmHalf)random.NextDouble() * (maxValue - minValue.y) + minValue.y);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec2 RandomUniform(Random random, GlmHalf minValue, hvec2 maxValue) => new hvec2((GlmHalf)random.NextDouble() * (maxValue.x - minValue) + minValue, (GlmHalf)random.NextDouble() * (maxValue.y - minValue) + minValue);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec2 RandomUniform(Random random, GlmHalf minValue, GlmHalf maxValue) => new hvec2((GlmHalf)random.NextDouble() * (maxValue - minValue) + minValue);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec2 RandomNormal(Random random, hvec2 mean, hvec2 variance) => new hvec2((GlmHalf)(Math.Sqrt((double)variance.x) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.x, (GlmHalf)(Math.Sqrt((double)variance.y) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.y);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec2 RandomNormal(Random random, hvec2 mean, GlmHalf variance) => new hvec2((GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.x, (GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.y);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec2 RandomNormal(Random random, GlmHalf mean, hvec2 variance) => new hvec2((GlmHalf)(Math.Sqrt((double)variance.x) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean, (GlmHalf)(Math.Sqrt((double)variance.y) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec2 RandomNormal(Random random, GlmHalf mean, GlmHalf variance) => new hvec2((GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec2 RandomGaussian(Random random, hvec2 mean, hvec2 variance) => new hvec2((GlmHalf)(Math.Sqrt((double)variance.x) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.x, (GlmHalf)(Math.Sqrt((double)variance.y) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.y);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec2 RandomGaussian(Random random, hvec2 mean, GlmHalf variance) => new hvec2((GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.x, (GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.y);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec2 RandomGaussian(Random random, GlmHalf mean, hvec2 variance) => new hvec2((GlmHalf)(Math.Sqrt((double)variance.x) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean, (GlmHalf)(Math.Sqrt((double)variance.y) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean);

        /// <summary>
        /// Returns a hvec2 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec2 RandomGaussian(Random random, GlmHalf mean, GlmHalf variance) => new hvec2((GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean);

        #endregion


        #region Component-Wise Operator Overloads

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&lt; (lhs &lt; rhs).
        /// </summary>
        public static bvec2 operator<(hvec2 lhs, hvec2 rhs) => new bvec2(lhs.x < rhs.x, lhs.y < rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&lt; (lhs &lt; rhs).
        /// </summary>
        public static bvec2 operator<(hvec2 lhs, GlmHalf rhs) => new bvec2(lhs.x < rhs, lhs.y < rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&lt; (lhs &lt; rhs).
        /// </summary>
        public static bvec2 operator<(GlmHalf lhs, hvec2 rhs) => new bvec2(lhs < rhs.x, lhs < rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&lt;= (lhs &lt;= rhs).
        /// </summary>
        public static bvec2 operator<=(hvec2 lhs, hvec2 rhs) => new bvec2(lhs.x <= rhs.x, lhs.y <= rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&lt;= (lhs &lt;= rhs).
        /// </summary>
        public static bvec2 operator<=(hvec2 lhs, GlmHalf rhs) => new bvec2(lhs.x <= rhs, lhs.y <= rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&lt;= (lhs &lt;= rhs).
        /// </summary>
        public static bvec2 operator<=(GlmHalf lhs, hvec2 rhs) => new bvec2(lhs <= rhs.x, lhs <= rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&gt; (lhs &gt; rhs).
        /// </summary>
        public static bvec2 operator>(hvec2 lhs, hvec2 rhs) => new bvec2(lhs.x > rhs.x, lhs.y > rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&gt; (lhs &gt; rhs).
        /// </summary>
        public static bvec2 operator>(hvec2 lhs, GlmHalf rhs) => new bvec2(lhs.x > rhs, lhs.y > rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&gt; (lhs &gt; rhs).
        /// </summary>
        public static bvec2 operator>(GlmHalf lhs, hvec2 rhs) => new bvec2(lhs > rhs.x, lhs > rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&gt;= (lhs &gt;= rhs).
        /// </summary>
        public static bvec2 operator>=(hvec2 lhs, hvec2 rhs) => new bvec2(lhs.x >= rhs.x, lhs.y >= rhs.y);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&gt;= (lhs &gt;= rhs).
        /// </summary>
        public static bvec2 operator>=(hvec2 lhs, GlmHalf rhs) => new bvec2(lhs.x >= rhs, lhs.y >= rhs);

        /// <summary>
        /// Returns a bvec2 from component-wise application of operator&gt;= (lhs &gt;= rhs).
        /// </summary>
        public static bvec2 operator>=(GlmHalf lhs, hvec2 rhs) => new bvec2(lhs >= rhs.x, lhs >= rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator+ (lhs + rhs).
        /// </summary>
        public static hvec2 operator+(hvec2 lhs, hvec2 rhs) => new hvec2(lhs.x + rhs.x, lhs.y + rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator+ (lhs + rhs).
        /// </summary>
        public static hvec2 operator+(hvec2 lhs, GlmHalf rhs) => new hvec2(lhs.x + rhs, lhs.y + rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator+ (lhs + rhs).
        /// </summary>
        public static hvec2 operator+(GlmHalf lhs, hvec2 rhs) => new hvec2(lhs + rhs.x, lhs + rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator- (lhs - rhs).
        /// </summary>
        public static hvec2 operator-(hvec2 lhs, hvec2 rhs) => new hvec2(lhs.x - rhs.x, lhs.y - rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator- (lhs - rhs).
        /// </summary>
        public static hvec2 operator-(hvec2 lhs, GlmHalf rhs) => new hvec2(lhs.x - rhs, lhs.y - rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator- (lhs - rhs).
        /// </summary>
        public static hvec2 operator-(GlmHalf lhs, hvec2 rhs) => new hvec2(lhs - rhs.x, lhs - rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator* (lhs * rhs).
        /// </summary>
        public static hvec2 operator*(hvec2 lhs, hvec2 rhs) => new hvec2(lhs.x * rhs.x, lhs.y * rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator* (lhs * rhs).
        /// </summary>
        public static hvec2 operator*(hvec2 lhs, GlmHalf rhs) => new hvec2(lhs.x * rhs, lhs.y * rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator* (lhs * rhs).
        /// </summary>
        public static hvec2 operator*(GlmHalf lhs, hvec2 rhs) => new hvec2(lhs * rhs.x, lhs * rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator/ (lhs / rhs).
        /// </summary>
        public static hvec2 operator/(hvec2 lhs, hvec2 rhs) => new hvec2(lhs.x / rhs.x, lhs.y / rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator/ (lhs / rhs).
        /// </summary>
        public static hvec2 operator/(hvec2 lhs, GlmHalf rhs) => new hvec2(lhs.x / rhs, lhs.y / rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator/ (lhs / rhs).
        /// </summary>
        public static hvec2 operator/(GlmHalf lhs, hvec2 rhs) => new hvec2(lhs / rhs.x, lhs / rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator+ (identity).
        /// </summary>
        public static hvec2 operator+(hvec2 v) => v;

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator- (-v).
        /// </summary>
        public static hvec2 operator-(hvec2 v) => new hvec2(-v.x, -v.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator% (lhs % rhs).
        /// </summary>
        public static hvec2 operator%(hvec2 lhs, hvec2 rhs) => new hvec2(lhs.x % rhs.x, lhs.y % rhs.y);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator% (lhs % rhs).
        /// </summary>
        public static hvec2 operator%(hvec2 lhs, GlmHalf rhs) => new hvec2(lhs.x % rhs, lhs.y % rhs);

        /// <summary>
        /// Returns a hvec2 from component-wise application of operator% (lhs % rhs).
        /// </summary>
        public static hvec2 operator%(GlmHalf lhs, hvec2 rhs) => new hvec2(lhs % rhs.x, lhs % rhs.y);

        #endregion

    }
}

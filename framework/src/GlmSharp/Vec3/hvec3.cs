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
    /// A vector of type GlmHalf with 3 components.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "vec")]
    [StructLayout(LayoutKind.Sequential)]
    public struct hvec3 : IReadOnlyList<GlmHalf>, IEquatable<hvec3>
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

        /// <summary>
        /// z-component
        /// </summary>
        [DataMember]
        public GlmHalf z;

        #endregion


        #region Constructors

        /// <summary>
        /// Component-wise constructor
        /// </summary>
        public hvec3(GlmHalf x, GlmHalf y, GlmHalf z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// all-same-value constructor
        /// </summary>
        public hvec3(GlmHalf v)
        {
            this.x = v;
            this.y = v;
            this.z = v;
        }

        /// <summary>
        /// from-vector constructor (empty fields are zero/false)
        /// </summary>
        public hvec3(hvec2 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = GlmHalf.Zero;
        }

        /// <summary>
        /// from-vector-and-value constructor
        /// </summary>
        public hvec3(hvec2 v, GlmHalf z)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = z;
        }

        /// <summary>
        /// from-vector constructor
        /// </summary>
        public hvec3(hvec3 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        /// <summary>
        /// from-vector constructor (additional fields are truncated)
        /// </summary>
        public hvec3(hvec4 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        /// <summary>
        /// From-array/list constructor (superfluous values are ignored, missing values are zero-filled).
        /// </summary>
        public hvec3(IReadOnlyList<GlmHalf> v)
        {
            var c = v.Count;
            this.x = c < 0 ? GlmHalf.Zero : v[0];
            this.y = c < 1 ? GlmHalf.Zero : v[1];
            this.z = c < 2 ? GlmHalf.Zero : v[2];
        }

        /// <summary>
        /// Generic from-array constructor (superfluous values are ignored, missing values are zero-filled).
        /// </summary>
        public hvec3(Object[] v)
        {
            var c = v.Length;
            this.x = c < 0 ? GlmHalf.Zero : (GlmHalf)v[0];
            this.y = c < 1 ? GlmHalf.Zero : (GlmHalf)v[1];
            this.z = c < 2 ? GlmHalf.Zero : (GlmHalf)v[2];
        }

        /// <summary>
        /// From-array constructor (superfluous values are ignored, missing values are zero-filled).
        /// </summary>
        public hvec3(GlmHalf[] v)
        {
            var c = v.Length;
            this.x = c < 0 ? GlmHalf.Zero : v[0];
            this.y = c < 1 ? GlmHalf.Zero : v[1];
            this.z = c < 2 ? GlmHalf.Zero : v[2];
        }

        /// <summary>
        /// From-array constructor with base index (superfluous values are ignored, missing values are zero-filled).
        /// </summary>
        public hvec3(GlmHalf[] v, int startIndex)
        {
            var c = v.Length;
            this.x = c + startIndex < 0 ? GlmHalf.Zero : v[0 + startIndex];
            this.y = c + startIndex < 1 ? GlmHalf.Zero : v[1 + startIndex];
            this.z = c + startIndex < 2 ? GlmHalf.Zero : v[2 + startIndex];
        }

        /// <summary>
        /// From-IEnumerable constructor (superfluous values are ignored, missing values are zero-filled).
        /// </summary>
        public hvec3(IEnumerable<GlmHalf> v)
            : this(v.ToArray())
        {
        }

        #endregion


        #region Implicit Operators

        /// <summary>
        /// Implicitly converts this to a vec3.
        /// </summary>
        public static implicit operator vec3(hvec3 v) => new vec3((float)v.x, (float)v.y, (float)v.z);

        /// <summary>
        /// Implicitly converts this to a dvec3.
        /// </summary>
        public static implicit operator dvec3(hvec3 v) => new dvec3((double)v.x, (double)v.y, (double)v.z);

        /// <summary>
        /// Implicitly converts this to a cvec3.
        /// </summary>
        public static implicit operator cvec3(hvec3 v) => new cvec3((Complex)v.x, (Complex)v.y, (Complex)v.z);

        #endregion


        #region Explicit Operators

        /// <summary>
        /// Explicitly converts this to a ivec2.
        /// </summary>
        public static explicit operator ivec2(hvec3 v) => new ivec2((int)v.x, (int)v.y);

        /// <summary>
        /// Explicitly converts this to a ivec3.
        /// </summary>
        public static explicit operator ivec3(hvec3 v) => new ivec3((int)v.x, (int)v.y, (int)v.z);

        /// <summary>
        /// Explicitly converts this to a ivec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator ivec4(hvec3 v) => new ivec4((int)v.x, (int)v.y, (int)v.z, 0);

        /// <summary>
        /// Explicitly converts this to a uvec2.
        /// </summary>
        public static explicit operator uvec2(hvec3 v) => new uvec2((uint)v.x, (uint)v.y);

        /// <summary>
        /// Explicitly converts this to a uvec3.
        /// </summary>
        public static explicit operator uvec3(hvec3 v) => new uvec3((uint)v.x, (uint)v.y, (uint)v.z);

        /// <summary>
        /// Explicitly converts this to a uvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator uvec4(hvec3 v) => new uvec4((uint)v.x, (uint)v.y, (uint)v.z, 0u);

        /// <summary>
        /// Explicitly converts this to a vec2.
        /// </summary>
        public static explicit operator vec2(hvec3 v) => new vec2((float)v.x, (float)v.y);

        /// <summary>
        /// Explicitly converts this to a vec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator vec4(hvec3 v) => new vec4((float)v.x, (float)v.y, (float)v.z, 0f);

        /// <summary>
        /// Explicitly converts this to a hvec2.
        /// </summary>
        public static explicit operator hvec2(hvec3 v) => new hvec2((GlmHalf)v.x, (GlmHalf)v.y);

        /// <summary>
        /// Explicitly converts this to a hvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator hvec4(hvec3 v) => new hvec4((GlmHalf)v.x, (GlmHalf)v.y, (GlmHalf)v.z, GlmHalf.Zero);

        /// <summary>
        /// Explicitly converts this to a dvec2.
        /// </summary>
        public static explicit operator dvec2(hvec3 v) => new dvec2((double)v.x, (double)v.y);

        /// <summary>
        /// Explicitly converts this to a dvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator dvec4(hvec3 v) => new dvec4((double)v.x, (double)v.y, (double)v.z, 0.0);

        /// <summary>
        /// Explicitly converts this to a decvec2.
        /// </summary>
        public static explicit operator decvec2(hvec3 v) => new decvec2((decimal)v.x, (decimal)v.y);

        /// <summary>
        /// Explicitly converts this to a decvec3.
        /// </summary>
        public static explicit operator decvec3(hvec3 v) => new decvec3((decimal)v.x, (decimal)v.y, (decimal)v.z);

        /// <summary>
        /// Explicitly converts this to a decvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator decvec4(hvec3 v) => new decvec4((decimal)v.x, (decimal)v.y, (decimal)v.z, 0m);

        /// <summary>
        /// Explicitly converts this to a cvec2.
        /// </summary>
        public static explicit operator cvec2(hvec3 v) => new cvec2((Complex)v.x, (Complex)v.y);

        /// <summary>
        /// Explicitly converts this to a cvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator cvec4(hvec3 v) => new cvec4((Complex)v.x, (Complex)v.y, (Complex)v.z, Complex.Zero);

        /// <summary>
        /// Explicitly converts this to a lvec2.
        /// </summary>
        public static explicit operator lvec2(hvec3 v) => new lvec2((long)v.x, (long)v.y);

        /// <summary>
        /// Explicitly converts this to a lvec3.
        /// </summary>
        public static explicit operator lvec3(hvec3 v) => new lvec3((long)v.x, (long)v.y, (long)v.z);

        /// <summary>
        /// Explicitly converts this to a lvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator lvec4(hvec3 v) => new lvec4((long)v.x, (long)v.y, (long)v.z, 0);

        /// <summary>
        /// Explicitly converts this to a bvec2.
        /// </summary>
        public static explicit operator bvec2(hvec3 v) => new bvec2(v.x != GlmHalf.Zero, v.y != GlmHalf.Zero);

        /// <summary>
        /// Explicitly converts this to a bvec3.
        /// </summary>
        public static explicit operator bvec3(hvec3 v) => new bvec3(v.x != GlmHalf.Zero, v.y != GlmHalf.Zero, v.z != GlmHalf.Zero);

        /// <summary>
        /// Explicitly converts this to a bvec4. (Higher components are zeroed)
        /// </summary>
        public static explicit operator bvec4(hvec3 v) => new bvec4(v.x != GlmHalf.Zero, v.y != GlmHalf.Zero, v.z != GlmHalf.Zero, false);

        /// <summary>
        /// Explicitly converts this to a GlmHalf array.
        /// </summary>
        public static explicit operator GlmHalf[](hvec3 v) => new [] { v.x, v.y, v.z };

        /// <summary>
        /// Explicitly converts this to a generic object array.
        /// </summary>
        public static explicit operator Object[](hvec3 v) => new Object[] { v.x, v.y, v.z };

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
                    case 2: return z;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
        }

        #endregion


        #region Properties

        /// <summary>
        /// Returns an object that can be used for arbitrary swizzling (e.g. swizzle.zy)
        /// </summary>
        public swizzle_hvec3 swizzle => new swizzle_hvec3(x, y, z);

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
        public hvec2 xz
        {
            get
            {
                return new hvec2(x, z);
            }
            set
            {
                x = value.x;
                z = value.y;
            }
        }

        /// <summary>
        /// Gets or sets the specified subset of components. For more advanced (read-only) swizzling, use the .swizzle property.
        /// </summary>
        public hvec2 yz
        {
            get
            {
                return new hvec2(y, z);
            }
            set
            {
                y = value.x;
                z = value.y;
            }
        }

        /// <summary>
        /// Gets or sets the specified subset of components. For more advanced (read-only) swizzling, use the .swizzle property.
        /// </summary>
        public hvec3 xyz
        {
            get
            {
                return new hvec3(x, y, z);
            }
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
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
        /// Gets or sets the specified subset of components. For more advanced (read-only) swizzling, use the .swizzle property.
        /// </summary>
        public hvec2 rb
        {
            get
            {
                return new hvec2(x, z);
            }
            set
            {
                x = value.x;
                z = value.y;
            }
        }

        /// <summary>
        /// Gets or sets the specified subset of components. For more advanced (read-only) swizzling, use the .swizzle property.
        /// </summary>
        public hvec2 gb
        {
            get
            {
                return new hvec2(y, z);
            }
            set
            {
                y = value.x;
                z = value.y;
            }
        }

        /// <summary>
        /// Gets or sets the specified subset of components. For more advanced (read-only) swizzling, use the .swizzle property.
        /// </summary>
        public hvec3 rgb
        {
            get
            {
                return new hvec3(x, y, z);
            }
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
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
        /// Gets or sets the specified RGBA component. For more advanced (read-only) swizzling, use the .swizzle property.
        /// </summary>
        public GlmHalf b
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }

        /// <summary>
        /// Returns an array with all values
        /// </summary>
        public GlmHalf[] Values => new[] { x, y, z };

        /// <summary>
        /// Returns the number of components (3).
        /// </summary>
        public int Count => 3;

        /// <summary>
        /// Returns the minimal component of this vector.
        /// </summary>
        public GlmHalf MinElement => GlmHalf.Min(GlmHalf.Min(x, y), z);

        /// <summary>
        /// Returns the maximal component of this vector.
        /// </summary>
        public GlmHalf MaxElement => GlmHalf.Max(GlmHalf.Max(x, y), z);

        /// <summary>
        /// Returns the euclidean length of this vector.
        /// </summary>
        public float Length => (float)Math.Sqrt(((x*x + y*y) + z*z));

        /// <summary>
        /// Returns the squared euclidean length of this vector.
        /// </summary>
        public float LengthSqr => ((x*x + y*y) + z*z);

        /// <summary>
        /// Returns the sum of all components.
        /// </summary>
        public GlmHalf Sum => ((x + y) + z);

        /// <summary>
        /// Returns the euclidean norm of this vector.
        /// </summary>
        public float Norm => (float)Math.Sqrt(((x*x + y*y) + z*z));

        /// <summary>
        /// Returns the one-norm of this vector.
        /// </summary>
        public float Norm1 => ((GlmHalf.Abs(x) + GlmHalf.Abs(y)) + GlmHalf.Abs(z));

        /// <summary>
        /// Returns the two-norm (euclidean length) of this vector.
        /// </summary>
        public float Norm2 => (float)Math.Sqrt(((x*x + y*y) + z*z));

        /// <summary>
        /// Returns the max-norm of this vector.
        /// </summary>
        public float NormMax => GlmHalf.Max(GlmHalf.Max(GlmHalf.Abs(x), GlmHalf.Abs(y)), GlmHalf.Abs(z));

        /// <summary>
        /// Returns a copy of this vector with length one (undefined if this has zero length).
        /// </summary>
        public hvec3 Normalized => this / (GlmHalf)Length;

        /// <summary>
        /// Returns a copy of this vector with length one (returns zero if length is zero).
        /// </summary>
        public hvec3 NormalizedSafe => this == Zero ? Zero : this / (GlmHalf)Length;

        #endregion


        #region Static Properties

        /// <summary>
        /// Predefined all-zero vector
        /// </summary>
        public static hvec3 Zero { get; } = new hvec3(GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero);

        /// <summary>
        /// Predefined all-ones vector
        /// </summary>
        public static hvec3 Ones { get; } = new hvec3(GlmHalf.One, GlmHalf.One, GlmHalf.One);

        /// <summary>
        /// Predefined unit-X vector
        /// </summary>
        public static hvec3 UnitX { get; } = new hvec3(GlmHalf.One, GlmHalf.Zero, GlmHalf.Zero);

        /// <summary>
        /// Predefined unit-Y vector
        /// </summary>
        public static hvec3 UnitY { get; } = new hvec3(GlmHalf.Zero, GlmHalf.One, GlmHalf.Zero);

        /// <summary>
        /// Predefined unit-Z vector
        /// </summary>
        public static hvec3 UnitZ { get; } = new hvec3(GlmHalf.Zero, GlmHalf.Zero, GlmHalf.One);

        /// <summary>
        /// Predefined all-MaxValue vector
        /// </summary>
        public static hvec3 MaxValue { get; } = new hvec3(GlmHalf.MaxValue, GlmHalf.MaxValue, GlmHalf.MaxValue);

        /// <summary>
        /// Predefined all-MinValue vector
        /// </summary>
        public static hvec3 MinValue { get; } = new hvec3(GlmHalf.MinValue, GlmHalf.MinValue, GlmHalf.MinValue);

        /// <summary>
        /// Predefined all-Epsilon vector
        /// </summary>
        public static hvec3 Epsilon { get; } = new hvec3(GlmHalf.Epsilon, GlmHalf.Epsilon, GlmHalf.Epsilon);

        /// <summary>
        /// Predefined all-NaN vector
        /// </summary>
        public static hvec3 NaN { get; } = new hvec3(GlmHalf.NaN, GlmHalf.NaN, GlmHalf.NaN);

        /// <summary>
        /// Predefined all-NegativeInfinity vector
        /// </summary>
        public static hvec3 NegativeInfinity { get; } = new hvec3(GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity);

        /// <summary>
        /// Predefined all-PositiveInfinity vector
        /// </summary>
        public static hvec3 PositiveInfinity { get; } = new hvec3(GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity);

        #endregion


        #region Operators

        /// <summary>
        /// Returns true iff this equals rhs component-wise.
        /// </summary>
        public static bool operator==(hvec3 lhs, hvec3 rhs) => lhs.Equals(rhs);

        /// <summary>
        /// Returns true iff this does not equal rhs (component-wise).
        /// </summary>
        public static bool operator!=(hvec3 lhs, hvec3 rhs) => !lhs.Equals(rhs);

        #endregion


        #region Functions

        /// <summary>
        /// Returns an enumerator that iterates through all components.
        /// </summary>
        public IEnumerator<GlmHalf> GetEnumerator()
        {
            yield return x;
            yield return y;
            yield return z;
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
        public string ToString(string sep) => ((x + sep + y) + sep + z);

        /// <summary>
        /// Returns a string representation of this vector using a provided seperator and a format provider for each component.
        /// </summary>
        public string ToString(string sep, IFormatProvider provider) => ((x.ToString(provider) + sep + y.ToString(provider)) + sep + z.ToString(provider));

        /// <summary>
        /// Returns a string representation of this vector using a provided seperator and a format for each component.
        /// </summary>
        public string ToString(string sep, string format) => ((x.ToString(format) + sep + y.ToString(format)) + sep + z.ToString(format));

        /// <summary>
        /// Returns a string representation of this vector using a provided seperator and a format and format provider for each component.
        /// </summary>
        public string ToString(string sep, string format, IFormatProvider provider) => ((x.ToString(format, provider) + sep + y.ToString(format, provider)) + sep + z.ToString(format, provider));

        /// <summary>
        /// Returns true iff this equals rhs component-wise.
        /// </summary>
        public bool Equals(hvec3 rhs) => ((x.Equals(rhs.x) && y.Equals(rhs.y)) && z.Equals(rhs.z));

        /// <summary>
        /// Returns true iff this equals rhs type- and component-wise.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is hvec3 && Equals((hvec3) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((((x.GetHashCode()) * 397) ^ y.GetHashCode()) * 397) ^ z.GetHashCode();
            }
        }

        /// <summary>
        /// Returns the p-norm of this vector.
        /// </summary>
        public double NormP(double p) => Math.Pow(((Math.Pow((double)GlmHalf.Abs(x), p) + Math.Pow((double)GlmHalf.Abs(y), p)) + Math.Pow((double)GlmHalf.Abs(z), p)), 1 / p);

        #endregion


        #region Static Functions

        /// <summary>
        /// Converts the string representation of the vector into a vector representation (using ', ' as a separator).
        /// </summary>
        public static hvec3 Parse(string s) => Parse(s, ", ");

        /// <summary>
        /// Converts the string representation of the vector into a vector representation (using a designated separator).
        /// </summary>
        public static hvec3 Parse(string s, string sep)
        {
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 3) throw new FormatException("input has not exactly 3 parts");
            return new hvec3(GlmHalf.Parse(kvp[0].Trim()), GlmHalf.Parse(kvp[1].Trim()), GlmHalf.Parse(kvp[2].Trim()));
        }

        /// <summary>
        /// Converts the string representation of the vector into a vector representation (using a designated separator and a type provider).
        /// </summary>
        public static hvec3 Parse(string s, string sep, IFormatProvider provider)
        {
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 3) throw new FormatException("input has not exactly 3 parts");
            return new hvec3(GlmHalf.Parse(kvp[0].Trim(), provider), GlmHalf.Parse(kvp[1].Trim(), provider), GlmHalf.Parse(kvp[2].Trim(), provider));
        }

        /// <summary>
        /// Converts the string representation of the vector into a vector representation (using a designated separator and a number style).
        /// </summary>
        public static hvec3 Parse(string s, string sep, NumberStyles style)
        {
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 3) throw new FormatException("input has not exactly 3 parts");
            return new hvec3(GlmHalf.Parse(kvp[0].Trim(), style), GlmHalf.Parse(kvp[1].Trim(), style), GlmHalf.Parse(kvp[2].Trim(), style));
        }

        /// <summary>
        /// Converts the string representation of the vector into a vector representation (using a designated separator and a number style and a format provider).
        /// </summary>
        public static hvec3 Parse(string s, string sep, NumberStyles style, IFormatProvider provider)
        {
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 3) throw new FormatException("input has not exactly 3 parts");
            return new hvec3(GlmHalf.Parse(kvp[0].Trim(), style, provider), GlmHalf.Parse(kvp[1].Trim(), style, provider), GlmHalf.Parse(kvp[2].Trim(), style, provider));
        }

        /// <summary>
        /// Tries to convert the string representation of the vector into a vector representation (using ', ' as a separator), returns false if string was invalid.
        /// </summary>
        public static bool TryParse(string s, out hvec3 result) => TryParse(s, ", ", out result);

        /// <summary>
        /// Tries to convert the string representation of the vector into a vector representation (using a designated separator), returns false if string was invalid.
        /// </summary>
        public static bool TryParse(string s, string sep, out hvec3 result)
        {
            result = Zero;
            if (string.IsNullOrEmpty(s)) return false;
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 3) return false;
            GlmHalf x = GlmHalf.Zero, y = GlmHalf.Zero, z = GlmHalf.Zero;
            var ok = ((GlmHalf.TryParse(kvp[0].Trim(), out x) && GlmHalf.TryParse(kvp[1].Trim(), out y)) && GlmHalf.TryParse(kvp[2].Trim(), out z));
            result = ok ? new hvec3(x, y, z) : Zero;
            return ok;
        }

        /// <summary>
        /// Tries to convert the string representation of the vector into a vector representation (using a designated separator and a number style and a format provider), returns false if string was invalid.
        /// </summary>
        public static bool TryParse(string s, string sep, NumberStyles style, IFormatProvider provider, out hvec3 result)
        {
            result = Zero;
            if (string.IsNullOrEmpty(s)) return false;
            var kvp = s.Split(new[] { sep }, StringSplitOptions.None);
            if (kvp.Length != 3) return false;
            GlmHalf x = GlmHalf.Zero, y = GlmHalf.Zero, z = GlmHalf.Zero;
            var ok = ((GlmHalf.TryParse(kvp[0].Trim(), style, provider, out x) && GlmHalf.TryParse(kvp[1].Trim(), style, provider, out y)) && GlmHalf.TryParse(kvp[2].Trim(), style, provider, out z));
            result = ok ? new hvec3(x, y, z) : Zero;
            return ok;
        }

        /// <summary>
        /// Returns true iff distance between lhs and rhs is less than or equal to epsilon
        /// </summary>
        public static bool ApproxEqual(hvec3 lhs, hvec3 rhs, float eps = 0.1f) => Distance(lhs, rhs) <= eps;

        /// <summary>
        /// OuterProduct treats the first parameter c as a column vector (matrix with one column) and the second parameter r as a row vector (matrix with one row) and does a linear algebraic matrix multiply c * r, yielding a matrix whose number of rows is the number of components in c and whose number of columns is the number of components in r.
        /// </summary>
        public static hmat3x2 OuterProduct(hvec2 c, hvec3 r) => new hmat3x2(c.x * r.x, c.y * r.x, c.x * r.y, c.y * r.y, c.x * r.z, c.y * r.z);

        /// <summary>
        /// OuterProduct treats the first parameter c as a column vector (matrix with one column) and the second parameter r as a row vector (matrix with one row) and does a linear algebraic matrix multiply c * r, yielding a matrix whose number of rows is the number of components in c and whose number of columns is the number of components in r.
        /// </summary>
        public static hmat2x3 OuterProduct(hvec3 c, hvec2 r) => new hmat2x3(c.x * r.x, c.y * r.x, c.z * r.x, c.x * r.y, c.y * r.y, c.z * r.y);

        /// <summary>
        /// OuterProduct treats the first parameter c as a column vector (matrix with one column) and the second parameter r as a row vector (matrix with one row) and does a linear algebraic matrix multiply c * r, yielding a matrix whose number of rows is the number of components in c and whose number of columns is the number of components in r.
        /// </summary>
        public static hmat3 OuterProduct(hvec3 c, hvec3 r) => new hmat3(c.x * r.x, c.y * r.x, c.z * r.x, c.x * r.y, c.y * r.y, c.z * r.y, c.x * r.z, c.y * r.z, c.z * r.z);

        /// <summary>
        /// OuterProduct treats the first parameter c as a column vector (matrix with one column) and the second parameter r as a row vector (matrix with one row) and does a linear algebraic matrix multiply c * r, yielding a matrix whose number of rows is the number of components in c and whose number of columns is the number of components in r.
        /// </summary>
        public static hmat3x4 OuterProduct(hvec4 c, hvec3 r) => new hmat3x4(c.x * r.x, c.y * r.x, c.z * r.x, c.w * r.x, c.x * r.y, c.y * r.y, c.z * r.y, c.w * r.y, c.x * r.z, c.y * r.z, c.z * r.z, c.w * r.z);

        /// <summary>
        /// OuterProduct treats the first parameter c as a column vector (matrix with one column) and the second parameter r as a row vector (matrix with one row) and does a linear algebraic matrix multiply c * r, yielding a matrix whose number of rows is the number of components in c and whose number of columns is the number of components in r.
        /// </summary>
        public static hmat4x3 OuterProduct(hvec3 c, hvec4 r) => new hmat4x3(c.x * r.x, c.y * r.x, c.z * r.x, c.x * r.y, c.y * r.y, c.z * r.y, c.x * r.z, c.y * r.z, c.z * r.z, c.x * r.w, c.y * r.w, c.z * r.w);

        /// <summary>
        /// Returns the inner product (dot product, scalar product) of the two vectors.
        /// </summary>
        public static GlmHalf Dot(hvec3 lhs, hvec3 rhs) => ((lhs.x * rhs.x + lhs.y * rhs.y) + lhs.z * rhs.z);

        /// <summary>
        /// Returns the euclidean distance between the two vectors.
        /// </summary>
        public static float Distance(hvec3 lhs, hvec3 rhs) => (lhs - rhs).Length;

        /// <summary>
        /// Returns the squared euclidean distance between the two vectors.
        /// </summary>
        public static float DistanceSqr(hvec3 lhs, hvec3 rhs) => (lhs - rhs).LengthSqr;

        /// <summary>
        /// Calculate the reflection direction for an incident vector (N should be normalized in order to achieve the desired result).
        /// </summary>
        public static hvec3 Reflect(hvec3 I, hvec3 N) => I - 2 * Dot(N, I) * N;

        /// <summary>
        /// Calculate the refraction direction for an incident vector (The input parameters I and N should be normalized in order to achieve the desired result).
        /// </summary>
        public static hvec3 Refract(hvec3 I, hvec3 N, GlmHalf eta)
        {
            var dNI = Dot(N, I);
            var k = 1 - eta * eta * (1 - dNI * dNI);
            if (k < 0) return Zero;
            return eta * I - (eta * dNI + (GlmHalf)Math.Sqrt(k)) * N;
        }

        /// <summary>
        /// Returns a vector pointing in the same direction as another (faceforward orients a vector to point away from a surface as defined by its normal. If dot(Nref, I) is negative faceforward returns N, otherwise it returns -N).
        /// </summary>
        public static hvec3 FaceForward(hvec3 N, hvec3 I, hvec3 Nref) => Dot(Nref, I) < 0 ? N : -N;

        /// <summary>
        /// Returns the outer product (cross product, vector product) of the two vectors.
        /// </summary>
        public static hvec3 Cross(hvec3 l, hvec3 r) => new hvec3(l.y * r.z - l.z * r.y, l.z * r.x - l.x * r.z, l.x * r.y - l.y * r.x);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed uniform values between 0.0 and 1.0.
        /// </summary>
        public static hvec3 Random(Random random) => new hvec3((GlmHalf)random.NextDouble(), (GlmHalf)random.NextDouble(), (GlmHalf)random.NextDouble());

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed uniform values between -1.0 and 1.0.
        /// </summary>
        public static hvec3 RandomSigned(Random random) => new hvec3((GlmHalf)(random.NextDouble() * 2.0 - 1.0), (GlmHalf)(random.NextDouble() * 2.0 - 1.0), (GlmHalf)(random.NextDouble() * 2.0 - 1.0));

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed values according to a normal distribution (zero mean, unit variance).
        /// </summary>
        public static hvec3 RandomNormal(Random random) => new hvec3((GlmHalf)(Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))), (GlmHalf)(Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))), (GlmHalf)(Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))));

        #endregion


        #region Component-Wise Static Functions

        /// <summary>
        /// Returns a bvec3 from component-wise application of Equal (lhs == rhs).
        /// </summary>
        public static bvec3 Equal(hvec3 lhs, hvec3 rhs) => new bvec3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of Equal (lhs == rhs).
        /// </summary>
        public static bvec3 Equal(hvec3 lhs, GlmHalf rhs) => new bvec3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of Equal (lhs == rhs).
        /// </summary>
        public static bvec3 Equal(GlmHalf lhs, hvec3 rhs) => new bvec3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);

        /// <summary>
        /// Returns a bvec from the application of Equal (lhs == rhs).
        /// </summary>
        public static bvec3 Equal(GlmHalf lhs, GlmHalf rhs) => new bvec3(lhs == rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of NotEqual (lhs != rhs).
        /// </summary>
        public static bvec3 NotEqual(hvec3 lhs, hvec3 rhs) => new bvec3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of NotEqual (lhs != rhs).
        /// </summary>
        public static bvec3 NotEqual(hvec3 lhs, GlmHalf rhs) => new bvec3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of NotEqual (lhs != rhs).
        /// </summary>
        public static bvec3 NotEqual(GlmHalf lhs, hvec3 rhs) => new bvec3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);

        /// <summary>
        /// Returns a bvec from the application of NotEqual (lhs != rhs).
        /// </summary>
        public static bvec3 NotEqual(GlmHalf lhs, GlmHalf rhs) => new bvec3(lhs != rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of GreaterThan (lhs &gt; rhs).
        /// </summary>
        public static bvec3 GreaterThan(hvec3 lhs, hvec3 rhs) => new bvec3(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of GreaterThan (lhs &gt; rhs).
        /// </summary>
        public static bvec3 GreaterThan(hvec3 lhs, GlmHalf rhs) => new bvec3(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of GreaterThan (lhs &gt; rhs).
        /// </summary>
        public static bvec3 GreaterThan(GlmHalf lhs, hvec3 rhs) => new bvec3(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z);

        /// <summary>
        /// Returns a bvec from the application of GreaterThan (lhs &gt; rhs).
        /// </summary>
        public static bvec3 GreaterThan(GlmHalf lhs, GlmHalf rhs) => new bvec3(lhs > rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of GreaterThanEqual (lhs &gt;= rhs).
        /// </summary>
        public static bvec3 GreaterThanEqual(hvec3 lhs, hvec3 rhs) => new bvec3(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of GreaterThanEqual (lhs &gt;= rhs).
        /// </summary>
        public static bvec3 GreaterThanEqual(hvec3 lhs, GlmHalf rhs) => new bvec3(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of GreaterThanEqual (lhs &gt;= rhs).
        /// </summary>
        public static bvec3 GreaterThanEqual(GlmHalf lhs, hvec3 rhs) => new bvec3(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z);

        /// <summary>
        /// Returns a bvec from the application of GreaterThanEqual (lhs &gt;= rhs).
        /// </summary>
        public static bvec3 GreaterThanEqual(GlmHalf lhs, GlmHalf rhs) => new bvec3(lhs >= rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of LesserThan (lhs &lt; rhs).
        /// </summary>
        public static bvec3 LesserThan(hvec3 lhs, hvec3 rhs) => new bvec3(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of LesserThan (lhs &lt; rhs).
        /// </summary>
        public static bvec3 LesserThan(hvec3 lhs, GlmHalf rhs) => new bvec3(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of LesserThan (lhs &lt; rhs).
        /// </summary>
        public static bvec3 LesserThan(GlmHalf lhs, hvec3 rhs) => new bvec3(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z);

        /// <summary>
        /// Returns a bvec from the application of LesserThan (lhs &lt; rhs).
        /// </summary>
        public static bvec3 LesserThan(GlmHalf lhs, GlmHalf rhs) => new bvec3(lhs < rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of LesserThanEqual (lhs &lt;= rhs).
        /// </summary>
        public static bvec3 LesserThanEqual(hvec3 lhs, hvec3 rhs) => new bvec3(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of LesserThanEqual (lhs &lt;= rhs).
        /// </summary>
        public static bvec3 LesserThanEqual(hvec3 lhs, GlmHalf rhs) => new bvec3(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of LesserThanEqual (lhs &lt;= rhs).
        /// </summary>
        public static bvec3 LesserThanEqual(GlmHalf lhs, hvec3 rhs) => new bvec3(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z);

        /// <summary>
        /// Returns a bvec from the application of LesserThanEqual (lhs &lt;= rhs).
        /// </summary>
        public static bvec3 LesserThanEqual(GlmHalf lhs, GlmHalf rhs) => new bvec3(lhs <= rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of IsInfinity (GlmHalf.IsInfinity(v)).
        /// </summary>
        public static bvec3 IsInfinity(hvec3 v) => new bvec3(GlmHalf.IsInfinity(v.x), GlmHalf.IsInfinity(v.y), GlmHalf.IsInfinity(v.z));

        /// <summary>
        /// Returns a bvec from the application of IsInfinity (GlmHalf.IsInfinity(v)).
        /// </summary>
        public static bvec3 IsInfinity(GlmHalf v) => new bvec3(GlmHalf.IsInfinity(v));

        /// <summary>
        /// Returns a bvec3 from component-wise application of IsFinite (!GlmHalf.IsNaN(v) &amp;&amp; !GlmHalf.IsInfinity(v)).
        /// </summary>
        public static bvec3 IsFinite(hvec3 v) => new bvec3(!GlmHalf.IsNaN(v.x) && !GlmHalf.IsInfinity(v.x), !GlmHalf.IsNaN(v.y) && !GlmHalf.IsInfinity(v.y), !GlmHalf.IsNaN(v.z) && !GlmHalf.IsInfinity(v.z));

        /// <summary>
        /// Returns a bvec from the application of IsFinite (!GlmHalf.IsNaN(v) &amp;&amp; !GlmHalf.IsInfinity(v)).
        /// </summary>
        public static bvec3 IsFinite(GlmHalf v) => new bvec3(!GlmHalf.IsNaN(v) && !GlmHalf.IsInfinity(v));

        /// <summary>
        /// Returns a bvec3 from component-wise application of IsNaN (GlmHalf.IsNaN(v)).
        /// </summary>
        public static bvec3 IsNaN(hvec3 v) => new bvec3(GlmHalf.IsNaN(v.x), GlmHalf.IsNaN(v.y), GlmHalf.IsNaN(v.z));

        /// <summary>
        /// Returns a bvec from the application of IsNaN (GlmHalf.IsNaN(v)).
        /// </summary>
        public static bvec3 IsNaN(GlmHalf v) => new bvec3(GlmHalf.IsNaN(v));

        /// <summary>
        /// Returns a bvec3 from component-wise application of IsNegativeInfinity (GlmHalf.IsNegativeInfinity(v)).
        /// </summary>
        public static bvec3 IsNegativeInfinity(hvec3 v) => new bvec3(GlmHalf.IsNegativeInfinity(v.x), GlmHalf.IsNegativeInfinity(v.y), GlmHalf.IsNegativeInfinity(v.z));

        /// <summary>
        /// Returns a bvec from the application of IsNegativeInfinity (GlmHalf.IsNegativeInfinity(v)).
        /// </summary>
        public static bvec3 IsNegativeInfinity(GlmHalf v) => new bvec3(GlmHalf.IsNegativeInfinity(v));

        /// <summary>
        /// Returns a bvec3 from component-wise application of IsPositiveInfinity (GlmHalf.IsPositiveInfinity(v)).
        /// </summary>
        public static bvec3 IsPositiveInfinity(hvec3 v) => new bvec3(GlmHalf.IsPositiveInfinity(v.x), GlmHalf.IsPositiveInfinity(v.y), GlmHalf.IsPositiveInfinity(v.z));

        /// <summary>
        /// Returns a bvec from the application of IsPositiveInfinity (GlmHalf.IsPositiveInfinity(v)).
        /// </summary>
        public static bvec3 IsPositiveInfinity(GlmHalf v) => new bvec3(GlmHalf.IsPositiveInfinity(v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Abs (GlmHalf.Abs(v)).
        /// </summary>
        public static hvec3 Abs(hvec3 v) => new hvec3(GlmHalf.Abs(v.x), GlmHalf.Abs(v.y), GlmHalf.Abs(v.z));

        /// <summary>
        /// Returns a hvec from the application of Abs (GlmHalf.Abs(v)).
        /// </summary>
        public static hvec3 Abs(GlmHalf v) => new hvec3(GlmHalf.Abs(v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of HermiteInterpolationOrder3 ((3 - 2 * v) * v * v).
        /// </summary>
        public static hvec3 HermiteInterpolationOrder3(hvec3 v) => new hvec3((3 - 2 * v.x) * v.x * v.x, (3 - 2 * v.y) * v.y * v.y, (3 - 2 * v.z) * v.z * v.z);

        /// <summary>
        /// Returns a hvec from the application of HermiteInterpolationOrder3 ((3 - 2 * v) * v * v).
        /// </summary>
        public static hvec3 HermiteInterpolationOrder3(GlmHalf v) => new hvec3((3 - 2 * v) * v * v);

        /// <summary>
        /// Returns a hvec3 from component-wise application of HermiteInterpolationOrder5 (((6 * v - 15) * v + 10) * v * v * v).
        /// </summary>
        public static hvec3 HermiteInterpolationOrder5(hvec3 v) => new hvec3(((6 * v.x - 15) * v.x + 10) * v.x * v.x * v.x, ((6 * v.y - 15) * v.y + 10) * v.y * v.y * v.y, ((6 * v.z - 15) * v.z + 10) * v.z * v.z * v.z);

        /// <summary>
        /// Returns a hvec from the application of HermiteInterpolationOrder5 (((6 * v - 15) * v + 10) * v * v * v).
        /// </summary>
        public static hvec3 HermiteInterpolationOrder5(GlmHalf v) => new hvec3(((6 * v - 15) * v + 10) * v * v * v);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Sqr (v * v).
        /// </summary>
        public static hvec3 Sqr(hvec3 v) => new hvec3(v.x * v.x, v.y * v.y, v.z * v.z);

        /// <summary>
        /// Returns a hvec from the application of Sqr (v * v).
        /// </summary>
        public static hvec3 Sqr(GlmHalf v) => new hvec3(v * v);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Pow2 (v * v).
        /// </summary>
        public static hvec3 Pow2(hvec3 v) => new hvec3(v.x * v.x, v.y * v.y, v.z * v.z);

        /// <summary>
        /// Returns a hvec from the application of Pow2 (v * v).
        /// </summary>
        public static hvec3 Pow2(GlmHalf v) => new hvec3(v * v);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Pow3 (v * v * v).
        /// </summary>
        public static hvec3 Pow3(hvec3 v) => new hvec3(v.x * v.x * v.x, v.y * v.y * v.y, v.z * v.z * v.z);

        /// <summary>
        /// Returns a hvec from the application of Pow3 (v * v * v).
        /// </summary>
        public static hvec3 Pow3(GlmHalf v) => new hvec3(v * v * v);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Step (v &gt;= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero).
        /// </summary>
        public static hvec3 Step(hvec3 v) => new hvec3(v.x >= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero, v.y >= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero, v.z >= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero);

        /// <summary>
        /// Returns a hvec from the application of Step (v &gt;= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero).
        /// </summary>
        public static hvec3 Step(GlmHalf v) => new hvec3(v >= GlmHalf.Zero ? GlmHalf.One : GlmHalf.Zero);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Sqrt ((GlmHalf)Math.Sqrt((double)v)).
        /// </summary>
        public static hvec3 Sqrt(hvec3 v) => new hvec3((GlmHalf)Math.Sqrt((double)v.x), (GlmHalf)Math.Sqrt((double)v.y), (GlmHalf)Math.Sqrt((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Sqrt ((GlmHalf)Math.Sqrt((double)v)).
        /// </summary>
        public static hvec3 Sqrt(GlmHalf v) => new hvec3((GlmHalf)Math.Sqrt((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of InverseSqrt ((GlmHalf)(1.0 / Math.Sqrt((double)v))).
        /// </summary>
        public static hvec3 InverseSqrt(hvec3 v) => new hvec3((GlmHalf)(1.0 / Math.Sqrt((double)v.x)), (GlmHalf)(1.0 / Math.Sqrt((double)v.y)), (GlmHalf)(1.0 / Math.Sqrt((double)v.z)));

        /// <summary>
        /// Returns a hvec from the application of InverseSqrt ((GlmHalf)(1.0 / Math.Sqrt((double)v))).
        /// </summary>
        public static hvec3 InverseSqrt(GlmHalf v) => new hvec3((GlmHalf)(1.0 / Math.Sqrt((double)v)));

        /// <summary>
        /// Returns a ivec3 from component-wise application of Sign (Math.Sign(v)).
        /// </summary>
        public static ivec3 Sign(hvec3 v) => new ivec3(Math.Sign(v.x), Math.Sign(v.y), Math.Sign(v.z));

        /// <summary>
        /// Returns a ivec from the application of Sign (Math.Sign(v)).
        /// </summary>
        public static ivec3 Sign(GlmHalf v) => new ivec3(Math.Sign(v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Max (GlmHalf.Max(lhs, rhs)).
        /// </summary>
        public static hvec3 Max(hvec3 lhs, hvec3 rhs) => new hvec3(GlmHalf.Max(lhs.x, rhs.x), GlmHalf.Max(lhs.y, rhs.y), GlmHalf.Max(lhs.z, rhs.z));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Max (GlmHalf.Max(lhs, rhs)).
        /// </summary>
        public static hvec3 Max(hvec3 lhs, GlmHalf rhs) => new hvec3(GlmHalf.Max(lhs.x, rhs), GlmHalf.Max(lhs.y, rhs), GlmHalf.Max(lhs.z, rhs));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Max (GlmHalf.Max(lhs, rhs)).
        /// </summary>
        public static hvec3 Max(GlmHalf lhs, hvec3 rhs) => new hvec3(GlmHalf.Max(lhs, rhs.x), GlmHalf.Max(lhs, rhs.y), GlmHalf.Max(lhs, rhs.z));

        /// <summary>
        /// Returns a hvec from the application of Max (GlmHalf.Max(lhs, rhs)).
        /// </summary>
        public static hvec3 Max(GlmHalf lhs, GlmHalf rhs) => new hvec3(GlmHalf.Max(lhs, rhs));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Min (GlmHalf.Min(lhs, rhs)).
        /// </summary>
        public static hvec3 Min(hvec3 lhs, hvec3 rhs) => new hvec3(GlmHalf.Min(lhs.x, rhs.x), GlmHalf.Min(lhs.y, rhs.y), GlmHalf.Min(lhs.z, rhs.z));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Min (GlmHalf.Min(lhs, rhs)).
        /// </summary>
        public static hvec3 Min(hvec3 lhs, GlmHalf rhs) => new hvec3(GlmHalf.Min(lhs.x, rhs), GlmHalf.Min(lhs.y, rhs), GlmHalf.Min(lhs.z, rhs));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Min (GlmHalf.Min(lhs, rhs)).
        /// </summary>
        public static hvec3 Min(GlmHalf lhs, hvec3 rhs) => new hvec3(GlmHalf.Min(lhs, rhs.x), GlmHalf.Min(lhs, rhs.y), GlmHalf.Min(lhs, rhs.z));

        /// <summary>
        /// Returns a hvec from the application of Min (GlmHalf.Min(lhs, rhs)).
        /// </summary>
        public static hvec3 Min(GlmHalf lhs, GlmHalf rhs) => new hvec3(GlmHalf.Min(lhs, rhs));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Pow ((GlmHalf)Math.Pow((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec3 Pow(hvec3 lhs, hvec3 rhs) => new hvec3((GlmHalf)Math.Pow((double)lhs.x, (double)rhs.x), (GlmHalf)Math.Pow((double)lhs.y, (double)rhs.y), (GlmHalf)Math.Pow((double)lhs.z, (double)rhs.z));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Pow ((GlmHalf)Math.Pow((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec3 Pow(hvec3 lhs, GlmHalf rhs) => new hvec3((GlmHalf)Math.Pow((double)lhs.x, (double)rhs), (GlmHalf)Math.Pow((double)lhs.y, (double)rhs), (GlmHalf)Math.Pow((double)lhs.z, (double)rhs));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Pow ((GlmHalf)Math.Pow((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec3 Pow(GlmHalf lhs, hvec3 rhs) => new hvec3((GlmHalf)Math.Pow((double)lhs, (double)rhs.x), (GlmHalf)Math.Pow((double)lhs, (double)rhs.y), (GlmHalf)Math.Pow((double)lhs, (double)rhs.z));

        /// <summary>
        /// Returns a hvec from the application of Pow ((GlmHalf)Math.Pow((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec3 Pow(GlmHalf lhs, GlmHalf rhs) => new hvec3((GlmHalf)Math.Pow((double)lhs, (double)rhs));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Log ((GlmHalf)Math.Log((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec3 Log(hvec3 lhs, hvec3 rhs) => new hvec3((GlmHalf)Math.Log((double)lhs.x, (double)rhs.x), (GlmHalf)Math.Log((double)lhs.y, (double)rhs.y), (GlmHalf)Math.Log((double)lhs.z, (double)rhs.z));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Log ((GlmHalf)Math.Log((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec3 Log(hvec3 lhs, GlmHalf rhs) => new hvec3((GlmHalf)Math.Log((double)lhs.x, (double)rhs), (GlmHalf)Math.Log((double)lhs.y, (double)rhs), (GlmHalf)Math.Log((double)lhs.z, (double)rhs));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Log ((GlmHalf)Math.Log((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec3 Log(GlmHalf lhs, hvec3 rhs) => new hvec3((GlmHalf)Math.Log((double)lhs, (double)rhs.x), (GlmHalf)Math.Log((double)lhs, (double)rhs.y), (GlmHalf)Math.Log((double)lhs, (double)rhs.z));

        /// <summary>
        /// Returns a hvec from the application of Log ((GlmHalf)Math.Log((double)lhs, (double)rhs)).
        /// </summary>
        public static hvec3 Log(GlmHalf lhs, GlmHalf rhs) => new hvec3((GlmHalf)Math.Log((double)lhs, (double)rhs));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec3 Clamp(hvec3 v, hvec3 min, hvec3 max) => new hvec3(GlmHalf.Min(GlmHalf.Max(v.x, min.x), max.x), GlmHalf.Min(GlmHalf.Max(v.y, min.y), max.y), GlmHalf.Min(GlmHalf.Max(v.z, min.z), max.z));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec3 Clamp(hvec3 v, hvec3 min, GlmHalf max) => new hvec3(GlmHalf.Min(GlmHalf.Max(v.x, min.x), max), GlmHalf.Min(GlmHalf.Max(v.y, min.y), max), GlmHalf.Min(GlmHalf.Max(v.z, min.z), max));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec3 Clamp(hvec3 v, GlmHalf min, hvec3 max) => new hvec3(GlmHalf.Min(GlmHalf.Max(v.x, min), max.x), GlmHalf.Min(GlmHalf.Max(v.y, min), max.y), GlmHalf.Min(GlmHalf.Max(v.z, min), max.z));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec3 Clamp(hvec3 v, GlmHalf min, GlmHalf max) => new hvec3(GlmHalf.Min(GlmHalf.Max(v.x, min), max), GlmHalf.Min(GlmHalf.Max(v.y, min), max), GlmHalf.Min(GlmHalf.Max(v.z, min), max));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec3 Clamp(GlmHalf v, hvec3 min, hvec3 max) => new hvec3(GlmHalf.Min(GlmHalf.Max(v, min.x), max.x), GlmHalf.Min(GlmHalf.Max(v, min.y), max.y), GlmHalf.Min(GlmHalf.Max(v, min.z), max.z));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec3 Clamp(GlmHalf v, hvec3 min, GlmHalf max) => new hvec3(GlmHalf.Min(GlmHalf.Max(v, min.x), max), GlmHalf.Min(GlmHalf.Max(v, min.y), max), GlmHalf.Min(GlmHalf.Max(v, min.z), max));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec3 Clamp(GlmHalf v, GlmHalf min, hvec3 max) => new hvec3(GlmHalf.Min(GlmHalf.Max(v, min), max.x), GlmHalf.Min(GlmHalf.Max(v, min), max.y), GlmHalf.Min(GlmHalf.Max(v, min), max.z));

        /// <summary>
        /// Returns a hvec from the application of Clamp (GlmHalf.Min(GlmHalf.Max(v, min), max)).
        /// </summary>
        public static hvec3 Clamp(GlmHalf v, GlmHalf min, GlmHalf max) => new hvec3(GlmHalf.Min(GlmHalf.Max(v, min), max));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Mix(hvec3 min, hvec3 max, hvec3 a) => new hvec3(min.x * (1-a.x) + max.x * a.x, min.y * (1-a.y) + max.y * a.y, min.z * (1-a.z) + max.z * a.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Mix(hvec3 min, hvec3 max, GlmHalf a) => new hvec3(min.x * (1-a) + max.x * a, min.y * (1-a) + max.y * a, min.z * (1-a) + max.z * a);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Mix(hvec3 min, GlmHalf max, hvec3 a) => new hvec3(min.x * (1-a.x) + max * a.x, min.y * (1-a.y) + max * a.y, min.z * (1-a.z) + max * a.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Mix(hvec3 min, GlmHalf max, GlmHalf a) => new hvec3(min.x * (1-a) + max * a, min.y * (1-a) + max * a, min.z * (1-a) + max * a);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Mix(GlmHalf min, hvec3 max, hvec3 a) => new hvec3(min * (1-a.x) + max.x * a.x, min * (1-a.y) + max.y * a.y, min * (1-a.z) + max.z * a.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Mix(GlmHalf min, hvec3 max, GlmHalf a) => new hvec3(min * (1-a) + max.x * a, min * (1-a) + max.y * a, min * (1-a) + max.z * a);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Mix(GlmHalf min, GlmHalf max, hvec3 a) => new hvec3(min * (1-a.x) + max * a.x, min * (1-a.y) + max * a.y, min * (1-a.z) + max * a.z);

        /// <summary>
        /// Returns a hvec from the application of Mix (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Mix(GlmHalf min, GlmHalf max, GlmHalf a) => new hvec3(min * (1-a) + max * a);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Lerp(hvec3 min, hvec3 max, hvec3 a) => new hvec3(min.x * (1-a.x) + max.x * a.x, min.y * (1-a.y) + max.y * a.y, min.z * (1-a.z) + max.z * a.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Lerp(hvec3 min, hvec3 max, GlmHalf a) => new hvec3(min.x * (1-a) + max.x * a, min.y * (1-a) + max.y * a, min.z * (1-a) + max.z * a);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Lerp(hvec3 min, GlmHalf max, hvec3 a) => new hvec3(min.x * (1-a.x) + max * a.x, min.y * (1-a.y) + max * a.y, min.z * (1-a.z) + max * a.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Lerp(hvec3 min, GlmHalf max, GlmHalf a) => new hvec3(min.x * (1-a) + max * a, min.y * (1-a) + max * a, min.z * (1-a) + max * a);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Lerp(GlmHalf min, hvec3 max, hvec3 a) => new hvec3(min * (1-a.x) + max.x * a.x, min * (1-a.y) + max.y * a.y, min * (1-a.z) + max.z * a.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Lerp(GlmHalf min, hvec3 max, GlmHalf a) => new hvec3(min * (1-a) + max.x * a, min * (1-a) + max.y * a, min * (1-a) + max.z * a);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Lerp(GlmHalf min, GlmHalf max, hvec3 a) => new hvec3(min * (1-a.x) + max * a.x, min * (1-a.y) + max * a.y, min * (1-a.z) + max * a.z);

        /// <summary>
        /// Returns a hvec from the application of Lerp (min * (1-a) + max * a).
        /// </summary>
        public static hvec3 Lerp(GlmHalf min, GlmHalf max, GlmHalf a) => new hvec3(min * (1-a) + max * a);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec3 Smoothstep(hvec3 edge0, hvec3 edge1, hvec3 v) => new hvec3(((v.x - edge0.x) / (edge1.x - edge0.x)).Clamp().HermiteInterpolationOrder3(), ((v.y - edge0.y) / (edge1.y - edge0.y)).Clamp().HermiteInterpolationOrder3(), ((v.z - edge0.z) / (edge1.z - edge0.z)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec3 Smoothstep(hvec3 edge0, hvec3 edge1, GlmHalf v) => new hvec3(((v - edge0.x) / (edge1.x - edge0.x)).Clamp().HermiteInterpolationOrder3(), ((v - edge0.y) / (edge1.y - edge0.y)).Clamp().HermiteInterpolationOrder3(), ((v - edge0.z) / (edge1.z - edge0.z)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec3 Smoothstep(hvec3 edge0, GlmHalf edge1, hvec3 v) => new hvec3(((v.x - edge0.x) / (edge1 - edge0.x)).Clamp().HermiteInterpolationOrder3(), ((v.y - edge0.y) / (edge1 - edge0.y)).Clamp().HermiteInterpolationOrder3(), ((v.z - edge0.z) / (edge1 - edge0.z)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec3 Smoothstep(hvec3 edge0, GlmHalf edge1, GlmHalf v) => new hvec3(((v - edge0.x) / (edge1 - edge0.x)).Clamp().HermiteInterpolationOrder3(), ((v - edge0.y) / (edge1 - edge0.y)).Clamp().HermiteInterpolationOrder3(), ((v - edge0.z) / (edge1 - edge0.z)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec3 Smoothstep(GlmHalf edge0, hvec3 edge1, hvec3 v) => new hvec3(((v.x - edge0) / (edge1.x - edge0)).Clamp().HermiteInterpolationOrder3(), ((v.y - edge0) / (edge1.y - edge0)).Clamp().HermiteInterpolationOrder3(), ((v.z - edge0) / (edge1.z - edge0)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec3 Smoothstep(GlmHalf edge0, hvec3 edge1, GlmHalf v) => new hvec3(((v - edge0) / (edge1.x - edge0)).Clamp().HermiteInterpolationOrder3(), ((v - edge0) / (edge1.y - edge0)).Clamp().HermiteInterpolationOrder3(), ((v - edge0) / (edge1.z - edge0)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec3 Smoothstep(GlmHalf edge0, GlmHalf edge1, hvec3 v) => new hvec3(((v.x - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3(), ((v.y - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3(), ((v.z - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec from the application of Smoothstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3()).
        /// </summary>
        public static hvec3 Smoothstep(GlmHalf edge0, GlmHalf edge1, GlmHalf v) => new hvec3(((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder3());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec3 Smootherstep(hvec3 edge0, hvec3 edge1, hvec3 v) => new hvec3(((v.x - edge0.x) / (edge1.x - edge0.x)).Clamp().HermiteInterpolationOrder5(), ((v.y - edge0.y) / (edge1.y - edge0.y)).Clamp().HermiteInterpolationOrder5(), ((v.z - edge0.z) / (edge1.z - edge0.z)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec3 Smootherstep(hvec3 edge0, hvec3 edge1, GlmHalf v) => new hvec3(((v - edge0.x) / (edge1.x - edge0.x)).Clamp().HermiteInterpolationOrder5(), ((v - edge0.y) / (edge1.y - edge0.y)).Clamp().HermiteInterpolationOrder5(), ((v - edge0.z) / (edge1.z - edge0.z)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec3 Smootherstep(hvec3 edge0, GlmHalf edge1, hvec3 v) => new hvec3(((v.x - edge0.x) / (edge1 - edge0.x)).Clamp().HermiteInterpolationOrder5(), ((v.y - edge0.y) / (edge1 - edge0.y)).Clamp().HermiteInterpolationOrder5(), ((v.z - edge0.z) / (edge1 - edge0.z)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec3 Smootherstep(hvec3 edge0, GlmHalf edge1, GlmHalf v) => new hvec3(((v - edge0.x) / (edge1 - edge0.x)).Clamp().HermiteInterpolationOrder5(), ((v - edge0.y) / (edge1 - edge0.y)).Clamp().HermiteInterpolationOrder5(), ((v - edge0.z) / (edge1 - edge0.z)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec3 Smootherstep(GlmHalf edge0, hvec3 edge1, hvec3 v) => new hvec3(((v.x - edge0) / (edge1.x - edge0)).Clamp().HermiteInterpolationOrder5(), ((v.y - edge0) / (edge1.y - edge0)).Clamp().HermiteInterpolationOrder5(), ((v.z - edge0) / (edge1.z - edge0)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec3 Smootherstep(GlmHalf edge0, hvec3 edge1, GlmHalf v) => new hvec3(((v - edge0) / (edge1.x - edge0)).Clamp().HermiteInterpolationOrder5(), ((v - edge0) / (edge1.y - edge0)).Clamp().HermiteInterpolationOrder5(), ((v - edge0) / (edge1.z - edge0)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec3 Smootherstep(GlmHalf edge0, GlmHalf edge1, hvec3 v) => new hvec3(((v.x - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5(), ((v.y - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5(), ((v.z - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec from the application of Smootherstep (((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5()).
        /// </summary>
        public static hvec3 Smootherstep(GlmHalf edge0, GlmHalf edge1, GlmHalf v) => new hvec3(((v - edge0) / (edge1 - edge0)).Clamp().HermiteInterpolationOrder5());

        /// <summary>
        /// Returns a hvec3 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec3 Fma(hvec3 a, hvec3 b, hvec3 c) => new hvec3(a.x * b.x + c.x, a.y * b.y + c.y, a.z * b.z + c.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec3 Fma(hvec3 a, hvec3 b, GlmHalf c) => new hvec3(a.x * b.x + c, a.y * b.y + c, a.z * b.z + c);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec3 Fma(hvec3 a, GlmHalf b, hvec3 c) => new hvec3(a.x * b + c.x, a.y * b + c.y, a.z * b + c.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec3 Fma(hvec3 a, GlmHalf b, GlmHalf c) => new hvec3(a.x * b + c, a.y * b + c, a.z * b + c);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec3 Fma(GlmHalf a, hvec3 b, hvec3 c) => new hvec3(a * b.x + c.x, a * b.y + c.y, a * b.z + c.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec3 Fma(GlmHalf a, hvec3 b, GlmHalf c) => new hvec3(a * b.x + c, a * b.y + c, a * b.z + c);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Fma (a * b + c).
        /// </summary>
        public static hvec3 Fma(GlmHalf a, GlmHalf b, hvec3 c) => new hvec3(a * b + c.x, a * b + c.y, a * b + c.z);

        /// <summary>
        /// Returns a hvec from the application of Fma (a * b + c).
        /// </summary>
        public static hvec3 Fma(GlmHalf a, GlmHalf b, GlmHalf c) => new hvec3(a * b + c);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Add (lhs + rhs).
        /// </summary>
        public static hvec3 Add(hvec3 lhs, hvec3 rhs) => new hvec3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Add (lhs + rhs).
        /// </summary>
        public static hvec3 Add(hvec3 lhs, GlmHalf rhs) => new hvec3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Add (lhs + rhs).
        /// </summary>
        public static hvec3 Add(GlmHalf lhs, hvec3 rhs) => new hvec3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);

        /// <summary>
        /// Returns a hvec from the application of Add (lhs + rhs).
        /// </summary>
        public static hvec3 Add(GlmHalf lhs, GlmHalf rhs) => new hvec3(lhs + rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Sub (lhs - rhs).
        /// </summary>
        public static hvec3 Sub(hvec3 lhs, hvec3 rhs) => new hvec3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Sub (lhs - rhs).
        /// </summary>
        public static hvec3 Sub(hvec3 lhs, GlmHalf rhs) => new hvec3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Sub (lhs - rhs).
        /// </summary>
        public static hvec3 Sub(GlmHalf lhs, hvec3 rhs) => new hvec3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);

        /// <summary>
        /// Returns a hvec from the application of Sub (lhs - rhs).
        /// </summary>
        public static hvec3 Sub(GlmHalf lhs, GlmHalf rhs) => new hvec3(lhs - rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Mul (lhs * rhs).
        /// </summary>
        public static hvec3 Mul(hvec3 lhs, hvec3 rhs) => new hvec3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Mul (lhs * rhs).
        /// </summary>
        public static hvec3 Mul(hvec3 lhs, GlmHalf rhs) => new hvec3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Mul (lhs * rhs).
        /// </summary>
        public static hvec3 Mul(GlmHalf lhs, hvec3 rhs) => new hvec3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);

        /// <summary>
        /// Returns a hvec from the application of Mul (lhs * rhs).
        /// </summary>
        public static hvec3 Mul(GlmHalf lhs, GlmHalf rhs) => new hvec3(lhs * rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Div (lhs / rhs).
        /// </summary>
        public static hvec3 Div(hvec3 lhs, hvec3 rhs) => new hvec3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Div (lhs / rhs).
        /// </summary>
        public static hvec3 Div(hvec3 lhs, GlmHalf rhs) => new hvec3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Div (lhs / rhs).
        /// </summary>
        public static hvec3 Div(GlmHalf lhs, hvec3 rhs) => new hvec3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);

        /// <summary>
        /// Returns a hvec from the application of Div (lhs / rhs).
        /// </summary>
        public static hvec3 Div(GlmHalf lhs, GlmHalf rhs) => new hvec3(lhs / rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Modulo (lhs % rhs).
        /// </summary>
        public static hvec3 Modulo(hvec3 lhs, hvec3 rhs) => new hvec3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Modulo (lhs % rhs).
        /// </summary>
        public static hvec3 Modulo(hvec3 lhs, GlmHalf rhs) => new hvec3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Modulo (lhs % rhs).
        /// </summary>
        public static hvec3 Modulo(GlmHalf lhs, hvec3 rhs) => new hvec3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);

        /// <summary>
        /// Returns a hvec from the application of Modulo (lhs % rhs).
        /// </summary>
        public static hvec3 Modulo(GlmHalf lhs, GlmHalf rhs) => new hvec3(lhs % rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of Degrees (Radians-To-Degrees Conversion).
        /// </summary>
        public static hvec3 Degrees(hvec3 v) => new hvec3((GlmHalf)(v.x * new GlmHalf(57.295779513082320876798154814105170332405472466564321)), (GlmHalf)(v.y * new GlmHalf(57.295779513082320876798154814105170332405472466564321)), (GlmHalf)(v.z * new GlmHalf(57.295779513082320876798154814105170332405472466564321)));

        /// <summary>
        /// Returns a hvec from the application of Degrees (Radians-To-Degrees Conversion).
        /// </summary>
        public static hvec3 Degrees(GlmHalf v) => new hvec3((GlmHalf)(v * new GlmHalf(57.295779513082320876798154814105170332405472466564321)));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Radians (Degrees-To-Radians Conversion).
        /// </summary>
        public static hvec3 Radians(hvec3 v) => new hvec3((GlmHalf)(v.x * new GlmHalf(0.0174532925199432957692369076848861271344287188854172)), (GlmHalf)(v.y * new GlmHalf(0.0174532925199432957692369076848861271344287188854172)), (GlmHalf)(v.z * new GlmHalf(0.0174532925199432957692369076848861271344287188854172)));

        /// <summary>
        /// Returns a hvec from the application of Radians (Degrees-To-Radians Conversion).
        /// </summary>
        public static hvec3 Radians(GlmHalf v) => new hvec3((GlmHalf)(v * new GlmHalf(0.0174532925199432957692369076848861271344287188854172)));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Acos ((GlmHalf)Math.Acos((double)v)).
        /// </summary>
        public static hvec3 Acos(hvec3 v) => new hvec3((GlmHalf)Math.Acos((double)v.x), (GlmHalf)Math.Acos((double)v.y), (GlmHalf)Math.Acos((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Acos ((GlmHalf)Math.Acos((double)v)).
        /// </summary>
        public static hvec3 Acos(GlmHalf v) => new hvec3((GlmHalf)Math.Acos((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Asin ((GlmHalf)Math.Asin((double)v)).
        /// </summary>
        public static hvec3 Asin(hvec3 v) => new hvec3((GlmHalf)Math.Asin((double)v.x), (GlmHalf)Math.Asin((double)v.y), (GlmHalf)Math.Asin((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Asin ((GlmHalf)Math.Asin((double)v)).
        /// </summary>
        public static hvec3 Asin(GlmHalf v) => new hvec3((GlmHalf)Math.Asin((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Atan ((GlmHalf)Math.Atan((double)v)).
        /// </summary>
        public static hvec3 Atan(hvec3 v) => new hvec3((GlmHalf)Math.Atan((double)v.x), (GlmHalf)Math.Atan((double)v.y), (GlmHalf)Math.Atan((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Atan ((GlmHalf)Math.Atan((double)v)).
        /// </summary>
        public static hvec3 Atan(GlmHalf v) => new hvec3((GlmHalf)Math.Atan((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Cos ((GlmHalf)Math.Cos((double)v)).
        /// </summary>
        public static hvec3 Cos(hvec3 v) => new hvec3((GlmHalf)Math.Cos((double)v.x), (GlmHalf)Math.Cos((double)v.y), (GlmHalf)Math.Cos((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Cos ((GlmHalf)Math.Cos((double)v)).
        /// </summary>
        public static hvec3 Cos(GlmHalf v) => new hvec3((GlmHalf)Math.Cos((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Cosh ((GlmHalf)Math.Cosh((double)v)).
        /// </summary>
        public static hvec3 Cosh(hvec3 v) => new hvec3((GlmHalf)Math.Cosh((double)v.x), (GlmHalf)Math.Cosh((double)v.y), (GlmHalf)Math.Cosh((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Cosh ((GlmHalf)Math.Cosh((double)v)).
        /// </summary>
        public static hvec3 Cosh(GlmHalf v) => new hvec3((GlmHalf)Math.Cosh((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Exp ((GlmHalf)Math.Exp((double)v)).
        /// </summary>
        public static hvec3 Exp(hvec3 v) => new hvec3((GlmHalf)Math.Exp((double)v.x), (GlmHalf)Math.Exp((double)v.y), (GlmHalf)Math.Exp((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Exp ((GlmHalf)Math.Exp((double)v)).
        /// </summary>
        public static hvec3 Exp(GlmHalf v) => new hvec3((GlmHalf)Math.Exp((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Log ((GlmHalf)Math.Log((double)v)).
        /// </summary>
        public static hvec3 Log(hvec3 v) => new hvec3((GlmHalf)Math.Log((double)v.x), (GlmHalf)Math.Log((double)v.y), (GlmHalf)Math.Log((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Log ((GlmHalf)Math.Log((double)v)).
        /// </summary>
        public static hvec3 Log(GlmHalf v) => new hvec3((GlmHalf)Math.Log((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Log2 ((GlmHalf)Math.Log((double)v, 2)).
        /// </summary>
        public static hvec3 Log2(hvec3 v) => new hvec3((GlmHalf)Math.Log((double)v.x, 2), (GlmHalf)Math.Log((double)v.y, 2), (GlmHalf)Math.Log((double)v.z, 2));

        /// <summary>
        /// Returns a hvec from the application of Log2 ((GlmHalf)Math.Log((double)v, 2)).
        /// </summary>
        public static hvec3 Log2(GlmHalf v) => new hvec3((GlmHalf)Math.Log((double)v, 2));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Log10 ((GlmHalf)Math.Log10((double)v)).
        /// </summary>
        public static hvec3 Log10(hvec3 v) => new hvec3((GlmHalf)Math.Log10((double)v.x), (GlmHalf)Math.Log10((double)v.y), (GlmHalf)Math.Log10((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Log10 ((GlmHalf)Math.Log10((double)v)).
        /// </summary>
        public static hvec3 Log10(GlmHalf v) => new hvec3((GlmHalf)Math.Log10((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Floor ((GlmHalf)Math.Floor(v)).
        /// </summary>
        public static hvec3 Floor(hvec3 v) => new hvec3((GlmHalf)Math.Floor(v.x), (GlmHalf)Math.Floor(v.y), (GlmHalf)Math.Floor(v.z));

        /// <summary>
        /// Returns a hvec from the application of Floor ((GlmHalf)Math.Floor(v)).
        /// </summary>
        public static hvec3 Floor(GlmHalf v) => new hvec3((GlmHalf)Math.Floor(v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Ceiling ((GlmHalf)Math.Ceiling(v)).
        /// </summary>
        public static hvec3 Ceiling(hvec3 v) => new hvec3((GlmHalf)Math.Ceiling(v.x), (GlmHalf)Math.Ceiling(v.y), (GlmHalf)Math.Ceiling(v.z));

        /// <summary>
        /// Returns a hvec from the application of Ceiling ((GlmHalf)Math.Ceiling(v)).
        /// </summary>
        public static hvec3 Ceiling(GlmHalf v) => new hvec3((GlmHalf)Math.Ceiling(v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Round ((GlmHalf)Math.Round(v)).
        /// </summary>
        public static hvec3 Round(hvec3 v) => new hvec3((GlmHalf)Math.Round(v.x), (GlmHalf)Math.Round(v.y), (GlmHalf)Math.Round(v.z));

        /// <summary>
        /// Returns a hvec from the application of Round ((GlmHalf)Math.Round(v)).
        /// </summary>
        public static hvec3 Round(GlmHalf v) => new hvec3((GlmHalf)Math.Round(v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Sin ((GlmHalf)Math.Sin((double)v)).
        /// </summary>
        public static hvec3 Sin(hvec3 v) => new hvec3((GlmHalf)Math.Sin((double)v.x), (GlmHalf)Math.Sin((double)v.y), (GlmHalf)Math.Sin((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Sin ((GlmHalf)Math.Sin((double)v)).
        /// </summary>
        public static hvec3 Sin(GlmHalf v) => new hvec3((GlmHalf)Math.Sin((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Sinh ((GlmHalf)Math.Sinh((double)v)).
        /// </summary>
        public static hvec3 Sinh(hvec3 v) => new hvec3((GlmHalf)Math.Sinh((double)v.x), (GlmHalf)Math.Sinh((double)v.y), (GlmHalf)Math.Sinh((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Sinh ((GlmHalf)Math.Sinh((double)v)).
        /// </summary>
        public static hvec3 Sinh(GlmHalf v) => new hvec3((GlmHalf)Math.Sinh((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Tan ((GlmHalf)Math.Tan((double)v)).
        /// </summary>
        public static hvec3 Tan(hvec3 v) => new hvec3((GlmHalf)Math.Tan((double)v.x), (GlmHalf)Math.Tan((double)v.y), (GlmHalf)Math.Tan((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Tan ((GlmHalf)Math.Tan((double)v)).
        /// </summary>
        public static hvec3 Tan(GlmHalf v) => new hvec3((GlmHalf)Math.Tan((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Tanh ((GlmHalf)Math.Tanh((double)v)).
        /// </summary>
        public static hvec3 Tanh(hvec3 v) => new hvec3((GlmHalf)Math.Tanh((double)v.x), (GlmHalf)Math.Tanh((double)v.y), (GlmHalf)Math.Tanh((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Tanh ((GlmHalf)Math.Tanh((double)v)).
        /// </summary>
        public static hvec3 Tanh(GlmHalf v) => new hvec3((GlmHalf)Math.Tanh((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Truncate ((GlmHalf)Math.Truncate((double)v)).
        /// </summary>
        public static hvec3 Truncate(hvec3 v) => new hvec3((GlmHalf)Math.Truncate((double)v.x), (GlmHalf)Math.Truncate((double)v.y), (GlmHalf)Math.Truncate((double)v.z));

        /// <summary>
        /// Returns a hvec from the application of Truncate ((GlmHalf)Math.Truncate((double)v)).
        /// </summary>
        public static hvec3 Truncate(GlmHalf v) => new hvec3((GlmHalf)Math.Truncate((double)v));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Fract ((GlmHalf)(v - Math.Floor(v))).
        /// </summary>
        public static hvec3 Fract(hvec3 v) => new hvec3((GlmHalf)(v.x - Math.Floor(v.x)), (GlmHalf)(v.y - Math.Floor(v.y)), (GlmHalf)(v.z - Math.Floor(v.z)));

        /// <summary>
        /// Returns a hvec from the application of Fract ((GlmHalf)(v - Math.Floor(v))).
        /// </summary>
        public static hvec3 Fract(GlmHalf v) => new hvec3((GlmHalf)(v - Math.Floor(v)));

        /// <summary>
        /// Returns a hvec3 from component-wise application of Trunc ((long)(v)).
        /// </summary>
        public static hvec3 Trunc(hvec3 v) => new hvec3((long)(v.x), (long)(v.y), (long)(v.z));

        /// <summary>
        /// Returns a hvec from the application of Trunc ((long)(v)).
        /// </summary>
        public static hvec3 Trunc(GlmHalf v) => new hvec3((long)(v));

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec3 Random(Random random, hvec3 minValue, hvec3 maxValue) => new hvec3((GlmHalf)random.NextDouble() * (maxValue.x - minValue.x) + minValue.x, (GlmHalf)random.NextDouble() * (maxValue.y - minValue.y) + minValue.y, (GlmHalf)random.NextDouble() * (maxValue.z - minValue.z) + minValue.z);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec3 Random(Random random, hvec3 minValue, GlmHalf maxValue) => new hvec3((GlmHalf)random.NextDouble() * (maxValue - minValue.x) + minValue.x, (GlmHalf)random.NextDouble() * (maxValue - minValue.y) + minValue.y, (GlmHalf)random.NextDouble() * (maxValue - minValue.z) + minValue.z);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec3 Random(Random random, GlmHalf minValue, hvec3 maxValue) => new hvec3((GlmHalf)random.NextDouble() * (maxValue.x - minValue) + minValue, (GlmHalf)random.NextDouble() * (maxValue.y - minValue) + minValue, (GlmHalf)random.NextDouble() * (maxValue.z - minValue) + minValue);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec3 Random(Random random, GlmHalf minValue, GlmHalf maxValue) => new hvec3((GlmHalf)random.NextDouble() * (maxValue - minValue) + minValue);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec3 RandomUniform(Random random, hvec3 minValue, hvec3 maxValue) => new hvec3((GlmHalf)random.NextDouble() * (maxValue.x - minValue.x) + minValue.x, (GlmHalf)random.NextDouble() * (maxValue.y - minValue.y) + minValue.y, (GlmHalf)random.NextDouble() * (maxValue.z - minValue.z) + minValue.z);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec3 RandomUniform(Random random, hvec3 minValue, GlmHalf maxValue) => new hvec3((GlmHalf)random.NextDouble() * (maxValue - minValue.x) + minValue.x, (GlmHalf)random.NextDouble() * (maxValue - minValue.y) + minValue.y, (GlmHalf)random.NextDouble() * (maxValue - minValue.z) + minValue.z);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec3 RandomUniform(Random random, GlmHalf minValue, hvec3 maxValue) => new hvec3((GlmHalf)random.NextDouble() * (maxValue.x - minValue) + minValue, (GlmHalf)random.NextDouble() * (maxValue.y - minValue) + minValue, (GlmHalf)random.NextDouble() * (maxValue.z - minValue) + minValue);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed uniform values between 'minValue' and 'maxValue'.
        /// </summary>
        public static hvec3 RandomUniform(Random random, GlmHalf minValue, GlmHalf maxValue) => new hvec3((GlmHalf)random.NextDouble() * (maxValue - minValue) + minValue);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec3 RandomNormal(Random random, hvec3 mean, hvec3 variance) => new hvec3((GlmHalf)(Math.Sqrt((double)variance.x) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.x, (GlmHalf)(Math.Sqrt((double)variance.y) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.y, (GlmHalf)(Math.Sqrt((double)variance.z) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.z);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec3 RandomNormal(Random random, hvec3 mean, GlmHalf variance) => new hvec3((GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.x, (GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.y, (GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.z);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec3 RandomNormal(Random random, GlmHalf mean, hvec3 variance) => new hvec3((GlmHalf)(Math.Sqrt((double)variance.x) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean, (GlmHalf)(Math.Sqrt((double)variance.y) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean, (GlmHalf)(Math.Sqrt((double)variance.z) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec3 RandomNormal(Random random, GlmHalf mean, GlmHalf variance) => new hvec3((GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec3 RandomGaussian(Random random, hvec3 mean, hvec3 variance) => new hvec3((GlmHalf)(Math.Sqrt((double)variance.x) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.x, (GlmHalf)(Math.Sqrt((double)variance.y) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.y, (GlmHalf)(Math.Sqrt((double)variance.z) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.z);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec3 RandomGaussian(Random random, hvec3 mean, GlmHalf variance) => new hvec3((GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.x, (GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.y, (GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean.z);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec3 RandomGaussian(Random random, GlmHalf mean, hvec3 variance) => new hvec3((GlmHalf)(Math.Sqrt((double)variance.x) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean, (GlmHalf)(Math.Sqrt((double)variance.y) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean, (GlmHalf)(Math.Sqrt((double)variance.z) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean);

        /// <summary>
        /// Returns a hvec3 with independent and identically distributed values according to a normal/Gaussian distribution with specified mean and variance.
        /// </summary>
        public static hvec3 RandomGaussian(Random random, GlmHalf mean, GlmHalf variance) => new hvec3((GlmHalf)(Math.Sqrt((double)variance) * Math.Cos(2 * Math.PI * random.NextDouble()) * Math.Sqrt(-2.0 * Math.Log(random.NextDouble()))) + mean);

        #endregion


        #region Component-Wise Operator Overloads

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&lt; (lhs &lt; rhs).
        /// </summary>
        public static bvec3 operator<(hvec3 lhs, hvec3 rhs) => new bvec3(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&lt; (lhs &lt; rhs).
        /// </summary>
        public static bvec3 operator<(hvec3 lhs, GlmHalf rhs) => new bvec3(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&lt; (lhs &lt; rhs).
        /// </summary>
        public static bvec3 operator<(GlmHalf lhs, hvec3 rhs) => new bvec3(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&lt;= (lhs &lt;= rhs).
        /// </summary>
        public static bvec3 operator<=(hvec3 lhs, hvec3 rhs) => new bvec3(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&lt;= (lhs &lt;= rhs).
        /// </summary>
        public static bvec3 operator<=(hvec3 lhs, GlmHalf rhs) => new bvec3(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&lt;= (lhs &lt;= rhs).
        /// </summary>
        public static bvec3 operator<=(GlmHalf lhs, hvec3 rhs) => new bvec3(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&gt; (lhs &gt; rhs).
        /// </summary>
        public static bvec3 operator>(hvec3 lhs, hvec3 rhs) => new bvec3(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&gt; (lhs &gt; rhs).
        /// </summary>
        public static bvec3 operator>(hvec3 lhs, GlmHalf rhs) => new bvec3(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&gt; (lhs &gt; rhs).
        /// </summary>
        public static bvec3 operator>(GlmHalf lhs, hvec3 rhs) => new bvec3(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&gt;= (lhs &gt;= rhs).
        /// </summary>
        public static bvec3 operator>=(hvec3 lhs, hvec3 rhs) => new bvec3(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&gt;= (lhs &gt;= rhs).
        /// </summary>
        public static bvec3 operator>=(hvec3 lhs, GlmHalf rhs) => new bvec3(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs);

        /// <summary>
        /// Returns a bvec3 from component-wise application of operator&gt;= (lhs &gt;= rhs).
        /// </summary>
        public static bvec3 operator>=(GlmHalf lhs, hvec3 rhs) => new bvec3(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator+ (lhs + rhs).
        /// </summary>
        public static hvec3 operator+(hvec3 lhs, hvec3 rhs) => new hvec3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator+ (lhs + rhs).
        /// </summary>
        public static hvec3 operator+(hvec3 lhs, GlmHalf rhs) => new hvec3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator+ (lhs + rhs).
        /// </summary>
        public static hvec3 operator+(GlmHalf lhs, hvec3 rhs) => new hvec3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator- (lhs - rhs).
        /// </summary>
        public static hvec3 operator-(hvec3 lhs, hvec3 rhs) => new hvec3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator- (lhs - rhs).
        /// </summary>
        public static hvec3 operator-(hvec3 lhs, GlmHalf rhs) => new hvec3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator- (lhs - rhs).
        /// </summary>
        public static hvec3 operator-(GlmHalf lhs, hvec3 rhs) => new hvec3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator* (lhs * rhs).
        /// </summary>
        public static hvec3 operator*(hvec3 lhs, hvec3 rhs) => new hvec3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator* (lhs * rhs).
        /// </summary>
        public static hvec3 operator*(hvec3 lhs, GlmHalf rhs) => new hvec3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator* (lhs * rhs).
        /// </summary>
        public static hvec3 operator*(GlmHalf lhs, hvec3 rhs) => new hvec3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator/ (lhs / rhs).
        /// </summary>
        public static hvec3 operator/(hvec3 lhs, hvec3 rhs) => new hvec3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator/ (lhs / rhs).
        /// </summary>
        public static hvec3 operator/(hvec3 lhs, GlmHalf rhs) => new hvec3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator/ (lhs / rhs).
        /// </summary>
        public static hvec3 operator/(GlmHalf lhs, hvec3 rhs) => new hvec3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator+ (identity).
        /// </summary>
        public static hvec3 operator+(hvec3 v) => v;

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator- (-v).
        /// </summary>
        public static hvec3 operator-(hvec3 v) => new hvec3(-v.x, -v.y, -v.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator% (lhs % rhs).
        /// </summary>
        public static hvec3 operator%(hvec3 lhs, hvec3 rhs) => new hvec3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator% (lhs % rhs).
        /// </summary>
        public static hvec3 operator%(hvec3 lhs, GlmHalf rhs) => new hvec3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);

        /// <summary>
        /// Returns a hvec3 from component-wise application of operator% (lhs % rhs).
        /// </summary>
        public static hvec3 operator%(GlmHalf lhs, hvec3 rhs) => new hvec3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);

        #endregion

    }
}
